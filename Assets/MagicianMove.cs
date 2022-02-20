using UnityEngine;

public class MagicianMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MagicianMoveInit");
    }

    // Update is called once per frame
    void Update()
    {
        float spaceSpeed;
        if (Input.GetKey(KeyCode.Space))
            spaceSpeed = 5;
        else
            spaceSpeed = 0;
        var realSpeed = moveSpeed + spaceSpeed;
        if (Input.GetKey(KeyCode.D)) transform.Translate(realSpeed * Time.deltaTime, 0, 0);

        if (Input.GetKey(KeyCode.A)) transform.Translate(-realSpeed * Time.deltaTime, 0, 0);

        if (Input.GetKey(KeyCode.W)) transform.Translate(0, realSpeed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.S)) transform.Translate(0, -realSpeed * Time.deltaTime, 0);
        // Time.deltaTime can avoid the frame rate problem
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet")) Debug.Log("MagicianHitBullet");
    }
}