public class AttackState : INPCState
{
    public INPCState DoState(EnemyMovement npc)
    {
        AttackPlayer(npc);
        if (npc.playerInSightRange && !npc.playerInAttackRange) return npc.patrolState;
        else if (!npc.playerInSightRange && !npc.playerInAttackRange) return npc.patrolState;
        else return npc.attackState;
    }

    private void AttackPlayer(EnemyMovement npc)
    {
        //stop enemy movement
        npc.enemyAgent.SetDestination(npc.transform.position);

        npc.transform.LookAt(npc.player);
        //Debug.Log("Attacking Player");
        npc.alreadyAttacked = true;
    }
}
