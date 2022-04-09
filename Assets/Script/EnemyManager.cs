// using System.Collections;
// using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public int control;
    public int level = 1;
    [SerializeField] private GameObject[] prefabs;
    private bool _start; // = false;

    // Start is called before the first frame update
    void Start()
    {
        control = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_start)
        {
            for (var i = 0; i <= level; i++)
            {
                SpawnEnemy();
            }

            _start = true;
        }

        bool isEnemy = GameObject.Find("Clone Enemy");
        if (!isEnemy)
        {
            level++;
            _start = false;
        }

        GameObject.Find("LevelNum").GetComponent<Text>().text = (level + 1).ToString();
        /*
        // var enemyNum = transform.childCount - 1;
        // GameObject.Find("EnemyNum").GetComponent<Text>().text = enemyNum.ToString();
        // Object in scene/canvas/EnemyNum
        */
    }

    private void SpawnEnemy()
    {
        var x = Random.Range(-11f, 11f);
        var y = Random.Range(-5.7f, 5.7f);
        var enemy = Instantiate(prefabs[0], new Vector3(x, y, 0), Quaternion.identity, transform);
        enemy.name = "Clone Enemy";
    }
}