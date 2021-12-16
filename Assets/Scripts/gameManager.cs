using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [SerializeField]
    public bool playerTurn = true;
    [SerializeField]
    public bool playerGBed = false;

    [SerializeField]
    public bool enemyTurn = false;
    [SerializeField]
    public bool enemyGBed = false;

    [SerializeField]
    playerController Player;

    [SerializeField]
    enemyController Enemy;

    [SerializeField]
    public int playerHealth = 100;
    [SerializeField]
    public int playerStamina = 100;
    [SerializeField]
    Image playerHealthImage;
    [SerializeField]
    Image playerStaminaImage;

    [SerializeField]
    GameObject playerLeftAttackMover;
    [SerializeField]
    GameObject playerRightAttackMover;
    [SerializeField]
    GameObject playerGBAndDodgeMover;



    [SerializeField]
    public int enemyHealth = 100;
    [SerializeField]
    public int enemyStamina = 100;
    [SerializeField]
    Image enemyHealthImage;
    [SerializeField]
    Image enemyStaminaImage;

    [SerializeField]
    GameObject enemyLeftAttackMover;
    [SerializeField]
    GameObject enemyRightAttackMover;
    [SerializeField]
    GameObject enemyGBAndDodgeMover;

    [SerializeField]
    public Text enemyIsGBed;



    public bool playerDone = false;
    public bool enemyDone = false;

    void Start()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }

    /*
     * Dodge = 0 - 0
     * GB = 1 - 0
     * Left = 2 - 10
     * Right = 3 - 10
     * Heavy = 4 - 30
     */

    void Update()
    {
        if (playerHealth <= 0 || enemyHealth <= 0)
        {
            return;
        }

        playerHealthImage.fillAmount = playerHealth / 100f;
        playerStaminaImage.fillAmount = playerStamina / 100f;

        enemyHealthImage.fillAmount = enemyHealth / 100f;
        enemyStaminaImage.fillAmount = enemyStamina / 100f;

        if (!playerTurn && !enemyTurn)
        {
            if(playerGBed && playerDone && enemyDone || enemyGBed && playerDone && enemyDone)
            {
                enemyGBed = false;
                playerGBed = false;
                enemyIsGBed.text = " ";
                Player.choiceText.text = " ";
            }

            if(Enemy.enemyChoice == -1)
            {
                enemyDone = true;
            }

            Debug.Log("Animation phase begun!");
            enemyIsGBed.text = "";
            
            //////PLAYER DODGES////////////*

            if (Player.playerChoice == 0 && !playerDone)
            {
                Debug.Log("Player is dodging...");
                
                playerDone = true;

                switch(Enemy.enemyChoice)
                {
                    case 0:
                        Debug.Log("...And so does the enemy!");
                        StartCoroutine(DodgeGBNormal(new Vector3(0, -4.5f, 0), playerGBAndDodgeMover, 1f));
                        StartCoroutine(DodgeGBNormal(new Vector3(0, 4.5f, 0), enemyGBAndDodgeMover, 1f));
                        break;
                    case 1:
                        Debug.Log("...And gets grabbed!");
                        StartCoroutine(DodgeGBNormal(new Vector3(0, -2.5f, 0), playerGBAndDodgeMover, 0.5f));
                        StartCoroutine(DodgeGBNormal(new Vector3(0, -2f, 0), enemyGBAndDodgeMover, 0.5f));
                        playerGBed = true;
                    
                        break;
                    case 2:
                        Debug.Log("...And evades the enemy's left attack!");
                        StartCoroutine(DodgeGBNormal(new Vector3(0, -4.5f, 0), playerGBAndDodgeMover, 1f));
                        StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, 90.0f), enemyLeftAttackMover, 1f));
                        break;
                    case 3:
                        Debug.Log("...And evades the enemy's right attack!");
                        StartCoroutine(DodgeGBNormal(new Vector3(0, -4.5f, 0), playerGBAndDodgeMover, 1f));
                        StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, -90.0f), enemyRightAttackMover, 1f));
                        break;
                    case 4:
                        Debug.Log("...And evades the enemy's heavy attack!");
                        StartCoroutine(DodgeGBNormal(new Vector3(0, -4.5f, 0), playerGBAndDodgeMover, 1f));
                        StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, 90.0f), enemyLeftAttackMover, 1f));
                        StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, -90.0f), enemyRightAttackMover, 1f));
                        break;
                }
                enemyDone = true;
            }

            ///////////////////////////////
            
            //////PLAYER GUARD BREAKS//////

            if(Player.playerChoice == 1 && !playerDone)
            {
                Debug.Log("Player guard breaks...");
                playerDone = true;
                switch (Enemy.enemyChoice)
                {
                    case 0:
                        Debug.Log("...And grabs the enemy!");
                        StartCoroutine(DodgeGBNormal(new Vector3(0, 2f, 0), playerGBAndDodgeMover, 0.5f));
                        StartCoroutine(DodgeGBNormal(new Vector3(0, 2.5f, 0), enemyGBAndDodgeMover, 0.5f));
                        enemyGBed = true;
                        break;
                    case 1:
                        Debug.Log("...And bounces off the enemy!");
                        StartCoroutine(DodgeGBNoWait(new Vector3(0, -0.25f, 0), playerGBAndDodgeMover, 0.5f));
                        StartCoroutine(DodgeGBNoWait(new Vector3(0, 0.25f, 0), enemyGBAndDodgeMover, 0.5f));
                        break;
                    case 2:
                        Debug.Log("...And loses to the enemy's left!");
                        StartCoroutine(DodgeGBNoWait(new Vector3(0, 0.5f, 0), playerGBAndDodgeMover, 0.5f));
                        StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, 90.0f), enemyLeftAttackMover, 1f));

                        playerHealth -= 10;

                        break;
                    case 3:
                        Debug.Log("...And loses to the enemy's right!");
                        StartCoroutine(DodgeGBNoWait(new Vector3(0, 0.5f, 0), playerGBAndDodgeMover, 0.5f));
                        StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, -90.0f), enemyRightAttackMover, 1f));

                        playerHealth -= 10;

                        break;
                    case 4:
                        Debug.Log("...And grabs the enemy!");
                        StartCoroutine(DodgeGBNormal(new Vector3(0, 1f, 0), playerGBAndDodgeMover, 1f));
                        StartCoroutine(AttackNoWait(Quaternion.Euler(0, 0, 45.0f), enemyLeftAttackMover, 0.5f));
                        StartCoroutine(AttackNoWait(Quaternion.Euler(0, 0, -45.0f), enemyRightAttackMover, 0.5f));
                        enemyGBed = true;
                        break;
                }
                enemyDone = true;
            }
            
            ///////////////////////////////

            //////PLAYER ATTACKS LEFT//////

            if (Player.playerChoice == 2 && Enemy.enemyChoice != 2 && Enemy.enemyChoice != 4 && !playerDone) //Player left flies
            {
                Debug.Log("Player left flies...");
                StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, -90.0f), playerLeftAttackMover, 1));
                playerDone = true;
                if(Enemy.enemyChoice == 0)
                {
                    Debug.Log("...and misses!");
                    StartCoroutine(DodgeGBNormal(new Vector3(0, 4.5f, 0), enemyGBAndDodgeMover, 1f));
                    enemyDone = true;
                } else if(Enemy.enemyChoice == 1)
                {
                    Debug.Log("...and beats a guard break!");
                    StartCoroutine(DodgeGBNoWait(new Vector3(0, -0.5f, 0), enemyGBAndDodgeMover, 0.5f));
                    enemyDone = true;
                    enemyHealth -= 10;
                } else
                {
                    Debug.Log("...and lands!");
                    enemyHealth -= 10;
                }
            }
            else if (Player.playerChoice == 2 && Enemy.enemyChoice == 2 && !playerDone) //Player left is blocked by enemy left
            {
                Debug.Log("Player left is blocked by enemy left!");
                StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, -45.0f), playerLeftAttackMover, 0.5f));
                StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, 45.0f), enemyLeftAttackMover, 0.5f));
                playerDone = true;
                enemyDone = true;
            }
            else if (Player.playerChoice == 2 && Enemy.enemyChoice == 4 && !playerDone) //player left loses to enemy heavy
            {
                Debug.Log("Player left loses to enemy heavy!");
                StartCoroutine(AttackNoWait(Quaternion.Euler(0, 0, -40.0f), playerLeftAttackMover, 0.5f));

                StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, 90.0f), enemyLeftAttackMover, 1f));
                StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, -90.0f), enemyRightAttackMover, 1f));

                playerHealth -= 30;

                playerDone = true;
                enemyDone = true;
            }
            ///////////////////////////////
            
            //////PLAYER ATTACKS RIGHT/////

            if (Player.playerChoice == 3 && Enemy.enemyChoice != 3 && Enemy.enemyChoice != 4 && !playerDone) //Player right flies
            {
                Debug.Log("Player right flies...");
                StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, 90.0f), playerRightAttackMover, 1));
                playerDone = true;
                if (Enemy.enemyChoice == 0)
                {
                    Debug.Log("...and misses!");
                    StartCoroutine(DodgeGBNormal(new Vector3(0, 4.5f, 0), enemyGBAndDodgeMover, 1f));
                    enemyDone = true;
                }
                else if (Enemy.enemyChoice == 1)
                {
                    Debug.Log("...and beats a guard break!");
                    StartCoroutine(DodgeGBNoWait(new Vector3(0, -0.5f, 0), enemyGBAndDodgeMover, 0.5f));
                    enemyDone = true;
                    enemyHealth -= 10;
                }
                else
                {
                    Debug.Log("...and lands!");
                    enemyHealth -= 10;
                }
            }
            else if (Player.playerChoice == 3 && Enemy.enemyChoice == 3 && !playerDone) //Player right is blocked by enemy right
            {
                Debug.Log("Player right is blocked by enemy right!");
                StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, 45.0f), playerRightAttackMover, 0.5f));
                StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, -45.0f), enemyRightAttackMover, 0.5f));
                playerDone = true;
                enemyDone = true;
            }
            else if (Player.playerChoice == 3 && Enemy.enemyChoice == 4 && !playerDone) //player right loses to enemy heavy
            {
                Debug.Log("Player right loses to enemy heavy!");
                StartCoroutine(AttackNoWait(Quaternion.Euler(0, 0, 40.0f), playerRightAttackMover, 0.5f));

                StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, 90.0f), enemyLeftAttackMover, 1f));
                StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, -90.0f), enemyRightAttackMover, 1f));

                playerHealth -= 30;

                playerDone = true;
                enemyDone = true;
            }

            ///////////////////////////////
            
            //////PLAYER HEAVY ATTACKS/////

            if(Player.playerChoice == 4 && Enemy.enemyChoice != 1 && Enemy.enemyChoice != 4 && !playerDone)
            {
                Debug.Log("Player throws a heavy...");
                StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, -90.0f), playerLeftAttackMover, 1f));
                StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, 90.0f), playerRightAttackMover, 1f));
                playerDone = true;
                if (Enemy.enemyChoice == 0)
                {
                    Debug.Log("...and misses!");
                    StartCoroutine(DodgeGBNormal(new Vector3(0, 4.5f, 0), enemyGBAndDodgeMover, 1f));
                    enemyDone = true;
                } else if(Enemy.enemyChoice == 2)
                {
                    Debug.Log("...and beats a left attack!");
                    StartCoroutine(AttackNoWait(Quaternion.Euler(0, 0, 40.0f), enemyLeftAttackMover, 0.5f));
                    enemyHealth -= 30;
                    enemyDone = true;
                }
                else if (Enemy.enemyChoice == 3)
                {
                    Debug.Log("...and beats a right attack!");
                    StartCoroutine(AttackNoWait(Quaternion.Euler(0, 0, -40.0f), enemyRightAttackMover, 0.5f));
                    enemyHealth -= 30;
                    enemyDone = true;
                }
                else
                {
                    Debug.Log("...and lands!");
                    enemyHealth -= 30;
                }
            } else if(Player.playerChoice == 4 && Enemy.enemyChoice == 4 && !playerDone)
            {
                Debug.Log("...And both heavies bounce off!");
                StartCoroutine(AttackNoWait(Quaternion.Euler(0, 0, -45.0f), playerLeftAttackMover, 0.5f));
                StartCoroutine(AttackNoWait(Quaternion.Euler(0, 0, 45.0f), playerRightAttackMover, 0.5f));
                StartCoroutine(AttackNoWait(Quaternion.Euler(0, 0, 45.0f), enemyLeftAttackMover, 0.5f));
                StartCoroutine(AttackNoWait(Quaternion.Euler(0, 0, -45.0f), enemyRightAttackMover, 0.5f));
                playerDone = true;
                enemyDone = true;
            } else if(Player.playerChoice == 4 && Enemy.enemyChoice == 1 && !playerDone)
            {
                Debug.Log("...And gets guard broken!");
                StartCoroutine(DodgeGBNormal(new Vector3(0, -1f, 0), enemyGBAndDodgeMover, 1f));
                StartCoroutine(AttackNoWait(Quaternion.Euler(0, 0, -45.0f), playerLeftAttackMover, 0.5f));
                StartCoroutine(AttackNoWait(Quaternion.Euler(0, 0, 45.0f), playerRightAttackMover, 0.5f));
                playerGBed = true;
                playerDone = true;
                enemyDone = true;
            }

            ///////////////////////////////

            //////LEFTOVER ACTIONS ////////

            if (!enemyDone)
            {
                switch (Enemy.enemyChoice)
                {
                    case 0:
                        StartCoroutine(DodgeGBNormal(new Vector3(0, 4.5f, 0), enemyGBAndDodgeMover, 1f));
                        enemyDone = true;
                        break;
                    case 1:
                        StartCoroutine(DodgeGBNormal(new Vector3(0, -1f, 0), enemyGBAndDodgeMover, 1f));
                        playerGBed = true;
                        enemyDone = true;
                        break;
                    case 2:
                        StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, 90.0f), enemyLeftAttackMover, 1f));
                        playerHealth -= 10;
                        enemyDone = true;
                        break;
                    case 3:
                        StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, -90.0f), enemyRightAttackMover, 1f));
                        playerHealth -= 10;
                        enemyDone = true;
                        break;
                    case 4:
                        StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, 90.0f), enemyLeftAttackMover, 1f));
                        StartCoroutine(AttackNormal(Quaternion.Euler(0, 0, -90.0f), enemyRightAttackMover, 1f));
                        playerHealth -= 30;
                        enemyDone = true;
                        break;
                }
            }

            ///////////////////////////////
            if (playerDone && enemyDone)
            {
                if (playerHealth <= 0)
                {
                    Debug.Log("GAME OVER!");
                    Player.choiceText.text = "YOU LOST!";
                    enemyIsGBed.text = "YOU LOST!";
                    StartCoroutine(GameOver(new Vector3(0, 0, 2), playerGBAndDodgeMover, 1.5f));
                    return;
                }
                else if (enemyHealth <= 0)
                {
                    Debug.Log("GAME OVER!");
                    Player.choiceText.text = "YOU WIN!";
                    enemyIsGBed.text = "YOU WIN!";
                    StartCoroutine(GameOver(new Vector3(0, 0, 2), enemyGBAndDodgeMover, 1.5f));
                    return;
                }
                //StopAllCoroutines();
                Debug.Log("Animation Phase has ended!");

                if (playerStamina < 0)
                {
                    playerStamina = 0;
                }

                if (enemyStamina < 0)
                {
                    enemyStamina = 0;
                }

                if (playerGBed)
                {
                    playerTurn = false;
                    Player.choiceText.text = "[[<>]]";
                    enemyTurn = true;
                } else if(!playerGBed)
                {
                    playerStamina += 30;
                    Player.choiceText.text = "...";
                    playerTurn = true;
                }

                if (enemyIsGBed)
                {
                    enemyIsGBed.text = "[[<>]]";
                    Enemy.enemyChoice = -1;
                }
                else
                {
                    enemyStamina += 30;
                    enemyIsGBed.text = " ";
                }

                if(playerStamina > 100)
                {
                    playerStamina = 100;
                }

                if (enemyStamina > 100)
                {
                    enemyStamina = 100;
                }

                if (enemyHealth <= 30)
                {
                    Enemy.choiceMod=-1;
                }
                if (playerHealth <= 50)
                {
                    Enemy.choiceMod+=1;
                }
                if (playerHealth <= 30)
                {
                    Enemy.choiceMod += 1;
                }

                Player.targetTime = 10.0f;
                Player.staminaCost = 0;
                Debug.Log("Is player GBed? " + playerGBed);
                Debug.Log("Is the enemy GBed? " + enemyGBed);
                Debug.Log("Is player's turn? " + playerTurn);
                Debug.Log("Is the enemy's turn? " + enemyTurn);
                return;
            }
        }
    }

    IEnumerator AttackNormal(Quaternion endValue, GameObject toRotate, float duration)
    {
        float time = 0;
        Quaternion startValue = toRotate.transform.rotation;

        while (time < duration)
        {
            toRotate.transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return 0;
        }
        toRotate.transform.rotation = endValue;

        yield return new WaitForSeconds(2f);

        time = 0;
        while (time < duration)
        {
            toRotate.transform.rotation = Quaternion.Lerp(toRotate.transform.rotation, startValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        toRotate.transform.rotation = startValue;
    }
    IEnumerator AttackNoWait(Quaternion endValue, GameObject toRotate, float duration)
    {
        float time = 0;
        Quaternion startValue = toRotate.transform.rotation;

        while (time < duration)
        {
            toRotate.transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return 0;
        }
        toRotate.transform.rotation = endValue;

        yield return new WaitForSeconds(0.01f);

        time = 0;
        while (time < duration)
        {
            toRotate.transform.rotation = Quaternion.Lerp(toRotate.transform.rotation, startValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        toRotate.transform.rotation = startValue;
    }

    IEnumerator DodgeGBNormal(Vector3 targetPosition, GameObject toMove, float duration)
    {
        //Debug.Log("Starting movement...");
        float time = 0;
        Vector3 startPosition = toMove.transform.position;

        while (time < duration)
        {
            //Debug.Log("...moving...");
            toMove.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return 0;
        }
        //Debug.Log("...finished moving!");
        toMove.transform.position = targetPosition;
        
        yield return new WaitForSeconds(2f);

        time = 0;
        while (time < duration)
        {
            toMove.transform.position = Vector3.Lerp(toMove.transform.position, startPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        toMove.transform.position = startPosition;
    }
    IEnumerator DodgeGBNoWait(Vector3 targetPosition, GameObject toMove, float duration)
    {
        //Debug.Log("Starting movement...");
        float time = 0;
        Vector3 startPosition = toMove.transform.position;

        while (time < duration)
        {
            //Debug.Log("...moving...");
            toMove.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return 0;
        }
        //Debug.Log("...finished moving!");
        toMove.transform.position = targetPosition;

        yield return new WaitForSeconds(0.01f);

        time = 0;
        while (time < duration)
        {
            toMove.transform.position = Vector3.Lerp(toMove.transform.position, startPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        toMove.transform.position = startPosition;
    }

    IEnumerator GameOver(Vector3 targetPosition, GameObject toMove, float duration)
    {
        //Debug.Log("Starting movement...");
        float time = 0;
        Vector3 startPosition = toMove.transform.position;

        while (time < duration)
        {
            //Debug.Log("...moving...");
            toMove.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return 0;
        }
        //Debug.Log("...finished moving!");
        toMove.transform.position = targetPosition;

        yield return new WaitForSeconds(4.5f);

        goToScene(0);
    }

    public void goToScene(int s)
    {
        SceneManager.LoadScene(s);
    }
}