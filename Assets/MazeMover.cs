using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //set our initial target position to be our starting position
        //so that the Update will, we, well, update to the target
        targetPos = transform.position;

        //this works as long as there is only ONE tulemap in the scene
        //Такой способ получить новый Tilemap - создав скрипт (я создал этот скрипт как модель) для tilemaps
        wallTileMap = GameManager.WallTilemap;
    }

    //Делегирует вызов? todo - повторить это
    public delegate void OnEnterNewTileDelegate();
    public event OnEnterNewTileDelegate OnEnterNewTile;


    float Speed = 3; // How many world-space "tiles" this unit moves in one second

    float tileDistanceTolerance = 0.01f; //how close to the target position counts arriving

    Vector2 desiredDirection; // THe currecnt direction we want to move in

    Vector2 targetPos; // Always a legal, empty tile

    Tilemap wallTileMap;

    // Update is called once per frame
    // this is the best place to read inputs and to thing like upgrating animations
    void Update()
    {
        //В классе, который принимает 1 метод EnemyMover данная переменная не инициализируется.
        if (wallTileMap == null)
            wallTileMap = GameManager.WallTilemap;

        UpdateTargetPosition();

        MoveToTargetPosition();


    }

    // FixedUpdate is called once per PHYSICS ENGINE frame
    // This is the best place update the phisic's system
    // velocity if you are using it ti move your object
    //void FixedUpdate()
    //{
    // our objects are not physics-enabled rigitbodies, so
    // the physics system isn/t moving us/ nor are we doing
    // "real" collisions, so we don't need to stress about FixedUpdate
    //}

    void UpdateTargetPosition(bool force = false)
    {
        if (force == false)
        {
            // Have ve reached our position
            float distanceToTarget = Vector3.Distance(transform.position, targetPos);

            if (distanceToTarget > 0)
                //not yet, no need to update anything
                return;
        }

        if (OnEnterNewTile != null)
            OnEnterNewTile();

        //If we get here, we've just entered a new tile
        OnEnterNewTilePlayer();

    }

    void OnEnterNewTilePlayer()
    {
        //if we get here, it meens we need a new target position
        targetPos += desiredDirection;

        targetPos = FloorPosition(targetPos);

        if (IsTileEmpty(targetPos))
            return;

        //if we get here, it meens that our position is OCCUPIED
        //Do we aren't allowed to move. Stand still
        targetPos = transform.position;
    }

    Vector2 FloorPosition(Vector2 pos)
    {
        //Normalize the target position
        // This might not line up right if we have a wierd offset tilemap
        // A " better" solution might be to lookup the tile at the new 
        // targetPos, then read back that Tile's world coordinate?
        return new Vector2(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
    }

    public bool IsGoingToHittingWall()
    {
        //returns true if our trgetPos is a wall
        return IsTileEmpty(targetPos + desiredDirection) == false;
    }


    bool IsTileEmpty(Vector2 pos)
    {
        // is there a tile at pos
        return GetTileAt(pos) == null;
    }

    TileBase GetTileAt(Vector2 pos)
    {
        // Get the tile from tilemap at position pos

        //First we need to change the World position, to a tile cell index

        Vector3Int cellPos = wallTileMap.WorldToCell(pos);

        //Now return the catual tile
        return wallTileMap.GetTile(cellPos);
    }


    void MoveToTargetPosition()
    {
        //how far cen we move this frame 
        float distanceThisUpdate = Speed * Time.deltaTime;

        //And in what direction is this movement?
        // Towards our target position
        Vector2 distToTarget = targetPos - (Vector2)transform.position;

        //normalized - делает вектор = 1 
        Vector2 movementThisUpdate = distToTarget.normalized * distanceThisUpdate;

        // What if we would be moving PAST the target?
        // We COULD change movementThisUpdate tp have the same magnitude as distance to target
        // magnitude возвращает длинну вектора, а функция SqrMagnitude возращает квадраты длинны вектора (т.е всегда положительныую велечину + эта функция быстрее просто длинны векторов.)
        if (distToTarget.SqrMagnitude() < movementThisUpdate.SqrMagnitude())
        {
            //Just set our posotion to the target, not a "move"
            transform.position = targetPos;
            return;
        }

        //do the move
        transform.Translate(movementThisUpdate);

        if (Vector2.Distance(targetPos, (Vector2)transform.position) < tileDistanceTolerance)
            transform.position = targetPos;
    }

    public void SetDesiredDirection(Vector2 newDir, bool preventInvalidDirection = false)
    {
        //TODO: Santy checks? 
        //Make sure not diagonal? in Theory, our PlayerMover/Enemy

        //If we're selection a direction that would sla us into a wall,
        //this will cause us to stop - which doesn't feel right

        if (!preventInvalidDirection)
            if (checkTestPosition(newDir))
                return;

        Vector2 oldDir = desiredDirection;
        desiredDirection = newDir;

        //if the input is to reserve our dirrection, do it instantlye
        //  if ((oldDir.x * newDir.x) < 0 || (oldDir.y * newDir.y) < 0)
        // .Dot - Для нормализованных векторов точка возвращает 1, если они указывают в одном и том же направлении;
        //-1, если они указывают в совершенно противоположных направлениях; и промежуточное число для других случаев
        //(например, точка возвращает ноль, если векторы перпендикулярны).
        if (Vector2.Dot(oldDir, newDir) < 0)
            UpdateTargetPosition(true);
    }

    bool checkTestPosition(Vector2 newDir)
    {
        Vector2 testPos = targetPos + newDir;
        if (IsTileEmpty(testPos) == false)
            return true;
        return false;
    }

    public Vector2 GetDiesiredDirection()
    {
        return desiredDirection;
    }
}
