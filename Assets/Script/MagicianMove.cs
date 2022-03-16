using System;
using UnityEngine;

public class MagicianMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    public float jumpCoolDown = 3f;
    public float gameJumpCoolDown;
    public float spaceSpeed = 80f;
    public Vector3 Position;
    public bool _isRotate;
    public bool _isLeft; // 需不需要反向
    public float MP_Origin = 200f;
    [SerializeField] float HP;
    [SerializeField] float MP;
    [SerializeField] GameObject HPBar;
    [SerializeField] GameObject MPBar;
    [SerializeField] GameObject JCDBar;
    private float _realSpeed;
    private string HitWall = "None"; // 檢查是否碰牆
    float HP_Origin = 100f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MagicianMoveInit");
        gameJumpCoolDown = jumpCoolDown;
        Position = transform.position; // 用來定位相機的初始位置
        HP = HP_Origin;
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

        // Time.deltaTime can avoid the frame rate problem
        if (gameJumpCoolDown < jumpCoolDown) gameJumpCoolDown += Time.deltaTime;
        if (gameJumpCoolDown >= jumpCoolDown) gameJumpCoolDown = jumpCoolDown;

        Position = transform.position;
        MP = Convert.ToInt32(GameObject.Find("BulletManager").GetComponent<BulletManager>().MP);
        ScaleHPBar(HP);
        ScaleMPBar(MP);
        ScaleJCDBar(gameJumpCoolDown);
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

        if (other.gameObject.CompareTag("Enemy")) ModifyHP(-5);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall_LEFT")) HitWall = "None";
        else if (other.gameObject.CompareTag("Wall_RIGHT")) HitWall = "None";
        else if (other.gameObject.CompareTag("Wall_UP")) HitWall = "None";
        else if (other.gameObject.CompareTag("Wall_DOWN")) HitWall = "None";
        else HitWall = "None";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.CompareTag("Bullet")) ModifyHP(-5);
    }

    void ModifyHP(int num)
    {
        HP += num;
        if (HP > 100) HP = 100;
        if (HP < 0) HP = 0;
    }

    void ScaleHPBar(float Hp)
    {
        float hp = Hp / HP_Origin;
        Vector3 Scale = new Vector3(hp, 1, 1);
        HPBar.transform.localScale = Scale;
    }

    void ScaleMPBar(float Mp)
    {
        float mp = Mp / MP_Origin;
        Vector3 Scale = new Vector3(mp, 1, 1);
        MPBar.transform.localScale = Scale;
    }

    void ScaleJCDBar(float Cd)
    {
        float cd = Cd / jumpCoolDown;
        Vector3 Scale = new Vector3(cd, 1, 1);
        JCDBar.transform.localScale = Scale;
    }
}