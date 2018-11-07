using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

//=======================================================
//  Created By Liam Gates
//  Updates: Edward ngo
//=======================================================

public class Interactions : MonoBehaviour
{
    private Items items;
    private InfoDisplay infoDisplay;

    //Raycast Pickups
    public Camera Cam;
    public float rayDistance = 10;
    private float RayLine = 15f;
    private float SphereRadius;
    //private int layerMask;

    private Vector3 origin;
    private Owl Owl;
    private TeddyBear Bear;
    //Camera Rotations
    private float RotateY;
    //private float Vertical = 0.0f;
    //private float Horizontal = 0.0f;
    public float lookSpeed;
    public float MaxRotation;
    public float MinRotation;

    // Camera
    private Vector3 OriginalCameraPos;
    [HideInInspector] public GameObject target;
    //CursorLockMode cursorLock;

	// Use this for initialization
	void Start ()
    {
        //cursorLock = CursorLockMode.Locked;
        Cam = this.GetComponent<Camera>();
        infoDisplay = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InfoDisplay>();
        items = GetComponent<Items>();
        Owl = GameObject.FindGameObjectWithTag("Owl").GetComponent<Owl>();
        Bear = GameObject.FindGameObjectWithTag("Bear").GetComponent<TeddyBear>();
        //SprayBottleActive = false;
        //WalkyTalkyActive = false;

        OriginalCameraPos = Cam.transform.localPosition;

        SphereRadius = 0.10f;
	}

    // Update is called once per frame
    void Update()
    {
        //TorchLine();
        //Changes the controlls if you're hiding
        //if (isHiding)
        //{
        //    HiddenMove();
        //}

        //Interactions
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
            AnimalInteract();
        }

