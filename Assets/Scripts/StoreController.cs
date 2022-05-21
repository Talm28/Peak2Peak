using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour {
    
    //Power ups sprites
    public Sprite doubleCoins;
    public Sprite freeze;
    public Sprite extraLife;
    public Sprite shield;

    //Power ups prices
    public Text doubleCoinsPriceText;
    int doubleCoinsPrice;
    public Text freezePriceText;
    int freezePrice;
    public Text extraLifePriceText;
    int extraLifePrice;
    public Text shieldPriceText;
    int shieldPrice;

    public Text keyPriceText;
    int keyPrice;

    bool isClickable;

    //Product info variable
    public GameObject productInfo;
    public Text productName_1;
    public Text productName_2;
    public Text productDescription;
    public Text productPrice;
    public Image productImage;
    public Image keyImage;
    string currentPowerUp;

    //Players sprites
    public Sprite player_1;
    public Sprite player_2;
    public Sprite player_3;
    public Sprite player_4;
    public Sprite player_5;
    public Sprite player_6;
    public Sprite player_7;
    public Sprite player_8;
    public Sprite player_9;
    public Sprite player_10;
    public Sprite player_13;
    public Sprite player_15;
    public Sprite player_16;
    public Sprite player_17;
    public Sprite player_18;
    public Sprite player_20;


    //Players prices
    public Text player_2_Price_Text;
    int player_2_Price;
    public Text player_3_Price_Text;
    int player_3_Price;
    public Text player_4_Price_Text;
    int player_4_Price;
    public Text player_5_Price_Text;
    int player_5_Price;
    public Text player_6_Price_Text;
    int player_6_Price;
    public Text player_7_Price_Text;
    int player_7_Price;
    public Text player_8_Price_Text;
    int player_8_Price;
    public Text player_9_Price_Text;
    int player_9_Price;
    public Text player_10_Price_Text;
    int player_10_Price;
    public Text player_13_Price_Text;
    int player_13_Price;
    public Text player_15_Price_Text;
    int player_15_Price;
    public Text player_16_Price_Text;
    int player_16_Price;
    public Text player_17_Price_Text;
    int player_17_Price;
    public Text player_18_Price_Text;
    int player_18_Price;
    public Text player_20_Price_Text;
    int player_20_Price;

    //Player info variable
    public GameObject playerInfo;
    public Text playerName_1;
    public Text playerName_2;
    public Text playerPrice;
    public Image playerImage;
    string currentPlayer;


    public GameObject menu;
    public GameObject store;
    public Text coinsText;
    public GameObject player;

    bool isScroll;
    bool isUpdate;
    bool isInfoOpened;

	void Start () {

        isInfoOpened = false;
        isClickable = true;
        coinsText.text = PlayerPrefs.GetInt("Snow coins").ToString();

        //Update Prices
        doubleCoinsPrice = 50;
        freezePrice = 250;
        extraLifePrice = 150;
        shieldPrice = 300;
        keyPrice = 200;

        doubleCoinsPriceText.text = doubleCoinsPrice.ToString();
        freezePriceText.text = freezePrice.ToString();
        extraLifePriceText.text = extraLifePrice.ToString();
        shieldPriceText.text = shieldPrice.ToString();

        keyPriceText.text = keyPrice.ToString();

        player_2_Price = 15;
        player_3_Price = 450;
        player_4_Price = 600;
        player_5_Price = 650;
        player_6_Price = 1200;
        player_7_Price = 600;
        player_8_Price = 600;
        player_9_Price = 850;
        player_10_Price = 1600;
        player_13_Price = 2800;
        player_15_Price = 5000;
        player_16_Price = 150;
        player_17_Price = 150;
        player_18_Price = 150;
        player_20_Price = 3000;

        player_2_Price_Text.text = player_2_Price.ToString();
        player_3_Price_Text.text = player_3_Price.ToString();
        player_4_Price_Text.text = player_4_Price.ToString();
        player_5_Price_Text.text = player_5_Price.ToString();
        player_6_Price_Text.text = player_6_Price.ToString();
        player_7_Price_Text.text = player_7_Price.ToString();
        player_8_Price_Text.text = player_8_Price.ToString();
        player_9_Price_Text.text = player_9_Price.ToString();
        player_10_Price_Text.text = player_10_Price.ToString();
        player_13_Price_Text.text = player_13_Price.ToString();
        player_15_Price_Text.text = player_15_Price.ToString();
        player_16_Price_Text.text = player_16_Price.ToString();
        player_17_Price_Text.text = player_17_Price.ToString();
        player_18_Price_Text.text = player_18_Price.ToString();
        player_20_Price_Text.text = player_20_Price.ToString();

        //Give all players
        //PlayerPrefs.SetInt("Player_2", 1);
        //PlayerPrefs.SetInt("Player_3", 1);
        //PlayerPrefs.SetInt("Player_4", 1);
        //PlayerPrefs.SetInt("Player_5", 1);
        //PlayerPrefs.SetInt("Player_6", 1);
        //PlayerPrefs.SetInt("Player_7", 1);
        //PlayerPrefs.SetInt("Player_8", 1);
        //PlayerPrefs.SetInt("Player_9", 1);
        //PlayerPrefs.SetInt("Player_10", 1);
        //PlayerPrefs.SetInt("Player_11", 1);
        //PlayerPrefs.SetInt("Player_12", 1);
        //PlayerPrefs.SetInt("Player_13", 1);
        //PlayerPrefs.SetInt("Player_14", 1);
        //PlayerPrefs.SetInt("Player_15", 1);
        //PlayerPrefs.SetInt("Player_16", 1);
        //PlayerPrefs.SetInt("Player_17", 1);
        //PlayerPrefs.SetInt("Player_18", 1);
        //PlayerPrefs.SetInt("Player_19", 1);
        //PlayerPrefs.SetInt("Player_20", 1);

        //update owned players
        UpdateStore();

	}
	

	void Update () {
        coinsText.text = PlayerPrefs.GetInt("Snow coins").ToString();

		if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
                isScroll = true;



            if (Input.touches[0].phase == TouchPhase.Ended)
                isScroll = false;
        }

        if (!isUpdate)
        {
            UpdateStore();
            isUpdate = true;
        }
	}

    public void backToMenu()
    {
        if (!isInfoOpened)
        {
            StartCoroutine(backToMenuCourotine());
        }
    }
    IEnumerator backToMenuCourotine()
    {
        SoundManager.PlaySound("Wosh out");
        store.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(0.5f);
        SoundManager.PlaySound("Wosh in");
        isUpdate = false;
        menu.SetActive(true);
        store.SetActive(false);
    }

    public void CloseProductInfo()
    {
        StartCoroutine(CloseProductInfoCourotine());
    }
    IEnumerator CloseProductInfoCourotine()
    {
        isInfoOpened = false;
        isClickable = true;
        productInfo.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(0.4f);
        SoundManager.PlaySound("Wosh out");
        yield return new WaitForSeconds(0.35f);
        productInfo.GetComponent<Animator>().SetBool("isClose", false);
        productInfo.SetActive(false);
    }

    public void DoubleCoinsInfo()
    {
        if(!isScroll&&isClickable)
        {
            isInfoOpened = true;
            isClickable = false;
            productImage.gameObject.SetActive(true);
            keyImage.gameObject.SetActive(false);
            productName_1.text = "Double coins";
            productName_2.text = "Double coins";
            productDescription.text = "For 30 seconds every snowflake you get is worth two!";
            productPrice.text = doubleCoinsPrice.ToString();
            productImage.sprite = doubleCoins;
            productInfo.SetActive(true);
            SoundManager.PlaySound("Wosh in");
            currentPowerUp = "Double coins";
        }

    }
    public void FreezeInfo()
    {
        if (!isScroll && isClickable)
        {
            isInfoOpened = true;
            isClickable = false;
            productImage.gameObject.SetActive(true);
            keyImage.gameObject.SetActive(false);
            productName_1.text = "Freeze";
            productName_2.text = "Freeze";
            productDescription.text = "For 15 seconds all the astroids move slower!";
            productPrice.text = freezePrice.ToString();
            productImage.sprite = freeze;
            productInfo.SetActive(true);
            SoundManager.PlaySound("Wosh in");
            currentPowerUp = "Freeze";
        }
    }
    public void ExtraLifeInfo()
    {
        if (!isScroll && isClickable)
        {
            isInfoOpened = true;
            isClickable = false;
            productImage.gameObject.SetActive(true);
            keyImage.gameObject.SetActive(false);
            productName_1.text = "Extra life";
            productName_2.text = "Extra life";
            productDescription.text = "By activating this power up you get one extra life!";
            productPrice.text = extraLifePrice.ToString();
            productImage.sprite = extraLife;
            productInfo.SetActive(true);
            SoundManager.PlaySound("Wosh in");
            currentPowerUp = "Extra life";
        }
    }
    public void ShieldInfo()
    {
        if (!isScroll && isClickable)
        {
            isInfoOpened = true;
            isClickable = false;
            productImage.gameObject.SetActive(true);
            keyImage.gameObject.SetActive(false);
            productName_1.text = "Shield";
            productName_2.text = "Shield";
            productDescription.text = "This item gives you immunity from astroids for 30 seconds!";
            productPrice.text = shieldPrice.ToString();
            productImage.sprite = shield;
            productInfo.SetActive(true);
            SoundManager.PlaySound("Wosh in");
            currentPowerUp = "Shield";
        }
    }
    public void KeyInfo()
    {
        if (!isScroll && isClickable)
        {
            isInfoOpened = true;
            isClickable = false;
            keyImage.gameObject.SetActive(true);
            productImage.gameObject.SetActive(false);
            productName_1.text = "Key";
            productName_2.text = "Key";
            productDescription.text = "This item used to open eagle crates";
            productPrice.text = keyPrice.ToString();
            productInfo.SetActive(true);
            SoundManager.PlaySound("Wosh in");
            currentPowerUp = "Key";
        }
    }


    public void BuyProduct()
    {
        
        switch (currentPowerUp)
        {
            case "Key":
                if (PlayerPrefs.GetInt("Snow coins") >= keyPrice)
                {
                    SoundManager.PlaySound("Cha ching");
                    PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - keyPrice);
                    PlayerPrefs.SetInt("Keys", PlayerPrefs.GetInt("Keys") + 1);
                }
                break;
            case "Double coins":
                if(PlayerPrefs.GetInt("Snow coins")>= doubleCoinsPrice)
                {
                    SoundManager.PlaySound("Cha ching");
                    PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - doubleCoinsPrice);
                    PlayerPrefs.SetInt("Double coins", PlayerPrefs.GetInt("Double coins") + 1);
                }
                break;

            case "Freeze":
                if (PlayerPrefs.GetInt("Snow coins") >= freezePrice)
                {
                    SoundManager.PlaySound("Cha ching");
                    PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - freezePrice);
                    PlayerPrefs.SetInt("Freeze", PlayerPrefs.GetInt("Freeze") + 1);
                }
                break;

            case "Extra life":
                if (PlayerPrefs.GetInt("Snow coins") >= extraLifePrice)
                {
                    SoundManager.PlaySound("Cha ching");
                    PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - extraLifePrice);
                    PlayerPrefs.SetInt("Extra life", PlayerPrefs.GetInt("Extra life") + 1);
                }
                break;

            case "Shield":
                if (PlayerPrefs.GetInt("Snow coins") >= shieldPrice)
                {
                    SoundManager.PlaySound("Cha ching");
                    PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - shieldPrice);
                    PlayerPrefs.SetInt("Shield", PlayerPrefs.GetInt("Shield") + 1);
                }
                break;
        }
    }

    public void Player_2_info()
    {
        if (!isScroll && isClickable)
        {
            if(PlayerPrefs.GetInt("Player_2")!=1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "Funky John";
                playerName_2.text = "Funky John";
                playerImage.sprite = player_2;
                playerPrice.text = player_2_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_2";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_3_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_3") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "Casual Steve";
                playerName_2.text = "Casual Steve";
                playerImage.sprite = player_3;
                playerPrice.text = player_3_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_3";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_4_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_4") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "lil' Bobby";
                playerName_2.text = "lil' Bobby";
                playerImage.sprite = player_4;
                playerPrice.text = player_4_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_4";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_5_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_5") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "Granny";
                playerName_2.text = "Granny";
                playerImage.sprite = player_5;
                playerPrice.text = player_5_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_5";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_6_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_6") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "Yeti";
                playerName_2.text = "Yeti";
                playerImage.sprite = player_6;
                playerPrice.text = player_6_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_6";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_7_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_7") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "James";
                playerName_2.text = "James";
                playerImage.sprite = player_7;
                playerPrice.text = player_7_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_7";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_8_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_8") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "Jessy";
                playerName_2.text = "Jessy";
                playerImage.sprite = player_8;
                playerPrice.text = player_8_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_8";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_9_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_9") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "Witchy witch";
                playerName_2.text = "Witchy witch";
                playerImage.sprite = player_9;
                playerPrice.text = player_9_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_9";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_10_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_10") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "Snowy";
                playerName_2.text = "Snowy";
                playerImage.sprite = player_10;
                playerPrice.text = player_10_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_10";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_13_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_13") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "Richard";
                playerName_2.text = "Richard";
                playerImage.sprite = player_13;
                playerPrice.text = player_13_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_13";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_15_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_15") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "Smoky";
                playerName_2.text = "Smoky";
                playerImage.sprite = player_15;
                playerPrice.text = player_15_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_15";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_16_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_16") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "Big Bobby";
                playerName_2.text = "Big Bobby";
                playerImage.sprite = player_16;
                playerPrice.text = player_16_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_16";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_17_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_17") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "Britney";
                playerName_2.text = "Britney";
                playerImage.sprite = player_17;
                playerPrice.text = player_17_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_17";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_18_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_18") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "Mike";
                playerName_2.text = "Mike";
                playerImage.sprite = player_18;
                playerPrice.text = player_18_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_18";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }
    public void Player_20_info()
    {
        if (!isScroll && isClickable)
        {
            if (PlayerPrefs.GetInt("Player_20") != 1)
            {
                isInfoOpened = true;
                isClickable = false;
                playerName_1.text = "Babs";
                playerName_2.text = "Babs";
                playerImage.sprite = player_20;
                playerPrice.text = player_20_Price.ToString();
                playerInfo.SetActive(true);
                currentPlayer = "Player_20";
                SoundManager.PlaySound("Wosh in");
            }
        }
    }

    public void BuyPlayer()
    {
        switch (currentPlayer)
        {
            case "Player_2":
                if (PlayerPrefs.GetInt("Snow coins")>=player_2_Price)
                {
                    if(PlayerPrefs.GetInt("Player_2")!=1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_2_Price);
                        PlayerPrefs.SetInt("Player_2", 1);
                        player_2_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_3":
                if (PlayerPrefs.GetInt("Snow coins") >= player_3_Price)
                {
                    if (PlayerPrefs.GetInt("Player_3") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_3_Price);
                        PlayerPrefs.SetInt("Player_3", 1);
                        player_3_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_4":
                if (PlayerPrefs.GetInt("Snow coins") >= player_4_Price)
                {
                    if (PlayerPrefs.GetInt("Player_4") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_4_Price);
                        PlayerPrefs.SetInt("Player_4", 1);
                        player_4_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_5":
                if (PlayerPrefs.GetInt("Snow coins") >= player_5_Price)
                {
                    if (PlayerPrefs.GetInt("Player_5") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_5_Price);
                        PlayerPrefs.SetInt("Player_5", 1);
                        player_5_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_6":
                if (PlayerPrefs.GetInt("Snow coins") >= player_6_Price)
                {
                    if (PlayerPrefs.GetInt("Player_6") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_6_Price);
                        PlayerPrefs.SetInt("Player_6", 1);
                        player_6_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_7":
                if (PlayerPrefs.GetInt("Snow coins") >= player_7_Price)
                {
                    if (PlayerPrefs.GetInt("Player_7") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_7_Price);
                        PlayerPrefs.SetInt("Player_7", 1);
                        player_7_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_8":
                if (PlayerPrefs.GetInt("Snow coins") >= player_8_Price)
                {
                    if (PlayerPrefs.GetInt("Player_8") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_8_Price);
                        PlayerPrefs.SetInt("Player_8", 1);
                        player_8_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_9":
                if (PlayerPrefs.GetInt("Snow coins") >= player_9_Price)
                {
                    if (PlayerPrefs.GetInt("Player_9") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_9_Price);
                        PlayerPrefs.SetInt("Player_9", 1);
                        player_9_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_10":
                if (PlayerPrefs.GetInt("Snow coins") >= player_10_Price)
                {
                    if (PlayerPrefs.GetInt("Player_10") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_10_Price);
                        PlayerPrefs.SetInt("Player_10", 1);
                        player_10_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_13":
                if (PlayerPrefs.GetInt("Snow coins") >= player_13_Price)
                {
                    if (PlayerPrefs.GetInt("Player_13") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_13_Price);
                        PlayerPrefs.SetInt("Player_13", 1);
                        player_13_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_15":
                if (PlayerPrefs.GetInt("Snow coins") >= player_15_Price)
                {
                    if (PlayerPrefs.GetInt("Player_15") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_15_Price);
                        PlayerPrefs.SetInt("Player_15", 1);
                        player_15_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_16":
                if (PlayerPrefs.GetInt("Snow coins") >= player_16_Price)
                {
                    if (PlayerPrefs.GetInt("Player_16") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_16_Price);
                        PlayerPrefs.SetInt("Player_16", 1);
                        player_16_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_17":
                if (PlayerPrefs.GetInt("Snow coins") >= player_17_Price)
                {
                    if (PlayerPrefs.GetInt("Player_17") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_17_Price);
                        PlayerPrefs.SetInt("Player_17", 1);
                        player_17_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_18":
                if (PlayerPrefs.GetInt("Snow coins") >= player_18_Price)
                {
                    if (PlayerPrefs.GetInt("Player_18") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_18_Price);
                        PlayerPrefs.SetInt("Player_18", 1);
                        player_18_Price_Text.text = "Owned!";
                    }
                }
                break;
            case "Player_20":
                if (PlayerPrefs.GetInt("Snow coins") >= player_20_Price)
                {
                    if (PlayerPrefs.GetInt("Player_20") != 1)
                    {
                        SoundManager.PlaySound("Cha ching");
                        PlayerPrefs.SetInt("Snow coins", PlayerPrefs.GetInt("Snow coins") - player_20_Price);
                        PlayerPrefs.SetInt("Player_20", 1);
                        player_20_Price_Text.text = "Owned!";
                    }
                }
                break;
        }
    }

    public void ClosePlayerInfo()
    {
        StartCoroutine(ClosePlayerInfoCourotine());
    }
    IEnumerator ClosePlayerInfoCourotine()
    {
        isInfoOpened = false;
        isClickable = true;
        SoundManager.PlaySound("Wosh out");
        playerInfo.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(0.75f);
        playerInfo.SetActive(false);
    }


    public void UpdateStore()
    {
        player_2_Price_Text.text = player_2_Price.ToString();
        player_3_Price_Text.text = player_3_Price.ToString();
        player_4_Price_Text.text = player_4_Price.ToString();
        player_5_Price_Text.text = player_5_Price.ToString();
        player_6_Price_Text.text = player_6_Price.ToString();
        player_7_Price_Text.text = player_7_Price.ToString();
        player_8_Price_Text.text = player_8_Price.ToString();
        player_9_Price_Text.text = player_9_Price.ToString();
        player_10_Price_Text.text = player_10_Price.ToString();
        player_13_Price_Text.text = player_13_Price.ToString();
        player_15_Price_Text.text = player_15_Price.ToString();
        player_16_Price_Text.text = player_16_Price.ToString();
        player_17_Price_Text.text = player_17_Price.ToString();
        player_18_Price_Text.text = player_18_Price.ToString();
        player_20_Price_Text.text = player_20_Price.ToString();

        if (PlayerPrefs.GetInt("Player_2") == 1)
            player_2_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_3") == 1)
            player_3_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_4") == 1)
            player_4_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_5") == 1)
            player_5_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_6") == 1)
            player_6_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_7") == 1)
            player_7_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_8") == 1)
            player_8_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_9") == 1)
            player_9_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_10") == 1)
            player_10_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_13") == 1)
            player_13_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_15") == 1)
            player_15_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_16") == 1)
            player_16_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_17") == 1)
            player_17_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_18") == 1)
            player_18_Price_Text.text = "Owned!";
        if (PlayerPrefs.GetInt("Player_20") == 1)
            player_20_Price_Text.text = "Owned!";

    }


}