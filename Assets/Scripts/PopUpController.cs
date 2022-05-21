using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpController : MonoBehaviour {

    Text text;

    void Start () {
        text = GetComponent<Text>();
	}
	
	void Update () {
        if (text.color.a == 0)
            this.gameObject.SetActive(false);
	}
}
