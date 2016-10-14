using UnityEngine;
using System.Collections;
using Assets;

public class Room : MonoBehaviour {
    public Material roomMaterial;

    private float top_Y;
    private float bottom_Y;
    private float left_X;
    private float right_X;
    private float front_Z;
    private float back_Z;
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
    private int cubeCount_X;
    private int cubeCount_Y;
    private int cubeCount_Z;

    private Vector3 localScale;
    private Vector3 cubeScale;
    private GameObject[,] leftWall;
    private GameObject[,] rightWall;
    private GameObject[,] frontWall;
    private GameObject[,] backWall;
    private GameObject[,] topWall;

    private bool perlinNoiseActivated = false;
    private bool perlinNoiseRestored = true;

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
        DoNoise();
	}

    void InitializeVariables()
    {
        room_X_Length = Constants.ROOM_X_LENGTH;
        room_Y_Length = Constants.ROOM_Y_LENGTH;
        room_Z_Length = Constants.ROOM_Z_LENGTH;
        localScale = gameObject.transform.localScale;

        cubeCount_X = Mathf.RoundToInt(room_X_Length / Constants.CUBE_SCALE);
        cubeCount_Y = Mathf.RoundToInt(room_Y_Length / Constants.CUBE_SCALE);
        cubeCount_Z = Mathf.RoundToInt(room_Z_Length / Constants.CUBE_SCALE);

        left_X = Constants.BASE_POS_X;
        right_X = Constants.BASE_POS_X + (cubeCount_X - 1) * Constants.CUBE_SCALE;
        front_Z = Constants.BASE_POS_Z + (cubeCount_Z - 1) * Constants.CUBE_SCALE;
        back_Z = Constants.BASE_POS_Z;
        bottom_Y = Constants.BASE_POS_Y;
        top_Y = Constants.BASE_POS_Y + (cubeCount_Y - 1) * Constants.CUBE_SCALE;

        filledStartIndex_X = (Constants.ROOM_ARRAY_SIZE / 2) - (cubeCount_X / 2);
        filledEndIndex_X = filledStartIndex_X + cubeCount_X;
        filledStartIndex_Y = (Constants.ROOM_ARRAY_SIZE / 2) - (cubeCount_Y / 2);
        filledEndIndex_Y = filledStartIndex_Y + cubeCount_Y;
        filledStartIndex_Z = (Constants.ROOM_ARRAY_SIZE / 2) - (cubeCount_Z / 2);
        filledEndIndex_Z = filledStartIndex_Z + cubeCount_Z;

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
        leftWall = new GameObject[Constants.ROOM_ARRAY_SIZE, Constants.ROOM_ARRAY_SIZE];
        rightWall = new GameObject[Constants.ROOM_ARRAY_SIZE, Constants.ROOM_ARRAY_SIZE];
        frontWall = new GameObject[Constants.ROOM_ARRAY_SIZE, Constants.ROOM_ARRAY_SIZE];
        backWall = new GameObject[Constants.ROOM_ARRAY_SIZE, Constants.ROOM_ARRAY_SIZE];
        topWall = new GameObject[Constants.ROOM_ARRAY_SIZE, Constants.ROOM_ARRAY_SIZE];

        CreateCubes(leftWall, Constants.LEFT_WALL, filledStartIndex_Y, filledEndIndex_Y, filledStartIndex_Z, filledEndIndex_Z);
        CreateCubes(rightWall, Constants.RIGHT_WALL, filledStartIndex_Y, filledEndIndex_Y, filledStartIndex_Z, filledEndIndex_Z);
        CreateCubes(frontWall, Constants.FRONT_WALL, filledStartIndex_X, filledEndIndex_X, filledStartIndex_Y, filledEndIndex_Y);
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
                GameObject cube = CreateCube();

                switch (wallType)
                {
                    case Constants.LEFT_WALL:
                        {
                            cube.transform.localPosition = new Vector3(left_X, Constants.BASE_POS_Y + countIndex1 * Constants.CUBE_SCALE, Constants.BASE_POS_Z + countIndex2 * Constants.CUBE_SCALE);
                            break;
                        }
                    case Constants.RIGHT_WALL:
                        {
                            cube.transform.localPosition = new Vector3(right_X, Constants.BASE_POS_Y + countIndex1 * Constants.CUBE_SCALE, Constants.BASE_POS_Z + countIndex2 * Constants.CUBE_SCALE);
                            break;
                        }
                    case Constants.FRONT_WALL:
                        {
                            cube.transform.localPosition = new Vector3(Constants.BASE_POS_X + countIndex1 * Constants.CUBE_SCALE, Constants.BASE_POS_Y + countIndex2 * Constants.CUBE_SCALE, front_Z);
                            break;
                        }
                    case Constants.BACK_WALL:
                        {
                            cube.transform.localPosition = new Vector3(Constants.BASE_POS_X + countIndex1 * Constants.CUBE_SCALE, Constants.BASE_POS_Y + countIndex2 * Constants.CUBE_SCALE, back_Z);
                            break;
                        }
                    case Constants.TOP_WALL:
                        {
                            cube.transform.localPosition = new Vector3(Constants.BASE_POS_X + countIndex1 * Constants.CUBE_SCALE, top_Y, Constants.BASE_POS_Z + countIndex2 * Constants.CUBE_SCALE);
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
            case Constants.LEFT:
                room_X_Length += Constants.ROOM_X_LENGTH / 2;
                currentStartIndex_X -= Mathf.RoundToInt((Constants.ROOM_X_LENGTH / 2) / Constants.CUBE_SCALE);
                break;
            case Constants.RIGHT:
                room_X_Length += Constants.ROOM_X_LENGTH / 2;
                currentEndIndex_X += Mathf.RoundToInt((Constants.ROOM_X_LENGTH / 2) / Constants.CUBE_SCALE);
                break;
            case Constants.FORWARD:
                room_Z_Length += Constants.ROOM_Z_LENGTH / 2;
                currentEndIndex_Z += Mathf.RoundToInt((Constants.ROOM_Z_LENGTH / 2) / Constants.CUBE_SCALE);
                break;
            case Constants.BACKWARD:
                room_Z_Length += Constants.ROOM_Z_LENGTH / 2;
                currentStartIndex_Z -= Mathf.RoundToInt((Constants.ROOM_Z_LENGTH / 2) / Constants.CUBE_SCALE);
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
        else if (currentStartIndex_X < filledStartIndex_X)
        {
            MoveLeftWall();
        }
        else if (currentEndIndex_Z > filledEndIndex_Z)
        {
            MoveFrontWall();
        }
        else if (currentStartIndex_Z < filledStartIndex_Z)
        {
            MoveBackWall();
        }
        else if (currentEndIndex_Y > filledEndIndex_Y)
        {
            MoveTopWall();
        }
    }

    void MoveLeftWall()
    {
        for (int i = filledStartIndex_Y; i < filledEndIndex_Y; i++)
        {
            for (int j = filledStartIndex_Z; j < filledEndIndex_Z; j++)
            {
                GameObject cube = leftWall[i, j];
                cube.transform.localPosition += Constants.PACE_SPEED * Vector3.left / 2;
            }
        }

        left_X += Constants.PACE_SPEED * -0.5f;

        filledStartIndex_X -= Mathf.RoundToInt((Constants.ROOM_X_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);
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

        right_X += Constants.PACE_SPEED * 0.5f;

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

        front_Z += Constants.PACE_SPEED * 0.5f;

        filledEndIndex_Z += Mathf.RoundToInt((Constants.ROOM_Z_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);
    }

    void MoveBackWall()
    {
        for (int i = filledStartIndex_X; i < filledEndIndex_X; i++)
        {
            for (int j = filledStartIndex_Y; j < filledEndIndex_Y; j++)
            {
                GameObject cube = backWall[i, j];
                cube.transform.localPosition += Constants.PACE_SPEED * Vector3.back / 2;
            }
        }

        back_Z += Constants.PACE_SPEED * -0.5f;

        filledStartIndex_Z -= Mathf.RoundToInt((Constants.ROOM_Z_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);
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

        top_Y += Constants.PACE_SPEED * 0.5f;

        filledEndIndex_Y += Mathf.RoundToInt((Constants.ROOM_Y_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);
    }

    void ExpandWalls()
    {
        if (currentEndIndex_X > filledEndIndex_X)
        {
            ExpandFrontOrBackWall(Constants.DIRECTION_POSITIVE_X, frontWall);
            ExpandFrontOrBackWall(Constants.DIRECTION_POSITIVE_X, backWall);
            ExpandTopWall(Constants.DIRECTION_POSITIVE_X);
        }
        else if (currentStartIndex_X < filledStartIndex_X)
        {
            ExpandFrontOrBackWall(Constants.DIRECTION_NEGATIVE_X, frontWall);
            ExpandFrontOrBackWall(Constants.DIRECTION_NEGATIVE_X, backWall);
            ExpandTopWall(Constants.DIRECTION_NEGATIVE_X);
        }
        else if (currentEndIndex_Y > filledEndIndex_Y)
        {
            ExpandLeftOrRightWall(Constants.DIRECTION_POSITIVE_Y, leftWall);
            ExpandLeftOrRightWall(Constants.DIRECTION_POSITIVE_Y, rightWall);
            ExpandFrontOrBackWall(Constants.DIRECTION_POSITIVE_Y, frontWall);
            ExpandFrontOrBackWall(Constants.DIRECTION_POSITIVE_Y, backWall);
        }
        else if (currentEndIndex_Z > filledEndIndex_Z)
        {
            ExpandLeftOrRightWall(Constants.DIRECTION_POSITIVE_Z, leftWall);
            ExpandLeftOrRightWall(Constants.DIRECTION_POSITIVE_Z, rightWall);
            ExpandTopWall(Constants.DIRECTION_POSITIVE_Z);
        }
        else if (currentStartIndex_Z < filledStartIndex_Z)
        {
            ExpandLeftOrRightWall(Constants.DIRECTION_NEGATIVE_Z, leftWall);
            ExpandLeftOrRightWall(Constants.DIRECTION_NEGATIVE_Z, rightWall);
            ExpandTopWall(Constants.DIRECTION_NEGATIVE_Z);
        }
    }

    void ExpandLeftOrRightWall(string expandMode, GameObject[,] wall)
    {
        if (Constants.DIRECTION_POSITIVE_Y.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_Y_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledEndIndex_Y; i < filledEndIndex_Y + numOfCubesToCreate; i++)
            {
                for (int j = filledStartIndex_Z; j < filledEndIndex_Z; j++)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = wall[i - 1, j];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.up * Constants.CUBE_SCALE;
                    wall[i, j] = cube;
                }
            }
        }
        else if (Constants.DIRECTION_POSITIVE_Z.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_Z_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledStartIndex_Y; i < filledEndIndex_Y; i++)
            {
                for (int j = filledEndIndex_Z; j < filledEndIndex_Z + numOfCubesToCreate; j++)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = wall[i, j - 1];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.forward * Constants.CUBE_SCALE;
                    wall[i, j] = cube;
                }
            }
        }
        else if (Constants.DIRECTION_NEGATIVE_Z.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_Z_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledStartIndex_Y; i < filledEndIndex_Y; i++)
            {
                for (int j = filledStartIndex_Z - 1; j > filledStartIndex_Z - numOfCubesToCreate - 1; j--)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = wall[i, j + 1];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.back * Constants.CUBE_SCALE;
                    wall[i, j] = cube;
                }
            }
        }
    }

    void ExpandFrontOrBackWall(string expandMode, GameObject[,] wall)
    {
        if (Constants.DIRECTION_POSITIVE_X.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_X_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledEndIndex_X; i < filledEndIndex_X + numOfCubesToCreate; i++)
            {
                for (int j = filledStartIndex_Y; j < filledEndIndex_Y; j++)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = wall[i - 1, j];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.right * Constants.CUBE_SCALE;
                    wall[i, j] = cube;
                }
            }
        }
        else if (Constants.DIRECTION_NEGATIVE_X.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_X_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledStartIndex_X - 1; i > filledStartIndex_X - numOfCubesToCreate - 1; i--)
            {
                for (int j = filledStartIndex_Y; j < filledEndIndex_Y; j++)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = wall[i + 1, j];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.left * Constants.CUBE_SCALE;
                    wall[i, j] = cube;
                }
            }
        }
        else if (Constants.DIRECTION_POSITIVE_Y.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_Y_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledStartIndex_X; i < filledEndIndex_X; i++)
            {
                for (int j = filledEndIndex_Y; j < filledEndIndex_Y + numOfCubesToCreate; j++)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = wall[i, j - 1];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.up * Constants.CUBE_SCALE;
                    wall[i, j] = cube;
                }
            }
        }
    }

    void ExpandTopWall(string expandMode)
    {
        if (Constants.DIRECTION_POSITIVE_X.Equals(expandMode))
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
        else if (Constants.DIRECTION_NEGATIVE_X.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_X_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledStartIndex_X - 1; i > filledStartIndex_X - numOfCubesToCreate - 1; i--)
            {
                for (int j = filledStartIndex_Z; j < filledEndIndex_Z; j++)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = topWall[i + 1, j];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.left * Constants.CUBE_SCALE;
                    topWall[i, j] = cube;
                }
            }
        }
        else if (Constants.DIRECTION_POSITIVE_Z.Equals(expandMode))
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
        else if (Constants.DIRECTION_NEGATIVE_Z.Equals(expandMode))
        {
            int numOfCubesToCreate = Mathf.RoundToInt((Constants.ROOM_Z_LENGTH / 2) / Constants.CUBE_SCALE * Constants.PACE_SPEED);

            for (int i = filledStartIndex_X; i < filledEndIndex_X; i++)
            {
                for (int j = filledStartIndex_Z - 1; j > filledStartIndex_Z - numOfCubesToCreate - 1; j--)
                {
                    GameObject cube = CreateCube();
                    GameObject prevCube = topWall[i, j + 1];
                    cube.transform.localPosition = prevCube.transform.localPosition + Vector3.back * Constants.CUBE_SCALE;
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

    void RestoreWalls()
    {
        RestoreWall(leftWall, filledStartIndex_Y, filledEndIndex_Y, filledStartIndex_Z, filledEndIndex_Z, left_X, Constants.LEFT_WALL);
        RestoreWall(rightWall, filledStartIndex_Y, filledEndIndex_Y, filledStartIndex_Z, filledEndIndex_Z, right_X, Constants.RIGHT_WALL);
        RestoreWall(frontWall, filledStartIndex_X, filledEndIndex_X, filledStartIndex_Y, filledEndIndex_Y, front_Z, Constants.FRONT_WALL);
        RestoreWall(backWall, filledStartIndex_X, filledEndIndex_X, filledStartIndex_Y, filledEndIndex_Y, back_Z, Constants.BACK_WALL);
        RestoreWall(topWall, filledStartIndex_X, filledEndIndex_X, filledStartIndex_Z, filledEndIndex_Z, top_Y, Constants.TOP_WALL);
    }

    void RestoreWall(GameObject[,] wall, int startIndex1, int endIndex1, int startIndex2, int endIndex2, float fixedAxis, string wallType)
    {
        for (int i = startIndex1; i < endIndex1; i++)
        {
            for (int j = startIndex2; j < endIndex2; j++)
            {
                GameObject cube = wall[i, j];
                Vector3 pos = cube.transform.localPosition;

                switch (wallType)
                {
                    case Constants.LEFT_WALL:
                    case Constants.RIGHT_WALL:
                        pos.x = fixedAxis;
                        break;
                    case Constants.FRONT_WALL:
                    case Constants.BACK_WALL:
                        pos.z = fixedAxis;
                        break;
                    case Constants.TOP_WALL:
                        pos.y = fixedAxis;
                        break;
                    default:
                        break;
                }
                
                cube.transform.localPosition = pos;
            }
        }
    }

    public Vector3 GetRoomSize()
    {
        return new Vector3(room_X_Length, room_Y_Length, room_Z_Length);
    }

    public void ExpandRoom(string moveType)
    {
        SetRoomSize(moveType);
    }

    public void PerlinNoise()
    {
        perlinNoiseActivated = perlinNoiseActivated ? false : true;
    }

    public void DoNoise()
    {
        Noise perlin = (Noise)GetComponent(typeof(Noise));

        if (perlinNoiseActivated)
        {
            perlin.CalcNoise(leftWall, filledStartIndex_Y, filledEndIndex_Y, filledStartIndex_Z, filledEndIndex_Z, left_X, Constants.DIRECTION_NEGATIVE_X);
            perlin.CalcNoise(rightWall, filledStartIndex_Y, filledEndIndex_Y, filledStartIndex_Z, filledEndIndex_Z, right_X, Constants.DIRECTION_POSITIVE_X);
            perlin.CalcNoise(frontWall, filledStartIndex_X, filledEndIndex_X, filledStartIndex_Y, filledEndIndex_Y, front_Z, Constants.DIRECTION_POSITIVE_Z);
            perlin.CalcNoise(backWall, filledStartIndex_X, filledEndIndex_X, filledStartIndex_Y, filledEndIndex_Y, back_Z, Constants.DIRECTION_NEGATIVE_Z);
            perlin.CalcNoise(topWall, filledStartIndex_X, filledEndIndex_X, filledStartIndex_Z, filledEndIndex_Z, top_Y, Constants.DIRECTION_POSITIVE_Y);

            perlinNoiseRestored = false;
        }
        else
        {
            if (!perlinNoiseRestored)
            {
                RestoreWalls();
                perlinNoiseRestored = true;
            }
        }
    }
}
