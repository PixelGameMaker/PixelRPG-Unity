using System;
using UnityEngine;
using UnityEngine.Serialization;

public class MagicianMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    public float jumpCoolDown = 3f;
    public float gameJumpCoolDown;
    public float spaceSpeed = 80f;
    [FormerlySerializedAs("Position")] public Vector3 position;
    [FormerlySerializedAs("_isRotate")] public bool isRotate;
    [FormerlySerializedAs("_isLeft")] public bool isLeft; // 需不需要反向
    [FormerlySerializedAs("MP_Origin")] public float mpOrigin = 200f;

    [FormerlySerializedAs("HP")] [SerializeField]
    private float hp;

    [FormerlySerializedAs("MP")] [SerializeField]
    private float mp;

    [FormerlySerializedAs("HPBar")] [SerializeField]
    private GameObject hpBar;

    [FormerlySerializedAs("MPBar")] [SerializeField]
    private GameObject mpBar;

    [FormerlySerializedAs("JCDBar")] [SerializeField]
    private GameObject jcdBar;

    private string _hitWall = "None"; // 檢查是否碰牆
    private float _realSpeed;
    float HP_Origin = 100f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MagicianMoveInit");
        gameJumpCoolDown = jumpCoolDown;
        position = transform.position; // 用來定位相機的初始位置
        hp = HP_Origin;
    }

    // Update is called once per frame
    void Update()
    {
        // 以下為跳躍(衝刺)的實現方法
        if (_hitWall == "None")
        {
            if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift)) && gameJumpCoolDown >= jumpCoolDown)
            {
                // JumpCD = 0;
                _realSpeed = moveSpeed + spaceSpeed;
                // Debug.Log("Jump");
                isRotate = true;
            }
            else _realSpeed = moveSpeed;
        }
        else if (_hitWall == "Over")
        {
            transform.position = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && _hitWall != "RIGHT")
        {
            if (_realSpeed > moveSpeed) gameJumpCoolDown = 0;
            transform.Translate(_realSpeed * Time.deltaTime, 0, 0);
            _hitWall = "None";
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && _hitWall != "LEFT")
        {
            if (_realSpeed > moveSpeed) gameJumpCoolDown = 0;
            transform.Translate(-_realSpeed * Time.deltaTime, 0, 0);
            _hitWall = "None";
            isLeft = true;
        }

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && _hitWall != "UP")
        {
            if (_realSpeed > moveSpeed) gameJumpCoolDown = 0;
            transform.Translate(0, _realSpeed * Time.deltaTime, 0);
            _hitWall = "None";
        }

        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && _hitWall != "DOWN")
        {
            if (_realSpeed > moveSpeed) gameJumpCoolDown = 0;
            transform.Translate(0, -_realSpeed * Time.deltaTime, 0);
            _hitWall = "None";
        }

        // Time.deltaTime can avoid the frame rate problem
        if (gameJumpCoolDown < jumpCoolDown) gameJumpCoolDown += Time.deltaTime;
        if (gameJumpCoolDown >= jumpCoolDown) gameJumpCoolDown = jumpCoolDown;

        position = transform.position;
        mp = Convert.ToInt32(GameObject.Find("BulletManager").GetComponent<BulletManager>().MP);
        ScaleHpBar(hp);
        ScaleMpBar(mp);
        ScaleJcdBar(gameJumpCoolDown);
    }

    // 檢測是否與牆體的碰撞箱碰撞
    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.CompareTag("EnemyBullet")) Debug.Log("MagicianHitBullet");
        if (other.gameObject.CompareTag("Wall_LEFT")) _hitWall = "LEFT";
        else if (other.gameObject.CompareTag("Wall_RIGHT")) _hitWall = "RIGHT";
        else if (other.gameObject.CompareTag("Wall_UP")) _hitWall = "UP";
        else if (other.gameObject.CompareTag("Wall_DOWN")) _hitWall = "DOWN";
        else _hitWall = "None";
        if (other.gameObject.CompareTag("EDGE")) _hitWall = "Over";

        if (other.gameObject.CompareTag("Enemy")) ModifyHp(-5);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall_LEFT")) _hitWall = "None";
        else if (other.gameObject.CompareTag("Wall_RIGHT")) _hitWall = "None";
        else if (other.gameObject.CompareTag("Wall_UP")) _hitWall = "None";
        else if (other.gameObject.CompareTag("Wall_DOWN")) _hitWall = "None";
        else _hitWall = "None";
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.CompareTag("Bullet")) ModifyHP(-5);
    }
    */
    private void ModifyHp(int num)
    {
        hp += num;
        if (hp > 100) hp = 100;
        if (hp < 0) hp = 0;
    }

    private void ScaleHpBar(float Hp)
    {
        var hp = Hp / HP_Origin;
        var Scale = new Vector3(hp, 1, 1);
        hpBar.transform.localScale = Scale;
    }

    private void ScaleMpBar(float Mp)
    {
        var mp = Mp / mpOrigin;
        var Scale = new Vector3(mp, 1, 1);
        mpBar.transform.localScale = Scale;
    }

    private void ScaleJcdBar(float Cd)
    {
        var cd = Cd / jumpCoolDown;
        var Scale = new Vector3(cd, 1, 1);
        jcdBar.transform.localScale = Scale;
    }
}