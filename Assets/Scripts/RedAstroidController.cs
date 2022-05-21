using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAstroidController : MonoBehaviour {

    public GameObject hole;
    GameObject gameController;

    Vector3 target = new Vector3(0, 2, 0);
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

    //Boom sound stuff
    bool isBoom;
    Vector3 collisionPoint;
    float distanceToCollision;

	void Start () {

        gameController = GameObject.Find("Game controller");
        gameState = gameController.GetComponent<GameController>().gameState;
        level = gameController.GetComponent<GameController>().level;

        speedFactor= 0.15f+(0.001f*(level-20));

        scale = 1;
        GetComponent<Transform>().localScale = new Vector3(scale, scale, 1);
        width = Random.Range(-3f,3f);

            transform.position = new Vector3(width, 6, 0);
            transform.rotation = Quaternion.Euler(0, 0, 90 + angle);
            snowTargetAngle = Random.Range(1.35f * Mathf.PI, 1.65f * Mathf.PI);

        gameController = GameObject.Find("Game controller");
        gameState = gameController.GetComponent<GameController>().gameState;

        freezeFactor = gameController.GetComponent<GameController>().freezeFactor;

        //Boom initialize
        collisionPoint = new Vector3(Mathf.Cos(snowTargetAngle) * 2, Mathf.Sin(snowTargetAngle) * 2);
        isBoom = false;

	}
	

	void Update () {

        freezeFactor = gameController.GetComponent<GameController>().freezeFactor;
        gameState = gameController.GetComponent<GameController>().gameState;

        if (gameState == "Play" || gameState == "Power ups")
        {
            speed += speedFactor * Time.deltaTime * freezeFactor;
            angle = Mathf.Atan2(transform.position.y - target.y, transform.position.x - target.x) * 180 / Mathf.PI - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * freezeFactor);

            if (isRotate)
            {
                radius += 5.5f * Time.deltaTime * freezeFactor;
                target = new Vector3(Mathf.Cos(snowTargetAngle) * radius, Mathf.Sin(snowTargetAngle) * radius);
            }
            else
                target.y -= 2.5f * Time.deltaTime * freezeFactor;


            if (target.y < 0)
            {
                isRotate = true;
            }
        }

        //Boom update
        distanceToCollision = Vector3.Distance(transform.position, collisionPoint);

        if (distanceToCollision < 3 && !isBoom)
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
        if(col.gameObject.tag == "U snow")
        {
            GameObject a = Instantiate(hole);
            a.GetComponent<SnowBlustController>().create(snowTargetAngle);
            Destroy(this.gameObject);
            
            
        }

    }
    

}
