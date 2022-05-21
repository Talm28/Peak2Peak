using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBlustController : MonoBehaviour {


    string gameState;
    GameObject gameController;

    float x;
    float y;
    float radius = 2f;
    float limit;
    float speed;
    float rotate;
    float angle;
    float freezeFactor;

    Color a = Color.white;

    void Start()
    {
        speed = -5f;
        limit = Random.Range(-1f, 1f);
        gameController = GameObject.Find("Game controller");
    }

    void Update()
    {
        
        gameState = gameController.GetComponent<GameController>().gameState;
        freezeFactor = gameController.GetComponent<GameController>().freezeFactor;

        if (gameState == "Play" || gameState == "Power ups")
        {
            radius += speed * Time.deltaTime * freezeFactor;
            x = Mathf.Cos(angle) * radius;
            y = Mathf.Sin(angle) * radius;
            transform.position = new Vector3(x, y, 0);

            speed += Time.deltaTime * 6 * freezeFactor;
            if (speed > 0)
                transform.rotation = Quaternion.Euler(0, 0, rotate);
        }

    }

    public void create(float newAngle)
    {
        angle = newAngle;
        rotate = (angle - Mathf.PI * 1.5f) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, rotate - 180);
        x = Mathf.Cos(angle) * radius;
        y = Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "U snow")
        {
            if (speed > 0)
                Destroy(this.gameObject);



        }

    }
}
