using UnityEngine;
using UnityEngine.AI;

public class PatrolState : INPCState
{
    public INPCState DoState(EnemyMovement npc) {
        
        Patrolling(npc);
        AudioManager.instance.Stop("Chase Music");
        if (npc.playerInSightRange && !npc.playerInAttackRange) return npc.chaseState;
        else if (npc.playerInSightRange && npc.playerInAttackRange) return npc.attackState;
        else return npc.patrolState;
    }

    private void Patrolling(EnemyMovement npc)
    {
        if (!npc.walkPointSet) SearchWalkPoint(npc);

        if (npc.walkPointSet)
        {

            npc.enemyAgent.SetDestination(npc.walkPoint);

        }
        Vector3 distanceToWalkPoint = npc.transform.position - npc.walkPoint;
        //WalkPoint Reached
        if (distanceToWalkPoint.magnitude < 1f) npc.walkPointSet = false;

    }

    private void SearchWalkPoint(EnemyMovement npc)
    {
        float randomZ = Random.Range(-npc.walkPointRange, npc.walkPointRange);
        float randomX = Random.Range(-npc.walkPointRange, npc.walkPointRange);

        npc.walkPoint = new Vector3(npc.transform.position.x + randomX, npc.transform.position.y, npc.transform.position.z + randomZ);


        if (Physics.Raycast(npc.walkPoint, -npc.transform.up, 2f, npc.whatIsGround))
        {
            NavMeshHit hit;
            Vector3 checkPosition = new Vector3(npc.walkPoint.x, 1, npc.walkPoint.z);
            if (NavMesh.SamplePosition(checkPosition, out hit, 1f, NavMesh.AllAreas))
            {
                npc.walkPointSet = true;
            }
        }
        Debug.DrawLine(npc.walkPoint, -npc.transform.up, Color.red);
    }
}
