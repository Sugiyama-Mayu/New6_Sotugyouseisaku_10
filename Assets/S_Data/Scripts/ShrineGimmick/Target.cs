using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// 的ギミックの移動
public class Target : MonoBehaviour
{
    [SerializeField] TargetManager targetManager;
    [SerializeField] bool moveFlag;
    private bool moveDirectionFlag;
    [SerializeField] GameObject plusPos;
    [SerializeField] GameObject minusPos;
    //[SerializeField] GameObject moveObj;
    void Update()
    {
        // 動かす的の場合
        if(moveFlag == true)
        {
            if(this.transform.position.z >= plusPos.transform.position.z)
            {
                moveDirectionFlag = true;
            }
            else if(this.transform.position.z <= minusPos.transform.position.z)
            {
                moveDirectionFlag = false;
            }

            if(moveDirectionFlag == true)
            {
                this.transform.Translate(0.0f, 0.0f, -0.6f);
            }
            else
            {
                this.transform.Translate(0.0f, 0.0f, 0.6f);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            targetManager.hitNum++;
            ParticleSystem effect_c =  Instantiate(targetManager.hitEffect);
            effect_c.transform.position = collision.transform.position;
            effect_c.Play();
            Destroy(effect_c.gameObject, 5.0f);
            this.gameObject.SetActive(false);
        }
    }
}
