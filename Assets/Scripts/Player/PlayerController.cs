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
    private float balanceValue;
    public float balance;
    float moveInput;
    public Rigidbody carRB;
    public GameObject _light1;
    public GameObject _light2;
    #endregion
   /* #region Sound
    public float minSpeed;
    public float maxSpeed;
    private float currentSpeed;
    public AudioSource carAudio;
    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;
    #endregion*/


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
        //TwoWheels();
    }
    // Update is called once per frame
    void Update()
    {

        AnimationWheels();
        //EngineSound();
        if (Input.GetKeyDown(KeyCode.R)){
            GameManager._GAME_MANAGER.ResetPlayer();
        }

        RaycastHit hit;
        
        if (Physics.Linecast(_light1.transform.position, _light1.transform.position+new Vector3(transform.forward.x, transform.forward.y , transform.forward.z ), out hit)){
           
            Debug.Log("hit");
            carRB.velocity = Vector3.zero;
        }
        Debug.DrawLine(_light1.transform.position, _light1.transform.position + new Vector3(transform.forward.x, transform.forward.y, transform.forward.z), Color.yellow);

    }
    private void Move()
    {                                  
        velocity = maxAcceleration * Time.deltaTime * 100 * InputManager._INPUT_MANAGER.leftAxisValue.y;
        if(velocity >= maxVelocity)
        {
            velocity = maxVelocity;
        }
        if(InputManager._INPUT_MANAGER.leftAxisValue.y == -1)
        {
            velocity = maxAcceleration * Time.deltaTime * 1000 * InputManager._INPUT_MANAGER.leftAxisValue.y;
        }
        if(velocity < -100)
        {
            velocity = -100;
        }
        if(InputManager._INPUT_MANAGER.leftAxisValue.y == 0 && velocity>0)
        {
            velocity -= maxAcceleration * Time.deltaTime * 1000;
        }
        if (InputManager._INPUT_MANAGER.leftAxisValue.y == 0 && velocity < 0)
        {
            velocity += maxAcceleration * Time.deltaTime * 1000;
        }
       
           // carRB.velocity = Vector3.zero;
        
        
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = velocity * Time.deltaTime * 300;
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
    /* void TwoWheels()
     {
         if (InputManager._INPUT_MANAGER.isTwoWheels == 1)
         {
             //balance++;
             balanceValue += Time.deltaTime ;
             balance = Mathf.Clamp(balanceValue, 0.5f, 7);
             carRB.AddRelativeTorque(transform.forward * balance, ForceMode.Acceleration);
             Debug.Log(balance);
         }if(InputManager._INPUT_MANAGER.isTwoWheels == 0)
         {
             balance = 0;
         }
     }*/

   /* void EngineSound()
    {
        currentSpeed = velocity;
        pitchFromCar = velocity / 60f;

        if (currentSpeed < minSpeed)
        {
            carAudio.pitch = minPitch;
        }

        if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carAudio.pitch = minPitch + pitchFromCar;
        }

        if (currentSpeed > maxSpeed)
        {
            carAudio.pitch = maxPitch;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MissionPoint")
        {
            GameManager._GAME_MANAGER.InitMission();
            
            Time.timeScale = 0;
            
        }
        
        if (other.gameObject.tag == "ArrivingPoint")
        {
            GameManager._GAME_MANAGER.EndMission();
           
            
        }
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "DepartPoint")
        {

            GameManager._GAME_MANAGER.isWaiting = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DepartPoint")
        {

            GameManager._GAME_MANAGER.isWaiting = false;

        }
    }

}
