using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HillsBackgroundController : MonoBehaviour {

    public Sprite image_1;
    public Sprite image_2;


    public GameObject leftHiil;
    public GameObject middleHill;
    public GameObject rightHill;

    float speed = 0.25f;

    void Start()
    {
        //Left hiil random sprite
        switch (Random.Range(1, 3))
        {
            case 1:
                leftHiil.GetComponent<SpriteRenderer>().sprite = image_1;
                break;
            case 2:
                leftHiil.GetComponent<SpriteRenderer>().sprite = image_2;
                break;

        }

        //Middle hiil random sprite
        switch (Random.Range(1, 3))
        {
            case 1:
                middleHill.GetComponent<SpriteRenderer>().sprite = image_1;
                break;
            case 2:
                middleHill.GetComponent<SpriteRenderer>().sprite = image_2;
                break;

        }

        //Right hiil random sprite
        switch (Random.Range(1, 3))
        {
            case 1:
                rightHill.GetComponent<SpriteRenderer>().sprite = image_1;
                break;
            case 2:
                rightHill.GetComponent<SpriteRenderer>().sprite = image_2;
                break;

        }
    }


    void Update()
    {
        leftHiil.transform.position = new Vector3(leftHiil.transform.position.x + speed * Time.deltaTime, leftHiil.transform.position.y, leftHiil.transform.position.z);
        middleHill.transform.position = new Vector3(middleHill.transform.position.x + speed * Time.deltaTime, middleHill.transform.position.y, middleHill.transform.position.z);
        rightHill.transform.position = new Vector3(rightHill.transform.position.x + speed * Time.deltaTime, rightHill.transform.position.y, rightHill.transform.position.z);

        if (middleHill.transform.position.x > 10 && middleHill.transform.position.x < 11 && rightHill.transform.position.x > 10)
        {
            rightHill.transform.position = new Vector3(leftHiil.transform.position.x - 20, rightHill.transform.position.y, rightHill.transform.position.z);
            switch (Random.Range(1, 3))
            {
                case 1:
                    rightHill.GetComponent<SpriteRenderer>().sprite = image_1;
                    break;
                case 2:
                    rightHill.GetComponent<SpriteRenderer>().sprite = image_2;
                    break;

            }
        }

        if (leftHiil.transform.position.x > 10 && leftHiil.transform.position.x < 11 && middleHill.transform.position.x > 10)
        {
            middleHill.transform.position = new Vector3(rightHill.transform.position.x - 20, middleHill.transform.position.y, middleHill.transform.position.z);
            switch (Random.Range(1, 3))
            {
                case 1:
                    middleHill.GetComponent<SpriteRenderer>().sprite = image_1;
                    break;
                case 2:
                    middleHill.GetComponent<SpriteRenderer>().sprite = image_2;
                    break;

            }
        }
        if (rightHill.transform.position.x > 10 && rightHill.transform.position.x < 11 && leftHiil.transform.position.x > 10)
        {
            leftHiil.transform.position = new Vector3(middleHill.transform.position.x - 20, leftHiil.transform.position.y, leftHiil.transform.position.z);
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHiil.GetComponent<SpriteRenderer>().sprite = image_1;
                    break;
                case 2:
                    leftHiil.GetComponent<SpriteRenderer>().sprite = image_2;
                    break;

            }
        }


    }
}
