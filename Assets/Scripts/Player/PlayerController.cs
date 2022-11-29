using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    #region Wheels
    public enum Axel
    {
        FRONT,
        REAR
    }
    public enum Side
    {
        RIGHT,
        LEFT
    }
    [System.Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
        public Side side;
    }
    public List<Wheel> wheels = new List<Wheel>();
    #endregion
    #region Variables 
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
    public float maxRot;
    private float currentRot;
    public float balance;
    float moveInput;
    public Rigidbody carRB;
    #endregion



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
        TwoWheels();
    }
    // Update is called once per frame
    void Update()
    {

        AnimationWheels();

    }
    private void Move()
    {                                  
        velocity = maxAcceleration * Time.deltaTime * 600 * InputManager._INPUT_MANAGER.leftAxisValue.y;
        if (velocity >= maxVelocity)
        {
            velocity = maxVelocity;
        }

        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = velocity * Time.deltaTime * 600;
        }
    }
    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.FRONT)
            {
                var _steerAngle = InputManager._INPUT_MANAGER.leftAxisValue.x * maxSteerAngle * turnSensivity;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }
    void Brake()
    {
        if (InputManager._INPUT_MANAGER.isBraking == 1)
        {
            Debug.Log("Freno");
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
        if (InputManager._INPUT_MANAGER.isTurbo == 1 && GameManager._GAME_MANAGER.theresTurboRemaining)
        {
            GameManager._GAME_MANAGER.isActive = true;
            maxVelocity = turboOnVelocity;
            maxAcceleration = turboOnAcceleration;
            GameManager._GAME_MANAGER.TurboOn();
        }
        if (InputManager._INPUT_MANAGER.isTurbo == 0)
        {
            GameManager._GAME_MANAGER.isActive = false;
            maxAcceleration = iniMaxAcceleration;
            maxVelocity = iniMaxVelocity;
            StartCoroutine(GameManager._GAME_MANAGER.RecoverTurbo());
        }
        else if (GameManager._GAME_MANAGER.theresTurboRemaining == false && InputManager._INPUT_MANAGER.isTurbo == 0)
        {
            maxAcceleration = iniMaxAcceleration;
            maxVelocity = iniMaxVelocity;
            StartCoroutine(GameManager._GAME_MANAGER.RecoverTurbo());
        }
    }
    void TwoWheels()
    {
        if (InputManager._INPUT_MANAGER.isTwoWheels == 1)
        {
            balance+=Time.deltaTime;
            Mathf.Clamp(balance, 0, 2);
            carRB.AddRelativeTorque(transform.forward * balance, ForceMode.Acceleration);
        }if(InputManager._INPUT_MANAGER.isTwoWheels == 0)
        {
            balance = 0;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(wheels[3].wheelCollider.transform.position + new Vector3(0, wheels[3].wheelCollider.transform.position.y - wheels[3].wheelCollider.radius * 3.25f, 0), 1f);
    }
}
