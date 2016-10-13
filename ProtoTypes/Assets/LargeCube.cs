using UnityEngine;
using System.Collections;

public class LargeCube : MonoBehaviour {
    GameObject largeCube;

    // Use this for initialization
    void Start()
    {
        largeCube = this.gameObject;
        largeCube.tag = "LargeCube";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
