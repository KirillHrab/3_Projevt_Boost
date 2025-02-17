﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;

    // Audio for our  Rocket
    [SerializeField] AudioClip MainEngine;
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip WinLevel;
 
    
    // Particle systems
    [SerializeField] ParticleSystem MaineEngineParticle; 
    [SerializeField] ParticleSystem DeathParticle;
    [SerializeField] ParticleSystem WinLevelParticle;

    Rigidbody rigidBody;
    AudioSource m_MyAudioSource;
   
    // behaviour of rocket when it cosision with something
    enum State { Alive, Dying, Transcending}; 
    State state = State.Alive;
    bool collisionsDisabled = false;

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
        if (Debug.isDebugBuild)
        {
            debugKeys();
        }
    }
    //use Colision for objeckt and rocket
    void OnCollisionEnter(Collision collision)
    {

        if (state != State.Alive || collisionsDisabled) { return;}// ignore colisions when I dead
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                Start_win();
                break;          
            default:
                Start_Death();
                break;
        }

    }

    private void Start_Death()
    {
        state = State.Dying;
        m_MyAudioSource.Stop();
        m_MyAudioSource.PlayOneShot(Death);
        DeathParticle.Play();
        Invoke("LoadCurrentLevel", levelLoadDelay);

    }

    private void Start_win()
    {
        state = State.Transcending;
        m_MyAudioSource.Stop();
        m_MyAudioSource.PlayOneShot(WinLevel);
        WinLevelParticle.Play();
        Invoke("LoadNextLevel", levelLoadDelay); // Invoke help to hold for 1 second befor use next level
    }

    private void LoadNextLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        int maxSceneNumber = SceneManager.sceneCountInBuildSettings;

        if (nextSceneIndex == maxSceneNumber)
        {
            SceneManager.LoadScene(0);
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void LoadCurrentLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void Thrust_of_rocket()
    {
        float ThrustThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))  //if we hit space, what happend
        {
            NewMethod(ThrustThisFrame);
        }
        else
        {
            m_MyAudioSource.Stop();
            MaineEngineParticle.Stop();
        }
    }

    private void NewMethod(float ThrustThisFrame)
    {
        ApllyThrust(ThrustThisFrame);
        MaineEngineParticle.Play();
    }

    private void ApllyThrust(float ThrustThisFrame)
    {
        rigidBody.AddRelativeForce(Vector3.up * ThrustThisFrame * Time.deltaTime);

        if (!m_MyAudioSource.isPlaying) // if audio effect sleep we awake it
        {
            m_MyAudioSource.PlayOneShot(MainEngine);
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
    private void debugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled;
        }
    }

}
