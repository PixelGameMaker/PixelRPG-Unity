using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] GameObject[] BulletPrefabs;
    public int Choose;
    public Vector3 Move_Vector = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] float CD = 0.3f;
    [SerializeField] float GameCD;
    public float MP;
    private int height;
    private float MP_ADD = 8f;
    private Vector3 Position = new Vector3(0.0f, 0.0f, 0.0f);
    private int width;
    private float x;
    private float y;

    void Start()
    {
        width = Screen.width;
        height = Screen.height;
        Debug.Log(width + " x " + height);
        MP = GameObject.Find("Magician").GetComponent<MagicianMove>().mpOrigin;
    }

    private void Update()
    {
        Position = GameObject.Find("Magician").GetComponent<MagicianMove>().position;
        if (Input.GetKey(KeyCode.Mouse0))
            if (GameCD >= CD && MP > 5)
            {
                SpawnBullet();
                ModifyMP(-10);
                GameCD = 0;
            }

        // else if (GameCD < CD && MP > 5) Debug.Log("CD");
        // else Debug.Log("MP=0");
        if (GameCD < CD) GameCD += Time.deltaTime;
        if (GameCD >= CD) GameCD = CD;
        ADDMP();
    }

    public void SpawnBullet()
    {
        Move_Vector = Move();
        GameObject B = Instantiate(BulletPrefabs[Choose], Position, Quaternion.identity, transform);
        B.name = "ATTACK";
    }

    public Vector3 Move()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 vector_Move;
        x = (mousePosition.x - (width / 2));
        y = (mousePosition.y - (height / 2));
        // vector_Move = new Vector3(x / Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) / 30, y / Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) / 30, 0f);
        vector_Move = new Vector3(x, y, 0);
        return vector_Move.normalized / 25;
    }

    void ModifyMP(int num)
    {
        MP += num;
        if (MP > 200) MP = 200;
        if (MP < 0) MP = 0;
    }

    void ADDMP()
    {
        float time = Time.deltaTime;
        MP += MP_ADD * time;
        if (MP >= 200) MP = 200;
    }
}