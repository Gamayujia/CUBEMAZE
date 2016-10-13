using UnityEngine;
using System.Collections;
using Assets;

public class Room : MonoBehaviour {
    public Material roomMaterial;

    private float currentX;
    private float currentY;
    private float currentZ;
    private int filledStartIndex_X;
    private int filledEndIndex_X;
    private int filledStartIndex_Y;
    private int filledEndIndex_Y;
    private int filledStartIndex_Z;
    private int filledEndIndex_Z;
    private int currentStartIndex_X;
    private int currentEndIndex_X;
    private int currentStartIndex_Y;
    private int currentEndIndex_Y;
    private int currentStartIndex_Z;
    private int currentEndIndex_Z;

    private Vector3 cubeScale;
    private GameObject[,] leftWall;
    private GameObject[,] backWall;
    private GameObject[,] topWall;

	// Use this for initialization
	void Start () {
        this.gameObject.tag = "Room";

        InitializeVariables();
        InitializeWalls();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitializeVariables()
    {
        int cubeCount;

        cubeCount = (int) (Constants.ROOM_X_LENGTH / Constants.CUBE_SCALE);
        filledStartIndex_X = (Constants.ROOM_ARRAY_SIZE / 2) - (cubeCount / 2);
        filledEndIndex_X = filledStartIndex_X + cubeCount;

        cubeCount = (int)(Constants.ROOM_Y_LENGTH / Constants.CUBE_SCALE);
        filledStartIndex_Y = (Constants.ROOM_ARRAY_SIZE / 2) - (cubeCount / 2);
        filledEndIndex_Y = filledStartIndex_Y + cubeCount;

        cubeCount = (int)(Constants.ROOM_Z_LENGTH / Constants.CUBE_SCALE);
        filledStartIndex_Z = (Constants.ROOM_ARRAY_SIZE / 2) - (cubeCount / 2);
        filledEndIndex_Z = filledStartIndex_Z + cubeCount;

        currentStartIndex_X = filledStartIndex_X;
        currentEndIndex_X = filledEndIndex_X;
        currentStartIndex_Y = filledStartIndex_Y;
        currentEndIndex_Y = filledEndIndex_Y;
        currentStartIndex_Z = filledEndIndex_Z;
        currentEndIndex_Z = filledEndIndex_Z;

        cubeScale = new Vector3(Constants.CUBE_SCALE, Constants.CUBE_SCALE, Constants.CUBE_SCALE);
    }

    void InitializeWalls()
    {
        leftWall = new GameObject[Constants.ROOM_ARRAY_SIZE, Constants.ROOM_ARRAY_SIZE];
        backWall = new GameObject[Constants.ROOM_ARRAY_SIZE, Constants.ROOM_ARRAY_SIZE];
        topWall = new GameObject[Constants.ROOM_ARRAY_SIZE, Constants.ROOM_ARRAY_SIZE];

        CreateCubes(leftWall, Constants.LEFT_WALL, filledStartIndex_Y, filledEndIndex_Y, filledStartIndex_Z, filledEndIndex_Z);
        CreateCubes(backWall, Constants.BACK_WALL, filledStartIndex_X, filledEndIndex_X, filledStartIndex_Y, filledEndIndex_Y);
        CreateCubes(topWall, Constants.TOP_WALL, filledStartIndex_X, filledEndIndex_X, filledStartIndex_Z, filledEndIndex_Z);
    }

    void CreateCubes(GameObject[,] wall, string wallType, int startIndex1, int endIndex1, int startIndex2, int endIndex2)
    {
        int countIndex1 = 0;
        int countIndex2 = 0;

        for (int i = startIndex1; i < endIndex1; i++)
        {
            for (int j = startIndex2; j < endIndex2; j++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.GetComponent<Renderer>().material = roomMaterial;
                cube.transform.SetParent(this.gameObject.transform);
                cube.transform.localScale = cubeScale;

                switch (wallType)
                {
                    case Constants.LEFT_WALL:
                        {
                            cube.transform.localPosition = new Vector3(Constants.BASE_POS_X, Constants.BASE_POS_Y + countIndex1 * Constants.CUBE_SCALE, Constants.BASE_POS_Z - countIndex2 * Constants.CUBE_SCALE);
                            break;
                        }
                    case Constants.BACK_WALL:
                        {
                            cube.transform.localPosition = new Vector3(Constants.BASE_POS_X - countIndex2 * Constants.CUBE_SCALE, Constants.BASE_POS_Y + countIndex1 * Constants.CUBE_SCALE, Constants.BASE_POS_Z);
                            break;
                        }
                    case Constants.TOP_WALL:
                        {
                            int cubeCount = (int)(Constants.ROOM_Y_LENGTH / Constants.CUBE_SCALE);
                            cube.transform.localPosition = new Vector3(Constants.BASE_POS_X - countIndex2 * Constants.CUBE_SCALE, Constants.BASE_POS_Y + cubeCount * Constants.CUBE_SCALE, Constants.BASE_POS_Z - countIndex1 * Constants.CUBE_SCALE);
                            break;
                        }
                    default:
                        break;
                }

                wall[i,j] = cube;
                countIndex2++;
            }

            countIndex2 = 0;
            countIndex1++;
        }
    }

    public Vector3 GetRoomSize()
    {
        return new Vector3(Constants.ROOM_X_LENGTH, Constants.ROOM_Y_LENGTH, Constants.ROOM_Z_LENGTH);
    }
}
