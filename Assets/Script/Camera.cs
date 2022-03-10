using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Vector3 Position;
    [SerializeField] GameObject[] prefabs; // 把地板的prefab放到這裡
    private Vector3 BGPosition; 
    private bool I,II,III,IV,VI,VII,VIII,IX; // 用來判斷該塊地板是否生成

    void Start()
    {
        // 設定鏡頭的基礎位置
        Position = GameObject.Find("Magician").GetComponent<MagicianMove>().Position;
        Position.z = -10;
        transform.position = Position;
    }
    // Update is called once per frame
    void Update()
    {
        // 跟隨腳色移動
        Position  = GameObject.Find("Magician").GetComponent<MagicianMove>().Position;
        Position.z = -10;
        transform.position = Position;

        // 自動生成邊界
        if (transform.position.y > 3 && !II) II = Spawn(BGPosition = new Vector3(0, 12.44f, 0), "II");
        else if (transform.position.y < 3 && II) II = destroy("II");
        if (transform.position.x < -6 && !IV) IV = Spawn(BGPosition = new Vector3(-22.89f, 0, 0), "IV");  //   x <-6  y>3
        else if (transform.position.x > -6 && IV) IV = destroy("IV");
        if (transform.position.x > 6 && !VI) VI = Spawn(BGPosition = new Vector3(22.89f, 0, 0), "VI");
        else if (transform.position.x < 6 && VI) VI = destroy("VI");
        if (transform.position.y < -3 && !VIII) VIII = Spawn(BGPosition = new Vector3(0, -12.44f, 0), "VIII");
        else if (transform.position.y > -3 && VIII) VIII = destroy("VIII");
        if ((II && IV) && !I) I = Spawn(BGPosition = new Vector3(-22.89f, 12.44f, 0), "I");
        else if ((!II || !IV) && I) I = destroy("I");
        if ((II && VI) && !III) III = Spawn(BGPosition = new Vector3(22.89f, 12.44f, 0), "III");
        else if ((!II || !VI) && III) III = destroy("III");
        if ((VIII && IV) && !VII) VII = Spawn(BGPosition = new Vector3(-22.89f, -12.44f, 0), "VII");
        else if ((!VIII || !IV) && VII) VII = destroy("VII");
        if ((VIII && VI) && !IX) IX = Spawn(BGPosition = new Vector3(22.89f, -12.44f, 0), "IX");
        else if ((!VIII || !VI) && IX) IX = destroy("IX");

    }

    // 生成地板
    bool Spawn(Vector3 vector, string name)
    {
        GameObject BG = Instantiate(prefabs[0], vector, Quaternion.identity);
        BG.name = name;
        return true;
    }

    // 銷毀地板
    bool destroy(string name)
    {
        var BG = GameObject.Find(name);
        if (BG) Destroy(BG);
        return false;
    }
}
