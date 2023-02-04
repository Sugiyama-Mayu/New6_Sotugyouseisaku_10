using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S �G�l�~�[�Ɩ�����������̏���(�X�^���A�R����)
// ���̃X�N���v�g�̐�������J�n
public class EnemyArrowCollEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem FireEnemyCollFX;  //��������������̃G�t�F�N�g
    [SerializeField] private ParticleSystem ElectEnemyCollFX; //�d�C������������̃G�t�F�N�g
    [SerializeField] private float stanTime;     //�d�C�|�ɓ����������̒�~����(���̓X�N���v�g�Őݒ�)
    [SerializeField] private Animator enemyAnim; //�G�l�~�[�̃A�j���[�^�[
    [SerializeField] private GameObject enemyObj; // �G�l�~�[�I�u�W�F
    private float stanCount;  //�X�^�����Ԃ̃J�E���g
    private bool electColl;   //�d�C�|�Փ˃t���O
    private float restartAnimTime; //�ăX�^�[�g�p�̎���
    private string animStateName;  //�~�܂����A�j���[�V�����̖��O
    private Vector3 savePos;       //�ʒu�ۑ�
    private Vector3 saveRotate;    //��]�p�x�̕ۑ�
    private GameObject collArrow;  //���������� 

    private void Start()
    {
        stanTime = 100.0f;
        electColl = false;
    }
    private void Update()
    {
        // �d�C��̐ڐG���������
        if (electColl == true)
        {
            // �X�^�����Ԃ𑫂��A�ΏۃG�l�~�[���~(�ʒu�A�p�x�̕ۑ�)
            stanCount++;
            enemyObj.transform.position = savePos;
            enemyObj.transform.localEulerAngles = saveRotate;
        }
        // �X�^�����Ԃ��o���Ă�����
        if (stanTime <= stanCount && electColl == true)
        {
            stanCount = 0; //�X�^�����ԏ�����
            enemyAnim.enabled = true; //�A�j���[�V������L��
            enemyObj.GetComponent<EnemyNavMesh>().enabled = true; //�ړ��X�N���v�g�L��
            electColl = false; 
            Destroy(collArrow); //�������
            //enemyAnim.Play(animStateName, 0, restartAnimTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // ������������ǂ���
        if (collision.gameObject.layer == 7)
        {
            if (collision.gameObject.tag == "FireObj") //�΂̖�Փ˃G�t�F�N�g
            {
                collArrow = collision.gameObject; //�ڐG������̕ۑ�
                //�G�t�F�N�g�̍Đ�
                ParticleSystem cloneFireFX = Instantiate(FireEnemyCollFX);
                cloneFireFX.transform.position = collision.transform.position;
                cloneFireFX.Play();
                Destroy(cloneFireFX.gameObject, 5.0f);
            }
            else if (collision.gameObject.tag == "ElectObj") //�d�C�̖�Փ˃G�t�F�N�g
            {
                if (collision.gameObject.GetComponent<Arrow>().electArrowStanSwitch == true)
                {
                    collArrow = collision.gameObject; //�ڐG������̕ۑ�
                    enemyObj.GetComponent<EnemyNavMesh>().enabled = false; //�ړ��X�N���v�g�𖳌�
                    enemyAnim.enabled = false; //�A�j���[�V�����𖳌�
                    // ��~�ʒu�̕ۑ�
                    savePos = enemyObj.gameObject.transform.position;
                    saveRotate = enemyObj.gameObject.transform.localEulerAngles;
                    //�d�C��ڐG�t���O�𗧂Ă�
                    collision.gameObject.GetComponent<Arrow>().electArrowStanSwitch = false;
                    electColl = true; 
                    //�G�t�F�N�g�̍Đ�
                    ParticleSystem cloneElectFX = Instantiate(ElectEnemyCollFX);
                    cloneElectFX.transform.position = collision.transform.position;
                    cloneElectFX.Play();
                    Destroy(cloneElectFX.gameObject, 5.0f);
                    //restartAnimTime = enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    /*if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree"))
                    {
                        animStateName = "Blend Tree";
                    }
                    else if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("UD_infantry_07_attack_A"))
                    {
                        animStateName = "UD_infantry_07_attack_A";
                    }
                    else if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                    {
                        animStateName = "attack";
                    }
                    else if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("attack0"))
                    {
                        animStateName = "attack0";
                    }
                    else if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
                    {
                        animStateName = "attack1";
                    }*/
                }
            }
        }
    }
}
