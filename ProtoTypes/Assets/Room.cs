﻿using UnityEngine;
using System.Collections;
using Assets;

public class Room : MonoBehaviour {
    public Material roomMaterial;

    private float room_X_Length;
    private float room_Y_Length;
    private float room_Z_Length;
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

    private Vector3 localScale;
    private Vector3 cubeScale;
    private GameObject[,] rightWall;
    private GameObject[,] frontWall;
    private GameObject[,] topWall;

	// Use this for initialization
	void Start () {
        this.gameObject.tag = "Room";

        InitializeVariables();
        InitializeWalls();
    }
	
	// Update is called once per frame
	void Update () {
        ExpandWalls();
        MoveWalls();
	}

    void InitializeVariables()
    {
        room_X_Length = Constants.ROOM_X_LENGTH;
        room_Y_Length = Constants.ROOM_Y_LENGTH;
        room_Z_Length = Constants.ROOM_Z_LENGTH;
        localScale = gameObject.transform.localScale;

        int cubeCount;

        cubeCount = Mathf.RoundToInt(room_X_Length / Constants.CUBE_SCALE);
        filledStartIndex_X = (Constants.ROOM_ARRAY_SIZE / 2) - (cubeCount / 2);
        filledEndIndex_X = filledStartIndex_X + cubeCount;

        cubeCount = Mathf.RoundToInt(room_Y_Length / Constants.CUBE_SCALE);
        filledStartIndex_Y = (Constants.ROOM_ARRAY_SIZE / 2) - (cubeCount / 2);
        filledEndIndex_Y = filledStartIndex_Y + cubeCount;

        cubeCount = Mathf.RoundToInt(room_Z_Length / Constants.CUBE_SCALE);
        filledStartIndex_Z = (Constants.ROOM_ARRAY_SIZE / 2) - (cubeCount / 2);
        filledEndIndex_Z = filledStartIndex_Z + cubeCount;

        currentStartIndex_X = filledStartIndex_X;
        currentEndIndex_X = filledEndIndex_X;
        currentStartIndex_Y = filledStartIndex_Y;
        currentEndIndex_Y = filledEndIndex_Y;
        currentStartIndex_Z = filledStartIndex_Z;
        currentEndIndex_Z = filledEndIndex_Z;

        cubeScale = new Vector3(Constants.CUBE_SCALE, Constants.CUBE_SCALE, Constants.CUBE_SCALE);
    }

