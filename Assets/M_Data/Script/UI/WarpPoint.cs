using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarpPoint : MonoBehaviour
{
    private Transform playerPos;
    [SerializeField] private RingSound ringSound;

    [Header("アイコン")]
    [SerializeField] private RectTransform warpIcon;
    [SerializeField] private Vector2[] iconPos;
    [Header("ボタン")]
    [SerializeField] private Image[] buttonImage;
    [SerializeField] private Sprite[] buttonSprite; // 0:白 1:灰 2:黒

    [Header("ワープポイント")]
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

    // 使えるワープの判断
    public void WarpOpen()
    {
        int i = 0;

        for (i = 0; i < buttonImage.Length; i++)
        {
            //すべて消す
            buttonImage[i].sprite = buttonSprite[1];
        }
        warpIcon.localPosition = iconPos[warpNum];
        buttonImage[warpNum].sprite = buttonSprite[0];

    }

    public Transform[] GetChildren(Transform parent)
    {
        // 子オブジェクトを格納する配列作成
        var children = new Transform[parent.childCount];
        var childIndex = 0;

        // 子オブジェクトを順番に配列に格納
        foreach (Transform child in parent)
        {
            children[childIndex] = child;
            childIndex++;
        }

        // 子オブジェクトが格納された配列
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
            //すべて消す
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
