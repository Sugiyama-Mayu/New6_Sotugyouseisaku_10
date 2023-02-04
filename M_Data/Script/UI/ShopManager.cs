using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [Header("アイテムリスト")]
    [SerializeField] private GameObject itemList;
    [SerializeField] private List<GameObject> shopItem;
    [SerializeField] private int listCount;
    [SerializeField] private float buttonInterval;

    [Header("スクロール")]
    private float scroll;
    private float minScroll;
    private float maxScroll;
    [SerializeField] private bool canScroll;
    [SerializeField] private float scrollSpeed = 3.5f;


    [SerializeField] private GameObject prefabButton;


    private void Awake()
    {
        ItemListCreate();

    }
    // Start is called before the first frame update
    void Start()
    {
        minScroll = scroll = 420.0f;

        // スクロールさせるか
        if (7 < shopItem.Count)
        {
            maxScroll = buttonInterval * (shopItem.Count - 7) + minScroll; // 
            canScroll = true;
        }
        else 
        {
            maxScroll = minScroll;
            canScroll = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && minScroll < scroll && canScroll)
        {
            ScrollUp();
        }
        if (Input.GetKey(KeyCode.DownArrow) && scroll < maxScroll && canScroll)
        {
            ScrollDown();
        }
    }

    // アイテムリストの生成
    private void ItemListCreate()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(prefabButton);
            // オブジェクト生成
            shopItem.Add(obj);
            // 座標設定
            shopItem[i].transform.SetParent(itemList.transform, false);
            shopItem[i].transform.localPosition = new Vector3(0, -i * buttonInterval ,0);
            // 数値入力

            // 画像表示

        }
    }

    public void ScrollUp()
    {
        if (minScroll < scroll && canScroll)
        {
            scroll -= scrollSpeed;
            itemList.transform.localPosition = new Vector3(0, scroll, 0);
        }

    }

    public void ScrollDown()
    {
        if (scroll < maxScroll && canScroll)
        {
            scroll += scrollSpeed;
            itemList.transform.localPosition = new Vector3(0, scroll, 0);
        }
    }

    // 購入処理
    public void ItemBuy()
    {

    }
}
