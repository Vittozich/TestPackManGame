using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //test:
        direction = new Vector2(0, 1);
    }

    float Speed = 3; // How many world-space "tiles" this unit moves in one second

    Vector2 direction; // THe currecnt direction we want to move in

    // Update is called once per frame
    // this is the best place to read inputs and to thing like upgrating animations
    void Update()
    {
      
        

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
}
