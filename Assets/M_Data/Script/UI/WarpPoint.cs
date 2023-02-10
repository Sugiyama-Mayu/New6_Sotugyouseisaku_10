using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarpPoint : MonoBehaviour
{
    private Transform playerPos;
    [SerializeField] private RingSound ringSound;

    [Header("�A�C�R��")]
    [SerializeField] private RectTransform warpIcon;
    [SerializeField] private Vector2[] iconPos;
    [Header("�{�^��")]
    [SerializeField] private Image[] buttonImage;
    [SerializeField] private Sprite[] buttonSprite; // 0:�� 1:�D 2:��

    [Header("���[�v�|�C���g")]
    [SerializeField] private Transform waprParent;
    [SerializeField] private Transform[] warpPoint;
    [SerializeField] private bool[] flag;
    [SerializeField] private int warpNum;
    [SerializeField] private int warpMaxNum;

    private void Start()
    {
        warpNum = 0;
        buttonImage[warpNum].sprite = buttonSprite[0];
        warpPoint = new Transform[warpMaxNum];

        GameObject playerObj = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerObj();
        playerPos = playerObj.transform;
        waprParent = GameObject.Find("WarpPoint").transform;
        warpPoint = GetChildren(waprParent);
        warpMaxNum = warpPoint.Length - 1;
    }


    public void PlayerWarp()
    {
        if (warpNum == 0) ringSound.RingBGM(3);
        else ringSound.RingBGM(1);
        Vector3 warpPos =  new Vector3 (warpPoint[warpNum].position.x, warpPoint[warpNum].position.y + 0.5f, warpPoint[warpNum].position.z - 3f);
        playerPos.position = warpPos;
        warpNum = 0;
    }

    // �g���郏�[�v�̔��f
    public void WarpOpen()
    {
        int i = 0;

        for (i = 0; i < buttonImage.Length; i++)
        {
            //���ׂď���
            buttonImage[i].sprite = buttonSprite[1];
        }
        warpIcon.localPosition = iconPos[warpNum];
        buttonImage[warpNum].sprite = buttonSprite[0];

    }

    public Transform[] GetChildren(Transform parent)
    {
        // �q�I�u�W�F�N�g���i�[����z��쐬
        var children = new Transform[parent.childCount];
        var childIndex = 0;

        // �q�I�u�W�F�N�g�����Ԃɔz��Ɋi�[
        foreach (Transform child in parent)
        {
            children[childIndex] = child;
            childIndex++;
        }

        // �q�I�u�W�F�N�g���i�[���ꂽ�z��
        return children;
    }

    public void NumUpDown(bool b)
    {

        if (b)
        {
            warpNum++;
            if (warpNum > warpMaxNum)  warpNum = 0;
        }
        else
        {
            warpNum--;
            if (warpNum < 0) warpNum = warpMaxNum;
        }
        for (int i = 0; i < buttonImage.Length; i++)
        {
            //���ׂď���
                buttonImage[i].sprite = buttonSprite[1];
        }
        warpIcon.localPosition = iconPos[warpNum];
        buttonImage[warpNum].sprite = buttonSprite[0];

    }

    public bool GetBool(int i)
    {
        return flag[i];
    }

    public void SetFlag(int i)
    {
        flag[i] = true;
    }

    public int GetSetSpriteNum
    {
        set { warpNum = value; }
        get { return warpNum; }
    }

}
