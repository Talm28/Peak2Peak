using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public static bool isInstruction;

    public GameObject gameFunctions;
    public GameController gameController;
    public GameObject snowHearts;
    public Animator playerAnimator;
    public GameObject blurEffect;
    public ParticleSystem snowParticleSys;

    Transform playerTransform;

    float scale;
    
    float x;
    float y;

    float angel;
    float maxSpeed;
    public float speed;
    float runSpeed;
    public float radius;
    float rotation;
    int direcionFactor;

    string moveDirection = "Left";
    public static string moveState = "Start";


    //charge bar things
    public Slider chargeBar;
    float chargeTimer;

    string gameState;

	
	void Start () {
        playerTransform = GetComponent<Transform>();
        angel = Mathf.PI * 2 + 0.055f;
        rotation = 360;
        radius = 2;
        maxSpeed = 5;
        gameState = gameController.GetComponent<GameController>().gameState;
        scale = 0.75f;
        playerAnimator.SetBool("isScate", false);

	}
	 
	
	void Update () {
        gameState = gameController.GetComponent<GameController>().gameState;

        if (gameState == "Play" || gameState == "Power ups")
        {
            if (gameState == "Play")
            {
                //Touch to start movement
                if (Input.touchCount > 0 && moveState == "Start")
                {
                    if (Input.touches[0].phase == TouchPhase.Stationary || Input.touches[0].phase == TouchPhase.Moved)
                    {
                        chargeTimer += 10 * Time.deltaTime;
                        if (chargeTimer > 2)
                        {
                            chargeBar.GetComponent<chargeBarControll>().Active();
                            chargeBar.value += 4 * Time.deltaTime;
                        }


                    }
                    if (Input.touches[0].phase == TouchPhase.Ended)
                    {
                        SoundManager.PlaySound("Ski");
                        playerAnimator.SetBool("isScate", true);
                        moveState = "Middle";
                        if (chargeTimer > 1)
                            speed -= chargeBar.value;
                        chargeBar.GetComponent<chargeBarControll>().DisActive();
                        chargeTimer = 0;
                        runSpeed = speed;
                    }

                }
            }
        

            #region player movenment
            if (moveDirection == "Right")
            {
                direcionFactor = 1;
                transform.localScale = new Vector3(scale, scale, 1);
            }
                
            if (moveDirection == "Left")
            {
                direcionFactor = -1;
                transform.localScale = new Vector3(-scale, scale, 1);
            }



            if (angel < Mathf.PI - 0.055f && angel > Mathf.PI - 1)
            {
                SoundManager.isFadeOut = true;
                playerAnimator.SetBool("isScate", false);
                moveDirection = "Right";
                moveState = "Start";
                angel = Mathf.PI-0.055f;
                calculateScore();
                speed = maxSpeed;
            }

            if (angel > Mathf.PI * 2 + 0.055f * 2 && angel < Mathf.PI * 2 + 1)
            {
                SoundManager.isFadeOut = true;
                playerAnimator.SetBool("isScate", false);
                moveDirection = "Left";
                moveState = "Start";
                angel = Mathf.PI * 2 + 0.055f;
                calculateScore();
                speed = maxSpeed;
            }

            if (moveState == "Middle")
            {
                    radius = 2;
                angel += Time.deltaTime * speed * direcionFactor;
                rotation = (180 * angel) / (Mathf.PI) + 90;
                if (moveDirection == "Left")
                {
                    if (rotation - 360 > 70 && rotation - 360 < 80 && speed == runSpeed)
                        speed += 0.5f;
                    if (rotation - 360 > 40 && rotation - 360 < 50 && speed == runSpeed + 0.5f)
                        speed += 0.5f;
                    if (rotation - 360 > -50 && rotation - 360 < -40 && speed == runSpeed + 1)
                        speed -= 0.5f;
                    if (rotation - 360 > -80 && rotation - 360 < -70 && speed == runSpeed + 0.5f)
                        speed -= 0.5f;

                }
                if (moveDirection == "Right")
                {
                    if (rotation - 360 > -80 && rotation - 360 < -70 && speed == runSpeed)
                        speed += 0.5f;
                    if (rotation - 360 > -50 && rotation - 360 < -40 && speed == runSpeed + 0.5f)
                        speed += 0.5f;
                    if (rotation - 360 > 40 && rotation - 360 < 50 && speed == runSpeed + 1)
                        speed -= 0.5f;
                    if (rotation - 360 > 70 && rotation - 360 < 80 && speed == runSpeed + 0.5f)
                        speed -= 0.5f;
                }

                //Snow particle
                snowParticleSys.Emit((int)speed);
              
            }
            if (moveState == "Start")
            {
                
                if (rotation - 360 < 0)
                    rotation += 500 * Time.deltaTime;
                if (rotation - 360 > 0)
                    rotation -= 500 * Time.deltaTime;
                if (rotation - 360 < 10 && rotation - 360 > -10)
                    rotation = 360;
                if (moveDirection == "Left")
                {
                    if (radius < 2.3f)
                    {
                        radius += 2.2f * Time.deltaTime;
                    }
                }
                if (moveDirection == "Right")
                {
                    if (radius < 2.25f)
                    {
                        radius += 2.2f * Time.deltaTime;
                    }
                }

            }

            playerTransform.rotation = Quaternion.Euler(0, 0, rotation);

            x = Mathf.Cos(angel) * radius;
            y = Mathf.Sin(angel) * radius;

            playerTransform.position = new Vector3(x, y, 0);

            #endregion
        }
        
        


    }

    void calculateScore()
    {
        if (!isInstruction)
        {
            if ((gameFunctions.GetComponent<GameFunctions>().playerIndex == 19 || gameFunctions.GetComponent<GameFunctions>().playerIndex == 14) && Random.Range(0, 3) == 2)
            {
                switch ((int)speed)
                {
                    case 5:
                        gameController.GetComponent<GameController>().AddScore(2);
                        break;
                    case 4:
                        gameController.GetComponent<GameController>().AddScore(4);
                        break;
                    case 3:
                        gameController.GetComponent<GameController>().AddScore(8);
                        break;
                    case 2:
                        gameController.GetComponent<GameController>().AddScore(20);
                        break;
                    case 1:
                        gameController.GetComponent<GameController>().AddScore(30);
                        break;
                }
            }
            else
            {
                switch ((int)speed)
                {
                    case 5:
                        gameController.GetComponent<GameController>().AddScore(1);
                        break;
                    case 4:
                        gameController.GetComponent<GameController>().AddScore(2);
                        break;
                    case 3:
                        gameController.GetComponent<GameController>().AddScore(4);
                        break;
                    case 2:
                        gameController.GetComponent<GameController>().AddScore(10);
                        break;
                    case 1:
                        gameController.GetComponent<GameController>().AddScore(15);
                        break;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Astroid")
        {
            Collision();
        }
        else if (col.gameObject.tag == "Snow blust")
        {
            Collision();
        }
        else  if (col.gameObject.tag == "Snowflake")
        {
            col.gameObject.GetComponent<SnowflakeController>().isCollected = true;
        }
        else if (col.gameObject.tag == "Crate")
        {
            SoundManager.PlaySound("Box collect");
            col.gameObject.GetComponent<CrateMovement>().Collect();
            PlayerPrefs.SetInt("Crates", PlayerPrefs.GetInt("Crates")+1);
        }
        else if (col.gameObject.tag == "Purpel astroid")
        {
            StartCoroutine(BlurCoroutine());
        }

    }

    void Collision()
    {
        if (!gameController.GetComponent<GameController>().isShilded)
        {
            if (gameFunctions.GetComponent<GameFunctions>().playerIndex == 11 || gameFunctions.GetComponent<GameFunctions>().playerIndex == 14)
            {
                
                if (Random.Range(0, 3) == 0)
                    return;
                else
                    snowHearts.GetComponent<SnowHeartsController>().lifeDown();
            }
            else
                snowHearts.GetComponent<SnowHeartsController>().lifeDown();
        }
        
    }

    public void Restart()
    {
        moveDirection = "Left";
        moveState = "Start";
        angel = Mathf.PI * 2 + 0.055f;
        rotation = 360;
        radius = 2;
        speed = maxSpeed;

        playerAnimator.SetBool("isScate", false);


        playerTransform.rotation = Quaternion.Euler(0, 0, rotation);

        x = Mathf.Cos(angel) * radius;
        y = Mathf.Sin(angel) * radius;

        playerTransform.position = new Vector3(x, y, 0);
    }

    IEnumerator BlurCoroutine()
    {
        blurEffect.SetActive(true);
        yield return new WaitForSeconds(13);
        blurEffect.SetActive(false);
    }

}
