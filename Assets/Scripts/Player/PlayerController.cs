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
    public float maxVelocity;
    private float iniMaxVelocity;
    private float iniMaxAcceleration;
    public float velocity;
    public float turnSensivity = 1f;
    public float maxSteerAngle;
    public Vector3 centerOfMass;
    private float turboOnVelocity;
    private float turboOnAcceleration;

    float moveInput;
    public Rigidbody carRB;

    
    void Start()
    {
        carRB.centerOfMass = centerOfMass;
        iniMaxVelocity = maxVelocity;
        iniMaxAcceleration = maxAcceleration;
        turboOnVelocity = maxVelocity * 2;
        turboOnAcceleration = maxAcceleration * 2;
    }
    private void LateUpdate()
    {
        Move();
        Steer();
        Brake();
        Turbo();
        
    }
    // Update is called once per frame
    void Update()
    {

        AnimationWheels();
    }
    private void Move()
    {
        velocity = maxAcceleration * Time.deltaTime * 600 * InputManager._INPUT_MANAGER.leftAxisValue.y;
        if(velocity >= maxVelocity)
        {
            velocity = maxVelocity;
        }
       
        foreach(var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = velocity * Time.deltaTime * 600;
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
    void Turbo()
    {
        if(InputManager._INPUT_MANAGER.isTurbo ==1 && GameManager._GAME_MANAGER.theresTurboRemaining)
        {
            maxVelocity = turboOnVelocity;
            maxAcceleration = turboOnAcceleration;
            GameManager._GAME_MANAGER.TurboOn();

        }
        if (InputManager._INPUT_MANAGER.isTurbo == 0)
        {

            maxAcceleration = iniMaxAcceleration;
            maxVelocity = iniMaxVelocity;
            StartCoroutine(GameManager._GAME_MANAGER.RecoverTurbo());
        }
        else if(GameManager._GAME_MANAGER.theresTurboRemaining == false && InputManager._INPUT_MANAGER.isTurbo == 0)
        {
            maxAcceleration = iniMaxAcceleration;
            maxVelocity = iniMaxVelocity;
            StartCoroutine(GameManager._GAME_MANAGER.RecoverTurbo());
        }
    }
}
