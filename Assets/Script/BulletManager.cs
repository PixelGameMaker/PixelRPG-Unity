using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] GameObject[] BulletPrefabs;
    public int Choose;
    private float x;
    private float y;
    private int width;
    private int height;
    public Vector3 Move_Vector = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 Position = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] float CD = 0.3f;
    [SerializeField] float GameCD;

    void Start()
    {
        width = Screen.width;
        height = Screen.height;
        Debug.Log(width);
        Debug.Log(height);
    }

    private void Update()
    {
        Position = GameObject.Find("Magician").GetComponent<MagicianMove>().Position;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (GameCD >= CD) 
            { 
                SpawnBullet();
                GameCD = 0; 
            }
            // else Debug.Log("CD");
        }
        if (GameCD < CD) GameCD += Time.deltaTime;
        if (GameCD >= CD) GameCD = CD;
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
        return vector_Move.normalized/30;
    }
}
