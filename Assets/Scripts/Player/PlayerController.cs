using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public WheelCollider[] wheelColliders;
    public float carPower;
    public float steerPower;

    public GameObject centerOfMass;
    public Rigidbody rb;
    void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var wheel in wheelColliders)
        {
            wheel.motorTorque = (InputManager._INPUT_MANAGER.leftAxisValue.y * -1) * ((carPower * 5) / 4);
        }
        for(int i = 0; i < wheelColliders.Length; i++)
        {
            if (i < 2)
            {
                wheelColliders[i].steerAngle = InputManager._INPUT_MANAGER.leftAxisValue.x * steerPower;
            }
        }
    }
}
