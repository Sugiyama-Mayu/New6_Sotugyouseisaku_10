using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// �K�̃M�~�b�N
// �A�^�b�N�u���b�N�̏���
public class AttackChangeMaterial : MonoBehaviour
{
    public bool clickFlag;      // �A�^�b�N�u���b�N�Ƃ̓����蔻��t���O
    public Material changeMat;  // �ύX�p�̃}�e���A��
    [SerializeField] private AttackBlockManager attackBlockManager;
    [SerializeField] private int thisObjNum;
    void Start()
    {
        clickFlag = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerWeapon"))
        {
            ParticleSystem effect_c = Instantiate(attackBlockManager.hitEffect);
            effect_c.transform.position = gameObject.transform.position;
            effect_c.Play();
            Destroy(effect_c.gameObject, 5.0f);
            clickFlag = true;
            this.GetComponent<Renderer>().material = changeMat;
            attackBlockManager.blockjudge[thisObjNum - 1] = 1;
        }
    }
}
