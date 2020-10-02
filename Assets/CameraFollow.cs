using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    Transform targetTransform;
    Vector3 currentVelocity;
    float smoothTime = 0.5f;
    float threshold = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (targetTransform == null)
            targetTransform = GameObject.FindObjectOfType<PlayerMover>()?.transform;
        if (targetTransform == null)
            return;

        Vector3 cleanPos = targetTransform.position;
        cleanPos.z = transform.position.z;

        if (Vector3.Distance(transform.position, cleanPos) < threshold)
        {
            currentVelocity = Vector3.zero;
            return;
        }
         

        transform.position = Vector3.SmoothDamp(transform.position, cleanPos, ref currentVelocity, smoothTime);

    }
}
