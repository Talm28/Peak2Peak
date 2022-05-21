using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFunctions : MonoBehaviour {

    public GameObject game;
    public GameObject player;

    //Player selection
    public int playerIndex;
    int[] availablePlayers;


	void Start () {
        playerIndex = 1;
	}
	

	void Update () {
		
	}

    public void ChangePlayer(int index)
    {
        playerIndex = index;
    }

    public void UpdatePlayer()
    {
        player.GetComponent<Animator>().SetInteger("Player index", playerIndex);
    }
}
