using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S アイテムドロップ後の処理
public class DropItem : MonoBehaviour
{
    [SerializeField]private DropItemManager dropItemManager;
    public bool doneDrop;    // アイテムドロップ済みフラグ
    private int countTime;           // 時間カウント
    private void Update()
    {
        if (doneDrop == true && countTime > dropItemManager.countLimit)
        {
            Destroy(this.gameObject);
        }
        else if (doneDrop == true)
        {
            countTime++;
        }
    } 
}