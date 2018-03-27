using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float maxLifeTime = 2f;
    public float maxDamage = 34f;
    public float explosionRadius = 5;
    public float explosionForce = 100f;

    public ParticleSystem explosionParticles;

    // Use this for initialization
    void Start()
    {

        Destroy(gameObject, maxLifeTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody targetRigidBody = collision.gameObject.GetComponent<Rigidbody>();
        if (targetRigidBody != null)
        {
            // adds an explosive force to the shell
            targetRigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

        }

        TankHealth health = collision.gameObject.GetComponent<TankHealth>();
        if (health != null)
        {
            float damage = CalculateDamage(targetRigidBody.position);
            health.TakeDamage(maxDamage);
        }
        MapDamage life = collision.gameObject.GetComponent<MapDamage>();
        if (life != null)
        {
            float damage = CalculateDamage(targetRigidBody.position);
            life.TakeDamage(maxDamage);
        }

        explosionParticles.transform.parent = null;
        explosionParticles.Play();

        Destroy(explosionParticles.gameObject, explosionParticles.main.duration);

        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;

        float damage = relativeDistance * maxDamage;

        damage = Mathf.Max(0f, damage);

        return damage;
    }

}
