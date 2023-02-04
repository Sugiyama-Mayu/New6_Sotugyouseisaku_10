using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private Enemy enemy;
    private EnemyNavMesh enemyNav;

    [SerializeField] private GameObject throwObjPre;
    [SerializeField] private Transform targetObj;
    [SerializeField] private Transform throwPoint;

    [SerializeField] private float throwAngle;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyNav = GetComponent<EnemyNavMesh>();
        targetObj = null;
    }

    public void ThrowingBall()
    {
        if (throwObjPre != null && targetObj != null && throwPoint != null)
        {
            // オブジェクトの生成
            GameObject ball = Instantiate(throwObjPre, throwPoint.position, Quaternion.identity);
            if(enemyNav.GetSetFellowMode == false)
            {
                ball.layer = LayerMask.NameToLayer("EnemyWeapon");
            }
            else
            {
                ball.layer = LayerMask.NameToLayer("PlayerWeapon");
            }

            // 標的の座標
            Vector3 targetPosition = targetObj.position;

            // 射出角度
            float angle = throwAngle;

            // 射出速度を算出
            Vector3 velocity = CalculateVelocity(transform.position, targetPosition, angle);
            // 射出
            Rigidbody rid = ball.GetComponent<Rigidbody>();
            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);
            Destroy(ball, 3);
        }
        else
        {
            throw new System.Exception("射出するオブジェクトまたは標的のオブジェクトが未設定です。");
            
        }
    }

    /// <summary>
    /// 標的に命中する射出速度の計算
    /// </summary>
    /// <param name="pointA">射出開始座標</param>
    /// <param name="pointB">標的の座標</param>
    /// <returns>射出速度</returns>
    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {
        // 射出角をラジアンに変換
        float rad = angle * Mathf.PI / 180;

        // 水平方向の距離x
        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));

        // 垂直方向の距離y
        float y = pointA.y - pointB.y;

        // 斜方投射の公式を初速度について解く
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            // 条件を満たす初速を算出できなければVector3.zeroを返す
            return Vector3.zero;
        }
        else
        {
            return new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed;
        }
    }

    public Transform SetTargetObj
    {
        set 
        { 
            targetObj = value;
            Debug.Log(value);
        }
    }
}
