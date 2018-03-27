using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour {


    public GameObject TankShellPrefab;
    public Transform fireTransform;
    public float launchForce = 30f;


    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetButtonDown("Shoot"))
        {
            Fire();
        }
       
    }

    private void Fire()
    {
        GameObject TankShell = Instantiate(TankShellPrefab, fireTransform.position, fireTransform.rotation);
        Rigidbody shellRigidBody = TankShell.GetComponent<Rigidbody>();

        shellRigidBody.velocity = launchForce * fireTransform.forward;
    }


	
}
