using UnityEngine;

public class SelectionItem : MonoBehaviour
{
    [SerializeField] GameObject MaskObj;
    Transform SelectMove;

    ControllScripts CS;

    public int n_PosTop = 3;
    public int n_PosUnder = 10;
    public int n_PosNum;
    public int XScrollCount; public int YScrollCount;
    public float movepos = 1;

    public Vector3 pos;
    void Start()
    {
        SelectMove =transform;
        pos = SelectMove.position;
    }

    void FixedUpdate()
    {
        if (MaskObj.transform.localScale.y != 1)
        {
            n_PosNum = 0;
            if (MaskObj.transform.localScale.y <= 0.7f)
            {
                ResetPos();

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("pos" + pos.y);
                Debug.Log("num" + n_PosNum);
            }
        }
    }
    public void ItemScroll(float scroll)
    {
        pos = SelectMove.position;
        if (scroll > 0.7f)
        {
            XScrollCount++;
            if (XScrollCount > 3)
            {
                if (n_PosNum > 0)
                {
                    n_PosNum--;
                }
                if (n_PosNum < 5 && pos.y > n_PosTop)     // テスト数値 315,570 3.124
                {
                    pos.y -= movepos;
                }
                XScrollCount = 0;
            }

        }
        if (scroll < -0.7f)
        {
            YScrollCount++;
            if (YScrollCount > 3)
            {
                if (n_PosNum < 9)
                {
                    n_PosNum++;
                }
                if (n_PosNum > 4 && pos.y < n_PosUnder)     // テスト数値 575,830  9.625
                {
                    pos.y += movepos;

                }
                YScrollCount = 0;
            }

        }
        SelectMove.position = pos;
    }

    public void ResetPos()
    {
        SelectMove.position = new Vector3(pos.x, 0.15f, pos.z);
    }


    public int GetPosNum
    {
        get
        {
            return n_PosNum;
        }
    }
}
