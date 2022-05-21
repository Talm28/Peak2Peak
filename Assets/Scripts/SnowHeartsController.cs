using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowHeartsController : MonoBehaviour {

    public GameObject heart_1;
    public GameObject heart_2;
    public GameObject heart_3;
    public GameObject plusHeart_1;
    public GameObject plusHeart_2;
    public GameObject plusHeart_3;
    public int lifes;

    public GameObject collotionEffect;

    public GameObject gamecontroller;


	void Start () {
        PlayerPrefs.SetInt("Lifes", 3);
        lifes = PlayerPrefs.GetInt("Lifes");
        switch (lifes)
        {
            case 1:
                heart_1.SetActive(true);
                heart_2.SetActive(false);
                heart_3.SetActive(false);
                plusHeart_1.SetActive(false);
                plusHeart_2.SetActive(false);
                plusHeart_3.SetActive(false);
                break;

            case 2:
                heart_1.SetActive(true);
                heart_2.SetActive(true);
                heart_3.SetActive(false);
                plusHeart_1.SetActive(false);
                plusHeart_2.SetActive(false);
                plusHeart_3.SetActive(false);
                break;

            case 3:
                heart_1.SetActive(true);
                heart_2.SetActive(true);
                heart_3.SetActive(true);
                plusHeart_1.SetActive(false);
                plusHeart_2.SetActive(false);
                plusHeart_3.SetActive(false);
                break;

            case 4:
                heart_1.SetActive(false);
                heart_2.SetActive(false);
                heart_3.SetActive(false);
                plusHeart_1.SetActive(true);
                plusHeart_2.SetActive(true);
                plusHeart_3.SetActive(true);
                break;
        }
		
	}
	
	void Update () {
		
	}

    public void lifeUP()
    {
        lifes += 1;
        switch (lifes)
        {
            case 0:
                heart_1.SetActive(false);
                heart_2.SetActive(false);
                heart_3.SetActive(false);
                gamecontroller.GetComponent<GameController>().Dead();
                break;
            case 1:
                heart_1.SetActive(true);
                heart_2.SetActive(false);
                heart_3.SetActive(false);
                break;

            case 2:
                heart_1.SetActive(true);
                heart_2.SetActive(true);
                heart_3.SetActive(false);
                break;

            case 3:
                heart_1.SetActive(true);
                heart_2.SetActive(true);
                heart_3.SetActive(true);
                plusHeart_1.SetActive(false);
                plusHeart_2.SetActive(false);
                plusHeart_3.SetActive(false);
                break;

            case 4:
                heart_1.SetActive(false);
                heart_2.SetActive(false);
                heart_3.SetActive(false);
                plusHeart_1.SetActive(true);
                plusHeart_2.SetActive(true);
                plusHeart_3.SetActive(true);
                break;
        }
    }

    public void lifeDown()
    {
        lifes -= 1;
        StartCoroutine(CollotionEffect());
        switch (lifes)
        {
            case 0:
                heart_1.SetActive(false);
                heart_2.SetActive(false);
                heart_3.SetActive(false);
                gamecontroller.GetComponent<GameController>().Dead();
                break;
            case 1:
                heart_1.SetActive(true);
                heart_2.SetActive(false);
                heart_3.SetActive(false);
                break;

            case 2:
                heart_1.SetActive(true);
                heart_2.SetActive(true);
                heart_3.SetActive(false);
                break;

            case 3:
                heart_1.SetActive(true);
                heart_2.SetActive(true);
                heart_3.SetActive(true);
                plusHeart_1.SetActive(false);
                plusHeart_2.SetActive(false);
                plusHeart_3.SetActive(false);
                break;
        }
        
    }

    public void Restart()
    {
        PlayerPrefs.SetInt("Lifes", 3);
        lifes = 3;
        heart_1.SetActive(true);
        heart_2.SetActive(true);
        heart_3.SetActive(true);
        plusHeart_1.SetActive(false);
        plusHeart_2.SetActive(false);
        plusHeart_3.SetActive(false);
    }

    IEnumerator CollotionEffect()
    {
        collotionEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        collotionEffect.SetActive(false);
    }
}
