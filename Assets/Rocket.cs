using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotThrust = 100f;
    [SerializeField] float upThrust = 2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;
    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State
    {
        Alive, Dying, Transcending
    }

    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    private void RespondToThrustInput()
    {


        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }

        else
        {
            audioSource.Stop();
        }
    }

    private void ApplyThrust()
    {
        float upThrustThisFrame = upThrust * Time.deltaTime;
        rigidBody.AddRelativeForce(Vector3.up * upThrustThisFrame);

        if (audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    private void RespondToRotateInput()
    {
        rigidBody.freezeRotation = true;

        float rotThisFrame = rotThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(Vector3.forward * rotThisFrame);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(-Vector3.forward * rotThisFrame);
        }

        rigidBody.freezeRotation = false;
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == State.Dying) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartDeathSequence();
                break;
        }
    }
    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.PlayOneShot(success);
        Invoke("LoadNextLevel", 1);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.PlayOneShot(death);
        Invoke("LoadFirstScene", 1);
    }
}
