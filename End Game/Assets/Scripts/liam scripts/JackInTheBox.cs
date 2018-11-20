using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackInTheBox : MonoBehaviour
{
    private bool inArea;
    private float Timer;
    private float countDownTimer;
    private float timesEntered;
    private GameManagerScript game;

    Animator animat;

    public AudioSource windup;
    public AudioSource death;

    private void Start()
    {
        timesEntered = 0;
        Timer = 5;
        countDownTimer = Timer;
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        animat = GetComponent<Animator>();
    }

    private void Update()
    {
        if(inArea == true)
        {
            if (countDownTimer <= 0)
            {
                windup.Stop();
                animat.SetBool("isCranking", false);
                animat.SetBool("isAttacking", true);
                KillPlayer();
            }
            else if (countDownTimer > 0)
            {
                animat.SetBool("isAttacking", false);
                animat.SetBool("isCranking", true);
                countDownTimer -= Time.deltaTime;
            }
        }
        Debug.Log(countDownTimer);
    }

    void KillPlayer()
    {
        death.Play();
        game.isGameOver = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inArea = true;
        }
        else if(other.gameObject.tag != "Player")
        {
            inArea = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (timesEntered <= 0)
            {
                windup.Play();
                timesEntered = timesEntered + 1;
            }
            else if (timesEntered > 0)
            {
                windup.UnPause();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            windup.Pause();
        }
    }
}