using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float x;
    private float y;
    private int width;
    private int height;
    private bool On = false;
    // Start is called before the first frame update
    void Start()
    {
        width = Screen.width;
        height = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            // Debug.Log("Attack");
            // Debug.Log(Input.mousePosition);
            Vector3 mousePosition = Input.mousePosition;
            x = (mousePosition.x - (width / 2));
            y = (mousePosition.y - (height / 2));
            On = true;
            // Debug.Log(Mathf.Sqrt(Mathf.Pow(x / Mathf.Sqrt((Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) / 10), 2)+ Mathf.Pow(y / Mathf.Sqrt((Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) / 10), 2)));
            // transform.Translate(x / Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) / 30, y / Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) / 30, 0);
        }
        if (On)
        {
            transform.Translate(x / Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) / 30, y / Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) / 30, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EDGE")) 
        { 
            Destroy(gameObject);
            Debug.Log("Hit");
        }
    }
}
