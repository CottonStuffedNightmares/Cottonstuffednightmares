using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MenuAI : NPC
{
    private float MoveSpeed;
    private float Monstertimer;
    private float BearTimer;

    private NavMeshAgent NMA;
    private Animator animat;

    private GameObject Bear;
    private GameObject Owl;
    private GameObject Croc;

    // Use this for initialization
    void Start ()
    {
        NMA = GetComponent<NavMeshAgent>();
        animat = GetComponent<Animator>();

        Monstertimer = 30;
        BearTimer = Monstertimer;
        MoveSpeed = 3;

        Bear = GameObject.FindGameObjectWithTag("Bear");
        Owl = GameObject.FindGameObjectWithTag("Owl");
        Croc = GameObject.FindGameObjectWithTag("Crocodile");

        Bear.gameObject.SetActive(false);
        Owl.gameObject.SetActive(false);
        Croc.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (BearTimer != 0)
        {
            if(BearTimer >= 0)
            {
                Debug.Log("FIIIIIIIIIISH");
            }
            BearTimer--;
        }
	}
}
