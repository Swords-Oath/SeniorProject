using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAINav : MonoBehaviour
{
    public GameObject theDestination;
    NavMeshAgent theAgent;

    private void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        theAgent.SetDestination(theDestination.transform.position);
    }
}
