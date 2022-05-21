using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpelAstroidController : MonoBehaviour {

    public GameObject hole;
    GameObject gameController;

    //Boom sound stuff
    bool isBoom;
    Vector3 collisionPoint;
    float distanceToCollision;

    Vector3 target;
    float width;
    float speed;
    float angle;
    float snowTargetAngle;
    float radius;
    bool isRotate = false;
    float scale;
    float speedFactor;
    string direction;
    float freezeFactor;

    string gameState;
    int level;

    void Start()
    {
        gameController = GameObject.Find("Game controller");
        gameState = gameController.GetComponent<GameController>().gameState;
        level = gameController.GetComponent<GameController>().level;

        scale = 1;
        speedFactor = 0.02f+(0.0005f*(level-30));
        GetComponent<Transform>().localScale = new Vector3(scale, scale, 1);
        width = Random.Range(-3f, 3f);

        transform.position = new Vector3(width, 6, 0);
        transform.rotation = Quaternion.Euler(0, 0, 90 + angle);
        target = new Vector3(0, -2, 0);
        snowTargetAngle = Mathf.PI * 1.5f;
        

        freezeFactor = gameController.GetComponent<GameController>().freezeFactor;

        //Boom initialize
        collisionPoint = new Vector3(Mathf.Cos(snowTargetAngle) * 2, Mathf.Sin(snowTargetAngle) * 2);
        isBoom = false;
    }


    void Update()
    {

        freezeFactor = gameController.GetComponent<GameController>().freezeFactor;
        gameState = gameController.GetComponent<GameController>().gameState;

        if (gameState == "Play" || gameState == "Power ups")
        {
            speed += speedFactor * Time.deltaTime * freezeFactor;
            angle = Mathf.Atan2(transform.position.y - target.y, transform.position.x - target.x) * 180 / Mathf.PI - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * freezeFactor);
        }

        speedFactor += 0.08f * Time.deltaTime;

        //Boom update
        distanceToCollision = Vector3.Distance(transform.position, collisionPoint);

        if (distanceToCollision < 2.5 && !isBoom)
        {
            isBoom = true;
            SoundManager.PlaySound("Boom");
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(target, 0.1f);
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "U snow")
        {
            GameObject a = Instantiate(hole);
            a.GetComponent<HoleController>().create(snowTargetAngle);
            Destroy(this.gameObject);
        }

    }
    

}
