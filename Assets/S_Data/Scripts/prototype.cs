using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S ドロップアイテムテスト用
public class prototype : MonoBehaviour
{
    [SerializeField]private DropItemManager dropItemManager;
    private void OnCollisionEnter(Collision collision)
    {
        dropItemManager.DropItemFunc('小', this.transform.position);
        Destroy(this.gameObject);
    }
}