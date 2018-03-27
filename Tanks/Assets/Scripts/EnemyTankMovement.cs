using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyTankMovement : MonoBehaviour
{

    public float stopDistance = 8.0f;

    public Transform m_turret;
    // public GameObject m_base;

    private GameObject m_player = null;
    private NavMeshAgent m_navAgent;
    private Rigidbody m_rigid;

    private bool m_follow = false;

    public GameObject[] waypoints;
    private int currentWaypoint = 0;


    // Use this for initialization
    void Start()
    {
        //m_player = GameObject.FindGameObjectWithTag("Player");

        m_navAgent = GetComponent<NavMeshAgent>();
        m_rigid = GetComponent<Rigidbody>();
        m_follow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_follow == true)
        {
            // enemy tank follows player
            float distance = (m_player.transform.position - transform.position).magnitude;
            if (distance > stopDistance)
            {
                m_navAgent.SetDestination(m_player.transform.position);
                m_navAgent.isStopped = false;
            }
            else
            {
                m_navAgent.isStopped = true;
            }

            if (m_turret != null)
            {
                m_turret.LookAt(m_player.transform);
            }
        }
        else
        {
            // patrol the waypoints
            if (m_navAgent.remainingDistance < 0.1f)
            {

                if (waypoints != null)
                {
                    // This makes the tanks randomly select a waypoint to travel to
                    currentWaypoint = Random.Range(0, waypoints.Length);

                    /*
                     this makes the waypoints advance in order
                      currentWaypoint += 1;
                    if (currentWaypoint >= waypoints.Length)
                    {
                        currentWaypoint = 0;
                    }*/

                    if (waypoints[currentWaypoint] != null)
                    {
                        GameObject randomWaypoint = waypoints[currentWaypoint];
                        m_navAgent.SetDestination(randomWaypoint.transform.position);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") == true)
        {
            m_player = other.gameObject;
            m_follow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //What happens when the player gets back out of range
        if (other.tag.Equals("Player") == true)
        {
            m_follow = false;
            m_navAgent.isStopped = true;

            if (waypoints != null && waypoints[currentWaypoint] != null)
            {
                GameObject randomWaypoint = waypoints[currentWaypoint];
                m_navAgent.isStopped = false;
                m_navAgent.SetDestination(randomWaypoint.transform.position);
            }
        }
    }
}
