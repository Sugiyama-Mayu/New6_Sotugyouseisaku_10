using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anim.Nav;

public class EnemyAnim : MonoBehaviour
{
    private Enemy enemy;
    private Animator animator;
    private NavController navController;
    private EnemyNavMesh enemyNav;

    [SerializeField] private WeaponDamage damage;

    [SerializeField] private Collider swordCol;

    [SerializeField]private Avatar[] avatars;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        navController = GetComponent<NavController>();
        enemyNav = GetComponent<EnemyNavMesh>();
        animator.avatar = avatars[0];
    }

    // アニメーション
    // 攻撃開始
    public void SetAttackTrigger(int meetAttack)
    {
        enemyNav.SetIsAiStateRunning = true;
        if (meetAttack == 1)
        {
            animator.SetTrigger("Attack");
        }
        else if (meetAttack == 2)
        {
            animator.SetTrigger("TAttack");
        }
    }
    // 攻撃リセット
    public void SetResetAttackTrigger()
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("TAttack");
        animator.avatar = avatars[0];
    }

    public void StartDamegeCol()
    {
        swordCol.enabled = true;
    }
    public void EndDamegeCol()
    {
        swordCol.enabled = false;
    }

    public void SetiingDamege(int i)
    {
        damage.SetiingDamege = i;
    }

    public void AnimAvatar(int i)
    {
        animator.avatar = avatars[i];
    }


    public void AnimSpeed(int i)
    {

        navController.GetSetSpeed = enemy.GetMoveSpeed(i);
        if (i == 0)
        {
            enemyNav.SetIsAiStateRunning = false;
            animator.avatar = avatars[0];
        }

    }

}
