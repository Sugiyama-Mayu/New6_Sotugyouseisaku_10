using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S �h���b�v�A�C�e���e�X�g�p
public class prototype : MonoBehaviour
{
    [SerializeField]private DropItemManager dropItemManager;
    private void OnCollisionEnter(Collision collision)
    {
        dropItemManager.DropItemFunc('��', this.transform.position);
        Destroy(this.gameObject);
    }
}