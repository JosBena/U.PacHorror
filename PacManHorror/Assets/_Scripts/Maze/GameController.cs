using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MazeConstructor))]
[RequireComponent(typeof(MazeGround))]

public class GameController : MonoBehaviour
{
    //1
    [SerializeField] private Player player;
    //[SerializeField] private Text timeLabel;
    //[SerializeField] private Text scoreLabel;

    private MeshCollider wallsCollider;

    private MazeConstructor generator;


    //2
    //private DateTime startTime;
    //private int timeLimit;
    //private int reduceLimitBy;

    private int score;
    private bool goalReached;

    [SerializeField]private int sizerows = 13, sizecolumns = 15;

    public void Awake()
    {
        sizerows = Scriptables.LevelManager.rowSize;
        sizecolumns = Scriptables.LevelManager.columnSize;

    }

    public int SizeRows
    {
        get { return sizerows; }
    }
    public int SizeColumns
    {
        get { return sizecolumns; }
    }

    //3
    void Start()
    {



        generator = GetComponent<MazeConstructor>();
        StartNewGame();
    }

    //4
    private void StartNewGame()
    {
        //timeLimit = 80;
        //reduceLimitBy = 5;
        //startTime = DateTime.Now;

        //score = 0;
        ////scoreLabel.text = score.ToString();

        StartNewMaze();
    }

    //5
    private void StartNewMaze()
    {
        if (sizerows % 2 == 0 || sizecolumns % 2 == 0)
        {
            print("uneven rows or columns");
            return;
        }

        generator.GenerateNewMaze(sizerows, sizecolumns, OnStartTrigger, OnGoalTrigger);

        float x = generator.startCol * generator.hallWidth;
        float y = 1;
        float z = generator.startRow * generator.hallWidth;
        player.transform.position = new Vector3(x, y, z);

        goalReached = false;
        player.enabled = true;

        // restart timer
        //timeLimit -= reduceLimitBy;
        //startTime = DateTime.Now;
    }

    //6
    void Update()
    {
        if (!player.enabled)
        {
            return;
        }

        //int timeUsed = (int)(DateTime.Now - startTime).TotalSeconds;
        //int timeLeft = timeLimit - timeUsed;

        //if (timeLeft > 0)
        //{
        //    //timeLabel.text = timeLeft.ToString();
        //}
        //else
        //{
        //    //timeLabel.text = "TIME UP";
        //    player.enabled = false;

        //    Invoke("StartNewGame", 4);
        //}
    }

    //7
    private void OnGoalTrigger(GameObject trigger, GameObject other)
    {

    }

    private void OnStartTrigger(GameObject trigger, GameObject other)
    {
        if (goalReached)
        {
            Debug.Log("Finish!");
            player.enabled = false;

            Invoke("StartNewMaze", 4);
        }
    }

    private void FixedUpdate()
    {

    }

}
