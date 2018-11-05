using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class MenuAI : MonoBehaviour
{
    private MenuMobManager mobManager;
    private float MoveSpeed;
    private float Monstertimer;
    private float BearTimer;

    public bool isWalking;

    private NavMeshAgent NMA;
    private Animator animat;
    public GameObject currentDestination;
    public float distOffset = 5;

    // Use this for initialization
    void Start ()
    {
        mobManager = GameObject.Find("Menu_MobManager").GetComponent<MenuMobManager>();
        NMA = GetComponent<NavMeshAgent>();
        animat = GetComponent<Animator>();
        currentDestination = null;

        Monstertimer = 30;
        BearTimer = Monstertimer;
        MoveSpeed = 3;

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (this.gameObject.activeInHierarchy && currentDestination == null) {
            StartPatrolLap();
        }

        //if (NMA.remainingDistance <= NMA.stoppingDistance) {
        //    GetNextPoint();
        //}
	}

    public void GetNextPoint() {
        if (currentDestination == mobManager.patrolCheckpoint1) {
            currentDestination = mobManager.patrolCheckpoint2;
            NMA.destination = currentDestination.transform.position;
        }

        else if (currentDestination == mobManager.patrolCheckpoint2) {
            currentDestination = mobManager.patrolCheckpoint3;
            NMA.destination = currentDestination.transform.position;
        }

        else if (currentDestination == mobManager.patrolCheckpoint3) {
            currentDestination = mobManager.StartCheckpoint;
            NMA.destination = currentDestination.transform.position;
        }
    }

    private void StartPatrolLap() {
        currentDestination = mobManager.patrolCheckpoint1;
        NMA.destination = currentDestination.transform.position;
    }

    private void FinishedPatrolLap() {
        currentDestination = null;
        gameObject.SetActive(false);
        mobManager.SetPatrolDelayTimer(4);
    }

    private void OnTriggerEnter(Collider other) {

        if (currentDestination == mobManager.patrolCheckpoint1 && other.gameObject.name == "CheckPoint1") {
            GetNextPoint();
        }

        else if (currentDestination == mobManager.patrolCheckpoint2 && other.gameObject.name == "CheckPoint2") {
            GetNextPoint();
        }

        else if (currentDestination == mobManager.patrolCheckpoint3 && other.gameObject.name == "CheckPoint3") {
            GetNextPoint();
        }

        else if (currentDestination == mobManager.StartCheckpoint && other.gameObject == mobManager.StartCheckpoint) {
            FinishedPatrolLap();
        }
    }

}
