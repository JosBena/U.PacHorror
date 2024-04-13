using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private string currentStateName;
    private INPCState currenState;

    //States
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public AttackState attackState = new AttackState();

    [HideInInspector]
    public NavMeshAgent enemyAgent; //
    public Transform player; //

    public LayerMask whatIsGround, whatIsPlayer; //

    //Patrolling
    public Vector3 walkPoint;
    public bool walkPointSet; //
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    //State Ranges
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void OnEnable()
    {
        currenState = patrolState;
    }

    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(PlayerTags.player).transform;
    }

    private void Update()
    {
        SwithStates();
    }


    public void SwithStates()
    {
        //Set Sight Range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        //Switch Sates
        currenState = currenState.DoState(this);
        currentStateName = currenState.ToString();

        //Change Scenes
        if (alreadyAttacked)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            alreadyAttacked = false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sightRange);

    }
}

