using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float HP = 50;
    Vector3 Back;
    int Control;
    Vector3 forward;
    Vector3 position;
    Vector3 Position;
    float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        Control = transform.parent.GetComponent<EnemyManager>().control;
    }

    // Update is called once per frame
    void Update()
    {
        x = Random.Range(-10f, 10f);
        y = Random.Range(-5f, 5f);
        position = transform.position;
        Position = GameObject.Find("Magician").GetComponent<MagicianMove>().position;
        forward = Position - position;
        transform.Translate(forward.normalized * Control / 50);
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
            // Debug.Log(Back);
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