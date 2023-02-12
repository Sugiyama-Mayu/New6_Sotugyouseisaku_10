using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// M.S 
// �K�̕�΃Q�b�g�X�N���v�g
public class GetJewel : MonoBehaviour
{
    [SerializeField] private ShrineClearScript shrineClearScript;
    [SerializeField] private ShrineWarpPoint shrineWarpPoint;
    [SerializeField] private ConnectionFile connectionFile;
    [SerializeField] private ShrineText shrineText;
    [SerializeField] private RingSound ringSound;
    [SerializeField] private int jewelNum;       //�l����ΐ�
    [SerializeField] private int shirineJewelId; //�KID�ԍ�
    private string writeStr = "";
    private void Start()
    {
        // �KID�𑼂̃X�N���v�g�ɓn��
        if (shirineJewelId > 0)
        {
            shrineClearScript.SetShrineNum(shirineJewelId);
            shrineWarpPoint.SetText(shirineJewelId);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // ��΂Ƀv���C���[���G�ꂽ���΂��Q�b�g
        if (other.name == "PlayerRoot" || other.name == "PlayerRootXR")
        {
            // �K�N���A�t���O�𗧂Ă�
            shrineClearScript.SetGetJewelFlag(true);
            // �f�[�^�x�[�X�ɏ������ޕ�����̍쐬
            switch (shirineJewelId)
            {
                case 401:
                    writeStr = shirineJewelId.ToString() + "," + "�E���̏�" + "," 
                        + "�E����Ղ𐧔e������" + "," + "1" + "\n";
                    break;
                case 402:
                    writeStr = shirineJewelId.ToString() + "," + "�N���@�V���̏�" + ","
                        + "�N���@�V����Ղ𐧔e������" + "," + "1" + "\n";
                    break;
                case 403:
                    writeStr = shirineJewelId.ToString() + "," + "�g�[���̏�" + ","
                        + "�g�[����Ղ𐧔e������" + "," + "1" + "\n";
                    break;
                case 404:
                    writeStr = shirineJewelId.ToString() + "," + "�Q���Z�~�̏�" + ","
                        + "�Q���Z�~��Ղ𐧔e������" + "," + "1" + "\n";
                    break;
                case 405:
                    writeStr = shirineJewelId.ToString() + "," + "�X���g�̏�" + ","
                        + "�X���g��Ղ𐧔e������" + "," + "1" + "\n";
                    break;
                case 406:
                    writeStr = shirineJewelId.ToString() + "," + "�e���[���̏�" + ","
                        + "�e���[����Ղ𐧔e������" + "," + "1" + "\n";
                    break;
                case 407:
                    writeStr = shirineJewelId.ToString() + "," + "���L�̏�" + ","
                        + "���L��Ղ𐧔e������" + "," + "1" + "\n";
                    break;
            }
            shrineText.shrineClearText.gameObject.SetActive(true); //�K�N���A�e�L�X�g�̕\��
            shrineWarpPoint.SetGetJewelFlag(true);              //��Ύ擾�t���O�𗧂Ă�
            connectionFile.WriteFile(shirineJewelId, writeStr); //�e�L�X�g�t�@�C���ɏ�������
            Destroy(this.gameObject); // ��΂�����
            ringSound.RingSE(19);
            jewelNum++;
        }
    } 
}
