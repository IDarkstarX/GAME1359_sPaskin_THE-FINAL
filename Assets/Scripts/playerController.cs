using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

    [SerializeField]
    public Text choiceText;

    [SerializeField]
    Image timerImage;

    public int playerChoice;

    public int staminaCost;

    public float targetTime = 10.0f;

    void Start()
    {
        
    }

    /*
     * Dodge = 0 - 15
     * GB = 1 - 0
     * Left = 2 - 10
     * Right = 3 - 10
     * Heavy = 4 - 50
     */

    void Update()
    {
        

        if (gameManager.instance.playerTurn && !gameManager.instance.playerGBed)
        {
            //Debug.Log("Time left for player: " + targetTime);

            targetTime -= Time.deltaTime;
            timerImage.fillAmount = targetTime/ 10.0f;

            if ((Input.GetButton("Fire1") && Input.GetButtonDown("Fire2"))|| (Input.GetButtonDown("Fire1") && Input.GetButton("Fire2"))) //Heavy
            {
                playerChoice = 4;
                choiceText.text = "Heavy Attack";
                staminaCost = 60;
            }
            else if (Input.GetButtonDown("Jump") && gameManager.instance.playerStamina >= 15) //Dodge
            {
                playerChoice = 0;
                choiceText.text = "Dodge";
                staminaCost = 15;
            }
            else if (Input.GetButtonDown("Fire3")) //GB
            {
                playerChoice = 1;
                choiceText.text = "Guard Break";
                staminaCost = 0;
            }
            else if (Input.GetButtonDown("Fire1")) //Left
            {
                playerChoice = 2;
                choiceText.text = "Left Attack";
                staminaCost = 20;
            }
            else if (Input.GetButtonDown("Fire2")) //Right
            {
                playerChoice = 3;
                choiceText.text = "Right Attack";
                staminaCost = 20;
            }

            if(gameManager.instance.playerStamina < staminaCost)
            {
                playerChoice = 1;
                choiceText.text = "Not Enough Stamina!";
                staminaCost = 0;
            }

            if (targetTime <= 0.0f)
            {
                gameManager.instance.playerTurn = false;
                if (!gameManager.instance.enemyGBed)
                {
                    gameManager.instance.enemyTurn = true;
                }
                else
                {
                    gameManager.instance.enemyGBed = false;
                    //gameManager.instance.enemyDone = true;
                }
                gameManager.instance.playerDone = false;
                

                gameManager.instance.playerStamina -= staminaCost;

                //targetTime = 10.0f;
                choiceText.text = " ";
            }
        }
    }
}
