using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//=================================================================================
// Script written by: Edward Ngo
//=================================================================================

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public class NPC : MonoBehaviour {

    public ParticleSystem FeedbackParticle;
    public bool isSearching;
    public bool isHunting;
    public bool isDemon;

    public Transform[] patrolPoints;           // list of points npc will patrol to
    //public List<Transform> patrolPoints;
    public Transform playerKillPos;         // set player pos here on death

    public float timeToTransform;           // countdown until transform
    public float timeToTransformMax;          // Set this as max time to transform

    public float timeToRevert;              // while in demon form, countdown to revert to toy
    public float timeToRevertMax;               // Set this as max time to revert


    public float scale;
    //public bool inToyForm;                 
    
    void Start ()
    {
        isSearching = false;
        isHunting = false;
        isDemon = false;
        //inToyForm = true;
    }

    public virtual void PlayFeedback() {; }


}
