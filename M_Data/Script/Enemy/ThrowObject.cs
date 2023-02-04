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
            // �I�u�W�F�N�g�̐���
            GameObject ball = Instantiate(throwObjPre, throwPoint.position, Quaternion.identity);
            if(enemyNav.GetSetFellowMode == false)
            {
                ball.layer = LayerMask.NameToLayer("EnemyWeapon");
            }
            else
            {
                ball.layer = LayerMask.NameToLayer("PlayerWeapon");
            }

            // �W�I�̍��W
            Vector3 targetPosition = targetObj.position;

            // �ˏo�p�x
            float angle = throwAngle;

            // �ˏo���x���Z�o
            Vector3 velocity = CalculateVelocity(transform.position, targetPosition, angle);
            // �ˏo
            Rigidbody rid = ball.GetComponent<Rigidbody>();
            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);
            Destroy(ball, 3);
        }
        else
        {
            throw new System.Exception("�ˏo����I�u�W�F�N�g�܂��͕W�I�̃I�u�W�F�N�g�����ݒ�ł��B");
            
        }
    }

    /// <summary>
    /// �W�I�ɖ�������ˏo���x�̌v�Z
    /// </summary>
    /// <param name="pointA">�ˏo�J�n���W</param>
    /// <param name="pointB">�W�I�̍��W</param>
    /// <returns>�ˏo���x</returns>
    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {
        // �ˏo�p�����W�A���ɕϊ�
        float rad = angle * Mathf.PI / 180;

        // ���������̋���x
        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));

        // ���������̋���y
        float y = pointA.y - pointB.y;

        // �Ε����˂̌����������x�ɂ��ĉ���
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            // �����𖞂����������Z�o�ł��Ȃ����Vector3.zero��Ԃ�
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
