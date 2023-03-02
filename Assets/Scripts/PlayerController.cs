using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isGameOver;
    
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip moneySound;

    public ParticleSystem moneyParticle;
    public ParticleSystem crashParticle;
    
    private float floatForce = 10f;
    private float upperBound = 6f;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    private int totalPoints;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isGameOver)
        {
            _rigidbody.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
            _audioSource.PlayOneShot(jumpSound, 1f);
        }

        if (transform.position.y >= upperBound)
        {
            transform.position = new Vector3(0, upperBound, 0);
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            GameOver();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Money"))
        {
            _audioSource.PlayOneShot(moneySound, 1f);
            moneyParticle.Play();
            AddPoints(other.gameObject.GetComponent<Collectable>().points);
            Destroy(other.gameObject);
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        _audioSource.PlayOneShot(crashSound, 1f);
        crashParticle.Play();
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
    }

    private void AddPoints(int points)
    {
        totalPoints += points;
        Debug.Log($"Tienes {totalPoints} puntos");
    }
}
