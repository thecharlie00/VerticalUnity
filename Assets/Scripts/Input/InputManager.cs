using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    private Input_Manager playerInputs;
    public static InputManager _INPUT_MANAGER;
    public Vector2 leftAxisValue = Vector2.zero;
    public float isBraking = 0f;
    public float isTurbo = 0f;
   
    private void Awake()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(_INPUT_MANAGER);

        }
        else
        {
            playerInputs = new Input_Manager();
            playerInputs.Enable();
            playerInputs.Player.Move.performed += LeftAxisUpdate;
            playerInputs.Player.BrakeStart.performed += x =>BrakePressed();
            playerInputs.Player.BrakeEnd.performed += x => BrakeReleased();
            playerInputs.Player.TurboStart.performed += x => TurboPressed();
            playerInputs.Player.TurboEnd.performed += x => TurboReleased();

            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }
        
    }

    public void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();
        Debug.Log("Magnitude: " + leftAxisValue.magnitude);
        Debug.Log("Normalize: " + leftAxisValue.normalized);
    }

   public void BrakePressed()
   {
        isBraking = 1;
   }
    public void BrakeReleased()
    {
        isBraking = 0;
    }
    public void TurboPressed()
    {
        isTurbo = 1;
    }
    public void TurboReleased()
    {
        isTurbo = 0;
    }
    private void Update()
    {

       
        
        InputSystem.Update();
    }
   
}