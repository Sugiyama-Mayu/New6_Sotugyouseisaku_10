using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private GameObject[] terrains;
    [SerializeField] private Transform playerTrams;
    [SerializeField] private float count; // カウント
    [SerializeField] private float maxCount; // 更新間隔
    private int offset = 200;

    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        DistanceComparison();
    }

    // Update is called once per frame
    private void Update()
    {
        if (maxCount <= count)
        {
            count = 0;
            DistanceComparison();
        }
        else
        {
            count += Time.deltaTime;
        }
    }

    // Terrainの表示非表示処理（処理軽減）
    public void DistanceComparison()
    {
        int i = 0;
        Vector3 playerPos = new Vector3(playerTrams.position.x, 0, playerTrams.position.z);
        foreach (GameObject terrainTrans in terrains)
        {
            if(i !=8)distance = Vector3.Distance(playerPos, new Vector3(terrainTrans.transform.position.x + offset, 0, terrainTrans.transform.position.z + offset));
            else distance = Vector3.Distance(playerPos, new Vector3(terrainTrans.transform.position.x + offset/2, 0, terrainTrans.transform.position.z + offset/2));
            i++;
          //  Debug.Log(terrainTrans.name + ":"+distance);
            if (distance < 400)
            {
                terrainTrans.SetActive(true);
            }
            else
            {
                terrainTrans.SetActive(false);

            }
        }
    }

}
