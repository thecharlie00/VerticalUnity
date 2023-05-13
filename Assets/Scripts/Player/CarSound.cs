using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    private float currentSpeed;

    private Rigidbody carRB;
    private AudioSource carAudio;

    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;

    private void Start()
    {
        carAudio = GetComponent<AudioSource>();
        carRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        EngineSound();
    }
    void EngineSound()
    {
        currentSpeed = carRB.velocity.magnitude;
        pitchFromCar = carRB.velocity.magnitude / 50;

        if(currentSpeed < minSpeed)
        {
            carAudio.pitch = minPitch;
        }
        if(currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carAudio.pitch = minPitch + pitchFromCar;
        }
        if(currentSpeed > maxSpeed)
        {
            carAudio.pitch = maxPitch;
        }
        
    }

}
