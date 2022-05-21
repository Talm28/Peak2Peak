using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxOpennerController : MonoBehaviour {

    public GameObject boxOpener;
    public GameObject menu;
    public Button boxButton;
    public Button keyButton;
    public Sprite crate;
    public Sprite crateOpen;
    public GameObject chains;
    public Sprite key;
    string boxType;
    bool isLocked;
    bool isOpened;
    bool isRotate;
    bool isActive;
    bool isFadeOut;
    public ParticleSystem particleSys;
    public ParticleSystem coinsParticleSys;
    public Text snowCoinsText;
    public Text keyQuantity;
    int keys;
    Color snowCoinColor;

    //prizes
    public GameObject moneyPrize;
    public Text moneyText;
    public GameObject powerUpPrize;
    public Sprite doubleCoins;
    public Sprite freeze;
    public Sprite extraLife;
    public Sprite shield;
    public Text powerUpText;
    public Image powerUpImage;
    public GameObject playerPrize;
    public Image playerImage;
    public Sprite player_11;
    public Sprite player_12;
    public Sprite player_14;
    public Sprite player_19;

    float angle;
    int angleFactor;
    int timesToOpen;
    int snowCoins;
    Vector3 target;

    bool isUpdate;


	void Start () {
        isUpdate = false;
        Initialize();
        target = new Vector3(0, 3.2f, 0);
	}
	
	void Update () {

        if(!isUpdate)
        {
            Initialize();
            isUpdate = true;
        }

        if (isRotate)
            RotateBox();

        if (isActive)
        {

            if (snowCoinColor.a < 1 && !isFadeOut)
            {
                snowCoinColor.a += Time.deltaTime;
                snowCoinsText.GetComponent<Text>().color = snowCoinColor;
            }


            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[coinsParticleSys.particleCount];
            coinsParticleSys.GetParticles(particles);

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].velocity = new Vector3(0, 0, 0);
                particles[i].position = Vector3.MoveTowards(particles[i].position, target, 1);
                if (particles[i].position == target)
                {
                    snowCoins+=2;
                    PlayerPrefs.SetInt("Snow coins", snowCoins);
                    snowCoinsText.text = snowCoins.ToString();

                    particles[i].position = new Vector3(1500, 1500, 150);
                }
            }


            coinsParticleSys.SetParticles(particles, particles.Length);
        }
        if (isFadeOut && snowCoinColor.a > 0)
        {
            //snowCoinColor.a -= Time.deltaTime;
            //snowCoinsText.GetComponent<Text>().color = snowCoinColor;
        }
	}

    void Initialize()
    {
        keys = PlayerPrefs.GetInt("Keys");
        keyQuantity.text = keys.ToString();
        boxButton.GetComponent<Image>().sprite = crate;
        chains.SetActive(true);
        isLocked = true;
        isOpened = false;
        angle = 0;
        angleFactor = 1;
        isActive = false;
        isFadeOut = false;
        snowCoinColor = snowCoinsText.GetComponent<Text>().color;
        snowCoinColor.a = 0;
        snowCoinsText.GetComponent<Text>().color = snowCoinColor;
        snowCoins = PlayerPrefs.GetInt("Snow coins");
        snowCoinsText.text = snowCoins.ToString();
    }

    public void CloseBoxOpener()
    {
        StartCoroutine(CloseBoxOpenerCoroutine());
    }
    IEnumerator CloseBoxOpenerCoroutine()
    {
        SoundManager.StopDrums();
        yield return new WaitForSeconds(0.5f);
        SoundManager.PlaySound("Wosh in");
        menu.SetActive(true);
        Initialize();
        isUpdate = false;
        playerPrize.SetActive(false);
        moneyPrize.gameObject.SetActive(false);
        powerUpText.gameObject.SetActive(false);
        powerUpPrize.SetActive(false);
        boxOpener.SetActive(false);
    }

    public void BoxClick()
    {
        if (!isLocked && !isOpened)
        {
            if(timesToOpen ==0)
            {
                isOpened = true;
                boxButton.GetComponent<Image>().sprite = crateOpen;
                RandomCratePrice();
                return;
            }
            else
                StartCoroutine(RotateBocxourotine());
            timesToOpen--;
            
            
        }    
    }

    public void UseKey()
    {
        if (isLocked&&keys>0)
        {
            SoundManager.PlaySound("Drums");
            keys--;
            PlayerPrefs.SetInt("Keys", keys);
            keyQuantity.text = keys.ToString();


            chains.SetActive(false);
            StartCoroutine(PopKey());
            isLocked = false;
            timesToOpen = Random.Range(0, 4);
        }
    }

    IEnumerator PopKey()
    {
        keyButton.GetComponent<Transform>().localScale = new Vector3(1.1f, 1.1f, 1);
        yield return new WaitForSeconds(0.1f);
        keyButton.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
    }
    IEnumerator RotateBocxourotine()
    {
        isRotate = true;
        yield return new WaitForSeconds(0.12f);
        isRotate = false;
        angle = 0;
        boxButton.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void RotateBox()
    {
        angle += 250*angleFactor * Time.deltaTime;
        boxButton.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (angle > 10||angle<-10)
            angleFactor = -angleFactor;
    }

    public void RandomCratePrice()
    {
        SoundManager.StopDrums();
        SoundManager.PlaySound("Ta da");
        if (PlayerPrefs.GetInt("Crates") > 0)
            PlayerPrefs.SetInt("Crates", PlayerPrefs.GetInt("Crates") - 1);
        int rnd = Random.Range(0, 150);
        switch (rnd)
        {
            //Demon win
            case 1:
                Playerwin(1);
                break;
            //Dark player win
            case 2:
            case 3:
            case 4:
                Playerwin(2);
                break;
            //Power up win
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
            case 24:
                PowerUpWin();
                break;
            //Money win
            default:
                MoneyWin();
                break;

        }
    }

    void Playerwin(int type)
    {
        if(type ==1)
        {
            if (PlayerPrefs.GetInt("Player_14") == 1)
            {
                MoneyWin();
            }
            else
            {
                particleSys.Emit(100);
                playerImage.sprite = player_14;
                playerPrize.SetActive(true);
                PlayerPrefs.SetInt("Player_14", 1);
            }
        }
        else
        {
            bool isChosen = false;

            if(PlayerPrefs.GetInt("Player_11") == 1&&PlayerPrefs.GetInt("Player_12") == 1&&PlayerPrefs.GetInt("Player_19") == 1)
            {
                MoneyWin();
                isChosen = true;
            }
            
            while(!isChosen)
            {
                switch(Random.Range(0,4))
                {
                    case 1:
                        if (PlayerPrefs.GetInt("Player_11") == 1)
                        {
                            
                        }
                        else
                        {
                            particleSys.Emit(100);
                            playerImage.sprite = player_11;
                            playerPrize.SetActive(true);
                            PlayerPrefs.SetInt("Player_11", 1);
                            isChosen = true;
                        }
                    break;
                    case 2:
                    if (PlayerPrefs.GetInt("Player_12") == 1)
                        {
                            
                        }
                        else
                        {
                            particleSys.Emit(100);
                            playerImage.sprite = player_12;
                            playerPrize.SetActive(true);
                            PlayerPrefs.SetInt("Player_12", 1);
                            isChosen = true;
                        }
                    break;
                    case 3:
                    if (PlayerPrefs.GetInt("Player_19") == 1)
                    {
                        
                    }
                    else
                    {
                        particleSys.Emit(100);
                        playerImage.sprite = player_19;
                        playerPrize.SetActive(true);
                        PlayerPrefs.SetInt("Player_19", 1);
                        isChosen = true;
                    }
                    break;

                }
            }
        }
    }
    void MoneyWin()
    {
        moneyPrize.gameObject.SetActive(true);
        switch(Random.Range(1,21))
        {
            case 1:
                moneyText.text = "500";
                StartCoroutine(PopCoinsParticles(500/2));
                break;
            case 2:case 3: case 4:
                moneyText.text = "450";
                StartCoroutine(PopCoinsParticles(450/2));
                break;
            case 5: case 6: case 7: case 8: 
                moneyText.text = "400";
                StartCoroutine(PopCoinsParticles(400/2));
                break;
            default:
                moneyText.text = "300";
                StartCoroutine(PopCoinsParticles(300/2));
                break;
        }

    }
    void PowerUpWin()
    {
        particleSys.Emit(25);
        int rnd;
        powerUpPrize.SetActive(true);
        switch(Random.Range(1,5))
        {
            case 1:
                powerUpImage.sprite = doubleCoins;
                powerUpText.gameObject.SetActive(true);
                rnd = Random.Range(1, 16);
                if(rnd==1)
                {
                    powerUpText.text = "3 X";
                }
                else if(rnd ==2 || rnd ==3 || rnd == 4 || rnd ==5|| rnd ==6)
                {
                    powerUpText.text = "2 X";
                }
                else
                {
                    powerUpText.text = "1 X";
                }
                break;
            case 2:
                powerUpImage.sprite = freeze;
                powerUpText.gameObject.SetActive(true);
                rnd = Random.Range(1, 16);
                if(rnd==1)
                {
                    powerUpText.text = "3 X";
                }
                else if(rnd ==2 || rnd ==3 || rnd == 4 || rnd ==5|| rnd ==6)
                {
                    powerUpText.text = "2 X";
                }
                else
                {
                    powerUpText.text = "1 X";
                }
                break;
            case 3:
                powerUpImage.sprite = extraLife;
                powerUpText.gameObject.SetActive(true);
                rnd = Random.Range(1, 16);
                if(rnd==1)
                {
                    powerUpText.text = "3 X";
                }
                else if(rnd ==2 || rnd ==3 || rnd == 4 || rnd ==5|| rnd ==6)
                {
                    powerUpText.text = "2 X";
                }
                else
                {
                    powerUpText.text = "1 X";
                }
                break;
            case 4:
                powerUpImage.sprite = shield;
                powerUpText.gameObject.SetActive(true);
                rnd = Random.Range(1, 16);
                if(rnd==1)
                {
                    powerUpText.text = "3 X";
                }
                else if(rnd ==2 || rnd ==3 || rnd == 4 || rnd ==5|| rnd ==6)
                {
                    powerUpText.text = "2 X";
                }
                else
                {
                    powerUpText.text = "1 X";
                }
                break;
        }

    }

    IEnumerator CoinsTextFade()
    {
        yield return new WaitForSeconds(1f);
        isFadeOut = true;
    }

    IEnumerator PopCoinsParticles(int num)
    {
        coinsParticleSys.Emit(num);
        yield return new WaitForSeconds(0.75f);
        isActive = true;
    }


}
