using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Vector3 Position;

    void Start()
    {
        Position = GameObject.Find("Magician").GetComponent<MagicianMove>().Position;
        Position.z = -10;
        transform.position = Position;
    }
    // Update is called once per frame
    void Update()
    {
        Position  = GameObject.Find("Magician").GetComponent<MagicianMove>().Position;
        Position.z = -10;
        transform.position = Position;
    }
}
