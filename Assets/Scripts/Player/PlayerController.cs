using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public enum Axel
    {
        FRONT,
        REAR
    }

   [System.Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }
    public List<Wheel> wheels = new List<Wheel>();
    public float maxAcceleration;
    public float maxBrakeAcceleration;
    public float turnSensivity = 1f;
    public float maxSteerAngle;
    public Vector3 centerOfMass;


    float moveInput;
    public Rigidbody carRB;

    
    void Start()
    {
        carRB.centerOfMass = centerOfMass;
    }
    private void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }
    // Update is called once per frame
    void Update()
    {
        AnimationWheels();
    }
    private void Move()
    {
        foreach(var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = InputManager._INPUT_MANAGER.leftAxisValue.y * 600  * maxAcceleration * Time.deltaTime;
        }
    }
    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if(wheel.axel == Axel.FRONT)
            {
                var _steerAngle = InputManager._INPUT_MANAGER.leftAxisValue.x * maxSteerAngle * turnSensivity;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if(InputManager._INPUT_MANAGER.isBraking == 1)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = maxBrakeAcceleration * 300 * Time.deltaTime;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }
    void AnimationWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rotation;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rotation);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rotation;
        }
    }
}
