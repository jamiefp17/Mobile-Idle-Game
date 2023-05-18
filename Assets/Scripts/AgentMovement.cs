using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{

    [SerializeField] Transform target;
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Vector3 dest = new Vector3(EmployeeSharedInformation.SCustomerPositions.positions[0].x, EmployeeSharedInformation.SCustomerPositions.positions[0].y, 0.0f);
        agent.SetDestination(dest);
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.position);
    }
}
