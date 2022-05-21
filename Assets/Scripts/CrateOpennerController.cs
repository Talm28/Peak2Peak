using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrateOpennerController : MonoBehaviour {

    public GameObject boxOpener;
    public GameObject menu;
    public Button boxButton;
    public Sprite gift;
    public Sprite giftOpen;
    string boxType;
    bool isOpened;
    bool isRotate;
    bool isActive;
    bool isFadeOut;
    public ParticleSystem particleSys;
    public Text snowCoinsText;
    Color snowCoinColor;

    //prices
    public GameObject moneyPrice;
    public Text moneyText;

    float angle;
    int angleFactor;
    int timesToOpen;
    int snowCoins;
    Vector3 target;

    bool isUpdate;


    void Start()
    {
        isUpdate = false;
        Initialize();
        target = new Vector3(0, 3.2f, 0);
    }

    void Update()
    {
        if(!isUpdate)
        {
            Initialize();
            isUpdate = true;
        }

        if (isRotate)
            RotateBox();

        if(isFadeOut&&snowCoinColor.a>0)
        {
            snowCoinColor.a -= Time.deltaTime;
            snowCoinsText.GetComponent<Text>().color = snowCoinColor;
        }

        if (isActive)
        {
            
            if(snowCoinColor.a<1&&!isFadeOut)
            {
                snowCoinColor.a += Time.deltaTime;
                snowCoinsText.GetComponent<Text>().color = snowCoinColor;
            }
            

            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSys.particleCount];
            particleSys.GetParticles(particles);

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].velocity = new Vector3(0, 0, 0);
                particles[i].position = Vector3.MoveTowards(particles[i].position, target, 1);
                if (particles[i].position == target)
                {
                    snowCoins++;
                    PlayerPrefs.SetInt("Snow coins", snowCoins);
                    snowCoinsText.text = snowCoins.ToString();

                    particles[i].position = new Vector3(1500, 1500, 150);
                }
            }


            particleSys.SetParticles(particles, particles.Length);
        }
    }

    void Initialize()
    {
        boxButton.GetComponent<Image>().sprite = gift;
        isOpened = false;
        angle = 0;
        angleFactor = 1;
        timesToOpen = Random.Range(0, 3);
        isActive = false;
        snowCoinColor = snowCoinsText.GetComponent<Text>().color;
        snowCoinColor.a = 0;
        snowCoinsText.GetComponent<Text>().color = snowCoinColor;
        snowCoins = PlayerPrefs.GetInt("Snow coins");
        snowCoinsText.text = snowCoins.ToString();
        isFadeOut = false;
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
        moneyPrice.gameObject.SetActive(false);
        boxOpener.SetActive(false);
    }

    public void BoxClick()
    {
        if (!isOpened)
        {
            if (timesToOpen == 0)
            {
                SoundManager.StopDrums();
                SoundManager.PlaySound("Ta da");
                isOpened = true;
                boxButton.GetComponent<Image>().sprite = giftOpen;
                RandomGiftPrice();

                return;
            }
            else
                StartCoroutine(RotateBocxourotine());
            timesToOpen--;


        }
    }

    IEnumerator PopParticles(int num)
    {
        particleSys.Emit(num);
        yield return new WaitForSeconds(0.75f);
        isActive = true;
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
        angle += 250 * angleFactor * Time.deltaTime;
        boxButton.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (angle > 10 || angle < -10)
            angleFactor = -angleFactor;
    }

    public void RandomGiftPrice()
    {
        if(PlayerPrefs.GetInt("Gifts")>0)
            PlayerPrefs.SetInt("Gifts", PlayerPrefs.GetInt("Gifts") - 1);
        
        StartCoroutine(CoinsTextFade());
        moneyPrice.gameObject.SetActive(true);
        int rnd = Random.Range(1, 21);
        int coins;
        switch (rnd)
        {
            case 1:
                coins = Random.Range(300, 351);
                moneyText.text = coins.ToString();
                StartCoroutine(PopParticles(coins));
                break;
            case 2: case 3:
                coins = Random.Range(250, 300);
                moneyText.text = coins.ToString();
                StartCoroutine(PopParticles(coins));
                break;
            case 4: case 5: case 6:
                coins = Random.Range(200, 250);
                moneyText.text = coins.ToString();
                StartCoroutine(PopParticles(coins));
                break;
            case 7: case 8: case 9: case 10:
                coins = Random.Range(150, 200);
                moneyText.text = coins.ToString();
                StartCoroutine(PopParticles(coins));
                break;
            default:
                coins = Random.Range(50, 150);
                moneyText.text = coins.ToString();
                StartCoroutine(PopParticles(coins));
                break;

                

        }
    }

    IEnumerator CoinsTextFade()
    {
        yield return new WaitForSeconds(2f);
        isFadeOut = true;
    }

}
