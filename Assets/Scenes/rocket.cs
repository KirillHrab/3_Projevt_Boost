using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour {

   [SerializeField] float rcsThrust = 100f;
   [SerializeField] float mainThrust = 100f;

    Rigidbody rigidBody;
    AudioSource m_MyAudioSource;


    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        m_MyAudioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        MovementRocket();
	}
    void OnCollisionEnter(Collision collision)
    {
        print("Colision");
    }

    private void MovementRocket() //we work on our input keys whitch help rocket to rotate and fly
    {
        Thrust_of_rocket();
        Rotate_of_Rocket();
    }


    private void Thrust_of_rocket()
    {
        float ThrustThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))  //if we hit space, what happend
        {
            rigidBody.AddRelativeForce(Vector3.up * ThrustThisFrame);

            if (m_MyAudioSource.isPlaying == false) // if audio effect sleep we awake it
            {
                m_MyAudioSource.Play();
            }
        }
        else
        {
            m_MyAudioSource.Stop();
        }
    }

    private void Rotate_of_Rocket()
    {
        rigidBody.freezeRotation = true;
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) // A
        {            
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D)) // D
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;
    }

   
}
