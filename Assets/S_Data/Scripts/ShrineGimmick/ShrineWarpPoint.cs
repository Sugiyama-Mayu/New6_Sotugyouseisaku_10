using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// M.S �K�̃��[�v����
public class ShrineWarpPoint : MonoBehaviour
{
    [SerializeField] GameObject warpPos;
    [SerializeField] GameObject player;
    [SerializeField] ShrineText shrineText;
    [SerializeField] RotateBall rotateBall;
    //[SerializeField] private GetJewel getJewel;
    [SerializeField] int warpMode;
    [SerializeField] bool rotateShrineFlag;
    private GameObject shrineTextObj; //�K�����e�L�X�g
    private int shrineNum;   //�KID�ԍ�
    private bool inShrine;   //�K�������t���O
    private float nowAlpha;  //�K�e�L�X�g�����x
    private float nowAlphaC; //�K�N���A�e�L�X�g�����x
    private bool getJewelFlag = false;
    [SerializeField]private GameObject ball;

    //��������
    //��  ���Fint shrineID  �KID�ԍ�
    //�߂�l�F�Ȃ�
    public void SetText(int shrineID)
    {
        nowAlpha = 1.0f;
        nowAlphaC = 1.0f;
        shrineNum = shrineID;
        inShrine = false;
        switch (shrineNum)
        {
            case 401:
                shrineTextObj = shrineText.shrineText1;
                break;
            case 402:
                shrineTextObj = shrineText.shrineText2;
                break;
            case 403:
                shrineTextObj = shrineText.shrineText3;
                break;
            case 404:
                shrineTextObj = shrineText.shrineText4;
                break;
            case 405:
                shrineTextObj = shrineText.shrineText5;
                break;
            case 406:
                shrineTextObj = shrineText.shrineText6;
                break;
            case 407:
                shrineTextObj = shrineText.shrineText7;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject soundObj = GameObject.Find("SoundObj");
        if (other.gameObject.tag == "Player")
        {
            player.transform.position = new Vector3(warpPos.transform.position.x, warpPos.transform.position.y, warpPos.transform.position.z);
            var scr = GameObject.Find("MainSceneObj").GetComponent<MainSceneObj>();
            if(warpMode == 0) //�������[�v
            {
                scr.menuOffMode = true;
                inShrine = true;
                shrineTextObj.SetActive(true);
                soundObj.GetComponent<RingSound>().RingBGM(2);
                if (rotateShrineFlag == true)
                {
                    rotateBall.rotateModeAllow = true; //�ʉ�]���[�h�����ɂ���
                    ball.SetActive(true);
                }
            }
            else //�o�����[�v
            {
                scr.menuOffMode = false;
                soundObj.GetComponent<RingSound>().RingBGM(1);
                if (rotateShrineFlag == true)
                {
                    rotateBall.rotateModeAllow = false; //�ʉ�]���[�h��񋖉ɂ���
                    rotateBall.RotateBallModeOff();     //�ʉ�]���[�h��߂�
                    ball.SetActive(false);
                }
            }

        }
    }
    private void Update()
    {
        //�e�L�X�g�\���R���[�`�����Ăяo��
        if (inShrine == true) {
            nowAlpha = 1.0f; //�����x�̐ݒ�
            StartCoroutine("InvisibleText"); //�K�����e�L�X�g�̕\��
        }
        if (getJewelFlag == true)
        {
            nowAlphaC = 1.0f; //�����x�̐ݒ�
            StartCoroutine("InvisibleClearText"); //�K�N���A�e�L�X�g�̕\��
        }
    }
    //�K�����e�L�X�g�̃R���[�`��
    IEnumerator InvisibleText()
    {
        //���X�ɓ����x�������ĕ\��
        for (int i = 0; i < 50; i++)
        {
            inShrine = false;
            nowAlpha = nowAlpha - 0.02f;
            //�����������x�Ńe�L�X�g��\��
            shrineTextObj.GetComponent<Image>().color
                = new Color(shrineTextObj.GetComponent<Image>().color.r, shrineTextObj.GetComponent<Image>().color.g, shrineTextObj.GetComponent<Image>().color.b,
                nowAlpha);
            //�����Ȃ��
            if (nowAlpha <= 0.0f)
            {
                shrineTextObj.GetComponent<Image>().color = new Color(shrineTextObj.GetComponent<Image>().color.r, shrineTextObj.GetComponent<Image>().color.g, shrineTextObj.GetComponent<Image>().color.b,
                   0.0f);
                shrineTextObj.gameObject.SetActive(false); //�e�L�X�g���\��
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    //�K�N���A�e�L�X�g�̃R���[�`��
    IEnumerator InvisibleClearText()
    {
        //���X�ɓ����x�������ĕ\��
        for (int i = 0; i < 50; i++)
        {
            getJewelFlag = false;
            nowAlphaC = nowAlphaC - 0.02f;
            //�����������x�Ńe�L�X�g��\��
            shrineText.shrineClearText.GetComponent<Image>().color
                = new Color(shrineText.shrineClearText.GetComponent<Image>().color.r, shrineText.shrineClearText.GetComponent<Image>().color.g, shrineText.shrineClearText.GetComponent<Image>().color.b,
                nowAlphaC);
            //�����Ȃ��
            if (nowAlphaC <= 0.0f)
            {
                shrineText.shrineClearText.GetComponent<Image>().color = new Color(shrineText.shrineClearText.GetComponent<Image>().color.r, shrineText.shrineClearText.GetComponent<Image>().color.g, shrineText.shrineClearText.GetComponent<Image>().color.b,
                    0.0f);
                shrineText.shrineClearText.gameObject.SetActive(false); //�e�L�X�g���\��
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    // �K�̐����̃Z�b�^�[
    public void SetShrineNum(int setShirineNum)
    {
        shrineNum = setShirineNum;
    }
    // ��΃Q�b�g�t���O�̃Z�b�^�[
    public void SetGetJewelFlag(bool getJewel)
    {
        getJewelFlag = getJewel;
    }
}
