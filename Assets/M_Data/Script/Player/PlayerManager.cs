using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameManager gameManager;
    private EquimentManager equimentManager;
    private GameOverProcess gameOverProcess;

    [Header("HP")]
    [SerializeField] private float maxHp;
    [SerializeField] private float hp;
    [SerializeField] private float regenHp;

    // �h��
    [SerializeField] private float defense;
    [SerializeField] private float e;

    private CapsuleCollider col;
    private float count;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameOverProcess = GameObject.Find("TitleScriptObj").GetComponent<GameOverProcess>();
        col = GetComponent<CapsuleCollider>();
        ColOffset();
        hp = maxHp;
    }
    private void FixedUpdate()
    {
        AutoRegen();
        if (regenHp != 0)
        {
            Regin();
        }

    }

    // ������
    private void AutoRegen()
    {
        if (hp < maxHp)
        {
            if (30 <= count) // ������
            {
                hp += 0.01f;
            }
            else // �����񕜂܂ł̃J�E���g
            {
                count += Time.deltaTime;
            }
        }
        else if(maxHp <hp)
        {
            hp = maxHp;
        }

    }


    public void Regin()
    {
        hp += 0.2f;
        if(regenHp <= hp)
        {
            regenHp = 0;
        }
    }

    // ���S����
    private void Dead()
    {
        gameOverProcess.CallGameOver();
        gameManager.createManager.AllDestroyEnemy();
        Debug.Log("PlayerDead");
    }
    public void ColOffset()
    {
        if (gameManager.GetSetXRMode == false)
        {
            col.center = new Vector3(0, 0.1f, 0);
        }
        else
        {
            col.center = new Vector3(0, 0.7f, 0);
        }

    }
    // �Q�b�^�[
    public float GetHp
    {
        get
        {
            return hp;
        }
    }
    public float GetMaxHp
    {
        get
        {
            return maxHp;
        }
    }
    /*
    public int GetEquipmentNum(int i)
    {
        return equipmentNum[i];
    }
    */
    // �Z�b�^�[
    // �_���[�W
    public float SetDamege
    {
        set
        {
            hp -= (value / 2) - (defense / 4);
            Debug.Log( "PDamege :"+((value / 2) - (defense / 4)));
            count = 0;
            if (hp < 0)
            {
                hp = 0;
                Dead();
            }
        }
    }

    public void SetMaxHp()
    {
        hp = maxHp;
    }

    // �񕜃A�C�e���g�p
    public void DragItem(int i)
    {
        float f = 0;
        switch (i)
        {
            case 0:
                f = maxHp / 10;
                break;
            case 1:
                f = maxHp / 5;
                break;
            case 2:
                f = maxHp / 2;
                break;
            case 3:
                f = maxHp;
                break;
            default:
                return;
        }
        if(hp + i <= maxHp)
        {
            regenHp = f + hp;

        }
        else
        {
            regenHp = maxHp;
        }
        Debug.Log("��");
    }
    // �Q�b�^�[�E�Z�b�^�[
    // �h���
    public float SetDefense
    {
        set 
        { 
            defense = value;
            Debug.Log("Defense :"+ defense);
        }
    }

    // �Z�[�u���[�h�p
    public Vector3 PlayerPos
    {
        get
        {
            return gameObject.transform.position;
        }
        set
        {
            gameObject.transform.position = value;
        }
    }
}
