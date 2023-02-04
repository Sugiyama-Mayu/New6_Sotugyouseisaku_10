using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllScripts : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Anime anime;
    [SerializeField] private SelectionItem selectionItem;
    [SerializeField] private ItemStorageDate[] itemStorage;

    [SerializeField] GameObject RingCanvas;
    [SerializeField] GameObject RingCommand;
    /*
    [SerializeField] GameObject Equipment;
    [SerializeField] GameObject Belongings;
    [SerializeField] GameObject Settings;
    */


    public Takap.Samples.RingCmdControl RingCmd;
    //public Takap.Samples.Equipment EqRing;
    //public Takap.Samples.Belongings BeRing;
    //public Takap.Samples.OptionSettings SeRing;

    public int SetRingCount;
    public int RotCount;
    public int XScrollCount;public int YScrollCount;
    public int ItemKindId;    // アイテム種類ID
    public int MenuCou;

    void Start()
    {
        RingCanvas.SetActive(false);
        MenuCou = 0;
    }
    //左クリック
    public void MouseLeftClick()
    {
        if (RingCanvas.activeSelf == false) return;
        int posNum = selectionItem.GetPosNum;
        if (MenuCou == 0  )
        {
            switch (RingCmd.n_remainder)
            {
                case 0:
                    ItemKindId = 1;
                    break;
                case 1:
                    ItemKindId = 8;
                    break;
                case 2:
                    ItemKindId = 7;
                    break;
                case 3:
                    ItemKindId = 6;
                    break;
                case 4:
                    ItemKindId = 5;
                    break;
                case 5:
                    ItemKindId = 4;
                    break;
                case 6:
                    ItemKindId = 3;
                    break;
                case 7:
                    ItemKindId = 2;
                    break;
                case -1:
                    ItemKindId = 2;
                    break;
                case -2:
                    ItemKindId = 3;
                    break;
                case -3:
                    ItemKindId = 4;
                    break;
                case -4:
                    ItemKindId = 5;
                    break;
                case -5:
                    ItemKindId = 6;
                    break;
                case -6:
                    ItemKindId = 7;
                    break;
                case -7:
                    ItemKindId = 8;
                    break;
            }
            HideRing();
            anime.SetListBool = true;
            MenuCou = 1;
            for(int i = 0; i < itemStorage.Length; i++)
            {
                itemStorage[i].ListUpdate();
            }
            itemStorage[posNum].ItemUpdate();
        }
        else if(MenuCou == 1)
        {
           // Debug.Log("Useitem " + "PosNum:" + posNum + " KintId:"+ ItemKindId);
            
            if (ItemKindId == 1 && -1 < posNum && posNum < 3)
            {
                gameManager.playerManager.DragItem(posNum);
                Debug.Log(posNum);
                
                // item消費処理
            }
        }

    }

    //右クリック
    public void MouseRightClick()
    {
        if (MenuCou == 1 && RingCanvas.activeSelf == true)
        {
            Invoke("ResetRing", 2.0f);
            anime.SetListBool = false;
            MenuCou = 0;
        }

    }

    //ホイールクリック
    public void OpenCloseMenu()
    {
        if (MenuCou == 0)
        {
            if (SetRingCount % 2 == 0)
            {
                SetRingCommand();
            }
            if (SetRingCount % 2 != 0)
            {
                HideRingCommand();
            }
            SetRingCount++;
        }
    }
    // マウススクロール
    public void MouseScroll(float scroll)
    {
 //       var scroll = Input.GetAxis("Mouse ScrollWheel");
        if(MenuCou == 0)
        {
            if (scroll > 0)
            {
                XScrollCount++;
                if (XScrollCount > 3)
                {
                    RingCmd.TurnRight();
                    Debug.Log("右回転");
                    XScrollCount = 0;
                }
            }else if (scroll < 0)
            {
                YScrollCount++;
                if (YScrollCount > 3)
                {
                    RingCmd.TurnLeft();
                    Debug.Log("左回転");
                    YScrollCount = 0;
                }
            }
        }else if (MenuCou == 1)
        {
            selectionItem.ItemScroll(scroll);
            itemStorage[selectionItem.GetPosNum].ItemUpdate();
        }
    }

    public void StickScroll(Vector2 scroll) // VR
    {
        //       var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (MenuCou == 0)
        {
            if (scroll.x > 0.65)
            {
                RingCmd.TurnRight();
            }
            else if (scroll.x < 0.65)
            {
                RingCmd.TurnLeft();
            }
        }
        else if (MenuCou == 1)
        {
            selectionItem.ItemScroll(scroll.y);
            itemStorage[selectionItem.GetPosNum].ItemUpdate();
        }
    }


    public void SetRingCommand()
    {
        RingCanvas.SetActive(true);
        RingCmd.Init();
        RotCount = 0;
        Debug.Log("メニュー表示");
    }
    public void HideRingCommand()
    {
        RingCanvas.SetActive(false);
        RingCommand.SetActive(true);
        gameManager.SetiingActionMap(0);
        Debug.Log("メニュー非表示");
    }


    public void ResetRing()
    {
        RingCommand.SetActive(true);
    }

    public void HideRing()
    {
        RingCommand.SetActive(false);
    }

    public bool RingCanvasActveSelf
    {
        get { return RingCanvas.activeSelf; }
    }

}
