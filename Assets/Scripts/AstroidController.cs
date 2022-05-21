using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidController : MonoBehaviour {

    public GameObject hole;
    public GameObject snowflake;
    GameObject gameController;

    //Boom sound stuff
    bool isBoom;
    Vector3 collisionPoint;
    float distanceToCollision;

    Vector3 target = new Vector3(0, 2, 0);
    float hight;
    float speed;
    float angle;
    float snowTargetAngle;
    float radius;
    bool isRotate = false;
    float scale;
    float minScale;
    float maxScale;
    public float speedFactor;
    float minSpeed;
    float maxSpeed;
    string direction;
    float freezeFactor;
    int level;

    string gameState;

	void Start () {

        //Getting game controller things
        gameController = GameObject.Find("Game controller");
        gameState = gameController.GetComponent<GameController>().gameState;
        level = gameController.GetComponent<GameController>().level;
        freezeFactor = gameController.GetComponent<GameController>().freezeFactor;

        //Setting speed min=0.05 max=0.3
        minSpeed = 0.05f + (0.004f * (level - 1));
        if (minSpeed > 0.25f)
            minSpeed = 0.25f;
        maxSpeed = minSpeed + 0.05f;
        speedFactor = Random.Range(minSpeed, maxSpeed);

        //Setting scale min=0.6 max =1.2
        minScale = 0.6f+(0.008f*(level-1));
        if (minScale > 1)
            minScale = 1;
        maxScale = minScale + 0.2f;
        scale = Random.Range(minScale,maxScale);
        GetComponent<Transform>().localScale = new Vector3(scale, scale, 1);

        //Choose a random direction and hight
        if (Random.Range(1, 3) == 1)
            direction = "Right";
        else
            direction = "Left";
        hight = Random.Range(2.2f, 3.5f);

        if(direction == "Right")
        {
            transform.position = new Vector3(-4, hight, 0);
            transform.rotation = Quaternion.Euler(0, 0, 90 + angle);
            snowTargetAngle = Random.Range(1.5f * Mathf.PI, 1.8f * Mathf.PI);
        }
        else if(direction == "Left")
        {
            transform.position = new Vector3(4, hight, 0);
            transform.rotation = Quaternion.Euler(0, 0, 90 - angle);
            snowTargetAngle = Random.Range(1.2f * Mathf.PI, 1.5f * Mathf.PI);
        }

        //Boom initialize
        collisionPoint = new Vector3(Mathf.Cos(snowTargetAngle) * 2, Mathf.Sin(snowTargetAngle) * 2);
        isBoom = false;
	}
	

	void Update () {

        freezeFactor = gameController.GetComponent<GameController>().freezeFactor;
        gameState = gameController.GetComponent<GameController>().gameState;
        level = gameController.GetComponent<GameController>().level;

        if (gameState == "Play" || gameState == "Power ups")
        {
            speed += speedFactor * Time.deltaTime*freezeFactor;
            angle = Mathf.Atan2(transform.position.y - target.y, transform.position.x - target.x) * 180 / Mathf.PI - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * freezeFactor);

            if (isRotate)
            {
                radius += speed * Time.deltaTime * 50 * freezeFactor;
                target = new Vector3(Mathf.Cos(snowTargetAngle) * radius, Mathf.Sin(snowTargetAngle) * radius);
            }
            else
                target.y -= speed * Time.deltaTime * 50 * freezeFactor;


            if (target.y < 0)
            {
                isRotate = true;
            }


            //Boom update
            distanceToCollision = Vector3.Distance(transform.position, collisionPoint);

            if (distanceToCollision < 2.5 && !isBoom)
            {
                isBoom = true;
                SoundManager.PlaySound("Boom");
            }


        }
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(target, 0.1f);
        Gizmos.DrawSphere(collisionPoint, 0.1f);
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "U snow")
        {
            SpawnObjects();
        }
    }

    void SpawnObjects()
    {
        GameObject a = Instantiate(hole);
        a.GetComponent<HoleController>().create(snowTargetAngle);
        if(Random.Range(0,3)==1)
        {
            GameObject b = Instantiate(snowflake);
            b.GetComponent<SnowflakeController>().create(snowTargetAngle);
        }
        

        Destroy(this.gameObject);

    }
    


}
