
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public Transform[] points;

    private int destPoint = 0;

    private EnemyAggro enemyAggro;

    private Transform playerTransform;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        enemyAggro = GetComponent<EnemyAggro>();

        if(playerTransform == null )
        playerTransform = FindAnyObjectByType<PlayerMovement>().transform; 
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        NextPoint();
    }

    private void NextPoint()
    {
        if (points == null || points.Length == 0)
            return;

        navMeshAgent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }

    private void Update()
    {
       EnemyMovement();
    }

    private void EnemyMovement()
    {
        if (enemyAggro.isAggro)
        {
            navMeshAgent.SetDestination(playerTransform.position);
            navMeshAgent.speed = 6f;
            navMeshAgent.stoppingDistance = 3;
        }
        else
        {
            navMeshAgent.stoppingDistance = 0;
            navMeshAgent.speed = 12f;

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                NextPoint();
            }
               
        }
    }

}
