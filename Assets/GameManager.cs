using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //for now - this wil change when we have multile levels
        WallTilemap = GameObject.FindObjectOfType<WallTilemapModel>().GetComponent<Tilemap>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    static public Tilemap WallTilemap;

}
