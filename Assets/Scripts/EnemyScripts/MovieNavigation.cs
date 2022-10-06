using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovieNavigation : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private List<Vector3> nextPosition;
    [SerializeField] private Transform playerTrans;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = playerTrans.position;
    }

    public void moveToPosition(Vector3 newPosition)
    {
        if(nextPosition.Count > 0)
        {
            Debug.Log("Wtf? More than one?!");
        }
        else
        {
            nextPosition.Add(newPosition);
            navMeshAgent.nextPosition = nextPosition[0];
        }


    }
}
