using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] GameObject Apple;
    [SerializeField] GameObject MPDrug;
    int level;
    public bool apple_isSpawn = false;
    public bool apple_isEat = false;
    public bool MPD_isSpawn = false;
    public bool MPD_isEat = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        level = GameObject.Find("EnemyManeger").GetComponent<EnemyManager>().level;
        if (level %3 == 2 && !apple_isSpawn && !apple_isEat)
        {
            var x = Random.Range(-11f, 11f);
            var y = Random.Range(-5.7f, 5.7f);
            Vector3 position = new Vector3(x, y, 0);
            GameObject AP = Instantiate(Apple, position, Quaternion.identity, transform);
            AP.name = "APPLE CLONE";
            apple_isSpawn = true;
        }
        if (level % 4 == 3 && !MPD_isSpawn && !MPD_isEat)
        {
            var x = Random.Range(-11f, 11f);
            var y = Random.Range(-5.7f, 5.7f);
            Vector3 position = new Vector3(x, y, 0);
            GameObject MPD = Instantiate(MPDrug, position, Quaternion.identity, transform);
            MPD.name = "MPDrug CLONE";
            MPD_isSpawn = true;
        }
    }
}
