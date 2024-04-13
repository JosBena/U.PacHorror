using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    private Scores _playerScores;
    private ActionBasedContinuousMoveProvider continousMovement;
    private int coinNumber;
    private Transform allCoins;
    private float magnetRadius = 1;
    [SerializeField]
    private bool hasMap, hasSpeed, hasMagnet;
    // Start is called before the first frame update
    private void Awake()
    {
        _playerScores = Scriptables.PlayerScore;

        continousMovement = GameObject.Find("/SetUp/Locomotion System").GetComponent<ActionBasedContinuousMoveProvider>();
        allCoins = GameObject.Find("/Systems/AllCoins").transform;

        coinNumber = _playerScores.coins;

        hasSpeed = Scriptables.Stim.active;
        hasMagnet = Scriptables.Magnet.active;
        hasMap = Scriptables.Minimap.active;

    }
    void Start()
    {
        if (hasSpeed)
            continousMovement.moveSpeed = Scriptables.Stim.UpgradableVariable;
        else
            continousMovement.moveSpeed = 4;

    }

    // Update is called once per frame
    void Update()
    {
        if(hasMagnet)
        {
            foreach (Transform child in allCoins)
            {
                float distanceSqr = (transform.position - child.position).sqrMagnitude;
                if (distanceSqr < magnetRadius)
                {
                    Destroy(child.gameObject);
                    _playerScores.coins++;
                }
            }
        }
    }

    public int GetCoinNumber()
    {
        return coinNumber;
    }

    public bool GetMap()
    {
        return hasMap;
    }
    public bool GetSpeed()
    {
        return hasSpeed;
    }
    public bool GetMagnet()
    {
        return hasMagnet;
    }
}
