using UnityEngine;

public class MagicianMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    public float jumpCoolDown = 5f;
    public float JumpCD = 5f;
    public float spaceSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MagicianMoveInit");
    }

    // Update is called once per frame
    void Update()
    {
        // float spaceSpeed;
        // if JumpCD return Unbound than JumpCD = jumpCoolDown
        if (Input.GetKey(KeyCode.Space) && JumpCD == jumpCoolDown)
        {
            spaceSpeed = moveSpeed * 2;
            JumpCD = 0;
        }
        else
        {
            spaceSpeed = 0;
        }

        var realSpeed = moveSpeed + spaceSpeed;
        if (Input.GetKey(KeyCode.D)) transform.Translate(realSpeed * Time.deltaTime, 0, 0);

        if (Input.GetKey(KeyCode.A)) transform.Translate(-realSpeed * Time.deltaTime, 0, 0);

        if (Input.GetKey(KeyCode.W)) transform.Translate(0, realSpeed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.S)) transform.Translate(0, -realSpeed * Time.deltaTime, 0);
        // Time.deltaTime can avoid the frame rate problem
        if (JumpCD < jumpCoolDown) JumpCD += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet")) Debug.Log("MagicianHitBullet");
    }
}