using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : INPCState
{
    public bool PlayedMusic;
    public INPCState DoState(EnemyMovement npc)
    {
        ChasePlayer(npc);
        if (!npc.playerInSightRange && !npc.playerInAttackRange) return npc.patrolState;
        else if (npc.playerInSightRange && npc.playerInAttackRange) return npc.attackState;
        else return npc.chaseState;
    }

    private void ChasePlayer(EnemyMovement npc)
    {
        npc.enemyAgent.SetDestination(npc.player.position);
        //Debug.Log("Chasing Player");
        if (!PlayedMusic)
        {
            AudioManager.instance.Play("Chase Music");
            PlayedMusic = true;
        }
    }
}
