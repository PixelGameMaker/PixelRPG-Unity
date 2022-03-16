using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Vector3 position;
    Vector3 Position;
    Vector3 forward;
    Vector3 Back;
    float x, y, z;
    int Control;
    [SerializeField]float HP = 50;

    // Start is called before the first frame update
    void Start()
    {
        Control = transform.parent.GetComponent<EnemyManeger>().control;
    }

    // Update is called once per frame
    void Update()
    {
        x = Random.Range(-10f, 10f);
        y = Random.Range(-5f, 5f);
        position = transform.position;
        Position = GameObject.Find("Magician").GetComponent<MagicianMove>().Position;
        forward = Position - position;
        transform.Translate(forward.normalized* Control / 50);
        if (HP <= 0) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Control = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Control = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("HIT");
            Back = collision.GetComponent<Bullet>().move_vector.normalized;
            Debug.Log(Back);
            transform.Translate(Back);
            ReduceHP(5);
            Destroy(collision.gameObject);
        }
        
    }
    void ReduceHP(int num)
    {
        HP -= num;
        if (HP < 0) HP = 0;
    }
}
