using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDamage : MonoBehaviour
{

    public float startHealth = 30f; 
    //shell does 34 damage per hit.

    public GameObject explosionPrefab;
    private ParticleSystem explosionParticles;

    // health conflicts with tank health script. changed to life for structure damage.

    private float life;
    private bool isDead = false;


    // Use this for initialization



    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        life = startHealth;
        isDead = false;

        explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosionParticles.gameObject.SetActive(false);

    }
        public void TakeDamage(float amount)
    {
        life -= amount;

        if (life <= 0 && isDead == false)
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
    }
}