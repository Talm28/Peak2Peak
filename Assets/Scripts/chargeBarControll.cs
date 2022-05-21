using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chargeBarControll : MonoBehaviour {

    public GameObject player;
    Transform chargeBarTransform;
    Slider chargeBar;

	void Start () {
        chargeBarTransform = GetComponent<Transform>();
        chargeBar = GetComponent<Slider>();
        chargeBar.value = chargeBar.minValue;
	}

    public void Active()
    {
        chargeBarTransform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.8f, player.transform.position.z);
    }
    public void DisActive()
    {
        chargeBarTransform.position = new Vector3(1000, 0, 0);
        chargeBar.value = chargeBar.minValue;
    }

	void Update () {
        
	}
}
