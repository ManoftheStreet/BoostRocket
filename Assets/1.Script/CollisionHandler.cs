using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 2f;
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip winClip;

    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem winPaticle;

    AudioSource audioSource;
    Movement movement;
    Rigidbody rb;

    bool isTransition = false;
    bool collisionDisabled = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransition || collisionDisabled) return;

        switch (collision.gameObject.tag)
        {
            case "Start":
                Debug.Log("Start");
                break;
            case "Finish":
                StartNextLevel();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void Update()
    {
        ResPondToDebugkeys();
    }

    void StartCrashSequence()
    {
        isTransition = true;
        deathParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(deathClip);
        movement.enabled = false;
        Invoke("ReLoadLevel", delayTime);
    }

    void ReLoadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void StartNextLevel()
    {
        isTransition = true;
        winPaticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(winClip);
        movement.enabled = false;
        Invoke("LoadNextLevel", delayTime);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSecneIndex = currentSceneIndex + 1;

        if(nextSecneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSecneIndex = 0;
        }
        SceneManager.LoadScene(nextSecneIndex);
    }

    private void ResPondToDebugkeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
}
