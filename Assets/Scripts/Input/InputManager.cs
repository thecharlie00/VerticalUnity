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

    private void Update()
    {

        
        
        InputSystem.Update();
    }
   
}