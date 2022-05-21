using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public ParticleSystem clicklParticle;
    public GameObject game;
    public GameObject menu;
    public GameObject menuPanel;
    public GameObject store;
    public GameObject gameFunctions;
    public GameObject playerSelection;
    public GameObject bonusLevel;
    public GameObject settings;
    public GameObject settingsController;
    bool isUpdate;
    public static bool isClickable;

    //Stats variables
    public GameObject statsPanel;
    public Text levelText_1;
    public Text levelText_2;
    public Text progressText_1;
    public Text progressText_2;
    public Text snowCoinsText;
    public Text doubleCoinsQuantity;
    public Text freezeQuantity;
    public Text extraLifeQuantity;
    public Text shieldQuantity;
    public Text keyQuantity;

    GameObject[] astroids;

    //Box opener
    public GameObject crateOpener;
    public Button crateButton;
    int crateQuantity;
    public Text crateQuantityText; 
    public GameObject giftOpener;
    public Button giftButton;
    int giftQuantity;
    public Text giftQuantityText; 


	void Start () {

        PlayerPrefs.SetInt("Crates", 50);
        PlayerPrefs.SetInt("Gifts", 50);

        isUpdate = false;
        isClickable = true;
        
	}
	
	void Update () {
        astroids = GameObject.FindGameObjectsWithTag("Astroid");
        if(astroids.Length>0)
        {
            DestroyObjects();
        }

        if (!isUpdate)
            UpdateMenu();

        

	}

    void UpdateMenu()
    {
        crateQuantity = PlayerPrefs.GetInt("Crates");
        giftQuantity = PlayerPrefs.GetInt("Gifts");

        if (crateQuantity > 0)
        {
            crateButton.gameObject.SetActive(true);
            crateQuantityText.text = crateQuantity.ToString();
        }
        else
            crateButton.gameObject.SetActive(false);

        if (giftQuantity > 0)
        {
            giftButton.gameObject.SetActive(true);
            giftQuantityText.text = giftQuantity.ToString();
        }
        else
            giftButton.gameObject.SetActive(false);

        isUpdate = true;
    }

    void DestroyObjects()
    {
        GameObject[] astroids = GameObject.FindGameObjectsWithTag("Astroid");
        GameObject[] holes = GameObject.FindGameObjectsWithTag("Hole");
        GameObject[] snowflakes = GameObject.FindGameObjectsWithTag("Snowflake");

        for (int i = 0; i < astroids.Length; i++)
        {
            Destroy(astroids[i].gameObject);
        }
        for (int i = 0; i < holes.Length; i++)
        {
            Destroy(holes[i].gameObject);
        }
        for (int i = 0; i < snowflakes.Length; i++)
        {
            Destroy(snowflakes[i].gameObject);
        }
    }

    public void Play()
    {
        if(isClickable)
            StartCoroutine(PlayCoroutine());
    }
    IEnumerator PlayCoroutine()
    {
        SoundManager.PlaySound("Wosh out");
        SoundManager.isPlay = true;
        clicklParticle.transform.position = new Vector3(0, 1, 0);
        clicklParticle.Emit(30);
        menu.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(0.4f);
        SoundManager.PlaySound("Wosh in");
        game.SetActive(true);
        gameFunctions.GetComponent<GameFunctions>().UpdatePlayer();
        if (PlayerPrefs.GetInt("Level") % 5 == 0)
        {
            SoundManager.PlaySound("Bonus level");
            bonusLevel.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        bonusLevel.SetActive(false);
        isUpdate = false;
        menu.SetActive(false);
    }

    public void Store()
    {
        if (isClickable)
            StartCoroutine(StoreCoroutine());
    }
    IEnumerator StoreCoroutine()
    {
        SoundManager.PlaySound("Wosh out");
        clicklParticle.transform.position = new Vector3(-1.2f, -1.9f, 0);
        clicklParticle.Emit(30);
        menu.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(0.6f);
        SoundManager.PlaySound("Wosh in");
        store.SetActive(true);
        isUpdate = false;
        menu.SetActive(false);
    }

    public void OpenPlayerSelection()
    {
        SoundManager.PlaySound("Wosh in");
        if(isClickable)
        {
            isClickable = false;
            clicklParticle.transform.position = new Vector3(0, -0.3f, 0);
            clicklParticle.Emit(30);
            playerSelection.SetActive(true);
            playerSelection.GetComponent<PlayerSelectionController>().UpdatePlayers();
        }
        
    }

    public void Settings()
    {
         if(isClickable)
            StartCoroutine(SettingsCoroutine());
    }
    IEnumerator SettingsCoroutine()
    {
        SoundManager.PlaySound("Wosh out");
        clicklParticle.transform.position = new Vector3(0, -3.2f, 0);
        clicklParticle.Emit(30);
        menu.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(0.6f);
        SoundManager.PlaySound("Wosh in");
        settings.SetActive(true);
        settingsController.GetComponent<SettingsController>().ActiveResetButton();
        isUpdate = false;
        menu.SetActive(false);
    }

    public void OpenStats()
    {
        SoundManager.PlaySound("Wosh in");
        if(isClickable)
        {
            clicklParticle.transform.position = new Vector3(1.2f, -1.9f, 0);
            clicklParticle.Emit(30);
            levelText_1.text = "Level: " + PlayerPrefs.GetInt("Level").ToString();
            levelText_2.text = levelText_1.text;
            int percentage = (int)((PlayerPrefs.GetInt("Level score") * 100) / (50 + (10 * (PlayerPrefs.GetInt("Level") - 1))));
            progressText_1.text = percentage.ToString() + "% " + "comleted!";
            progressText_2.text = progressText_1.text;
            snowCoinsText.text = PlayerPrefs.GetInt("Snow coins").ToString();

            isClickable = false;

            //Update items
                doubleCoinsQuantity.text = PlayerPrefs.GetInt("Double coins").ToString();

                freezeQuantity.text = PlayerPrefs.GetInt("Freeze").ToString();

                extraLifeQuantity.text = PlayerPrefs.GetInt("Extra life").ToString();

                shieldQuantity.text = PlayerPrefs.GetInt("Shield").ToString();

                keyQuantity.text = PlayerPrefs.GetInt("Keys").ToString();

            statsPanel.SetActive(true);
        }
       
    }

    public void CloseStats()
    {
        StartCoroutine(CloseStatsCourotine());
    }
    IEnumerator CloseStatsCourotine()
    {
        SoundManager.PlaySound("Wosh out");
        isClickable = true;
        statsPanel.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(0.5f);
        statsPanel.SetActive(false);
    }



    //--------------------------Box opener functions------------------

    public void OpenBoxOpener(string type)
    {
        if (isClickable)
        {
            if (type == "Crate")
                StartCoroutine(OpenCrateOpennerCourotine());
            if (type == "Gift")
                StartCoroutine(OpenGiftOpennerCourotine());
        }
    }
    IEnumerator OpenCrateOpennerCourotine()
    {
        SoundManager.PlaySound("Wosh out");
        menu.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(0.5f);
        crateOpener.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        isUpdate = false;
        menu.SetActive(false);

    }
    IEnumerator OpenGiftOpennerCourotine()
    {
        SoundManager.PlaySound("Wosh out");
        menu.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(0.5f);
        SoundManager.PlaySound("Drums");
        giftOpener.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        isUpdate = false;
        menu.SetActive(false);
    }
   

    

    

}
