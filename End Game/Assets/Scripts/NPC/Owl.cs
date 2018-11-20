using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Owl : NPC
{
    private GameObject player;
    private Rigidbody RB;
    private NavMeshAgent NMA;

    GameManagerScript game;
    Interactions Inter;
    private Collider coll;

    public float OwlTime;
    private Animator animat;

    private GameObject BearPos;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        NMA = GetComponent<NavMeshAgent>();
        player = GameObject.Find("FPSController");
        BearPos = GameObject.FindGameObjectWithTag("BearEye");

        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        Inter = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Interactions>();
        transform.GetChild(0).gameObject.SetActive(false);
        animat = GetComponent<Animator>();

        coll = GetComponent<Collider>();
        coll.enabled = !coll.enabled;

        //timeToRevertMax = 30;

        timeToTransform = timeToTransformMax;
        //timeToRevert = timeToRevertMax;

        isDemon = false;
        isSearching = false;
        //inToyForm = true;
    }

    // Funtoin to set daze and animation and position.




    void Update()
    {

        //dazeTimer -= Time.deltaTime;
        //if (dazeTime > 0)
        //{
        //    T.pos = stunnpos
        //    return;
        //}
            //=================================================================================
        // Countdowns and timers
        //=================================================================================

        //Countdown to demon form
        if (timeToTransform >= 0 && game.isTutorialFinished)
        {
            timeToTransform -= Time.deltaTime;
        }
        //// In demon form, countdown to toy form
        //if (timeToRevert >= 0 && !inToyForm) {
        //    timeToRevert -= Time.deltaTime;
        //}

        //=================================================================================
        // Timers  while in toy/demon form
        //=================================================================================

        // when in TOY form, and not 'taken care of' and countdown reaches 0, transform to demon
        if (OwlTime >= 0)
        {
            animat.SetBool("isAttacking", false);
            animat.SetBool("isWalking", false);
            StopSearching();
            OwlTime -= 1;
            //Debug.Log(OwlTime);
        }
        else if(OwlTime <= 0)
        { 
            if (isSearching)
            {
                animat.SetBool("isAttacking", false);
                animat.SetBool("isWalking", true);
                FollowPlayer();
            }
            else if (timeToTransform <= 0 && !isSearching)
            {
                DemonForm();
                if(!isDemon)
                {
                    coll.enabled = !coll.enabled;
                    isDemon = true;
                }
            }
        }

        //// when in DEMON form, and conditions met, turns back to toy form
        //if (timeToRevert <= 0 && !inToyForm) {
        //    ToyForm();
        //}
    }

    //public void FixedUpdate() {
    //    //else if (!isSearching && inToyForm) {
    //    //    StopSearching();
    //    //}
    //}

    public void DemonForm()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        NMA.isStopped = false;
        isSearching = true;
        RB.AddForce(0, 10, 0);
        this.gameObject.transform.localScale = new Vector3(scale, scale, scale); //scale size
    }

    public void ToyForm()
    {
        StopSearching();
        //inToyForm = true;
        this.gameObject.transform.localScale = new Vector3(scale, scale, scale); // scale size
        timeToTransform = timeToTransformMax;
    }

    public void FollowPlayer()
    {
        NMA.destination = player.transform.position;
    }

    public void StopSearching()
    {
        RB.velocity = new Vector3(0, 0, 0);
        NMA.isStopped = true;
        isSearching = false;
    }

    public void KillPlayer()
    {
        // CALL CAMERA FUNTION FROM PLAYER SCRIPT
        // PUT PLAYER INFRONT OF MONSTER
        //playerCam.transform.position = playerKillPos.position;
        player.transform.LookAt(BearPos.transform.position);
        // GET PLAYER TO FACE MONSTER
        //I AM HERE

        // PLAY KILL ANIMATION
        game.isGameOver = true;
        // SET GAMEOVER THINGS
    }

    public void Patrol()
    {
        // DO PATROL STUFF
    }

    public void PlayAnimation()
    {
        animat.SetBool("isAttacking", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopSearching();
            PlayAnimation();
            KillPlayer();
        }
    }
}
