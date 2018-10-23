using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TeddyBear : NPC
{
    //private GameObject VisRange;
    private float VisDist = 30;
    private Vector3 origen;
    [SerializeField]
    private float yOffSet;

    private float MoveSpeed;

    private GameObject player;
    private Rigidbody RB;
    private NavMeshAgent NMA;

    GameManagerScript game;
    private Collider coll;

    private bool ReachedTarget = false;
    private Vector3 SeekPosition = Vector3.zero;
    private int PatrolIterator = 0;
    private Animator animat;

    void Start ()
    {
        RB = GetComponent<Rigidbody>();
        NMA = GetComponent<NavMeshAgent>();
        player = GameObject.Find("FPSController");

        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        animat = GetComponent<Animator>();

        transform.GetChild(0).gameObject.SetActive(false);
        coll = GetComponent<Collider>();
        coll.enabled = !coll.enabled;

        timeToTransformMax = 30;
        MoveSpeed = 10f;

        timeToTransform = timeToTransformMax;

        isSearching = false;

        SeekPosition = transform.position;
        NMA.SetDestination(SeekPosition);
    }

    void Update() {
        //=================================================================================
        // Countdowns and timers
        //=================================================================================

        //Debug.Log(timeToTransform);
        // Countdown to demon form
        if (timeToTransform >= 0 && game.isTutorialFinished)
        {
            timeToTransform -= Time.deltaTime;
        }

        //=================================================================================
        // Timers  while in toy/demon form
        //=================================================================================

        // when in TOY form, and not 'taken care of' and countdown reaches 0, transform to demon
        if (timeToTransform <= 0 && !isSearching)
        {
            DemonForm();
        }

        ReachedTarget = NMA.remainingDistance < 0.2f;
        VisualRange();
        if (isHunting)
        {
            FollowPlayer();
        }
        if(isSearching && !isHunting)
        {
            Patrol();
        }
    }
    
    public void DemonForm()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        coll.enabled = !coll.enabled;
        NMA.isStopped = false;
        isSearching = true;
        RB.AddForce(0, MoveSpeed, 0);
        this.gameObject.transform.localScale = new Vector3(scale, scale, scale); //scale size
    }

    public void ToyForm()
    {
        StopSearching();
        //inToyForm = true;
        this.gameObject.transform.localScale = new Vector3(1, 1, 1); // scale size
        timeToTransform = timeToTransformMax;
    }

    public void FollowPlayer()
    {
        animat.SetBool("isWalking", false);
        animat.SetBool("isRunning", true);
        NMA.speed = 100;
        NMA.destination = player.transform.position;
    }

    public void StopSearching()
    {
        RB.velocity = new Vector3(0, 0, 0);
        NMA.isStopped = true;
        isSearching = false;
    }

    public void KillPlayer() {
        // CALL CAMERA FUNTION FROM PLAYER SCRIPT
        // PUT PLAYER INFRONT OF MONSTER
        //playerCam.transform.position = playerKillPos.position;

        // GET PLAYER TO FACE MONSTER
        //I AM HERE

        // PLAY KILL ANIMATION
        game.isGameOver = true;
        // SET GAMEOVER THINGS
    }

    public void Patrol()
    {
        // DO PATROL STUFF
        NMA.speed = 3;
        animat.SetBool("isWalking", true);
        animat.SetBool("isRunning", false);
        if (ReachedTarget)
        {
            if (PatrolIterator >= patrolPoints.Length)
            {
                // Wrap index in case of overflow.
                PatrolIterator = 0;
            }

            //Debug.Log(patrolPoints[PatrolIterator]);
            NMA.SetDestination(patrolPoints[PatrolIterator].position);
            PatrolIterator++;
            PatrolIterator %= patrolPoints.Length;
        }
    }

    public void VisualRange()
    {
        RaycastHit hit;
        origen = new Vector3(transform.position.x, transform.position.y + yOffSet, transform.position.z);

        Debug.DrawRay(origen, transform.forward * VisDist, Color.red);
        Debug.DrawRay(origen, (transform.forward + transform.right) * VisDist, Color.red);
        Debug.DrawRay(origen, (transform.forward - transform.right) * VisDist, Color.red);
        Debug.DrawRay(origen, (transform.forward + transform.up) * VisDist, Color.red);
        Debug.DrawRay(origen, (transform.forward - transform.up) * VisDist, Color.red);

        if(Physics.Raycast(origen, transform.forward, out hit, VisDist))
        {
            if(hit.collider.tag == "Player" && isSearching)
            {
                isSearching = false;
                isHunting = true;
            }
            else if(hit.collider.tag == "Player" && isSearching)
            {
                isHunting = false;
                isSearching = true;
            }
        }
        if(Physics.Raycast(origen, transform.forward + transform.right, out hit, VisDist))
        {
            if(hit.collider.tag == "Player" && isSearching)
            {
                isSearching = false;
                isHunting = true;
            }
        }
        if(Physics.Raycast(origen, transform.forward - transform.right, out hit, VisDist))
        {
            if(hit.collider.tag == "Player" && isSearching)
            {
                isSearching = false;
                isHunting = true;
            }
        }
        if(Physics.Raycast(origen, transform.forward + transform.up, out hit, VisDist))
        {
            if(hit.collider.tag == "Player" && isSearching)
            {
                isSearching = false;
                isHunting = true;
            }
        }
        if(Physics.Raycast(origen, transform.forward - transform.up, out hit, VisDist))
        {
            if(hit.collider.tag == "Player" && isSearching)
            {
                isSearching = false;
                isHunting = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player")
        {
            StopSearching();
            KillPlayer();
        }
    }
}
