using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 move_vector;

    // Start is called before the first frame update
    private void Start()
    {
        move_vector = GameObject.Find("BulletManager").GetComponent<BulletManager>().Move_Vector;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(move_vector);
    }
    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall_LEFT")) Destroy(gameObject);
        else if (other.gameObject.CompareTag("Wall_RIGHT")) Debug.Log("HIT");
        else if (other.gameObject.CompareTag("Wall_UP")) Debug.Log("HIT");
        else if (other.gameObject.CompareTag("Wall_DOWN")) Debug.Log("HIT");
    }
    */
}
