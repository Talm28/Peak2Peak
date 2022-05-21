using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowCoinControll : MonoBehaviour {

     Text snowCoinsText;
     public GameObject coin;

	void Start () {
        snowCoinsText = GetComponent<Text>();
	}
	

	void Update () {
        coin.transform.position = new Vector3(0.4f + (snowCoinsText.text.Length - 1) * 0.12f, coin.transform.position.y, coin.transform.position.z);

	}
}
