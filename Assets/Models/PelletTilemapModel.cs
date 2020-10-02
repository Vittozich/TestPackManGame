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

    //What happens when we eat a pellet on this map?
    public int PelletPoints = 1;
    public bool RequiredForLevelCompletion = false;
    public float PowerSeconds = 0;


    // Update is called once per frame
    void Update()
    {
        foreach (PelletEater pe in pelletEaters)
        {
            CheckPellet(pe);
        }
    }

    void CheckPellet(PelletEater pelletEater)
    {
        Vector2 offsetPosition = (Vector2)pelletEater.transform.position + new Vector2(0.5f, 0.5f);

        //what tile is the pellet eater @in@? And does it have a pellet?

        TileBase tile = GetTileAt(offsetPosition);

        if (tile == null)
            return;

        Debug.Log("NOM");

        EatPellet(offsetPosition);
    }

    void EatPellet(Vector2 pos)
    {
        SetTileAt(pos, null);

        //todo: do the things ... points and whatnot
    }

    void SetTileAt(Vector2 pos, TileBase tile)
    {
        //change the world position to a tile cell index
        Vector3Int cellPos = pelletTilemap.WorldToCell(pos);

        //set the actual tile
        pelletTilemap.SetTile(cellPos, tile);
    }


    TileBase GetTileAt(Vector2 pos)
    {
        //change the world position to a tile cell index
        Vector3Int cellPos = pelletTilemap.WorldToCell(pos);

        //return the actual tile
        return pelletTilemap.GetTile(cellPos);
    }


}
