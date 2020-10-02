using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PelletTilemapModel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        pelletEaters = GameObject.FindObjectsOfType<PelletEater>();

        pelletTilemap = GetComponent<Tilemap>();
    }

    PelletEater[] pelletEaters;

    Tilemap pelletTilemap;


    // Update is called once per frame
    void Update()
    {
        foreach (PelletEater pe in pelletEaters)
        {
            CheckPellet(pe);
        }
    }

    void CheckPellet(PelletEater peletEater)
    {
        //what tile is the pellet eater @in@? And does it have a pellet?

        TileBase tile = GetTileAt((Vector2)peletEater.transform.position);

        if (tile == null)
            return;

        Debug.Log("NOM");
    }

    bool IsTileEmpty(Vector2 pos)
    {
        //is there a tile at pos?
        return GetTileAt(pos) == null;
    }

    TileBase GetTileAt(Vector2 pos)
    {
        //change the world position to a tile cell index
        Vector3Int cellPos = pelletTilemap.WorldToCell(pos);

        //return the actual tile
        return pelletTilemap.GetTile(cellPos);
    }


}
