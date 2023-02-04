using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameManager gameManager;
    private EquimentManager equimentManager;

    [Header("HP")]
    [SerializeField] private float maxHp;
    [SerializeField] private float hp;
    [SerializeField] private float regenHp;

    [SerializeField] private int[] equipmentNum = { 0, 0, 0, 0 };

    // 防具
    [SerializeField] private float defense;
    [SerializeField] private float e;

    private CapsuleCollider col;
    private float count;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        col = GetComponent<CapsuleCollider>();
        ColOffset();
        hp = maxHp;
    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            DragItem(10);
        }
    }
    */
    private void FixedUpdate()
    {
        AutoRegen();
        if (regenHp != 0)
        {
            Regin();
        }

    }

    // 自動回復
    private void AutoRegen()
    {
        if (hp < maxHp)
        {
            if (30 <= count) // 自動回復
            {
                hp += 0.01f;
            }
            else // 自動回復までのカウント
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
            Debug.Log("kanryou");
        }
    }

    // 死亡処理
    private void Dead()
    {
        Debug.Log("dead");
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

    // 防御力
    public void DefenseValue()
    {
        equimentManager.GetDefenseValue(equipmentNum[2],equipmentNum[3]);
    }

    // ゲッター
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

    public int GetEquipmentNum(int i)
    {
        return equipmentNum[i];
    }

    // セッター
    // 
    public void SetEquipmentNum(int type ,int id)
    {
        equipmentNum[type] = id;
    }
    // ダメージ
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

    // 回復アイテム使用
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
        Debug.Log("回復");
    }
    // ゲッター・セッター
    // 防御力
    public float SetDefense
    {
        set 
        { 
            defense = value;
            Debug.Log("Defense :"+ defense);
        }
    }

    // セーブロード用
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
