using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    private GameManager gameManager;

    private string playerName;
    private Vector3 hitPos;
    [SerializeField] private GameObject hitFx;
    [SerializeField] private int damage;
    [SerializeField] private bool isPlayer;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject playerObj = gameManager.GetPlayerObj();
        playerName = playerObj.name;
        if (gameObject.transform.root.tag == "Player") isPlayer = true;
        else isPlayer = false;
    }

    // プレイヤーか敵の当たり判定
    private void OnCollisionEnter(Collision col)
    {
        foreach (ContactPoint contact in col.contacts)
        {
           hitPos = contact.point;
        }
        DamegeJudgment(col.gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
       hitPos = col.ClosestPointOnBounds(transform.position);
        DamegeJudgment(col.gameObject);
    }

    private void DamegeJudgment(GameObject col)
    {
        // 
        if (col.tag == "DamegeCheck")
        {
            DamegeCheck(col);
        }

        if (col.layer == LayerMask.NameToLayer("Enemy") && gameObject.layer == LayerMask.NameToLayer("PlayerWeapon"))
        {
            EnemyDamege(col);
            StartHitEffect(hitPos , isPlayer);
        }

        if (col.layer == LayerMask.NameToLayer("Player") && gameObject.layer == LayerMask.NameToLayer("EnemyWeapon"))
        {
            if (col.gameObject.name == playerName) PlayerDamege(col);
            else 
            {
                EnemyDamege(col);
                StartHitEffect(hitPos ,false);
            }
        }

    }

    // ダメージ
    public void PlayerDamege(GameObject obj)
    {
        obj.gameObject.GetComponent<PlayerManager>().SetDamege = damage;
        gameManager.uiManager.HitDamege();
    }
    public void EnemyDamege(GameObject obj)
    {
        obj.transform.parent.gameObject.GetComponent<Enemy>().SetDamege = damage;
    }
    public void DamegeCheck(GameObject obj)
    {
        obj.gameObject.GetComponent<DamegeCheck>().SetDamege = damage;
    }

    private void StartHitEffect(Vector3 pos ,bool b)
    {
        if (!b)pos = new Vector3(pos.x, pos.y + 0.5f, pos.z);
        GameObject fx = Instantiate(hitFx, pos ,Quaternion.identity);
        hitPos = Vector3.zero;
        Destroy(fx, 2);
    }


    // ダメージの変更
    public int SetiingDamege
    {
        set
        {
            damage = value;
        }
    }

}
