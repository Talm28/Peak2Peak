using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionController : MonoBehaviour {

    public GameObject[] players = new GameObject[20];
    public GameObject[] selected = new GameObject[20];

    public GameObject gameFunctions;
    public GameObject playerSelection;
    public GameObject menu;

    int playersNum;

	void Start () {
        playersNum = 20;
        UpdatePlayers();
	}
	

	void Update () {
        UpdatePlayers();
	}

    public void UpdatePlayers()
    {
        for (int i = 2; i < playersNum+1; i++)
        {
            if (PlayerPrefs.GetInt("Player_" + i) == 1)
            {
                players[i - 1].SetActive(true);
            }
            else
                players[i - 1].SetActive(false);
        }
    }

    public void Close()
    {
        
        StartCoroutine(CloseCourotine());
        
    }
    IEnumerator CloseCourotine()
    {
        SoundManager.PlaySound("Wosh out");
        MenuController.isClickable = true;
        playerSelection.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(0.75f);
        playerSelection.SetActive(false);
    }

    public void SelectPlayer(int index)
    {
        gameFunctions.GetComponent<GameFunctions>().ChangePlayer(index);

        for (int i = 1; i < playersNum+1; i++)
        {
            if (i==index)
            {
                selected[i - 1].SetActive(true);
            }
            else
                selected[i - 1].SetActive(false);
        }
    }
}
