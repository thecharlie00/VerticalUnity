using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float movSmoothness;
    public float rotSmoothness;

    public Vector3 offset;
    public Vector3 rotOffset;

    public Transform carTarget;




    private void FixedUpdate()
    {
        FollowPlayer();
        
    }
    void FollowPlayer()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        Vector3 targetPos = new Vector3();
        targetPos = carTarget.TransformPoint(offset);

        transform.position = Vector3.Lerp(transform.position, targetPos, movSmoothness * Time.deltaTime);

    }

    void HandleRotation()
    {
        var direction = carTarget.position - transform.position;
        var rotation = new Quaternion();

        rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);


        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness * Time.deltaTime);

    }
}
