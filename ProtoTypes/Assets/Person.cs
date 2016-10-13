using UnityEngine;
using System.Collections;
using Assets;

public class Person : MonoBehaviour
{
    GameObject person;
    GameObject room;
    Room roomScript;
    Vector3 roomSize;
    Vector3 localScale;

    // Use this for initialization
    void Start()
    {
        gameObject.tag = "Person";
        person = gameObject;
        room = GameObject.FindGameObjectWithTag("Room");
        roomScript = (Room) room.GetComponent(typeof(Room));
        roomSize = roomScript.GetRoomSize();
        localScale = person.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        string moveType = Movement();
        ExpandRoom(moveType);
    }

    string Movement()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!Mathf.Approximately(transform.localPosition.x, -roomSize.x / 2) && transform.localPosition.x > -roomSize.x / 2 + localScale.x / 2)
            {
                transform.localPosition += localScale.x * Vector3.left / 2;
                return Constants.LEFT;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!Mathf.Approximately(transform.localPosition.x, roomSize.x / 2) && transform.localPosition.x < roomSize.x / 2 - localScale.x / 2)
            {
                transform.localPosition += localScale.x * Vector3.right / 2;
                return Constants.RIGHT;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!Mathf.Approximately(transform.localPosition.z, roomSize.z / 2) && transform.localPosition.z < roomSize.z / 2 - localScale.z / 2)
            {
                transform.localPosition += localScale.z * Vector3.forward / 2;
                return Constants.FORWARD;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!Mathf.Approximately(transform.localPosition.z, roomSize.z / 2) && transform.localPosition.z > -roomSize.z / 2 + localScale.z / 2)
            {
                transform.localPosition += localScale.z * Vector3.back / 2;
                return Constants.BACKWARD;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Mathf.Approximately(transform.localPosition.y, roomSize.y) && transform.localPosition.y < roomSize.y - localScale.y / 2)
            {
                transform.localPosition += localScale.y * Vector3.up / 2;
                return Constants.UP;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!Mathf.Approximately(transform.localPosition.y, 0.05f) && transform.localPosition.y > localScale.y / 2)
            {
                transform.localPosition += localScale.y * Vector3.down / 2;
                return Constants.DOWN;
            }
        }

        return "";
    }

    void ExpandRoom(string moveType)
    {
        GameObject room = GameObject.FindGameObjectWithTag("Room");
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
        room.transform.position = positionVector;
    }
}

