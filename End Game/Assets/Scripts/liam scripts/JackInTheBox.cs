using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackInTheBox : MonoBehaviour
{
    private bool inArea;
    private float Timer;
    private float countDownTimer;
    private GameManagerScript game;

    private void Start()
    {
        Timer = 60;
        countDownTimer = Timer;
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    private void Update()
    {
        if(inArea == true)
        {
            if (countDownTimer <= 0)
            {
                KillPlayer();
            }
            else if (countDownTimer > 0)
            {
                countDownTimer -= Time.deltaTime;
            }
        }
        Debug.Log(countDownTimer);
    }

    void KillPlayer()
    {
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
}
