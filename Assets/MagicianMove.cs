using UnityEngine;

public class MagicianMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    public float jumpCoolDown = 5f;
    public float JumpCD;
    public float spaceSpeed = 20f;
    private float realSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MagicianMoveInit");
        JumpCD = jumpCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && JumpCD >= jumpCoolDown)
        {
            JumpCD = 0;
            realSpeed = moveSpeed + spaceSpeed;
            // Debug.Log("Jump");
        }
        else
        {
            realSpeed = moveSpeed;
        }

        if (Input.GetKey(KeyCode.D)) transform.Translate(realSpeed * Time.deltaTime, 0, 0);

        if (Input.GetKey(KeyCode.A)) transform.Translate(-realSpeed * Time.deltaTime, 0, 0);

        if (Input.GetKey(KeyCode.W)) transform.Translate(0, realSpeed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.S)) transform.Translate(0, -realSpeed * Time.deltaTime, 0);
        // Time.deltaTime can avoid the frame rate problem
        if (JumpCD < jumpCoolDown) JumpCD += Time.deltaTime;
        if (JumpCD >= jumpCoolDown) JumpCD = jumpCoolDown;
        // Debug.Log(JumpCD);
        /*
        if (realSpeed > moveSpeed)
        {
            // Debug.Log(realSpeed);
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet")) Debug.Log("MagicianHitBullet");
    }
}