    void InitializeWalls()
    {
        rightWall = new GameObject[Constants.ROOM_ARRAY_SIZE, Constants.ROOM_ARRAY_SIZE];
        frontWall = new GameObject[Constants.ROOM_ARRAY_SIZE, Constants.ROOM_ARRAY_SIZE];
        topWall = new GameObject[Constants.ROOM_ARRAY_SIZE, Constants.ROOM_ARRAY_SIZE];

        CreateCubes(rightWall, Constants.RIGHT_WALL, filledStartIndex_Y, filledEndIndex_Y, filledStartIndex_Z, filledEndIndex_Z);
        CreateCubes(frontWall, Constants.FRONT_WALL, filledStartIndex_X, filledEndIndex_X, filledStartIndex_Y, filledEndIndex_Y);
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
                GameObject cube = CreateCube();

                switch (wallType)
                {
                    case Constants.RIGHT_WALL:
                        {
                            int cubeCount = Mathf.RoundToInt(Constants.ROOM_X_LENGTH / Constants.CUBE_SCALE);
                            cube.transform.localPosition = new Vector3(Constants.BASE_POS_X + (cubeCount-1) * Constants.CUBE_SCALE, Constants.BASE_POS_Y + countIndex1 * Constants.CUBE_SCALE, Constants.BASE_POS_Z + countIndex2 * Constants.CUBE_SCALE);
                            break;
                        }
                    case Constants.FRONT_WALL:
                        {
                            int cubeCount = Mathf.RoundToInt(Constants.ROOM_Z_LENGTH / Constants.CUBE_SCALE);
                            cube.transform.localPosition = new Vector3(Constants.BASE_POS_X + countIndex1 * Constants.CUBE_SCALE, Constants.BASE_POS_Y + countIndex2 * Constants.CUBE_SCALE, Constants.BASE_POS_Z + (cubeCount - 1) * Constants.CUBE_SCALE);
                            break;
                        }
                    case Constants.TOP_WALL:
                        {
                            int cubeCount = Mathf.RoundToInt(Constants.ROOM_Y_LENGTH / Constants.CUBE_SCALE);
                            cube.transform.localPosition = new Vector3(Constants.BASE_POS_X + countIndex1 * Constants.CUBE_SCALE, Constants.BASE_POS_Y + (cubeCount - 1) * Constants.CUBE_SCALE, Constants.BASE_POS_Z + countIndex2 * Constants.CUBE_SCALE);
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

    void SetRoomSize(string moveType)
    {
        switch(moveType)
        {
            case Constants.RIGHT:
                room_X_Length += Constants.ROOM_X_LENGTH / 2;
                currentEndIndex_X += Mathf.RoundToInt((Constants.ROOM_X_LENGTH / 2) / Constants.CUBE_SCALE);
                break;
            case Constants.FORWARD:
                room_Z_Length += Constants.ROOM_Z_LENGTH / 2;
                currentEndIndex_Z += Mathf.RoundToInt((Constants.ROOM_Z_LENGTH / 2) / Constants.CUBE_SCALE);
                break;
            case Constants.UP:
                room_Y_Length += Constants.ROOM_Y_LENGTH / 2;
                currentEndIndex_Y += Mathf.RoundToInt((Constants.ROOM_Y_LENGTH / 2) / Constants.CUBE_SCALE);
                break;
            default:
                break;
        }
    }

    void MoveWalls()
    {
        if (currentEndIndex_X > filledEndIndex_X)
        {
            MoveRightWall();
        }
        else if (currentEndIndex_Z > filledEndIndex_Z)
        {
            MoveFrontWall();
        }
        else if (currentEndIndex_Y > filledEndIndex_Y)
        {
            MoveTopWall();
        }
    }

    void MoveRightWall()
    {
        for (int i = filledStartIndex_Y; i < filledEndIndex_Y; i++)
        {
            for (int j = filledStartIndex_Z; j < filledEndIndex_Z; j++)
            {
                GameObject cube = rightWall[i, j];
                cube.transform.localPosition += Constants.PACE_SPEED * Vector3.right / 2;
            }
        }

        filledEndIndex_X += Mathf.RoundToInt((Constants.ROOM_X_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);
    }

    void MoveFrontWall()
    {
        for (int i = filledStartIndex_X; i < filledEndIndex_X; i++)
        {
            for (int j = filledStartIndex_Y; j < filledEndIndex_Y; j++)
            {
                GameObject cube = frontWall[i, j];
                cube.transform.localPosition += Constants.PACE_SPEED * Vector3.forward / 2;
            }
        }

        filledEndIndex_Z += Mathf.RoundToInt((Constants.ROOM_Z_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);
    }

    void MoveTopWall()
    {
        for (int i = filledStartIndex_X; i < filledEndIndex_X; i++)
        {
            for (int j = filledStartIndex_Z; j < filledEndIndex_Z; j++)
            {
                GameObject cube = topWall[i, j];
                cube.transform.localPosition += Constants.PACE_SPEED * Vector3.up / 2;
            }
        }

        filledEndIndex_Y += Mathf.RoundToInt((Constants.ROOM_Y_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);
    }

    void ExpandWalls()
    {
        if (currentEndIndex_X > filledEndIndex_X)
        {
            ExpandFrontWall(Constants.EXPAND_DIRECTION_POSITIVE_X);
            ExpandTopWall(Constants.EXPAND_DIRECTION_POSITIVE_X);
        }

        if (currentEndIndex_Y > filledEndIndex_Y)
        {
            ExpandFrontWall(Constants.EXPAND_DIRECTION_POSITIVE_Y);
            ExpandRightWall(Constants.EXPAND_DIRECTION_POSITIVE_Y);
        }

        if (currentEndIndex_Z > filledEndIndex_Z)
        {
            ExpandRightWall(Constants.EXPAND_DIRECTION_POSITIVE_Z);
            ExpandTopWall(Constants.EXPAND_DIRECTION_POSITIVE_Z);
        }
    }

    void ExpandRightWall(string expandMode)
    {
        if (Constants.EXPAND_DIRECTION_POSITIVE_Y.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_Y_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledEndIndex_Y; i < filledEndIndex_Y + numOfCubesToCreate; i++)
            {
                for (int j = filledStartIndex_Z; j < filledEndIndex_Z; j++)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = rightWall[i - 1, j];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.up * Constants.CUBE_SCALE;
                    rightWall[i, j] = cube;
                }
            }
        }
        else if (Constants.EXPAND_DIRECTION_POSITIVE_Z.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_Z_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledStartIndex_Y; i < filledEndIndex_Y; i++)
            {
                for (int j = filledEndIndex_Z; j < filledEndIndex_Z + numOfCubesToCreate; j++)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = rightWall[i, j - 1];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.forward * Constants.CUBE_SCALE;
                    rightWall[i, j] = cube;
                }
            }
        }
    }

    void ExpandFrontWall(string expandMode)
    {
        if (Constants.EXPAND_DIRECTION_POSITIVE_X.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_X_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledEndIndex_X; i < filledEndIndex_X + numOfCubesToCreate; i++)
            {
                for (int j = filledStartIndex_Y; j < filledEndIndex_Y; j++)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = frontWall[i - 1, j];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.right * Constants.CUBE_SCALE;
                    frontWall[i, j] = cube;
                }
            }
        }
        else if (Constants.EXPAND_DIRECTION_POSITIVE_Y.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_Y_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledStartIndex_X; i < filledEndIndex_X; i++)
            {
                for (int j = filledEndIndex_Y; j < filledEndIndex_Y + numOfCubesToCreate; j++)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = frontWall[i, j - 1];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.up * Constants.CUBE_SCALE;
                    frontWall[i, j] = cube;
                }
            }
        }
    }

    void ExpandTopWall(string expandMode)
    {
        if (Constants.EXPAND_DIRECTION_POSITIVE_X.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_X_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledEndIndex_X; i < filledEndIndex_X + numOfCubesToCreate; i++)
            {
                for (int j = filledStartIndex_Z; j < filledEndIndex_Z; j++)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = topWall[i - 1, j];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.right * Constants.CUBE_SCALE;
                    topWall[i, j] = cube;
                }
            }
        }
        else if (Constants.EXPAND_DIRECTION_POSITIVE_Z.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_Z_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledStartIndex_X; i < filledEndIndex_X; i++)
            {
                for (int j = filledEndIndex_Z; j < filledEndIndex_Z + numOfCubesToCreate; j++)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = topWall[i, j - 1];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.forward * Constants.CUBE_SCALE;
                    topWall[i, j] = cube;
                }
            }
        }
    }

    GameObject CreateCube()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().material = roomMaterial;
        cube.transform.SetParent(gameObject.transform);
        cube.transform.localScale = cubeScale;

        return cube;
    }

    public Vector3 GetRoomSize()
    {
        return new Vector3(room_X_Length, room_Y_Length, room_Z_Length);
    }

    public void ExpandRoom(string moveType)
    {
        SetRoomSize(moveType);
    }
}
