using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S �A�C�e���h���b�v��̏���
public class DropItem : MonoBehaviour
{
    [SerializeField]private DropItemManager dropItemManager;
    public bool doneDrop;    // �A�C�e���h���b�v�ς݃t���O
    private int countTime;           // ���ԃJ�E���g
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