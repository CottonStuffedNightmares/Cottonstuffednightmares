using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaSense : MonoBehaviour
{
    private GameObject Player;
    public GameObject[] hidding; 
    private RedPanda panda;
    private float timeCheck;
    public float counter;

    private float rayDistance;
    private float SphereRadius;
    private Vector3 origen;

    [HideInInspector] public bool isRunning;

	// Use this for initialization
	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        hidding = GameObject.FindGameObjectsWithTag("Hideable");
        panda = GameObject.FindGameObjectWithTag("RedPanda").GetComponent<RedPanda>();
        isRunning = false;
        timeCheck = 1000.0f;
        counter = timeCheck;

        rayDistance = 15;
        SphereRadius = 2.0f;
	}

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(counter);
        this.gameObject.transform.position = panda.gameObject.transform.position;
        if (counter <= 0)
        {
            counter = timeCheck;
            panda.Sense();
        }
        counter--;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player && isRunning)
        {
           panda.DemonForm();
           panda.FollowPlayer();
           //Debug.Log("||REEEEEEEEEEEEEEEEE||");
        }
    }

    //public void Sense()
    //{
    //    RaycastHit hit;
    //    origen = panda.gameObject.transform.position;

    //    if (Physics.SphereCast(origen, SphereRadius, transform.forward, out hit))
    //    {
    //        for (int i = 0; i < hidding.Length; i++)
    //        {
    //            if (hidding[i] == gameObject)
    //            {
    //                panda.Search();
    //            }
    //        }
    //    }
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origen, origen + transform.forward * rayDistance);
    }

    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject == Player && counter <= 0)
    //    {
    //        panda.MoveToOrigen();
    //    }
    //}
}