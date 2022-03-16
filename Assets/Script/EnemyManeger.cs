using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManeger : MonoBehaviour
{
    public int control;
    public int level = 1;
    bool start = false;
    [SerializeField] GameObject[] prefabs;
    
    // Start is called before the first frame update
    void Start()
    {
        control = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            for(int i = 0; i <= level; i++)
            {
                SpawnEnemy();
            }
            start = true;
        }
        bool is_Enemy = GameObject.Find("Clone Enemy");
        if (!is_Enemy)
        {
            level++;
            start = false;
        }
        GameObject.Find("LevelNum").GetComponent<Text>().text = (level+1).ToString();
        int EnemyNum = transform.childCount - 1;
        GameObject.Find("EnemyNum").GetComponent<Text>().text = EnemyNum.ToString();
    }
    void SpawnEnemy()
    {
        float x = Random.Range(-11f, 11f);
        float y = Random.Range(-5.7f, 5.7f);
        GameObject Enemy = Instantiate(prefabs[0], new Vector3(x, y, 0), Quaternion.identity, transform);
        Enemy.name = "Clone Enemy";
    }
}
