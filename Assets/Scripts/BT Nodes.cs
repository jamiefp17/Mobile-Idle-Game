using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNodes : MonoBehaviour
{

    //instance of node path that is global to all of the nodes, allowing them to all push to the vector during their tick.
    static CCurrentNodePath nodePath = new CCurrentNodePath();

    public abstract class CNode
    {
        /*internal*/public enum ETickReturn //The responces a node can return to its parent when ticked.
        {
            Success,
		    Failure,
		    Running
        };

        public abstract ETickReturn Tick(int agentID); //Virtual function that is present in every node, no matter the type. Each node has the ability to be ticked.
        
    };

    //Both leaf and check are open classes, which do not provide any additional data or functionality. The difference between them is the intent of the node.
    //More functionality can be added if it is ever required, so this keeps the door open to expansion.
    public abstract class CLeaf : CNode 
    {
        public abstract override ETickReturn Tick(int agentID);
    };

    public abstract class CCheck : CNode
    {
        public abstract override ETickReturn Tick(int agentID);
    };


    public abstract class CComposite : CNode
    {
        protected List<CNode> children = new List<CNode>(); //Can hold many children.

        public void PushChild(CNode child) //Pushes a child to the end of the vector. Any children pushed after this one will have less priority.
        {
            children.Add(child);
        }
        public abstract ETickReturn CallChildren(int agentID); //Calls the composite's children.
        public override ETickReturn Tick(int agentID)
        {
            return CallChildren(agentID);
        }
    
    };

    public abstract class CService : CNode
    {
        private	CComposite child; //Has a single composite child.
        public void SetChild(CComposite c)
        {
            child = c;
        }
        public override ETickReturn Tick(int agentID)
        {
            nodePath.PushNode(child); //Adds the child to the node path. This node will have been added by its own parent, so it doesn't have to add itself.
            nodePath.RunService(); //Runs any service node functions that are in the node path.
            CNode.ETickReturn childCall = child.Tick(agentID); //Calls its child's behaviour.
            nodePath.PopNode(); //After the child has returned a value, remove the child from the node path before returning the value.
            return childCall;
        }
        public abstract void ServiceFunction(); //This class is abstract, but any children will have a service function, which is repeatedly called when a descendant is running.
    };

    public class CCurrentNodePath
    {
        //A vector that holds information on the path of nodes that are currently running. This is used in the excecution of service nodes, allowing the program to run
        //the function of any service node that is in the path.
        private List<CNode> nodePathVector;

        public void PushNode(CNode node) //Pushes a node to the back of the vector.
        {
            nodePathVector.Add(node);
        }
        public void PopNode() //Removes the last node that was added.
        {
            nodePathVector.RemoveAt(nodePathVector.Count - 1);
        }
        public void RunService() //Loops through every node in the list, and if they're service nodes, run their associated function.
        {
            int count = nodePathVector.Count - 1; //The size of the vector.
            for (int i = count; i >= 0; i--) //Works backwards, so lower service functions are called first.
            {
                if (nodePathVector[i] is CService) //Checks to see if the node is a service node.
                {
                    ((CService)nodePathVector[i]).ServiceFunction();

                }
            }
        }
    };

    public abstract class CRequestHandler : CNode
    {
        private CNode child; //Has a single node child.
        private string key; //Used as the key for the blackboard.
        public void SetChild(CNode c)
        {
            child = c;
        }
        public abstract override ETickReturn Tick(int agentID);
    };

    public abstract class CRoot : CNode
    {
        private CNode child; //Has a single node child.

        public void SetChild(CNode c)
        {
            child = c;
        }
        public override ETickReturn Tick(int agentID)
        {
            nodePath.PushNode(child); //No point in calling RunService after adding the child, as there cannot be a service node yet.
            ETickReturn childCall = child.Tick(agentID);
            nodePath.PopNode();
            return childCall;
        }
    };

    public abstract class CSelector : CComposite
    {
        public override ETickReturn CallChildren(int agentID)
        {
            for (int i = 0; i < children.Count; i++)
            {
                nodePath.RunService();
                nodePath.PushNode(children[i]);
                ETickReturn childReturn = children[i].Tick(agentID);
                nodePath.PopNode();
                if (childReturn == ETickReturn.Success)
                {
                    return ETickReturn.Success; //If a single child succeeds, return a success
                }
                else if (childReturn != ETickReturn.Failure)
                {
                    return ETickReturn.Running; //Hasn't succeeded or failed, so it's still running.
                }
            }
            return ETickReturn.Failure; //All of the children have been checked, and none succeeded. This is a failure.
        }
    };

    public abstract class CSequence : CComposite
    {
        public override ETickReturn CallChildren(int agentID)
        {
            for (int i = 0; i < children.Count; i++)
            {
                nodePath.RunService();
                nodePath.PushNode(children[i]);
                ETickReturn childReturn = children[i].Tick(agentID);
                nodePath.PopNode();
                if (childReturn == ETickReturn.Failure)
                {
                    return ETickReturn.Failure; //A single child has failed, so return failure.
                }
                else if (childReturn != ETickReturn.Success)
                {
                    return ETickReturn.Running; //The child hasn't Failed or succeeded, so it's still running.
                }
            }
            return ETickReturn.Success; //All of the children have returned success.
        }
    };
}
