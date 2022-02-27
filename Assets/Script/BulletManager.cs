using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] GameObject[] BulletPrefabs;
    public int Choose;

    public void SpawnBullet()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(BulletPrefabs[Choose], transform);
        }
    } 
}
