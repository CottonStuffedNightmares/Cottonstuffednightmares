using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMobManager : MonoBehaviour {

    public GameObject Bear;
    public GameObject Owl;
    public GameObject Croc;
    [SerializeField] private GameObject currentActive;

    public GameObject StartCheckpoint;
    public GameObject patrolCheckpoint1;
    public GameObject patrolCheckpoint2;
    public GameObject patrolCheckpoint3;

    public float startDelayTimer;
    public bool countingDown;

    // Use this for initialization
    void Start () {

        currentActive = null;

        Bear.SetActive(false);
        Owl.SetActive(false);
        Croc.SetActive(false);

        countingDown = true;
               
        startDelayTimer = 3;
	}
	
	// Update is called once per frame
	void Update () {
        if (startDelayTimer > 0 && CountingDown()) {
            startDelayTimer -= Time.deltaTime;
        }

        if (startDelayTimer <= 0 && CountingDown()) {

            if (currentActive == null) {
                ToggleActiveMob();
            }

            else if (currentActive != null) {
                ToggleActiveMob();
            }

            countingDown = false;
    
        }
	}

    private void ToggleActiveMob() {
        if (currentActive == null) {
            Bear.SetActive(true);
            currentActive = Bear;
            Bear.transform.position = StartCheckpoint.transform.position;
        }

        else if (currentActive == Bear) {
            Owl.SetActive(true);
            currentActive = Owl;
            Owl.transform.position = StartCheckpoint.transform.position;
        }

        else if (currentActive == Owl) {
            Croc.SetActive(true);
            currentActive = Croc;
            Croc.transform.position = StartCheckpoint.transform.position;
        }

        else if (currentActive == Croc) {
            Bear.SetActive(true);
            currentActive = Bear;
            Bear.transform.position = StartCheckpoint.transform.position;
        }
    }
    
    public void SetPatrolDelayTimer(float timer) {
        startDelayTimer = timer;
        countingDown = true;

    }

    public bool CountingDown() {
        if (countingDown) {
            return true;
        }

        return false;
    }

    

}