        LookAt();
	}

    void Interact()
    {
        //creats a ray of specific distance between the objects you're
        //looking at the screen
        RaycastHit hit;
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            //cheacks if you are able to pick up the object
            if (hit.collider.tag == "Pickup")
            {
                //sets whatever you hit as the current target
                //Debug.Log("Object hit");
                target = hit.collider.gameObject;
                //makes the world obejct invisable
                if (hit.collider.name == "SprayBottle")
                {
                    target.SetActive(false);
                    //adds the obejcts to your inventory
                    items.BottleAcquired = true;
                    //Debug.Log("picked up spray bottle");
                }
                if (hit.collider.name == "WalkyTalky")
                {
                    target.SetActive(false);
                    items.WalkyAcquired = true;
                    //Debug.Log("picked up Walky Talky");
                }

                if (hit.collider.name == "Teapot") {
                    target.SetActive(false);
                    items.TeapotAcquired = true;
                }
            }

            //if(hit.collider.tag == "Climbable")
            //{
            //    //Debug.Log("can climb this");
            //    target = hit.collider.gameObject;

            //    GameObject.Find("FPSController").transform.position = GameObject.Find("ClimbingPoint").transform.position;
            //}
        
            //cheacks if you are able to hide in the target and
            //only if you're not alrady hiding
            //if (hit.collider.tag == "Hideable")
            //{
            //    //sets the hideable object to the current target
            //    //Debug.Log("Can Hide Here");
            //    target = hit.collider.gameObject;

            //    //finds scripts and colliders attached to the player and turns them off
            //    GameObject.Find("FPSController").GetComponent<FlashlightLean>().enabled = false;
            //    GameObject.Find("Character").GetComponent<CapsuleCollider>().enabled = false;
            //    GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;

            //    //switches the your main camera from the plays to the hidden one
            //    mainCamera.enabled = false;
            //    //camera.transform.position = target.transform.position;
            //    //this.camera.transform.position = target.transform.position;
            //    //this.GetComponent<Camera>()

            //}
            ////if you are already hidding lets you leave the hiding place
            //else if (isHiding == true)
            //{
            //    //turns the scripts attached to the play back on
            //    GameObject.Find("FPSController").GetComponent<FlashlightLean>().enabled = true;
            //    GameObject.Find("Character").GetComponent<CapsuleCollider>().enabled = false;
            //    GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = true;

            //    //changes the main camera back to players from the hidden one
            //    mainCamera.enabled = true;

            //    //you are no longer hidding
            //    isHiding = false;
            //}
        }
    }

    void AnimalInteract()
    {
        RaycastHit hit;
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, rayDistance))
        {
            // TURN OFF LATER
            //if(hit.collider.tag == "PlushieBear")
            //{
            //    if(Bear.timeToTransform < Bear.timeToTransformMax)
            //    {
            //        Bear.timeToTransform = Bear.timeToTransformMax;
            //    }
            //    //Play Giving Bear tea animation
            //}

            if(hit.collider.tag == "PlushieOwl")
            {
                if(Owl.timeToTransform < Owl.timeToTransformMax)
                {
                    Owl.timeToTransform = Owl.timeToTransformMax;
                }
                //Play Winding Owl animation
            }
        }
    }

    //void HiddenMove()
    //{
    //    Vector3 Angles = HideCamera.transform.localEulerAngles;
    //    //gets the mouse input to rotate the camera
    //    RotateY = lookSpeed * Input.GetAxis("Mouse X") /* Time.deltaTime*/;
    //    //RotatX = lookSpeed * Input.GetAxis("Mouse X") /* Time.deltaTime*/;

    //    //clamps the camera to a certain preset rainge
    //    RotateY = Mathf.Clamp(Angles.y + RotateY, MinRotation, MaxRotation);
    //    //RotatX = Mathf.Clamp(Angles.x + RotatX, MinRotation, MaxRotation);

    //    //changes the cameras rotation using the
    //    //previusly set horizontal and vertical axis
    //    HideCamera.transform.localEulerAngles = new Vector3(Angles.x, RotateY, Angles.z);
    //    //HideCamera.transform.Rotate(RotatY, RotatX, 0.0f);

    //    //Debug.Log("Horizontal");
    //    //Debug.Log("Vertical");
    //}

    void LookAt()
    {
        RaycastHit hit;
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, rayDistance)) {
            if (hit.collider.tag == "Pickup") {
                //infoDisplay.DisplayTooltip("[E] Take " + hit.collider.name);
                infoDisplay.ChangeTooltip(1);
            }

            //if (hit.collider.tag == "Reciever") {
            //    GameObject temp = hit.collider.gameObject;
            //    infoDisplay.DisplayTooltip("reciever " + temp.GetComponent<WalkieReciever>().walkyNumber);
            //}

            //if (hit.collider.tag == "Hideable") {
            //    infoDisplay.DisplayTooltip("Hide in " + hit.collider.name);
            //}

            if (hit.collider.tag == "Plushie") {
                GameObject temp = hit.collider.transform.parent.gameObject;

                if (temp.tag == "PlushieOwl") {
                    //infoDisplay.DisplayTooltip("[E] Wind up Owl" /*+ temp.tag.ToString()*/);
                    infoDisplay.ChangeTooltip(3);
                }

                else {
                    //infoDisplay.DisplayTooltip(temp.tag.ToString());
                }
            }

        }

        // CHECKS IF NOT LOOKING AT OBJECT, CLEARS TOOLTIP
        // ADD ITEMS THAT CAN BE DISPLAYED ON TOOLTIP HERE
       if (Physics.Raycast(ray, out hit, 50)) {
           if (hit.collider.tag != "Pickup" && hit.collider.tag != "PlushieCroc" && hit.collider.tag != "PlushieBear" && 
               hit.collider.tag != "PlushieOwl" && hit.collider.tag != "Reciever" &&
              (hit.collider.tag != "Crocodile" || hit.collider.tag != "Owl" || hit.collider.tag != "Bear" ))  {
                //infoDisplay.ClearTooltip();
                infoDisplay.ClearTooltipImage();
           }
       }
    }

    public void TorchLine()
    {
        RaycastHit point;
        Ray torchline = Cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(torchline, out point, RayLine))
        {
            if (point.collider.tag == "Owl")
            {
                target = point.collider.gameObject;
                Owl.OwlTime = 10;
                //Owl.StopSearching();
                Debug.Log(target);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        origin = transform.position;
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + transform.forward * RayLine);
    }
}