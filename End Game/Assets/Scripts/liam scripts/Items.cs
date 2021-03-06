﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=======================================================
//  Created By Liam Gates
//  Updates: Edward ngo
//  - Callum Stirrup-Prazak
//=======================================================

public class Items : MonoBehaviour
{
    // Script references
    private Interactions interactions;
    private Camera cam;
    private TeddyBear tBear;
    private Crocodile Croc;
    private WalkieTalkie wt;
    private InfoDisplay infoDisplay;

    // Gameobject references
    private GameObject m_SprayBottle;
    private GameObject m_WalkyTalky;
    private GameObject m_Teapot;

    //if cheack Bools 
    [HideInInspector] public bool BottleAcquired;
    [HideInInspector] public bool WalkyAcquired;
    [HideInInspector] public bool TeapotAcquired;

    public ParticleSystem spray1;
    public ParticleSystem spray2;
    public ParticleSystem Pouring;

    private float rayDistance = 7;
    private float SphereRadius;
    private int replenishTimer;

    public enum ITEMTYPE {
        NONE,
        SPRAYBOTTLE,
        WALKYTALKY,
        TEAPOT
    }

    public ITEMTYPE currentItem;

	void Start ()
    {
        // Component References
        cam = GetComponent<Camera>();
        interactions = GetComponent<Interactions>();
        wt = GetComponent<WalkieTalkie>();
        infoDisplay = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InfoDisplay>();

        // Toy/Demon references
        Croc = GameObject.FindGameObjectWithTag("PlushieCroc").GetComponentInParent<Crocodile>();
        tBear = GameObject.FindGameObjectWithTag("PlushieBear").GetComponentInParent<TeddyBear>();

        // Items attached to player
        m_SprayBottle = GameObject.Find("PlayerBottle");
        m_WalkyTalky = GameObject.Find("PlayerWalky");
        m_Teapot = GameObject.Find("PlayerTeapot");

        // Check if player has an item
        BottleAcquired = false;
        WalkyAcquired = false;
        TeapotAcquired = false;

        // Set items to inactive (default)
        m_SprayBottle.SetActive(false);
        //m_WalkyTalky.SetActive(false);
        m_Teapot.SetActive(false);

        // Other variables
        currentItem = ITEMTYPE.NONE;
        replenishTimer = 1;
        SphereRadius = 0.10f;

    }

	void Update ()
    {
        // SWITCH ITEM
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchItem(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (BottleAcquired && currentItem != ITEMTYPE.SPRAYBOTTLE) {
                SwitchItem(2);
            }
        }

        //if (Input.GetKeyDown(KeyCode.Alpha3)) {
        //    if (WalkyAcquired && currentItem != ITEMTYPE.WALKYTALKY) {
        //        SwitchItem(3);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (TeapotAcquired && currentItem != ITEMTYPE.TEAPOT) {
                SwitchItem(4);
            }
        }

        // if teapot currently equipped
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            if (currentItem == ITEMTYPE.TEAPOT) {
                if (BottleAcquired) {
                    SwitchItem(2);  
                }
            }

            else if (currentItem == ITEMTYPE.SPRAYBOTTLE) {
                SwitchItem(0);
            }
        }

        // if bottle currently equipped
        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            if (currentItem == ITEMTYPE.SPRAYBOTTLE) {
                if (TeapotAcquired) {
                    SwitchItem(4);
                }
            }

            else if (currentItem == ITEMTYPE.NONE) {
                if (BottleAcquired) {
                    SwitchItem(2);
                }
            }
        }

       // // USE ITEM ON LEFT CLICK
       // if (currentItem == ITEMTYPE.NONE) {
       //     return;
       // }

        if (currentItem == ITEMTYPE.SPRAYBOTTLE && Input.GetMouseButton(0)) {
            UseSprayBottle();
        }

        if (currentItem == ITEMTYPE.WALKYTALKY && Input.GetMouseButtonDown(0)) {
            UseWalky();
        }

        if (currentItem == ITEMTYPE.TEAPOT && Input.GetMouseButtonDown(0)) {
            UseTeapot();
        }
    }

    void SwitchItem(int val) {

        if (val == 0) {
            currentItem = ITEMTYPE.NONE;

            if (m_SprayBottle != null || m_WalkyTalky != null) {
                m_SprayBottle.SetActive(false);
                m_WalkyTalky.SetActive(false);
                m_Teapot.SetActive(false);
            }
        }

        if (val == 2) {
            currentItem = ITEMTYPE.SPRAYBOTTLE;
            m_WalkyTalky.SetActive(false);
            m_Teapot.SetActive(false);
            if (m_SprayBottle != null) {
                m_SprayBottle.SetActive(true);
                infoDisplay.TemporaryTooltipDisplay(1.5f);
            }
        }

        if (val == 3) {
            currentItem = ITEMTYPE.WALKYTALKY;
            m_SprayBottle.SetActive(false);
            m_Teapot.SetActive(false);
            if (m_WalkyTalky != null) {
                m_WalkyTalky.SetActive(true);
            }
        }

        if (val == 4) {
            currentItem = ITEMTYPE.TEAPOT;
            m_SprayBottle.SetActive(false);
            m_WalkyTalky.SetActive(false);
            if (m_Teapot != null) {
                m_Teapot.SetActive(true);
                infoDisplay.TemporaryTooltipDisplay(1.5f);
            }
        }
    }

    void UseSprayBottle()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (!spray1.isPlaying) {
            spray1.Play();
        }

        if (!spray2.isPlaying){
            spray2.Play();
        }

        if(BottleAcquired == true) {
            if (Physics.SphereCast(ray, SphereRadius, out hit, rayDistance)) {                
                if ((hit.collider.tag == "PlushieCroc" && spray1.isPlaying) || (hit.collider.tag == "PlushieCroc" && spray2.isPlaying)) {
                    if (Croc.timeToTransform < Croc.timeToTransformMax) {
                        Croc.timeToTransform = Croc.timeToTransformMax;

                        Croc.PlayFeedback();
                    }
                }
                
            }
        }
    }

    void UseTeapot() {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (!Pouring.isPlaying) {
            Pouring.Play();
        }

        if (TeapotAcquired == true)
        {
            if (Physics.SphereCast(ray, SphereRadius, out hit, rayDistance))
            {
                if (hit.collider.tag == "PlushieBear" && Pouring.isPlaying)
                {
                    if (tBear.timeToTransform < tBear.timeToTransformMax)
                    {
                        tBear.timeToTransform = tBear.timeToTransformMax;

                        tBear.PlayFeedback();
                    }
                }
            }
        }
    }
    void UseWalky() {
        wt.MakeNoise();
        
    }

}