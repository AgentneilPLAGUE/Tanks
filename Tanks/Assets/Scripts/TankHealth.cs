using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{

    public float startHealth = 100f;

    public GameObject explosionPrefab;
    private ParticleSystem explosionParticles;

    private float health;
    private bool isDead = false;


    // Use this for initialization

    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        health = startHealth;
        isDead = false;

        explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosionParticles.gameObject.SetActive(false);

        Debug.Log("Tank Initialized");
    }



    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0 && isDead == false)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        isDead = true;

        explosionParticles.transform.position = transform.position;
        explosionParticles.gameObject.SetActive(true);
        explosionParticles.Play();

        gameObject.SetActive(false);


       /* if (tag != "Player")
        {
            GameObject spawnPoint = GameObject.Find("Enemy Spawn");
            if (spawnPoint != null)
            {
                transform.position = spawnPoint.transform.position;
                Initialize();
                gameObject.SetActive(true);
            }
        }*/
    }
}
