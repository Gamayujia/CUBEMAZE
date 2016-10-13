using UnityEngine;
using System.Collections;
using Assets;

public class SmallCube : MonoBehaviour {
    Transform cubeTrans;
    Vector3 startPos, currentPos;
    GameObject smallCube;

    // Use this for initialization
    void Start()
    {
        cubeTrans = gameObject.GetComponent<Transform>();
        startPos = cubeTrans.position;
        smallCube = this.gameObject;
        smallCube.tag = "SmallCube";
    }

    // Update is called once per frame
    void Update()
    {
        string moveType = Movement();
        ExpandRoom(moveType);
        currentPos = cubeTrans.position;
        print(startPos);
        print(currentPos);
    }

    string Movement()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cubeTrans.Translate(Vector3.left);
            return Constants.LEFT;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cubeTrans.Translate(Vector3.right);
            return Constants.RIGHT;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cubeTrans.Translate(Vector3.forward);
            return Constants.FORWARD;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cubeTrans.Translate(Vector3.back);
            return Constants.BACKWARD;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cubeTrans.Translate(Vector3.up);
            return Constants.UP;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            cubeTrans.Translate(Vector3.down);
            return Constants.DOWN;
        }

        return "";
    }

    void ExpandRoom(string moveType)
    {
        GameObject room = GameObject.FindGameObjectWithTag("LargeCube");
        Vector3 expandVector = room.transform.localScale;
        Vector3 positionVector = room.transform.localPosition;

        switch (moveType)
        {
            case Constants.LEFT:
                expandVector.x += 1f;
                positionVector.x -= .5f;
                break;
            case Constants.RIGHT:
                expandVector.x += 1f;
                positionVector.x += .5f;
                break;
            case Constants.FORWARD:
                expandVector.z += 1f;
                positionVector.z += .5f;
                break;
            case Constants.BACKWARD:
                expandVector.z += 1f;
                positionVector.z -= .5f;
                break;
            case Constants.UP:
                expandVector.y += 1f;
                positionVector.y += .5f;
                break;
            case Constants.DOWN:
                expandVector.y += 1f;
                positionVector.y -= .5f;
                break;
            default:
                break;
        }

        room.transform.localScale = expandVector;
        room.transform.localPosition = positionVector;
    }
}
