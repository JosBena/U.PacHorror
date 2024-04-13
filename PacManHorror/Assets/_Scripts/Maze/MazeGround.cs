using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGround : MonoBehaviour
{
    GameController player;
    private Vector3 _dimensions, _trueDimensions;
    GameObject ground;
    private void Awake()
    {
        //player = GetComponent<GameController>();
        //_dimensions = new Vector3(player.SizeRows,0, player.SizeColumns);
        //_trueDimensions = _dimensions / 3;
        //ground = Instantiate(Scriptables.teleportationArea,Vector3.zero, Quaternion.identity);
        //ground.transform.position = _trueDimensions*5;
        //ground.transform.localScale = _trueDimensions;
    }
}
