using UnityEngine;

public class MagicianMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    public float jumpCoolDown = 5f;
    public float gameJumpCoolDown;
    public float spaceSpeed = 200f;
    private float _realSpeed;
    private string HitWall = "None";
    private int width;
    private int height;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MagicianMoveInit");
        gameJumpCoolDown = jumpCoolDown;
        width = Screen.width;
        height = Screen.height;
        Debug.Log(width);
        Debug.Log(height);
    }

    // Update is called once per frame
    void Update()
    {
        if (HitWall == "None")
        {
            if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift)) && gameJumpCoolDown >= jumpCoolDown)
            {
                // JumpCD = 0;
                _realSpeed = moveSpeed + spaceSpeed;
                // Debug.Log("Jump");
            }
            else _realSpeed = moveSpeed;
        }
        else if (HitWall == "Over")
        {
            transform.position = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && HitWall != "RIGHT")
        {
            if (_realSpeed > moveSpeed) gameJumpCoolDown = 0;
            transform.Translate(_realSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && HitWall != "LEFT")
        {
            if (_realSpeed > moveSpeed) gameJumpCoolDown = 0;
            transform.Translate(-_realSpeed * Time.deltaTime, 0, 0);
            HitWall = "None";
        }

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && HitWall != "UP")
        {
            if (_realSpeed > moveSpeed) gameJumpCoolDown = 0;
            transform.Translate(0, _realSpeed * Time.deltaTime, 0);
            HitWall = "None";
        }

        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && HitWall != "DOWN")
        {
            if (_realSpeed > moveSpeed) gameJumpCoolDown = 0;
            transform.Translate(0, -_realSpeed * Time.deltaTime, 0);
            HitWall = "None";
        }

        // Time.deltaTime can avoid the frame rate problem
        if (gameJumpCoolDown < jumpCoolDown) gameJumpCoolDown += Time.deltaTime;
        if (gameJumpCoolDown >= jumpCoolDown) gameJumpCoolDown = jumpCoolDown;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 mousePosition = Input.mousePosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.CompareTag("EnemyBullet")) Debug.Log("MagicianHitBullet");
        if (other.gameObject.CompareTag("Wall_LEFT")) HitWall = "LEFT";
        else if (other.gameObject.CompareTag("Wall_RIGHT")) HitWall = "RIGHT";
        else if (other.gameObject.CompareTag("Wall_UP")) HitWall = "UP";
        else if (other.gameObject.CompareTag("Wall_DOWN")) HitWall = "DOWN";
        else HitWall = "None";
        
        if (other.gameObject.CompareTag("EDGE")) HitWall = "Over";
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall_LEFT")) HitWall = "None";
        else if (other.gameObject.CompareTag("Wall_RIGHT")) HitWall = "None";
        else if (other.gameObject.CompareTag("Wall_UP")) HitWall = "None";
        else if (other.gameObject.CompareTag("Wall_DOWN")) HitWall = "None";
        else HitWall = "None";
    }
}