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
    private float turboOnVelocity;
    private float turboOnAcceleration;
    float moveInput;
    public CharacterController controller;
    private Vector3 finalVelocity = Vector3.zero;
    private float velocityXZ = 5f;
    public float steerPower;
    private float gravity = 20f;
    private float brake;
    private bool isMoving;
    public float timeToZeroVelocity;
    private float currentTime;
    #endregion



    void Start()
    {
        
        iniMaxVelocity = maxVelocity;
        iniMaxAcceleration = maxAcceleration;
        turboOnVelocity = maxVelocity * 2;
        turboOnAcceleration = maxAcceleration * 2;
        isMoving = false;
    }
    private void LateUpdate()
    {
        Move();
        //Brake();
        Steer();
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
        /*if(InputManager._INPUT_MANAGER.leftAxisValue.y == 1 || InputManager._INPUT_MANAGER.leftAxisValue.y == -1)
        {
            velocity = maxAcceleration * Time.deltaTime * 600;
        }
        
        Vector3 direction = InputManager._INPUT_MANAGER.leftAxisValue.y * transform.forward;
        direction.Normalize();
        //Calcular velocidad XZ
        finalVelocity.x = direction.x * velocity * Time.deltaTime;
        finalVelocity.z = direction.z * velocity * Time.deltaTime;
       
          
        direction.y = -1f;
        finalVelocity.y += direction.y * gravity * Time.deltaTime;
        controller.Move(finalVelocity * Time.deltaTime *100);*/
        
       if(InputManager._INPUT_MANAGER.leftAxisValue.y != 0)
       {
            velocity = maxAcceleration * Time.deltaTime * 600;
            steerPower = maxSteerAngle * Time.deltaTime;
       }
       if(InputManager._INPUT_MANAGER.leftAxisValue.y == 0)
       {
            velocity = 0;
            steerPower = 0;
       }
       if(velocity >= maxVelocity)
       {
            velocity = maxVelocity;
       }


        
        transform.Rotate(0.0f, InputManager._INPUT_MANAGER.leftAxisValue.x, 0.0f);
        
        Vector3 direction = InputManager._INPUT_MANAGER.leftAxisValue.y * transform.forward;
        direction.Normalize();
            
        finalVelocity.x = direction.x * steerPower * Time.deltaTime;
        finalVelocity.z = direction.z * velocity * Time.deltaTime;


        direction.y = -1f;
            
        finalVelocity.y += direction.y * gravity * Time.deltaTime;
        controller.Move(finalVelocity * Time.deltaTime * 100);
        
    }
    
    void Steer()
    {
        
    }
    /*void Brake()
    {
        if(InputManager._INPUT_MANAGER.isBraking == 1)
        {
            brake = maxBrakeAcceleration * 300 * Time.deltaTime;
            velocity = velocity - brake * Time.deltaTime;
        }
        if (velocity <= 0)
        {
            velocity = 0;
        }
       
    }*/
    void AnimationWheels()
    {
        /*/foreach (var wheel in wheels)
        {
            Quaternion rotation;
            Vector3 pos;
            //wheel.wheelCollider.GetWorldPose(out pos, out rotation);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rotation;
        }*/
    }
    void Turbo()
    {
        if(InputManager._INPUT_MANAGER.isTurbo ==1 && GameManager._GAME_MANAGER.theresTurboRemaining)
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
        else if(GameManager._GAME_MANAGER.theresTurboRemaining == false && InputManager._INPUT_MANAGER.isTurbo == 0)
        {
            maxAcceleration = iniMaxAcceleration;
            maxVelocity = iniMaxVelocity;
            StartCoroutine(GameManager._GAME_MANAGER.RecoverTurbo());
        }
    }
    void TwoWheels()
    {
        if(InputManager._INPUT_MANAGER.isTwoWheels == 1)
        {
           // carRB.MoveRotation(carRB.transform.rotation * Quaternion.AngleAxis(10, carRB.transform.forward));
            
            /*foreach(var wheel in wheels)
            {
                if(wheel.side ==)
            }
            carRB.MoveRotation(Quaternion.AngleAxis(1*Time.fixedDeltaTime, carRB.transform.right));*/
        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(wheels[3].wheelCollider.transform.position+new Vector3(0, wheels[3].wheelCollider.transform.position.y- wheels[3].wheelCollider.radius*3.25f,0), 1f);
    }
}
