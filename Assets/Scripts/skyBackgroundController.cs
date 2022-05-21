using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skyBackgroundController : MonoBehaviour {

    public Sprite image_1;
    public Sprite image_2;
    public Sprite image_3;
    public Sprite image_4;

    public GameObject leftSky;
    public GameObject middleSky;
    public GameObject rightSky;

    float speed = 1;

	void Start () {
        //Left sky random sprite
        switch (Random.Range(1, 5))
        {
            case 1:
                leftSky.GetComponent<SpriteRenderer>().sprite = image_1;
                break;
            case 2:
                leftSky.GetComponent<SpriteRenderer>().sprite = image_2;
                break;
            case 3:
                leftSky.GetComponent<SpriteRenderer>().sprite = image_3;
                break;
            case 4:
                leftSky.GetComponent<SpriteRenderer>().sprite = image_4;
                break;
        }

        //Middle sky random sprite
        switch (Random.Range(1, 5))
        {
            case 1:
                middleSky.GetComponent<SpriteRenderer>().sprite = image_1;
                break;
            case 2:
                middleSky.GetComponent<SpriteRenderer>().sprite = image_2;
                break;
            case 3:
                middleSky.GetComponent<SpriteRenderer>().sprite = image_3;
                break;
            case 4:
                middleSky.GetComponent<SpriteRenderer>().sprite = image_4;
                break;
        }

        //Right sky random sprite
        switch (Random.Range(1, 5))
        {
            case 1:
                rightSky.GetComponent<SpriteRenderer>().sprite = image_1;
                break;
            case 2:
                rightSky.GetComponent<SpriteRenderer>().sprite = image_2;
                break;
            case 3:
                rightSky.GetComponent<SpriteRenderer>().sprite = image_3;
                break;
            case 4:
                rightSky.GetComponent<SpriteRenderer>().sprite = image_4;
                break;
        }
	}
	

	void Update () {
        leftSky.transform.position = new Vector3(leftSky.transform.position.x + speed * Time.deltaTime, leftSky.transform.position.y, leftSky.transform.position.z);
        middleSky.transform.position = new Vector3(middleSky.transform.position.x + speed * Time.deltaTime, middleSky.transform.position.y, middleSky.transform.position.z);
        rightSky.transform.position = new Vector3(rightSky.transform.position.x + speed * Time.deltaTime, rightSky.transform.position.y, rightSky.transform.position.z);

        if (middleSky.transform.position.x > 10 && middleSky.transform.position.x < 11&& rightSky.transform.position.x>10)
        {
            rightSky.transform.position = new Vector3(leftSky.transform.position.x - 20, rightSky.transform.position.y, rightSky.transform.position.z);
            switch (Random.Range(1, 5))
            {
                case 1:
                    rightSky.GetComponent<SpriteRenderer>().sprite = image_1;
                    break;
                case 2:
                    rightSky.GetComponent<SpriteRenderer>().sprite = image_2;
                    break;
                case 3:
                    rightSky.GetComponent<SpriteRenderer>().sprite = image_3;
                    break;
                case 4:
                    rightSky.GetComponent<SpriteRenderer>().sprite = image_4;
                    break;
            }
        }
            
        if (leftSky.transform.position.x > 10 && leftSky.transform.position.x < 11&& middleSky.transform.position.x>10)
        {
            middleSky.transform.position = new Vector3(rightSky.transform.position.x - 20, middleSky.transform.position.y, middleSky.transform.position.z);
            switch (Random.Range(1, 5))
            {
                case 1:
                    middleSky.GetComponent<SpriteRenderer>().sprite = image_1;
                    break;
                case 2:
                    middleSky.GetComponent<SpriteRenderer>().sprite = image_2;
                    break;
                case 3:
                    middleSky.GetComponent<SpriteRenderer>().sprite = image_3;
                    break;
                case 4:
                    middleSky.GetComponent<SpriteRenderer>().sprite = image_4;
                    break;
            }
        }
        if (rightSky.transform.position.x > 10 && rightSky.transform.position.x < 11 && leftSky.transform.position.x > 10)
        {
            leftSky.transform.position = new Vector3(middleSky.transform.position.x - 20, leftSky.transform.position.y, leftSky.transform.position.z);
            switch (Random.Range(1, 5))
            {
                case 1:
                    leftSky.GetComponent<SpriteRenderer>().sprite = image_1;
                    break;
                case 2:
                    leftSky.GetComponent<SpriteRenderer>().sprite = image_2;
                    break;
                case 3:
                    leftSky.GetComponent<SpriteRenderer>().sprite = image_3;
                    break;
                case 4:
                    leftSky.GetComponent<SpriteRenderer>().sprite = image_4;
                    break;
            }
        }
            

	}
}
