using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleMovement : MonoBehaviour {

    GameObject gameController;
    string gameState;

    string direction;
    Vector3 position;
    Vector3 target;
    float speed;
    float scale;
    public float spawnCrate;
    bool isDropped;

    public GameObject crate;


	void Start () {

        gameController = gameController = GameObject.Find("Game controller");
        gameState = gameController.GetComponent<GameController>().gameState;

        speed = 0.002f;
        scale = Random.Range(0.4f, 0.6f);

        if (Random.Range(0, 2) == 1)
            direction = "Left";
        else
            direction = "Right";

        if(direction == "Left")
        {
            position = new Vector3(4, Random.Range(1f, 3f), 0);
            target = new Vector3(-4, Random.Range(1f, 3f), 0);
            transform.localScale = new Vector3(-scale, scale, 1);

            spawnCrate = Random.Range(0.0f, 2.0f);
        }
        else if (direction == "Right")
        {
            position = new Vector3(-4, Random.Range(1f, 3f), 0);
            target = new Vector3(4, Random.Range(1f, 3f), 0);
            transform.localScale = new Vector3(scale, scale, 1);

            spawnCrate = Random.Range(0.0f, -2.0f);
        }

        transform.position = position;
        isDropped = false;

        SoundManager.PlaySound("Eagle");
	}
	

	void Update () {

        gameState = gameController.GetComponent<GameController>().gameState;

        if (gameState == "Play" || gameState == "Power ups")
        {
            GetComponent<Animator>().enabled = true;
            speed += Time.deltaTime * 0.1f;
            transform.position = Vector3.MoveTowards(transform.position, target, speed);

            if (direction == "Left" && !isDropped)
            {
                if (transform.position.x < spawnCrate)
                {
                    SoundManager.PlaySound("Pop");
                    isDropped = true;
                    GameObject a = Instantiate(crate);
                    a.GetComponent<CrateMovement>().Create(new Vector3(transform.position.x, transform.position.y - 0.1f, 0));
                }
            }
            else if (direction == "Right" && !isDropped)
            {
                if (transform.position.x > spawnCrate)
                {
                    SoundManager.PlaySound("Pop");
                    isDropped = true;
                    GameObject a = Instantiate(crate);
                    a.GetComponent<CrateMovement>().Create(new Vector3(transform.position.x, transform.position.y - 0.1f, 0));
                }
            }

        }
        else
            GetComponent<Animator>().enabled = false;
        if (transform.position == target)
        {
            Destroy(this.gameObject);
        }
	}
}
