using UnityEngine;
using System.Collections;
using Assets;

public class Person : MonoBehaviour
{
    Transform cubeTrans;
    Vector3 startPos, currentPos, targetPos;
    GameObject person;

    // Use this for initialization
    void Start()
    {
        cubeTrans = gameObject.GetComponent<Transform>();
        startPos = cubeTrans.position;
        currentPos = cubeTrans.position;
        targetPos = cubeTrans.position;
        person = this.gameObject;
        person.tag = "Person";
    }

    // Update is called once per frame
    void Update()
    {
        string moveType = Movement();
        UpdateMovement();
        ExpandRoom(moveType);
        print("Start: " + startPos);
        print("Current: " + currentPos);
        print("Target: " + targetPos);
    }

    string Movement()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetPos.x -= 1;
            return Constants.LEFT;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cubeTrans.Translate(Vector3.right / 2);
            return Constants.RIGHT;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cubeTrans.Translate(Vector3.forward / 2);
            return Constants.FORWARD;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cubeTrans.Translate(Vector3.back / 2);
            return Constants.BACKWARD;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            cubeTrans.Translate(Vector3.up / 2);
            return Constants.UP;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            cubeTrans.Translate(Vector3.down / 2);
            return Constants.DOWN;
        }

        return "";
    }

    void UpdateMovement()
    {
        if (!Mathf.Approximately(targetPos.x, currentPos.x))
        {
            if (targetPos.x.CompareTo(currentPos.x) > 0)
            {
                currentPos.x += Constants.PACE_SPEED;
            }
            else if (targetPos.x.CompareTo(currentPos.x) < 0)
            {
                currentPos.x -= Constants.PACE_SPEED;
            }
        }

        //cubeTrans.transform.position = currentPos;
    }

    void ExpandRoom(string moveType)
    {
        /*GameObject room = GameObject.FindGameObjectWithTag("Room");
        Vector3 expandVector = room.transform.localScale;
        Vector3 positionVector = room.transform.position;

        switch (moveType)
        {
            case Constants.Left:
                expandVector.x += 10f;
                positionVector.x -= 5f;
                break;
            case Constants.Right:
                expandVector.x += 10f;
                positionVector.x += 5f;
                break;
            case Constants.Forward:
                expandVector.z += 10f;
                positionVector.z += 5f;
                break;
            case Constants.Backward:
                expandVector.z += 10f;
                positionVector.z -= 5f;
                break;
            case Constants.Up:
                expandVector.y += 10f;
                positionVector.y += 5f;
                break;
            case Constants.Down:
                expandVector.y += 10f;
                positionVector.y -= 5f;
                break;
            default:
                break;
        }

        room.transform.localScale = expandVector;
        room.transform.position = positionVector;*/
    }
}

