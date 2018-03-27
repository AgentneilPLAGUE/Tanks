using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankShooting : MonoBehaviour
{

    public GameObject TankShellPrefab;
    public Transform fireTransform;
    public float launchForce = 30f;
    public float shootDelay = 1f;

    private bool canShoot = false;
    private float shootTimer = 0;


    // Use this for initialization
    void Start()
    {
        canShoot = false;
        shootTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot == true)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                shootTimer = shootDelay;
                Fire();
            }
        }
    }

    private void Fire()
    {
        GameObject TankShell = Instantiate(TankShellPrefab, fireTransform.position, fireTransform.rotation);
        Rigidbody TankShellRigidBody = TankShell.GetComponent<Rigidbody>();

        TankShellRigidBody.velocity = launchForce * fireTransform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canShoot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canShoot = false;
        }
    }
}
