using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowflakeController : MonoBehaviour {

    GameObject gameController;
    string gameState;

    float x;
    float y;
    float radius = 1.8f;
    float rotate;
    float angle;

    Color a = Color.white;

    public bool isCollected;
    Vector3 origin;
    float speed;
    float distance;
    bool isPop;

    void Start()
    {
        isPop = false;
        gameController = GameObject.Find("Game controller");

        isCollected = false;
        origin = new Vector3(0, 4.5f, 0);
        speed = 0.05f;
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, origin);
        gameState = gameController.GetComponent<GameController>().gameState;
        if (gameState == "Play" || gameState == "Power ups")
        {
            if(!isCollected)
            {
                a.a -= 0.4f * Time.deltaTime;
                GetComponent<SpriteRenderer>().color = a;
            }
            if (a.a < 0.2)
                Destroy(this.gameObject);
        }
        if(isCollected)
        {
            a.a = 1;
            GetComponent<SpriteRenderer>().color = a;
            speed += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, origin, speed);
        }
        if(transform.position == origin)
        {
            gameController.GetComponent<GameController>().AddCoins(false);
            Destroy(this.gameObject);
        }
        if(!isPop && distance<1.5)
        {
            SoundManager.PlaySound("Coin");
            isPop = true;
        }

    }

    public void create(float newAngle)
    {
        angle = newAngle;
        rotate = (angle - Mathf.PI * 1.5f) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, rotate);
        x = Mathf.Cos(angle) * radius;
        y = Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, 0);
    }
}
