using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TeddyBear : NPC
{
    //private GameObject VisRange;
    private float VisDist = 30;
    //Default look
    private Vector3 origenCenter;
    private Vector3 origenRight;
    private Vector3 origenLeft;
    private Vector3 origenUp;
    private Vector3 origenDown;
    //Extra look
    private Vector3 origenUR;
    private Vector3 origenUL;
    private Vector3 origenDR;
    private Vector3 origenDL;

    [SerializeField]
    private float yOffSet;
    [SerializeField]
    private float xOffSet;

    private float MoveSpeed;

    private GameObject player;
    private GameObject BearPos;
    private Rigidbody RB;
    private NavMeshAgent NMA;

    GameManagerScript game;
    private Collider coll;

    private bool ReachedTarget = false;
    private Vector3 SeekPosition = Vector3.zero;
    private int PatrolIterator = 0;
    private Animator animat;
    private float DeathTime;

    private Owl owl;
    private Crocodile Croc;

    void Start ()
    {
        RB = GetComponent<Rigidbody>();
        NMA = GetComponent<NavMeshAgent>();
        player = GameObject.Find("FPSController");
        BearPos = GameObject.FindGameObjectWithTag("BearEye");

        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        animat = GetComponent<Animator>();

        transform.GetChild(0).gameObject.SetActive(false);
        coll = GetComponent<Collider>();
        coll.enabled = !coll.enabled;

        MoveSpeed = 10f;
        DeathTime = 0.3f;

        timeToTransform = timeToTransformMax;

        isDemon = false;
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
            if(!isDemon)
            {
                coll.enabled = !coll.enabled;
                isDemon = true;
            }
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
        animat.SetBool("isAttacking", false);
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

    public void KillPlayer()
    {
        // CALL CAMERA FUNTION FROM PLAYER SCRIPT
        // PUT PLAYER INFRONT OF MONSTER
        //playerCam.transform.position = playerKillPos.position;
        RB.velocity = new Vector3(0, 0, 0);
        NMA.isStopped = true;
        isSearching = false;
        // GET PLAYER TO FACE MONSTER
        //I AM HERE
        player.transform.LookAt(BearPos.transform.position);

        // PLAY KILL ANIMATION
        if (DeathTime <= 0)
        {
            game.isGameOver = true;
        }
            // SET GAMEOVER THINGS
    }

    public void Patrol()
    {
        // DO PATROL STUFF
        NMA.speed = 3;
        animat.SetBool("isAttacking", false);
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
        //default look
        origenCenter = new Vector3(transform.position.x, transform.position.y + yOffSet, transform.position.z);
        origenRight = new Vector3(transform.position.x + xOffSet, transform.position.y + yOffSet, transform.position.z);
        origenLeft = new Vector3(transform.position.x - xOffSet, transform.position.y + yOffSet, transform.position.z);
        origenUp = new Vector3(transform.position.x, transform.position.y + yOffSet + 0.5f, transform.position.z);
        origenDown = new Vector3(transform.position.x, transform.position.y + yOffSet - 0.5f, transform.position.z);

        //Extra look
        origenUR = new Vector3(transform.position.x + xOffSet - 0.5f, transform.position.y + yOffSet + 0.3f, transform.position.z);
        origenUL = new Vector3(transform.position.x - xOffSet + 0.5f, transform.position.y + yOffSet + 0.3f, transform.position.z);
        origenDR = new Vector3(transform.position.x + xOffSet - 0.5f, transform.position.y + yOffSet - 0.3f, transform.position.z);
        origenDL = new Vector3(transform.position.x - xOffSet + 0.5f, transform.position.y + yOffSet - 0.3f, transform.position.z);

        //Default look
        Debug.DrawRay(origenCenter, transform.forward * VisDist, Color.red);
        Debug.DrawRay(origenRight, transform.forward * VisDist, Color.red);
        Debug.DrawRay(origenLeft, transform.forward * VisDist, Color.red);
        Debug.DrawRay(origenUp, transform.forward * VisDist, Color.red);
        Debug.DrawRay(origenDown, transform.forward * VisDist, Color.red);

        //Extra looks
        Debug.DrawRay(origenUR, transform.forward * VisDist, Color.red);
        Debug.DrawRay(origenUL, transform.forward * VisDist, Color.red);
        Debug.DrawRay(origenDR, transform.forward * VisDist, Color.red);
        Debug.DrawRay(origenDL, transform.forward * VisDist, Color.red);

        if(Physics.Raycast(origenCenter, transform.forward, out hit, VisDist))
        {
            if(hit.collider.tag == "Player" && isSearching)
            {
                isSearching = false;
                isHunting = true;
            }
            else if(hit.collider.tag != "Player" && isSearching)
            {
                isHunting = false;
                isSearching = true;
            }
        }

        if (Physics.Raycast(origenRight, transform.forward, out hit, VisDist) && Physics.Raycast(origenUR, transform.forward, out hit, VisDist))
        {
            if(hit.collider.tag == "Player" && isSearching)
            {
                isSearching = false;
                isHunting = true;
            }
        }
        if (Physics.Raycast(origenRight, transform.forward, out hit, VisDist) && Physics.Raycast(origenDR, transform.forward, out hit, VisDist))
        {
            if (hit.collider.tag == "Player" && isSearching)
            {
                isSearching = false;
                isHunting = true;
            }
        }

        if (Physics.Raycast(origenLeft, transform.forward, out hit, VisDist) && Physics.Raycast(origenUL, transform.forward, out hit, VisDist))
        {
            if (hit.collider.tag == "Player" && isSearching)
            {
                isSearching = false;
                isHunting = true;
            }
        }
        if (Physics.Raycast(origenLeft, transform.forward, out hit, VisDist) && Physics.Raycast(origenDL, transform.forward, out hit, VisDist))
        {
            if (hit.collider.tag == "Player" && isSearching)
            {
                isSearching = false;
                isHunting = true;
            }
        }

        if (Physics.Raycast(origenUp, transform.forward, out hit, VisDist))
        {
            if(hit.collider.tag == "Player" && isSearching)
            {
                isSearching = false;
                isHunting = true;
            }
        }
        if (Physics.Raycast(origenDown, transform.forward, out hit, VisDist))
        {
            if(hit.collider.tag == "Player" && isSearching)
            {
                isSearching = false;
                isHunting = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopSearching();
            DeathTime -= 0.1f;
            animat.SetBool("isAttacking", true);
            KillPlayer();
        }
        if (other.gameObject.tag == "Corcodile")
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), Croc.GetComponent<Collider>());
        }
        if (other.gameObject.tag == "Owl")
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), owl.GetComponent<Collider>());
        }
    }
}
