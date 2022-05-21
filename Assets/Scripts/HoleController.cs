using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour {
    string gameState;

    float x;
    float y;
    float radius= 2.3f;
    float rotate;
    float angle;

    Color a = Color.white;

	void Start () {

	}
	
	void Update () {
        gameState = GameObject.Find("Game controller").GetComponent<GameController>().gameState;
        if (gameState == "Play" || gameState == "Power ups")
        {
            a.a -= 0.5f * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = a;
            if (a.a < 0)
                Destroy(this.gameObject);
        }
        
	}

    public void create (float newAngle)
    {
        angle = newAngle;
        rotate = (angle - Mathf.PI * 1.5f) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, rotate);
        x = Mathf.Cos(angle) * radius;
        y = Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, 0);
    }
}
