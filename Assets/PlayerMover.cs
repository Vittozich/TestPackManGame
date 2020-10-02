using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MazeMover))]
public class PlayerMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        mazeMover = GetComponent<MazeMover>();
    }

    MazeMover mazeMover;

    // Update is called once per frame
    void Update()
    {


        Vector2 newDir = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (newDir.SqrMagnitude() < 0.05f)
            return;

        if (Mathf.Abs(newDir.x) >= Mathf.Abs(newDir.y))
            newDir.y = 0;
        else
            newDir.x = 0;


        mazeMover.SetDesiredDirection(newDir.normalized);

        // mazeMover.GetComponent<MazeMover>();
    }

}
