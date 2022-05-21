using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MountainBackgroundController : MonoBehaviour {

    public Sprite image_1;
    public Sprite image_2;


    public GameObject leftMountain;
    public GameObject middleMountain;
    public GameObject rightMountain;

    float speed = 0.5f;

    void Start()
    {
        //Left mountain random sprite
        switch (Random.Range(1, 3))
        {
            case 1:
                leftMountain.GetComponent<SpriteRenderer>().sprite = image_1;
                break;
            case 2:
                leftMountain.GetComponent<SpriteRenderer>().sprite = image_2;
                break;

        }

        //Middle mountain random sprite
        switch (Random.Range(1, 3))
        {
            case 1:
                middleMountain.GetComponent<SpriteRenderer>().sprite = image_1;
                break;
            case 2:
                middleMountain.GetComponent<SpriteRenderer>().sprite = image_2;
                break;

        }

        //Right mountain random sprite
        switch (Random.Range(1, 3))
        {
            case 1:
                rightMountain.GetComponent<SpriteRenderer>().sprite = image_1;
                break;
            case 2:
                rightMountain.GetComponent<SpriteRenderer>().sprite = image_2;
                break;

        }
    }


    void Update()
    {
        leftMountain.transform.position = new Vector3(leftMountain.transform.position.x + speed * Time.deltaTime, leftMountain.transform.position.y, leftMountain.transform.position.z);
        middleMountain.transform.position = new Vector3(middleMountain.transform.position.x + speed * Time.deltaTime, middleMountain.transform.position.y, middleMountain.transform.position.z);
        rightMountain.transform.position = new Vector3(rightMountain.transform.position.x + speed * Time.deltaTime, rightMountain.transform.position.y, rightMountain.transform.position.z);

        if (middleMountain.transform.position.x > 10 && middleMountain.transform.position.x < 11 && rightMountain.transform.position.x > 10)
        {
            rightMountain.transform.position = new Vector3(leftMountain.transform.position.x - 20,rightMountain.transform.position.y, rightMountain.transform.position.z);
            switch (Random.Range(1, 3))
            {
                case 1:
                    rightMountain.GetComponent<SpriteRenderer>().sprite = image_1;
                    break;
                case 2:
                    rightMountain.GetComponent<SpriteRenderer>().sprite = image_2;
                    break;

            }
        }

        if (leftMountain.transform.position.x > 10 && leftMountain.transform.position.x < 11 && middleMountain.transform.position.x > 10)
        {
            middleMountain.transform.position = new Vector3(rightMountain.transform.position.x - 20, middleMountain.transform.position.y, middleMountain.transform.position.z);
            switch (Random.Range(1, 3))
            {
                case 1:
                    middleMountain.GetComponent<SpriteRenderer>().sprite = image_1;
                    break;
                case 2:
                    middleMountain.GetComponent<SpriteRenderer>().sprite = image_2;
                    break;

            }
        }
        if (rightMountain.transform.position.x > 10 && rightMountain.transform.position.x < 11 && leftMountain.transform.position.x > 10)
        {
            leftMountain.transform.position = new Vector3(middleMountain.transform.position.x - 20, leftMountain.transform.position.y, leftMountain.transform.position.z);
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftMountain.GetComponent<SpriteRenderer>().sprite = image_1;
                    break;
                case 2:
                    leftMountain.GetComponent<SpriteRenderer>().sprite = image_2;
                    break;

            }
        }


    }
}
