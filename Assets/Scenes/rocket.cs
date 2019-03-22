using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource m_MyAudioSource;


    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        m_MyAudioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);

            if (m_MyAudioSource.isPlaying == false)
            {
                m_MyAudioSource.Play();
            }
        }
        else 
        {
            m_MyAudioSource.Stop();
        }
        
         if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
            print("Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);  
            print("Right");         
        }
    }
}
