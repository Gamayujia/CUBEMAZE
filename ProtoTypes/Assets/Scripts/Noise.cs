using UnityEngine;
using System.Collections;
using Assets;

public class Noise : MonoBehaviour {
    [Range(1.0f, 5.0f)]
    public float heightScale = 1.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CalcNoise(GameObject[,] wall, int startIndex1, int endIndex1, int startIndex2, int endIndex2, float fixedAxis, string direction)
    {
        for (int i = startIndex1; i < endIndex1; i++)
        {
            for (int j = startIndex2; j < endIndex2; j++)
            {
                switch (direction)
                {
                    case Constants.DIRECTION_POSITIVE_X:
                        {
                            GameObject cube = wall[i, j];
                            Vector3 pos = cube.transform.localPosition;
                            float height = heightScale * cube.transform.localScale.x * Mathf.PerlinNoise(Time.time * pos.y, Time.time * pos.z);
                            pos.x = fixedAxis + height;
                            cube.transform.localPosition = pos;
                            break;
                        }
                    case Constants.DIRECTION_NEGATIVE_X:
                        {
                            GameObject cube = wall[i, j];
                            Vector3 pos = cube.transform.localPosition;
                            float height = heightScale * cube.transform.localScale.x * Mathf.PerlinNoise(Time.time * pos.y, Time.time * pos.z);
                            pos.x = fixedAxis - height;
                            cube.transform.localPosition = pos;
                            break;
                        }
                    case Constants.DIRECTION_POSITIVE_Z:
                        {
                            GameObject cube = wall[i, j];
                            Vector3 pos = cube.transform.localPosition;
                            float height = heightScale * cube.transform.localScale.z * Mathf.PerlinNoise(Time.time * pos.x, Time.time * pos.y);
                            pos.z = fixedAxis + height;
                            cube.transform.localPosition = pos;
                            break;
                        }
                    case Constants.DIRECTION_NEGATIVE_Z:
                        {
                            GameObject cube = wall[i, j];
                            Vector3 pos = cube.transform.localPosition;
                            float height = heightScale * cube.transform.localScale.z * Mathf.PerlinNoise(Time.time * pos.x, Time.time * pos.y);
                            pos.z = fixedAxis - height;
                            cube.transform.localPosition = pos;
                            break;
                        }
                    case Constants.DIRECTION_POSITIVE_Y:
                        {
                            GameObject cube = wall[i, j];
                            Vector3 pos = cube.transform.localPosition;
                            float height = heightScale * cube.transform.localScale.y * Mathf.PerlinNoise(Time.time * pos.x, Time.time * pos.z);
                            pos.y = fixedAxis + height;
                            cube.transform.localPosition = pos;
                            break;
                        }
                    default:
                        break;

                }
                
            }
        }
    }
}
