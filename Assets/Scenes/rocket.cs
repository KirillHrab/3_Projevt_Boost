using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ProcessImput();
		
	}

    private void ProcessImput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("Space pressed");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            print("Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("Right");
        }
    }
}
