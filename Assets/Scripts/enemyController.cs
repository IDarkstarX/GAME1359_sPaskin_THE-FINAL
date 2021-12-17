using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyController : MonoBehaviour
{

    public int enemyChoice;

    int rInt;

    public int staminaCost;

    public int choiceMod = 0;

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

    // Update is called once per frame
    void Update()
    {
        if (gameManager.instance.enemyTurn && !gameManager.instance.enemyGBed)
        {
            rInt = Random.Range(0, 5) + choiceMod;

            if (rInt <= 0) // dodge
            {
                enemyChoice = 0;
                staminaCost = 15;
            }
            else if (rInt == 1) // GB
            {
                enemyChoice = 1;
                staminaCost = 0;
            }
            else if (rInt == 2) // left
            {
                enemyChoice = 2;
                staminaCost = 20;
            }
            else if (rInt == 3) // right
            {
                enemyChoice = 3;
                staminaCost = 20;
            }
            else if (rInt >= 4) // heavy
            {
                enemyChoice = 4;
                staminaCost = 60;
            }

            if (gameManager.instance.enemyStamina >= 50 && gameManager.instance.playerGBed)
            {
                enemyChoice = 4;
                staminaCost = 60;
            }

            if (gameManager.instance.enemyStamina >= staminaCost)
            {
                choiceMod = 0;

                Debug.Log("Enemy chooses: " + enemyChoice);
                gameManager.instance.enemyGBed = false;
                gameManager.instance.playerGBed = false;
                gameManager.instance.enemyTurn = false;
                gameManager.instance.enemyDone = false;
                gameManager.instance.enemyStamina -= staminaCost;
            }
        }

        ////
        else if (gameManager.instance.enemyTurn && gameManager.instance.enemyGBed)
        {
            Debug.Log("THE AI IS GUARDBROKEN (Enemy script)!!!! " + gameManager.instance.enemyGBed);
            gameManager.instance.enemyGBed = false;
            gameManager.instance.enemyIsGBed.text = " ";
            enemyChoice = -1;
            gameManager.instance.enemyTurn = false;
            gameManager.instance.playerTurn = true;
        }
    }
}
