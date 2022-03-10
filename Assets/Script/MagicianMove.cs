using UnityEngine;

public class MagicianMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    public float jumpCoolDown = 5f;
    public float gameJumpCoolDown;
    public float spaceSpeed = 80f;
    private float _realSpeed;
    private string HitWall = "None"; // 檢查是否碰牆
    public Vector3 Position;
    private float _rotateSpeed = 1800f;
    private bool _isRotate = false;
    private Quaternion Rotation = new Quaternion(0,0,0,1); // 基礎方向
    private int RotateTimes; // 旋轉次數
    public bool _isLeft; // 需不需要反向


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MagicianMoveInit");
        gameJumpCoolDown = jumpCoolDown;
        Position = transform.position; // 用來定位相機的初始位置

    }

    // Update is called once per frame
    void Update()
    {
        
        // 以下為跳躍(衝刺)的實現方法
        if (HitWall == "None")
        {
            if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift)) && gameJumpCoolDown >= jumpCoolDown)
            {
                // JumpCD = 0;
                _realSpeed = moveSpeed + spaceSpeed;
                // Debug.Log("Jump");
                _isRotate = true;
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
            HitWall = "None";
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && HitWall != "LEFT")
        {
            if (_realSpeed > moveSpeed) gameJumpCoolDown = 0;
            transform.Translate(-_realSpeed * Time.deltaTime, 0, 0);
            HitWall = "None";
            _isLeft = true;
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

        if (_isRotate && RotateTimes < 15 && _isLeft)
        {
            transform.Rotate(0, 0, _rotateSpeed*Time.deltaTime);
            RotateTimes++;
            gameJumpCoolDown = 0;
        }
        else if (_isRotate && RotateTimes < 15 && !_isLeft)
        {
            transform.Rotate(0, 0, -_rotateSpeed * Time.deltaTime);
            RotateTimes++;
            gameJumpCoolDown = 0;
        }
        else
        {
            _isRotate = false;
            RotateTimes = 0;
            transform.rotation = Rotation;
        }

        // Time.deltaTime can avoid the frame rate problem
        if (gameJumpCoolDown < jumpCoolDown) gameJumpCoolDown += Time.deltaTime;
        if (gameJumpCoolDown >= jumpCoolDown) gameJumpCoolDown = jumpCoolDown;

        Position = transform.position;
    }

    // 檢測是否與牆體的碰撞箱碰撞
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
