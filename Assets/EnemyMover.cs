using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        mazeMover = GetComponent<MazeMover>();
        mazeMover.OnEnterNewTile += OnEnterNewTile;
    }

    MazeMover mazeMover;

    public float forwardWeight = 0.5f; //chance of continuing forward instead of turning at an intersection

    // Update is called once per frame
    void Update()
    {


    }

    void DoTurn(bool preventInvalidDirection = false)
    {

        Vector2 newDir = Vector2.zero;

        Vector2 oldDir = mazeMover.GetDiesiredDirection();

        if (Mathf.Abs(oldDir.x) > 0)
            newDir.y = Random.Range(0, 2) == 0 ? -1 : 1;
        else
            newDir.x = Random.Range(0, 2) == 0 ? -1 : 1;

        mazeMover.SetDesiredDirection(newDir, preventInvalidDirection);
    }

    void OnEnterNewTile()
    {
        if (Random.Range(0f, 1f) < forwardWeight)
        {
            if (mazeMover.IsGoingToHittingWall())
                DoTurn(true);
            return;
        }
      

        DoTurn();
    }
}
