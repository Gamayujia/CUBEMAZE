using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

    Transform cube;
    Vector3 startPos, currentPos, randomPos;
    public float speed = 1;
    public bool random;
    int blueNum, yellowNum, greenNum, purpleNum;

   GameObject bluePlane, yellowPlane, greenPlane, purplePlane;

    public GameObject[] blue, yellow, green, purple;

	// Use this for initialization
	void Start () {

        cube = GetComponent<Transform>();

        for (int i = 0; i < blue.Length; i++) {
            bluePlane = blue[i];
            bluePlane.SetActive(false);
        }
        for (int i = 0; i < yellow.Length; i++)
        {
            yellowPlane = yellow[i];
            yellowPlane.SetActive(false);
        }
        for (int i = 0; i < purple.Length; i++)
        {
            purplePlane = purple[i];
            purplePlane.SetActive(false);
        }
        for (int i = 0; i < green.Length; i++)
        {
            greenPlane = green[i];
            greenPlane.SetActive(false);
        }


        blueNum = 0;
        bluePlane = blue[blueNum];
        yellowNum = 0;
        yellowPlane = yellow[yellowNum];
        greenNum = 0;
        greenPlane = green[greenNum];
        purpleNum = 0;
        purplePlane = purple[purpleNum];


        if (random) {
            randomPos = new Vector3(Random.Range(-2, 2), 0.442f, Random.Range(-2, 2));
            cube.position = randomPos;
        }


        startPos = cube.position;


    }
	
	// Update is called once per frame
	void Update () {

        currentPos = cube.position;
        print("Start = " + startPos);
        print("Current = " + currentPos);


        MainFunction();
        Movement();

    }

    void MainFunction() {

        if (currentPos.x >= startPos.x + 1) {
            bluePlane.SetActive(true);
            blueNum++;
            if (blueNum < blue.Length)
            {
                bluePlane = blue[blueNum];
            }
            startPos = currentPos;
        }

        if (currentPos.x <= startPos.x - 1)
        {
                purplePlane.SetActive(true);
                purpleNum++;
            if (purpleNum < purple.Length)
            {
                purplePlane = purple[purpleNum];
            }
                startPos = currentPos;
        }

        if (currentPos.z >= startPos.z + 1)
        {
                yellowPlane.SetActive(true);
                yellowNum++;
            if (yellowNum < yellow.Length)
            {
                yellowPlane = yellow[yellowNum];
            }
                startPos = currentPos;
        }

        if (currentPos.z <= startPos.z - 1)
        {
                greenPlane.SetActive(true);
                greenNum++;
            if (greenNum < green.Length)
            {
                greenPlane = green[greenNum];
            }
                startPos = currentPos;
        }

    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * speed;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * speed;
        }
    }
}
