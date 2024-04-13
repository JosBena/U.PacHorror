using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.AI;

public class MazeConstructor : MonoBehaviour
{
    public bool ShowDebug;

    public float height = 1.75f;

    #region MazeMaterial
    [SerializeField] private Material mazeMat1;
    [SerializeField] private Material mazeMat2;
    [SerializeField] private Material startMat;
    [SerializeField] private Material treasureMat;
    #endregion MazeMaterial

    //data
    private MazeDataGenerator dataGenerator;

    //maze mesh
    private MazeMeshGenerator meshGenerator;


    //prefab
    [SerializeField] private GameObject coin, enemy, meta;


    public int[,] data
    {
        get; private set;
    }

    #region mazeVariables
    public float hallWidth { get; private set; }
    public float hallHeight { get; private set; }
    public int startRow { get; private set; }
    public int startCol { get; private set; }
    public int goalRow { get; private set; }
    public int goalCol { get; private set; }
    #endregion mazeVariables


    //XR
    private GameObject interactionManager;

    private void Awake()
    {
        //default to wallks surrounding a single empty cell
        data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };

        //instantiate generator
        dataGenerator = new MazeDataGenerator();

        //instantiate mesh generator
        meshGenerator = new MazeMeshGenerator();



    }

    public void GenerateNewMaze(int sizeRows, int sizeCols,
    TriggerEventHandler startCallback = null, TriggerEventHandler goalCallback = null)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }

        DisposeOldMaze();

        data = dataGenerator.FromDimensions(sizeRows, sizeCols);

        FindStartPosition();
        FindGoalPosition();

        // store values used to generate this mesh
        hallWidth = meshGenerator.width;
        hallHeight = meshGenerator.height;

        DisplayMaze();

        PlaceStartTrigger(startCallback);
        PlaceGoalTrigger(goalCallback);
    }

    private void OnGUI()
    {
        if (!ShowDebug)
        {
            return;
        }

        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";

        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    msg += "....";
                }
                else
                {
                    msg += "==";
                }
            }
            msg += "\n";
        }
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }

    //mesh maze generator
    private void DisplayMaze()
    {
        GameObject go = new GameObject();
        GameObject gow = new GameObject();
        go.transform.position = Vector3.zero;
        go.name = "Procedural Maze Floor";
        go.tag = "Generated";
        

        TeleportationArea teleport = go.AddComponent<TeleportationArea>();
        


        MeshFilter mf = go.AddComponent<MeshFilter>();
        mf.mesh = meshGenerator.FromData(data);

        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        
        mr.materials = new Material[2] { mazeMat1, mazeMat2 };

        go.AddComponent<BoxCollider>();

        go.AddComponent<NavMeshSurface>();
        go.GetComponent<NavMeshSurface>().collectObjects = CollectObjects.Children;
        go.GetComponent<NavMeshSurface>().BuildNavMesh();



        gow.transform.position = Vector3.zero;
        gow.name = "Procedural Maze Walls";
        gow.tag = "Generated";

        

        MeshFilter mfw = gow.AddComponent<MeshFilter>();
        mfw.mesh = meshGenerator.FromDataWalls(data);


        MeshRenderer mrw = gow.AddComponent<MeshRenderer>();
        gow.AddComponent<MeshCollider>().convex = true;
        gow.GetComponent<MeshCollider>().isTrigger = true;
        gow.AddComponent<Rigidbody>().useGravity = false;
        gow.GetComponent<Rigidbody>().isKinematic = true;

        mrw.materials = new Material[2] { mazeMat1, mazeMat2 };

        gow.AddComponent<OutofBoundsScript>();
        instantiateCoins();

        go.layer = 6;
    }

    private void instantiateCoins()
    {
        //coins
        for (int i = 0; i < meshGenerator.coinPosition.Count-1; i++)
        {
            if (i != 0)
            {
                GameObject coinGameObject = Instantiate(coin, new Vector3( meshGenerator.coinPosition[i].x, height, meshGenerator.coinPosition[i].z), Quaternion.identity);
                coinGameObject.transform.parent = GameObject.FindWithTag("coinPurse").transform;
            }
        }
    }
    public void DisposeOldMaze()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Generated");
        foreach (GameObject go in objects)
        {
            Destroy(go);
        }
    }

    private void FindStartPosition()
    {
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    startRow = i;
                    startCol = j;
                    return;
                }
            }
        }
    }

    private void FindGoalPosition()
    {
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        // loop top to bottom, right to left
        for (int i = rMax; i >= 0; i--)
        {
            for (int j = cMax; j >= 0; j--)
            {
                if (maze[i, j] == 0)
                {
                    goalRow = i;
                    goalCol = j;
                    return;
                }
            }
        }
    }

    private void PlaceStartTrigger(TriggerEventHandler callback)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = new Vector3(startCol * hallWidth, height, startRow * hallWidth);
        go.name = "Start Trigger";
        go.tag = "Generated";

        go.GetComponent<BoxCollider>().isTrigger = true;
        go.GetComponent<MeshRenderer>().sharedMaterial = startMat;
        go.SetActive(false);
        TriggerEventRouter tc = go.AddComponent<TriggerEventRouter>();
        tc.callback = callback;
    }

    private void PlaceGoalTrigger(TriggerEventHandler callback)
    {

        GameObject go = Instantiate(meta,new Vector3(goalCol * hallWidth, height, goalRow * hallWidth), Quaternion.identity );
        go.transform.position = new Vector3(goalCol * hallWidth, height, goalRow * hallWidth);
        go.name = "Meta";
        go.tag = "Generated";

        go.GetComponent<MeshCollider>().isTrigger = true;

        TriggerEventRouter tc = go.AddComponent<TriggerEventRouter>();
        tc.callback = callback;

        //enemy
        GameObject packman = Instantiate(enemy, new Vector3(goalCol * hallWidth, 1.5f, goalRow * hallWidth), Quaternion.Euler(-90,0,0));
        packman.transform.parent = GameObject.Find("Systems").transform;
    }
}
