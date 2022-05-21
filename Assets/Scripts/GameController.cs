using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //0- Press the screen
    //1- Charge
    //2- avoid astroids
    //3- End
    public static int instructionStage;
    public static bool isInstruction;
    public static bool isSking;
    public Text instructionText;

    //Power ups quantity
    #region
    public Text doubleCoinsQuantity;
    public Text freezeQuantity;
    public Text extraLifeQuantity;
    public Text shieldQuantity;
    #endregion

    //Freeze effect
    #region
    public float freezeFactor;
    float freezeTimer;
    public GameObject freezetimerObject;
    public Text freezeTimerText;
    bool isFreezed;
    #endregion

    //Double coins effect
    #region
    int doubleCoinsFactor;
    bool isDoubleCoins;
    float doubleCoinsTimer;
    public GameObject doubleCoinsTimerObject;
    public Text doubleCoinsTimerText;
    #endregion

    //Shield effect
    #region
    public GameObject shield;
    public bool isShilded;
    float shieldTimer;
    public GameObject shieldTimerObject;
    public Text shieldTimerText;
    #endregion

    //Extra life effect
    #region
    bool isUsedExtraLife;
    #endregion


    int snowCoins;
    int levelScore;
    public int level;
    int maxScore;

    float snowballRotation;

    bool isPowerUpsOpen;

    public GameObject settings;
    public GameObject settingsController;
    public Text coinsText;
    public Text popUpText;
    public Text levelText;
    public Slider scoreSlider;
    public GameObject scoreSliderSnowball;
    public GameObject pausePanel;
    public GameObject pausePanelLogs;
    public GameObject game;
    public GameObject menu;
    public GameObject gameOver;
    public GameObject player;
    public GameObject snowHearts;
    public GameObject gameFunctions;
    public Text gameOverLevelText_1;
    public Text gameOverLevelText_2;
    public Text gameOverPercentageText_1;
    public Text gameOverPercentageText_2;
    public GameObject levelUp;
    public GameObject fist;
    public GameObject powerUpsPanel;
    public GameObject bonusLevel;

    public static bool isSettings;
    public string gameState;

    //Snow astroids spawner
    #region
    public GameObject snowAstroid;
    float astroidTimer;
    float timeToSpawnAstroids;
    public GameObject redAstroid;
    float redAstroidTimer;
    float timeToSpawnRedAstroids;
    public GameObject blueAstroid;
    float blueAstroidsTimer;
    public float timeToSpawnBlueAstroids;
    public GameObject purpelAstroid;
    float purpelAstroidTimer;
    float timeToSpawnPurpelAstroid;
    #endregion
    public GameObject eagle;
    float eagleTimer;
    float timeToSpanEagle;

    bool isBackToMenu;

	void Start () {

        //Instructions initialize
        instructionStage = PlayerPrefs.GetInt("Instructions");
        if (instructionStage != 3)
        {
            isInstruction = true;
            PlayerMovement.isInstruction = true;
        }
        else
        {
            isInstruction = false;
            PlayerMovement.isInstruction = false;
        }
        isSking = false;

        snowCoins = PlayerPrefs.GetInt("Snow coins");
        coinsText.text = snowCoins.ToString();
        levelScore = PlayerPrefs.GetInt("Level score");
        level = PlayerPrefs.GetInt("Level");
        levelText.text = "Level: " + level.ToString();

        maxScore = 50 + (10 * (level - 1));
        scoreSlider.maxValue = maxScore;
        scoreSlider.value = levelScore;

        snowballRotation = scoreSliderSnowball.transform.rotation.z;
        timeToSpawnAstroids = 0.5f;
        timeToSpawnRedAstroids = 0.5f;
        timeToSpawnBlueAstroids = 0.5f;
        timeToSpawnPurpelAstroid = 0.5f;
        timeToSpanEagle = 20;

        isBackToMenu = false;
        gameState = "Play";

        isPowerUpsOpen = false;

        freezeFactor = 1f;
        doubleCoinsFactor = 1;
        isUsedExtraLife = false;

        isSettings = false;
	}
	

	void Update () {

        //Update snow coins
        snowCoins = PlayerPrefs.GetInt("Snow coins");
        coinsText.text = snowCoins.ToString();

        //Set the play state as play
        if (!pausePanel.active && gameState == "Pause")
            gameState = "Play";
        if(!gameOver.active && gameState == "Game over")
            gameState = "Play";

        //Level up
		if(scoreSlider.value == scoreSlider.maxValue)
        {
            LevelUp();
        }
        //Update the slider
        if (scoreSlider.value < levelScore)
        {
            RollBall();
            scoreSlider.value += 8 * Time.deltaTime;

        }

        //Astroids spawn
        if (gameState == "Play" || gameState == "Power ups")
        {
            if (instructionStage != 0 && instructionStage != 1)
            {
                SpawnSnowAstroids();
                if (level > 9)
                    SpawnBlueAstroids();
                if (level > 19)
                    SpawnRedAstroids();
                if (level > 29)
                    SpawnPurpelAstroids();

                SpawnEagels();
            }

        }

        //Power ups effects
        if (isFreezed)
            DoFreezeEffect();
        if (isDoubleCoins)
            DoDoubleCoins();
        if (isShilded)
            DoShieldEffect();

        //Instructions updates
        if(isInstruction)
        {
            switch(instructionStage)
            {
                case 0:
                    instructionText.text = "Press the screen once to slide to the other side";
                    break;
                case 1:
                    instructionText.text = "You can gain more points by charging your player and slide slower";
                    break;
                case 2:
                    instructionText.text = "Avoid the snow astroids and gain snowflakes!";
                    break;
                case 3:
                    StartCoroutine(EndInstructions());
                    break;
            }
            if(PlayerMovement.moveState == "Middle")
            {
                isSking = true;
            }
            if(PlayerMovement.moveState == "Start" && isSking)
            {
                if (instructionStage < 3)
                {
                    PlayerPrefs.SetInt("Instructions", instructionStage + 1);
                    instructionStage++;
                    isSking = false;
                }
            }
        }

        Debug.Log(isInstruction +" " + instructionStage.ToString()+ " " +PlayerMovement.moveState);

	}
    
    IEnumerator EndInstructions()
    {
        instructionText.text = "Have fun!";
        yield return new WaitForSeconds(2f);
        isInstruction = false;
        PlayerMovement.isInstruction = false;
        instructionText.text = "";
    }

    public void AddScore(int score)
    {
        PlayerPrefs.SetInt("Level score", levelScore + score);
        levelScore += score;
        PopUpScore(score);
    }

    public void RollBall()
    {
        snowballRotation -= 120 *Time.deltaTime;
        scoreSliderSnowball.transform.rotation = Quaternion.Euler(0, 0, snowballRotation);
    }

    public void PopUpScore(int score)
    {
        popUpText.gameObject.SetActive(true);
        popUpText.text = "+" + score.ToString();
    }

    void SpawnSnowAstroids()
    {
        astroidTimer += Time.deltaTime * freezeFactor;
        if (astroidTimer > timeToSpawnAstroids)
        {
            if(Random.Range(0,2) == 1)
            {
                Instantiate(snowAstroid);
            }
            astroidTimer = 0;

            timeToSpawnAstroids = 0.8f - (0.0025f * (level - 1));
            if (timeToSpawnAstroids < 0.5f)
                timeToSpawnAstroids = 0.5f;
                

                
            
        }  
    }
    void SpawnRedAstroids()
    {
        redAstroidTimer += Time.deltaTime * freezeFactor;
        if (redAstroidTimer > timeToSpawnRedAstroids)
        {
            if (Random.Range(0, 4) == 1)
            {
                Instantiate(redAstroid);
            }
            redAstroidTimer = 0;
            if (level == 20)
                timeToSpawnRedAstroids = 0.8f;
            else
                timeToSpawnRedAstroids = Random.Range(1f, 2f);
            
        }  
    }
    void SpawnBlueAstroids()
    {
        blueAstroidsTimer += Time.deltaTime * freezeFactor;
        if(blueAstroidsTimer>timeToSpawnBlueAstroids)
        {
            if (Random.Range(0, 3) == 1)
            {
                Instantiate(blueAstroid);
            }
            blueAstroidsTimer = 0;
            if (level == 10)
                timeToSpawnBlueAstroids = 0.8f;
            else if (level == 15)
                timeToSpawnBlueAstroids = 0.5f;
            else
            {
                timeToSpawnBlueAstroids = 1 - (0.005f * (level - 10));
                if (timeToSpawnBlueAstroids < 0.6)
                    timeToSpawnBlueAstroids = 0.6f;
            }
                

        }
    }
    void SpawnPurpelAstroids()
    {
        purpelAstroidTimer += Time.deltaTime * freezeFactor;
        if(purpelAstroidTimer>timeToSpawnPurpelAstroid)
        {
            if(Random.Range(0,4)==1)
            {
                Instantiate(purpelAstroid);
            }
            purpelAstroidTimer = 0;
        }
    }

    void SpawnEagels()
    {
        eagleTimer += Time.deltaTime;
        if(eagleTimer>timeToSpanEagle)
        {
            int a = Random.Range(1, 81);
            if(a==1)
            {
                Instantiate(eagle);
            }
            eagleTimer = 0;
        }
    }

    public void PauseGame()
    {
        if (gameState == "Play")
        {
            gameState = "Pause";
            pausePanel.SetActive(true);
            SoundManager.PlaySound("Wosh in");
        }
        
    }

    public void OpenSettings()
    {
        
        isSettings = true;
        settings.SetActive(true);
        SoundManager.PlaySound("Wosh in");
        settingsController.GetComponent<SettingsController>().SetPlay();
        
        
    }


    public void ResumeGame()
    {
        if (!isSettings)
        {
            StartCoroutine(ResumeGameCourotine());
        }
    }
    IEnumerator ResumeGameCourotine()
    {
        SoundManager.PlaySound("Wosh out");
        pausePanel.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(0.5f);
        pausePanel.SetActive(false);
        gameState = "Play";
    }

    public void BackToMenu()
    {
        if (!isSettings)
        {
            StartCoroutine(BackToMenuCourotine());
        }
    }
    IEnumerator BackToMenuCourotine()
    {
        SoundManager.isPlay = false;
        ResetGame();
        pausePanel.GetComponent<Animator>().SetBool("isClose", true);
        game.GetComponent<Animator>().SetBool("isClose", true);
        SoundManager.PlaySound("Wosh out");
        yield return new WaitForSeconds(0.75f);
        SoundManager.PlaySound("Wosh in");
        pausePanel.SetActive(false);
        gameState = "Play";
        menu.SetActive(true);
        gameOver.SetActive(false);
        game.SetActive(false);
    }

    void DestroyObjects()
    {
        GameObject[] astroids = GameObject.FindGameObjectsWithTag("Astroid");
        GameObject[] purpelAstroids = GameObject.FindGameObjectsWithTag("Purpel astroid");
        GameObject[] holes = GameObject.FindGameObjectsWithTag("Hole");
        GameObject[] snowflakes = GameObject.FindGameObjectsWithTag("Snowflake");
        GameObject[] snowBlusts = GameObject.FindGameObjectsWithTag("Snow blust");
        GameObject[] crates = GameObject.FindGameObjectsWithTag("Crate");
        GameObject[] eagles = GameObject.FindGameObjectsWithTag("Eagle");

        for (int i =0;i<astroids.Length;i++)
        {
            Destroy(astroids[i].gameObject);
        }
        for (int i = 0; i < purpelAstroids.Length; i++)
        {
            Destroy(purpelAstroids[i].gameObject);
        }
        for (int i = 0; i < holes.Length; i++)
        {
            Destroy(holes[i].gameObject);
        }
        for (int i =0;i<snowflakes.Length;i++)
        {
            Destroy(snowflakes[i].gameObject);
        }
        for (int i = 0; i < snowBlusts.Length; i++)
        {
            Destroy(snowBlusts[i].gameObject);
        }
        for (int i = 0; i < crates.Length; i++)
        {
            Destroy(crates[i].gameObject);
        }
        for (int i = 0; i < eagles.Length; i++)
        {
            Destroy(eagles[i].gameObject);
        }
    }

    public void Dead()
    {
        SoundManager.StopSkiSound();
        gameState = "Game over";
        gameOverLevelText_1.text = "Level: " + PlayerPrefs.GetInt("Level").ToString();
        gameOverLevelText_2.text = gameOverLevelText_1.text;
        int percentage = (int)((scoreSlider.value * 100) / scoreSlider.maxValue);
        gameOverPercentageText_1.text = percentage.ToString() + "% " + "comleted!";
        gameOverPercentageText_2.text = gameOverPercentageText_1.text;
        gameOver.SetActive(true);

    }

    public void ResetGame()
    {
        SoundManager.StopSkiSound();
        DestroyObjects();
        player.GetComponent<PlayerMovement>().Restart();
        levelUp.SetActive(false);
        bonusLevel.SetActive(false);
    }

    public void ReloadGame()
    {
        StartCoroutine(ReloadGameCourotine());
    }
    IEnumerator ReloadGameCourotine()
    {
        gameOver.GetComponent<Animator>().SetBool("isClose", true);
        ResetGame();
        PlayerPrefs.SetInt("Level score", 0);
        levelScore = 0;
        isUsedExtraLife = false;
        scoreSlider.value = scoreSlider.minValue;
        snowHearts.GetComponent<SnowHeartsController>().Restart();
        yield return new WaitForSeconds(0.5f);
        gameOver.SetActive(false);
    }

    public void BackToMenuAndReset()
    {
        StartCoroutine(BackToMenuAndResetCourotine());
    }
    IEnumerator BackToMenuAndResetCourotine()
    {
        SoundManager.StopSkiSound();
        levelUp.SetActive(false);
        bonusLevel.SetActive(false);
        gameOver.GetComponent<Animator>().SetBool("isClose", true);
        game.GetComponent<Animator>().SetBool("isClose", true);
        ReloadGame();
        yield return new WaitForSeconds(0.75f);
        pausePanel.SetActive(false);
        menu.SetActive(true);
        game.SetActive(false);
    }

    public void PowerUpsPanelStart()
    {
        if (gameState == "Play" || gameState == "Power ups")
        {
            StartCoroutine(PowerUpsPanelStartCourotine());
        }
    }
    IEnumerator PowerUpsPanelStartCourotine()
    {
        if (!isPowerUpsOpen)
        {
            isPowerUpsOpen = true;
            gameState = "Power ups";
            powerUpsPanel.SetActive(true);
            updatePowerUps();
            yield break;
        }
        if(isPowerUpsOpen)
        {
            isPowerUpsOpen = false;
            powerUpsPanel.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            gameState = "Play";
            yield break;
        }
    }
    void updatePowerUps()
    {
        doubleCoinsQuantity.text = PlayerPrefs.GetInt("Double coins").ToString();
        freezeQuantity.text = PlayerPrefs.GetInt("Freeze").ToString();
        extraLifeQuantity.text = PlayerPrefs.GetInt("Extra life").ToString();
        shieldQuantity.text = PlayerPrefs.GetInt("Shield").ToString();
    }

    public void AddCoins(bool isLevelUp)
    {
        if(isLevelUp)
        {
            PlayerPrefs.SetInt("Snow coins", snowCoins + (1));
            snowCoins = PlayerPrefs.GetInt("Snow coins");
            coinsText.text = snowCoins.ToString();
        }
        else
        {
            //if burgler played?
            if (gameFunctions.GetComponent<GameFunctions>().playerIndex == 12 || gameFunctions.GetComponent<GameFunctions>().playerIndex == 14)
            {
                int r = Random.Range(0, 4);

                //double coins
                if(r == 1 || r == 2)
                {
                    PlayerPrefs.SetInt("Snow coins", snowCoins + (1 * doubleCoinsFactor * 2));
                    snowCoins = PlayerPrefs.GetInt("Snow coins");
                    coinsText.text = snowCoins.ToString();
                }
                //triple coins
                else if(r==3)
                {
                    PlayerPrefs.SetInt("Snow coins", snowCoins + (1 * doubleCoinsFactor * 3));
                    snowCoins = PlayerPrefs.GetInt("Snow coins");
                    coinsText.text = snowCoins.ToString();
                }
                //ragular coins
                else
                {
                    PlayerPrefs.SetInt("Snow coins", snowCoins + (1 * doubleCoinsFactor));
                    snowCoins = PlayerPrefs.GetInt("Snow coins");
                    coinsText.text = snowCoins.ToString();
                }
            }
            else
            {
                PlayerPrefs.SetInt("Snow coins", snowCoins + (1 * doubleCoinsFactor));
                snowCoins = PlayerPrefs.GetInt("Snow coins");
                coinsText.text = snowCoins.ToString();
            }
        }
    }

    void LevelUp ()
    {
        levelUp.SetActive(true);
        PlayerPrefs.SetInt("Level score", 0);
        levelScore = 0;
        scoreSlider.value = scoreSlider.minValue;

        PlayerPrefs.SetInt("Level", level + 1);
        level += 1;
        levelText.text = "Level: " + level.ToString();


        maxScore = 50 + (10 * (level - 1));
        scoreSlider.maxValue = maxScore;
        StartCoroutine(levelUp.GetComponent<LevelUpController>().LevelUp());
        
    }

    public void StartPowerUp(string name)
    {
        switch(name)
        {
            case "Freeze":
                if (!isFreezed && PlayerPrefs.GetInt("Freeze") > 0)
                {
                    freezetimerObject.SetActive(true);
                    isFreezed = true;
                    freezeFactor = 0.5f;
                    freezeTimer = 15;
                    PlayerPrefs.SetInt("Freeze", PlayerPrefs.GetInt("Freeze") - 1);
                    updatePowerUps();
                }
                break;
            case "Double coins":
                if (!isDoubleCoins && PlayerPrefs.GetInt("Double coins") > 0)
                {
                    doubleCoinsTimerObject.SetActive(true);
                    isDoubleCoins = true;
                    doubleCoinsFactor = 2;
                    doubleCoinsTimer = 30;
                    PlayerPrefs.SetInt("Double coins", PlayerPrefs.GetInt("Double coins") - 1);
                    updatePowerUps();
                }
                break;
            case "Shield":
                if (!isShilded && PlayerPrefs.GetInt("Shield") > 0)
                {
                    shield.SetActive(true);
                    shieldTimerObject.SetActive(true);
                    isShilded = true;
                    shieldTimer = 30;
                    PlayerPrefs.SetInt("Shield", PlayerPrefs.GetInt("Shield") - 1);
                    updatePowerUps();
                }
                break;
            case "Extra life":
                DoExtraLife();
                break;
        }
    }

    void DoFreezeEffect()
    {
        if (gameState == "Play" || gameState == "Power ups")
        {
            freezeTimer -= Time.deltaTime;
            freezeTimerText.text = freezeTimer.ToString("0");

            if (freezeTimer < 0)
            {
                isFreezed = false;
                freezeFactor = 1;
                freezeTimer = 15;
                freezetimerObject.SetActive(false);
            }
        }
        
    }
    void DoDoubleCoins()
    {
        if (gameState == "Play" || gameState == "Power ups")
        {
            doubleCoinsTimer -= Time.deltaTime;
            doubleCoinsTimerText.text = doubleCoinsTimer.ToString("0");

            if (doubleCoinsTimer < 0)
            {
                isDoubleCoins = false;
                doubleCoinsFactor = 1;
                doubleCoinsTimer = 30;
                doubleCoinsTimerObject.SetActive(false);
            }
        }
    }
    void DoShieldEffect()
    {
        if (gameState == "Play" || gameState == "Power ups")
        {
            shieldTimer -= Time.deltaTime;
            shieldTimerText.text = shieldTimer.ToString("0");
            if(shieldTimer<=5)
            {
                switch(shieldTimer.ToString("0.0"))
                {
                    case "5.0":
                        shield.SetActive(false);
                        break;
                        case "4.5":
                        shield.SetActive(true);
                        break;
                        case "4.0":
                        shield.SetActive(false);
                        break;
                        case "3.5":
                        shield.SetActive(true);
                        break;
                        case "3.0":
                        shield.SetActive(false);
                        break;
                        case "2.5":
                        shield.SetActive(true);
                        break;
                        case "2.0":
                        shield.SetActive(false);
                        break;
                        case "1.5":
                        shield.SetActive(true);
                        break;
                        case "1.0":
                        shield.SetActive(false);
                        break;
                        case "0.5":
                        shield.SetActive(true);
                        break;

                }
                
            }
            if (shieldTimer < 0)
            {
                shield.SetActive(false);
                isShilded = false;
                shieldTimer = 30;
                shieldTimerObject.SetActive(false);
            }
        }
    }
    void DoExtraLife()
    {
        if (!isUsedExtraLife&&PlayerPrefs.GetInt("Extra life")>0)
        {
            PlayerPrefs.SetInt("Extra life", PlayerPrefs.GetInt("Extra life") - 1);
            snowHearts.GetComponent<SnowHeartsController>().lifeUP();
            isUsedExtraLife = true;
            updatePowerUps();
        }
    
    }


}
