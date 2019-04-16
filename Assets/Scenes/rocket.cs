using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour {

   [SerializeField] float rcsThrust = 100f;
   [SerializeField] float mainThrust = 100f;

    Rigidbody rigidBody;
    AudioSource m_MyAudioSource;

    enum State { Alive, Dying, Transcending }; // behaviour of rocket when it cosision with something
    State state = State.Alive;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        m_MyAudioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive)
        {
            Thrust_of_rocket(); //we work on our input keys whitch help rocket to rotate and fly
            Rotate_of_Rocket();
        }
    }
    void OnCollisionEnter(Collision collision)//use Colision for objeckt and rocket
    {
        if (state != State.Alive) { return;}// ignore colisions when I dead
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
               Invoke("LoadNextLevel", 1f); // Invoke help to hold for 1 second befor use next level
                break;
            case "Finish1":
                SceneManager.LoadScene(2);
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                break;
        }

    }

    private void LoadNextLevel() 
    {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel()
    {
            SceneManager.LoadScene(0);
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
