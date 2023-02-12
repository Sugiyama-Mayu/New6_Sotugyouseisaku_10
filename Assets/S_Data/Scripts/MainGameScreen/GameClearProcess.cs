using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// M.S �Q�[���N���A����
public class GameClearProcess : MonoBehaviour
{
    [SerializeField] private ConnectionFile connectionFile;
    [SerializeField] private SaveDataFile saveDataFile;
    [SerializeField] private GameObject GameClearObj;   //�Q�[���N���A�I�u�W�F�N�g
    [SerializeField] private GameObject gameClearImage; //�Q�[���N���A�C���[�W
    private float nowAlpha;
    private void Start()
    {
        nowAlpha = 1.0f;
    }
    void Update()
    {
        //�Q�[���N���A��������x�����Ă��Ȃ��A�S�Ă��K���N���A���Ă�����
        //�Q�[���N���A�e�L�X�g�̕\��
        if (saveDataFile.doneGameClear == false)
        {
            if (connectionFile.GetClearShrineNum() >= 7)
            {
                GameObject ringObj = GameObject.Find("SoundObj");
                ringObj.GetComponent<RingSound>().RingSE(2);
                saveDataFile.doneGameClear = true;
                saveDataFile.WriteSaveData();
                StartCoroutine("DrawGameClearText");
            }
        }
    }
    //�Q�[���N���A�e�L�X�g�̕\��
    IEnumerator DrawGameClearText()
    {
        //�摜���A�N�e�B�u
        GameClearObj.gameObject.SetActive(true);
        for (int i = 0; i < 100; i++)
        {
            //�A���t�@�����X�Ɍ��炵�ăC���[�W�ɐݒ�
            nowAlpha = nowAlpha - 0.01f;
            gameClearImage.GetComponent<Image>().color = new Color(gameClearImage.GetComponent<Image>().color.r, gameClearImage.GetComponent<Image>().color.g,
                gameClearImage.GetComponent<Image>().color.b, nowAlpha);
            //�A���t�@��0�ȉ��Ȃ��
            if(nowAlpha <= 0.0f)
            {
                GameClearObj.gameObject.SetActive(false); //�摜���\��
                yield break;
            }
            yield return new WaitForSeconds(0.01f); //�����҂�
        }
    }
}
