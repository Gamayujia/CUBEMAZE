using UnityEngine;
using System.Collections;
using Assets;

public class PersonVR : MonoBehaviour
{
    GameObject person;
    GameObject room;
    Room roomScript;
    Vector3 roomSize;
    Vector3 localScale;
    Vector3 personStartPos, personCurrentPos;
    public float range;
    int expansionNum;

    [FMODUnity.EventRef]
    public string music;
    FMOD.Studio.EventInstance musicInstance;

    [FMODUnity.EventRef]
    public string wallMove;

    // Use this for initialization
    void Start()
    {
        gameObject.tag = "Person";
        person = gameObject;
        personStartPos = person.transform.position;
        room = GameObject.FindGameObjectWithTag("Room");
        roomScript = (Room) room.GetComponent(typeof(Room));
        roomSize = roomScript.GetRoomSize();
        localScale = person.transform.localScale;

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(music);
        musicInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        personCurrentPos = person.transform.position;
        string moveType = Movement();
        string reshapeType = Interaction();
        ExpandRoom(moveType);
        ReshapeRoom(reshapeType);

        musicInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        musicInstance.setParameterValue("ExpansionS",expansionNum);
    }

    string Movement()
    {
        if (personCurrentPos.x < personStartPos.x - range)
        {
            personStartPos = person.transform.position;
            expansionNum++;
            FMODUnity.RuntimeManager.PlayOneShot(wallMove,transform.position);
            return Constants.LEFT;
        }
        else if (personCurrentPos.x > personStartPos.x + range)
        {
            personStartPos = person.transform.position;
            expansionNum++;
            FMODUnity.RuntimeManager.PlayOneShot(wallMove, transform.position);
            return Constants.RIGHT;
            
        }
        else if (personCurrentPos.z > personStartPos.z + range)
        {
            personStartPos = person.transform.position;
            expansionNum++;
            FMODUnity.RuntimeManager.PlayOneShot(wallMove, transform.position);
            return Constants.FORWARD;
        }
        else if (personCurrentPos.z < personStartPos.z - range)
        {
            personStartPos = person.transform.position;
            expansionNum++;
            FMODUnity.RuntimeManager.PlayOneShot(wallMove, transform.position);
            return Constants.BACKWARD;
            
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

    string Interaction()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            return Constants.PERLIN_NOISE;
        }

        return "";
    }

    void ExpandRoom(string moveType)
    {
        roomScript.ExpandRoom(moveType);
    }

    void ReshapeRoom(string reshapeType)
    {
        switch (reshapeType)
        {
            case Constants.PERLIN_NOISE:
                roomScript.PerlinNoise();
                break;
            default:
                break;
        }
    }
}

