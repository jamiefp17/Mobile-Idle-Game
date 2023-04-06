using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTree : MonoBehaviour
{

    public class CRoot : BTNodes.CRoot { } //Makes an instance of an abstract class.
    public CRoot root = null; //The only node the tree needs to be aware of is the root, which it ticks every frame.

    private List<int> requestHandlerKeys; //A list of keys for request handlers so they can be accessed.
    private CBlackboard blackboard = new CBlackboard();


    public void RunTree(int agentID) //Ticks the root.
    {
        if (root != null) //Check to make sure the tree has a root.
        {
            CheckObservers(agentID);
            root.Tick(agentID);
        }
        else Debug.Log("Tree has no Root");
    }

    public void SetRoot(CRoot r) //Sets the root.
    {
        root = r;
    }

    public void CheckObservers(int agentID) //Checks the observers to see if the tree should jump to another node.
    {
        for (int i = 0; i < requestHandlerKeys.Count; i++) //Loops through all of the keys.
        {
            KeyValuePair<BTNodes.CRequestHandler, Vector3> requestHandler = blackboard.GetObserverData(requestHandlerKeys[i]); //Blackboard returns both.
            if (!requestHandler.Value.Equals(Vector3.zero)) // Check if the Vector3 has a value.
            {
                requestHandler.Key.Tick(agentID); // Access the Key property of the KeyValuePair.
                return;
            }
        }
    }

    public void SetBlackboard(CBlackboard bb) //Sets the blackboard.
    {
        blackboard = bb;
    }

}
