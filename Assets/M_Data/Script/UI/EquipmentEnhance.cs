using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Text.RegularExpressions;

public class EquipmentEnhance :MonoBehaviour
{
    public EquimentManager equimentManager;

    [Header("武器画像")]
    [SerializeField] private Image weaponImage;
    [SerializeField] private Sprite[] weaponSprites;

    [Header("アイテム画像")]
    [SerializeField] private Image[] itemImage;
    [SerializeField] private TextMeshProUGUI[] itemQuantity;
    [SerializeField] private Sprite[] itemSprite;

    [Header("ボタン画像")]
    [SerializeField] private Image[] buttonImage;
    [SerializeField] private Sprite[] buttonSprite;
    [SerializeField] private TextMeshProUGUI statasText;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private Transform parent;
    [SerializeField]private UIButton[] button;

    private int id =10;
    private int num;
    private bool canEnhance;
    [SerializeField]private bool wait;

    private static readonly Regex regex = new Regex("[^0-9]");

    // Start is called before the first frame update
    void Start()
    { 
        id = 10;
        for (int i = 0; i < button.Length; i++)
        {
            buttonImage[i].sprite = buttonSprite[1];
        }
        button = GetChildren(parent);

    }

    // ウェポンアップグレード
    public void WeaponUpgrade()
    {
        // 素材があるか判定
        if (!canEnhance || wait) return;
        wait = true;
        int i = id / 10;
        int j = id % 10;
        //Debug.Log(j + " : " + j);
        if (equimentManager.GetItem(i, 0) == null) return;
        StartCoroutine(ItemDBUpdate());
        switch (i)
        {
            case 1:
                equimentManager.WeaponUpgrade(0, j);
                break;
            case 2:
                equimentManager.WeaponUpgrade(1, j);
                break;
            case 3:
                equimentManager.ArmorUpgrade(0, j);
                break;
            case 4:
                equimentManager.ArmorUpgrade(1, j);
                break;
            case 5:
                equimentManager.PickUpgrade();
                break;
            default:
                Debug.Log("範囲外" + id);
                return;
        }

       // StartCoroutine(ItemDBUpdate1());
    }

    public void NumUpDown(bool b)
    {
        if (wait) return;
        if (b)
        {
            num++;
            if (num > button.Length - 1) num = 0;
        }
        else
        {
            num--;
            if (num < 0) num = button.Length - 1;
        }
        canEnhance = false;
        id = button[num].GetNum;
        ImageUpdate();
        EquipmentImage();
    }

    // 画像更新 
    private void ImageUpdate()
    {
        // 武器画像
        if (weaponSprites[num] != null)
            weaponImage.sprite = weaponSprites[num];

        // ボタン画像
        for (int i = 0; i < button.Length; i++)
        {
            buttonImage[i].sprite = buttonSprite[1];
        }
        buttonImage[num].sprite = buttonSprite[0];

        // アイテム画像 + 強化判定
        itemImageUpdate();
    }

    // アイテム画像表示 + 強化できるか
    private void itemImageUpdate()
    {
        // 初期化
        string[] str;
        itemImage[0].sprite = null;
        itemImage[1].sprite = null;
        itemQuantity[0].text = 0 + " / " + 0;
        itemQuantity[1].text = 0 + " / " + 0;

        // 必要アイテム取得
        if (equimentManager.GetItem(id / 10, 0) == null) return;
        str = equimentManager.GetItem(id / 10, 0);
        //Debug.Log(id / 10);

        bool judge = true;
        for (int i = 0; i < str.Length; i++)
        {
            // 必要アイテムのデータ変換 例:骨10
            string value = regex.Replace(str[i], string.Empty);
            string name = str[i].Replace(value, "");
            //Debug.Log(str[i] + " 数値:" + value + ", 名前:" + name);

            // 文字表示
            int itemh = int.Parse(value);
            int haveItem = equimentManager.GetSelectItemDataBase(name);
            itemQuantity[i].text = haveItem + " / " + value;


            // 画像表示
            itemImage[i].sprite = itemSprite[equimentManager.GetSelectItemDataName(name)];

            // 強化できるか（一度でもfalseなら強化できない）
            if (haveItem <= itemh)
            {
                if (judge) judge = false;
                //Debug.Log(name + "の素材が足りません");
            }

        }
        if (judge) canEnhance = true;
        //Debug.Log(canEnhance);

    }

    // 情報更新（数値、画像）
    private void EquipmentImage()
    {
        int i = id / 10;
        int j = id % 10;

        string level = equimentManager.GetLevel(i);
        if (level == "0") levelText.text = "";
        else levelText.text = "+" + level;

        //equimentManager.GetItemDataBase();

        if (i != 5)
        {   // 数値取得（攻撃・防御）
            int[] a = equimentManager.GetState(i, j);
            if (a[0] != a[1])
            {
                string str = a[0].ToString() + "→" + a[1].ToString();
                statasText.text = str;
            }
            else
            {   // レベル最大
                string str = a[0].ToString();
                statasText.text = str;
            }
        }
        else
        {
            string str = "Level:" + level;
            statasText.text = str;
        }
    }
    public UIButton[] GetChildren(Transform parent)
    {
        // 子オブジェクトを格納する配列作成
        var children = new Transform[parent.childCount];
        var children1 = new UIButton[parent.childCount];
        var childIndex = 0;

        // 子オブジェクトを順番に配列に格納
        foreach (Transform child in parent)
        {
            children[childIndex] = child;
            children1[childIndex] = children[childIndex].GetComponent<UIButton>();
            childIndex++;
        }

        // 子オブジェクトが格納された配列
        return children1;
    }

    public void OpenUI()
    {
        EquipmentImage();
        equimentManager.GetItemDataBase();
        Invoke("ImageUpdate", 0.02f);

    }

    private IEnumerator ItemDBUpdate()
    {
        string[] str;
        str = equimentManager.GetItem(id / 10, 0);
        Debug.Log(str[0]+str[1]);
        for (int num = 0; num < str.Length; num++)
        {
            // 必要アイテムのデータ変換 例:骨10
            string value = regex.Replace(str[num], string.Empty);
            string name = str[num].Replace(value, "");
             Debug.Log(str[num] + " 数値:" + value + ", 名前:" + name);
           // equimentManager.SetMaterialNum(name, int.Parse(value));
            equimentManager.connectionFile.SetMaterialNum(false, name, int.Parse(value));
        }
        yield return new WaitForSeconds(1f);
        equimentManager.SaveLevel();
        yield return new WaitForSeconds(1f);

        EquipmentImage();
        itemImageUpdate();
        wait = false;

    }
}
