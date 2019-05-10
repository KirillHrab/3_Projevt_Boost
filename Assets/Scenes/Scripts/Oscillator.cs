using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]//told us that we can use only 1 object
public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;

    [SerializeField]
    [Range(0, 1)] //0 it's 0%, 1 it's 100%
    float MovementFactor;//this is % of our oscillate 
    Vector3 startingPos;

	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if ( period <= Mathf.Epsilon) { return; } // if period = 0 obstacle do nothing 

        float cycles = Time.time / period; //grows from 0 to continually
        const float tau = Mathf.PI * 2; // our tau - 6.28
        float rowSinWave = Mathf.Sin(cycles * tau);

        print(rowSinWave);

        MovementFactor = rowSinWave / 2f + 0.5f;

        Vector3 offset = MovementFactor * movementVector;
        transform.position = startingPos + offset;
	}
}
