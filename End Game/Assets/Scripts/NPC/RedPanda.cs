using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RedPanda : NPC
{
    private GameObject player;
    private GameObject Player;
    private GameObject origen;
    public GameObject[] hidding;
    private Rigidbody RB;
    private NavMeshAgent NMA;
    private int rand;

    private float timeCheck;
    public float counter;
    private float VisDist;

    private float rayDistance;
    private float SphereRadius;
    private Vector3 Origen;

    private GameObject target;
    private Collider coll;

    [HideInInspector] public bool isRunning;

    //Interactions cam;
    GameManagerScript game;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        NMA = GetComponent<NavMeshAgent>();
        player = GameObject.Find("FPSController");

        game = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        origen = GameObject.Find("Origen");
        //hidding = GameObject.FindGameObjectsWithTag("Hideable");
         
        Player = GameObject.FindGameObjectWithTag("Player");
        hidding = GameObject.FindGameObjectsWithTag("Hideable");

        transform.GetChild(0).gameObject.SetActive(false);
        coll = GetComponent<Collider>();
        coll.enabled = !coll.enabled;

        isRunning = false;
        timeCheck = 100.0f;
        counter = timeCheck;

        rayDistance = 15;
        VisDist = rayDistance;
        SphereRadius = 2.0f;

        //timeToTransformMax = 5;
        //timeToRevertMax = 30;

        //timeToTransform = timeToTransformMax;
        //timeToRevert = timeToRevertMax;

        isSearching = false;
        //inToyForm = true;
    }

    void Update()
    {
        //=================================================================================
        // Countdowns and timers
        //=================================================================================

        //// Countdown to demon form
        //if (timeToTransform >= 0 && inToyForm) {
        //    timeToTransform -= Time.deltaTime;
        //}

        //// In demon form, countdown to toy form
        //if (timeToRevert >= 0 && !inToyForm) {
        //    timeToRevert -= Time.deltaTime;
        //}

        //=================================================================================
        // Timers  while in toy/demon form
        //=================================================================================
        Origen = transform.position;

        if (counter <= 0)
        {
            counter = timeCheck;
            Sense();
        }
        counter--;

        // when in TOY form, and not 'taken care of' and countdown reaches 0, transform to demon
        //if (timeToTransform <= 0 && inToyForm) {
        //    DemonForm();
        //}

        //// when in DEMON form, and conditions met, turns back to toy form
        //if (timeToRevert <= 0 && !inToyForm) {
        //    ToyForm();
        //}
    }

    public void FixedUpdate()
    {
        //if (isSearching && !inToyForm) {
        //    FollowPlayer();
        //}
        //else if (!isSearching && inToyForm) {
        //    StopSearching();
        //}
    }

    public void DemonForm()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        coll.enabled = !coll.enabled;
        NMA.isStopped = false;
        //inToyForm = false;
        isSearching = true;
        RB.AddForce(0, 10, 0);
        this.gameObject.transform.localScale = new Vector3(scale, scale, scale); //scale size
        timeToRevert = timeToRevertMax;
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
        NMA.destination = player.transform.position;
    }

    public void MoveToOrigen()
    {
        isSearching = false;
        isHunting = false;
        NMA.destination = origen.transform.position;
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

        // GET PLAYER TO FACE MONSTER
        //I AM HERE

        // PLAY KILL ANIMATION
        game.isGameOver = true;
        // SET GAMEOVER THINGS
    }

    public void Search()
    {
        //DO SEARCH STUFF
        rand = Random.Range(0, 2);

        Debug.Log(rand);
        if(rand < 1)
        {
            Debug.Log("Checked Hidding place");

            KillPlayer();
        }
        else
        {
            Debug.Log("Didnt check hidding place");
            MoveToOrigen();
        }
    }

    public void Distracted()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopSearching();
            KillPlayer();
        }
    }

    public void Sense()
    {
        RaycastHit hit; 

        //0 degrees
        if (Physics.Raycast(Origen, transform.forward, out hit, rayDistance))
        {
            target = hit.collider.gameObject;

            for (int i = 0; i < hidding.Length; i++)
            {
                if (target == hidding[i])
                {
                    Search();
                }
            }
            if (hit.collider.tag == "Player")
            {
                FollowPlayer();
            }
        }

        //45 degrees
        if (Physics.Raycast(Origen, (transform.forward + transform.right), out hit, rayDistance))
        {
            target = hit.collider.gameObject;

            for (int i = 0; i < hidding.Length; i++)
            {
                if (target == hidding[i])
                {
                    Search();
                }
            }

            if (hit.collider.tag == "Player")
            {
                FollowPlayer();
            }
        }

        //90 degrees
        if (Physics.Raycast(Origen, transform.right, out hit, rayDistance))
        {
            target = hit.collider.gameObject;

            for (int i = 0; i < hidding.Length; i++)
            {
                if (target == hidding[i])
                {
                    Search();
                }
            }

            if (hit.collider.tag == "Player")
            {
                FollowPlayer();
            }
        }

        //135 degrees
        if (Physics.Raycast(Origen, (transform.right -transform.forward), out hit, rayDistance))
        {
            target = hit.collider.gameObject;

            for (int i = 0; i < hidding.Length; i++)
            {
                if (target == hidding[i])
                {
                    Search();
                }
            }

            if (hit.collider.tag == "Player")
            {
                FollowPlayer();
            }
        }

        //180 degrees
        if (Physics.Raycast(Origen, -transform.forward, out hit, rayDistance))
        {
            target = hit.collider.gameObject;

            for (int i = 0; i < hidding.Length; i++)
            {
                if (target == hidding[i])
                {
                    Search();
                }
            }

            if (hit.collider.tag == "Player")
            {
                FollowPlayer();
            }
        }

        //225 degrees
        if (Physics.Raycast(Origen, (-transform.forward + -transform.right), out hit, rayDistance))
        {
            target = hit.collider.gameObject;

            for (int i = 0; i < hidding.Length; i++)
            {
                if (target == hidding[i])
                {
                    Search();
                }
            }

            if (hit.collider.tag == "Player")
            {
                FollowPlayer();
            }
        }

        //270 degrees
        if (Physics.Raycast(Origen, -transform.right, out hit, rayDistance))
        {
            target = hit.collider.gameObject;

            for (int i = 0; i < hidding.Length; i++)
            {
                if (target == hidding[i])
                {
                    Search();
                }
            }

            if (hit.collider.tag == "Player")
            {
                FollowPlayer();
            }
        }

        //395 degrees
        if (Physics.Raycast(Origen, (-transform.forward + transform.forward), out hit, rayDistance))
        {
            target = hit.collider.gameObject;

            for (int i = 0; i < hidding.Length; i++)
            {
                if (target == hidding[i])
                {
                    Search();
                }
            }

            if (hit.collider.tag == "Player")
            {
                FollowPlayer();
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //0 degrees
        Debug.DrawLine(Origen, Origen + transform.forward * VisDist);
        //45 degrees
        Debug.DrawLine(Origen, Origen + (transform.forward + transform.right) * (VisDist - 2));
        //90 degrees
        Debug.DrawLine(Origen, Origen + transform.right * VisDist);
        //135 degrees
        Debug.DrawLine(Origen, Origen + (transform.right + -transform.forward) * (VisDist - 2));
        //180 degrees
        Debug.DrawLine(Origen, Origen + -transform.forward * VisDist);
        //225 degrees
        Debug.DrawLine(Origen, Origen + (-transform.forward + -transform.right) * (VisDist - 2));
        //270 degrees
        Debug.DrawLine(Origen, Origen + -transform.right * VisDist);
        //395 degrees
        Debug.DrawLine(Origen, Origen + (-transform.right + transform.forward) * (VisDist - 2));

       
        //Gizmos.DrawWireSphere(Origen + transform.forward * VisDist, SphereRadius);
    }
}
