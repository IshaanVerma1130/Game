using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotThrust = 100f;
    [SerializeField] float upThrust = 2f;

    Rigidbody rigidBody;
    AudioSource rocketThrusters;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        rocketThrusters = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void Thrust()
    {
        float upThrustThisFrame = upThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * upThrustThisFrame);

            if (rocketThrusters.isPlaying == false)
            {
                rocketThrusters.Play();
            }
        }

        else
        {
            rocketThrusters.Stop();
        }
    }

    void Rotate()
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
}
