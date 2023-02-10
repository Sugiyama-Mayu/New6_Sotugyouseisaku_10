using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S ���̊Ǘ�
public class RingSound : MonoBehaviour
{
    //BGM BGM�ԍ�0�`5
    [SerializeField] private AudioClip titleBGM;     //�^�C�g��
    [SerializeField] private AudioClip fieldBGM;     //�t�B�[���h
    [SerializeField] private AudioClip shrineBGM;    //�K
    [SerializeField] private AudioClip villageBGM;   //��
    [SerializeField] private AudioClip gameClearBGM; //�Q�[���N���A
    [SerializeField] private AudioClip gameOverBGM;  //�Q�[���I�[�o�[
    //SE
     //���� SE�ԍ��ォ��1�`18
    [SerializeField] private AudioClip clickButton; //�{�^���N���b�N(�Q�[�����[�h�I��)
    [SerializeField] private AudioClip digOreSE;    //�z�΂��@��
    [SerializeField] private AudioClip gameClearSE; //�Q�[���N���A��
    //�A�C�e��
    [SerializeField] private AudioClip useItemSE;       //�A�C�e���g�p
    [SerializeField] private AudioClip enterTheStoreSE; //���X��
    [SerializeField] private AudioClip buyOrSellSE;     //����       
     //�N�G�X�g
    [SerializeField] private AudioClip clearQuestSE;         //�N�G�X�g�N���A
    [SerializeField] private AudioClip talkQuestSE;          //�N�G�X�g��b
    [SerializeField] private AudioClip orderReceivedQuestSE; //�N�G�X�g��
    //��ԕω�
    [SerializeField] private AudioClip damegeSE; //�_���[�W
    [SerializeField] private AudioClip diedSE;   //���S  
     //����
    [SerializeField] private AudioClip fireArrowSE;   //���̖�
    [SerializeField] private AudioClip electArrowSE;  //�d�C�̖�
    [SerializeField] private AudioClip arrowAttackSE; //�|�U��
    [SerializeField] private AudioClip arrowDrawSE;   //�|���i��
    [SerializeField] private AudioClip swordAttackSE; //���U��
     //�K
    [SerializeField] private AudioClip shrineElectGimmickSE; //�K�̓d�C�M�~�b�N
    [SerializeField] private AudioClip shrineFireGimmickSE;  //�K�̉��M�~�b�N
    [SerializeField] private AudioClip ShrineGimmickClearSE; //�M�~�b�N�N���A
    [SerializeField] private AudioClip ShrineGetJewelSE;     //�؂���ɓ��ꂽ��

    public AudioSource audioSorceArrow;
    public AudioSource audioSordeDrawShootArrow;
    AudioSource audioSorce;
    private void Start()
    {
        audioSorce = this.GetComponent<AudioSource>();
        audioSorceArrow = audioSorceArrow.GetComponent<AudioSource>();
        audioSordeDrawShootArrow = audioSordeDrawShootArrow.GetComponent<AudioSource>();
    }
    public void RingBGM(int audioNumber)
    {
        switch (audioNumber)
        {
            case 0:
                audioSorce.clip = titleBGM;
                break;
            case 1:
                audioSorce.clip = fieldBGM;
                break;
            case 2:
                audioSorce.clip = shrineBGM;
                break;
            case 3:
                audioSorce.clip = villageBGM;
                break;
            case 5:
                audioSorce.clip = gameOverBGM;
                break;
        }
        audioSorce.Play();
    }
    public void RingSE(int audioNumber)
    {
        switch (audioNumber)
        {
            case 0: //�{�^���N���b�N
                audioSorce.PlayOneShot(clickButton);
                break;
            case 1: //�̌@
                audioSorce.PlayOneShot(digOreSE);
                break;
            case 2: //�Q�[���N���A��
                audioSorce.PlayOneShot(gameClearSE);
                break;
            case 3: //�A�C�e���g�p��
                audioSorce.PlayOneShot(useItemSE);
                break;
            case 4: //�V���b�v���X��
                audioSorce.PlayOneShot(enterTheStoreSE);
                break;
            case 5: //������
                audioSorce.PlayOneShot(buyOrSellSE);
                break;
            case 6: //�N�G�X�g�N���A
                audioSorce.PlayOneShot(clearQuestSE);
                break;
            case 7: //�N�G�X�g��b
                audioSorce.PlayOneShot(talkQuestSE);
                break;
            case 8: //�N�G�X�g��
                audioSorce.PlayOneShot(orderReceivedQuestSE);
                break;
            case 9: //�G�����_���[�W
                audioSorce.PlayOneShot(damegeSE);
                break;
            case 10: //�G�������S
                audioSorce.PlayOneShot(diedSE);
                break;
            case 11: // ���̖�
                audioSorceArrow.PlayOneShot(fireArrowSE);
                break;
            case 12: // �d�C�̖�
                audioSorceArrow.PlayOneShot(electArrowSE);
                break;
            case 13: // �|�U��
                audioSordeDrawShootArrow.PlayOneShot(arrowAttackSE);
                break;
            case 14: // �|���i��
                audioSordeDrawShootArrow.PlayOneShot(arrowDrawSE);
                break;
            case 15: // ���U��
                audioSorce.PlayOneShot(swordAttackSE);
                break;
            case 16: // �K(�d�C�M�~�b�N)
                audioSorce.PlayOneShot(shrineElectGimmickSE);
                break;
            case 17: // �K(�R����M�~�b�N)
                audioSorce.PlayOneShot(shrineFireGimmickSE);
                break;
            case 18: // �M�~�b�N�N���A
                audioSorce.PlayOneShot(ShrineGimmickClearSE);
                break;
            case 19: // �؂���ɓ��ꂽ��
                audioSorce.PlayOneShot(ShrineGetJewelSE);
                break;
        }
    }

    public void StopSE(int selectAudioSorce)
    {
        switch (selectAudioSorce)
        {
            case 0:
                audioSorceArrow.Stop();
                break;
            case 1:
                audioSordeDrawShootArrow.Stop();
                break;
        }
        /*switch (audioNumber)
        {
            case 11: // ���̖�
                audioSorceArrow.PlayOneShot(fireArrowSE);
                break;
        }*/
    }
}
