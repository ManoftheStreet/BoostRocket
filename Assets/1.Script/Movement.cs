using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //�����Ϳ��� ������ ������
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftThrusterPaticle;
    [SerializeField] ParticleSystem rightThrusterPaticle;

    //ĳ��(������Ʈ ���۷���)
    Rigidbody rb;
    AudioSource audioSource;

    //STATE �� �����̺� ����


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!mainEngineParticle.isPlaying)
        {
            mainEngineParticle.Play();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    void StopThrusting()
    {
        mainEngineParticle.Stop();
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            return;
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);

        if (!rightThrusterPaticle.isPlaying)
        {
            rightThrusterPaticle.Play();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);

        if (!leftThrusterPaticle.isPlaying)
        {
            leftThrusterPaticle.Play();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    void StopRotating()
    {
        rightThrusterPaticle.Stop();
        leftThrusterPaticle.Stop();
        if (!Input.GetKey(KeyCode.Space))
        {
            audioSource.Stop();
        }
    }
}
