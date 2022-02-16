using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKey(KeyCode.D))
            {
                gameObject.transform.position += new Vector3(0.1f,0,0)
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
