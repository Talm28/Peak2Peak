using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateMovement : MonoBehaviour {

    GameObject gameController;
    string gameState;

    Vector3 target = new Vector3(0, -2, 0);
    Vector2 collectTarget = new Vector3(0, 4.5f, 0);
    float speed;
    bool isFalling;
    bool isCollected;
    float angle;
    int angleFactor;
    Color a;


	void Start () {

        gameController = gameController = GameObject.Find("Game controller");
        gameState = gameController.GetComponent<GameController>().gameState;

        isFalling = true;
        isCollected = false;
        speed = 0.005f;
        angle = 0f;
        angleFactor = 1;

        a = Color.white;

	}


	void Update () {

        gameState = gameController.GetComponent<GameController>().gameState;

        if (gameState == "Play" || gameState == "Power ups")
        {
            if (isFalling)
            {
                speed += Time.deltaTime * 0.02f;
                transform.position = Vector3.MoveTowards(transform.position, target, speed);

                if (transform.position == target)
                {
                    isFalling = false;
                    angle = 0;
                }

                angle += 40 * Time.deltaTime * angleFactor;
                if (angle > 10)
                    angleFactor = -1;
                else if (angle < -10)
                    angleFactor = 1;

                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                a.a -= 1.1F * Time.deltaTime;
                GetComponent<SpriteRenderer>().color = a;

                if (a.a < 0.1f)
                    Destroy(this.gameObject);
            }
        }
        if(isCollected)
        {
            speed += Time.deltaTime ;
            transform.position = Vector3.MoveTowards(transform.position, collectTarget, speed);

            a.a -= 1.5F * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = a;

            if (a.a < 0.1f)
                Destroy(this.gameObject);
        }
	}

    public void Create(Vector3 position)
    {
        transform.position = position;

    }

    public void Collect()
    {
        isCollected = true;
        isFalling = false;
        speed = 0.1f;
        angle = 0;

        a.a = 1;
        GetComponent<SpriteRenderer>().color = a;
    }
}
