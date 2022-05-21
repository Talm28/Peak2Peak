using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoImageMovement : MonoBehaviour {

    public Transform playerTransform;
    float angel;
    float offset;
	
	void Start () {
        angel = 0;
        offset = 1;
	}
	
	
	void Update () {
        angel += Time.deltaTime*25*offset;
        transform.rotation = Quaternion.Euler(0, 0, angel);

        if (angel < -25)
            offset = 1;
        else if (angel > 25)
            offset = -1;
	}
}
