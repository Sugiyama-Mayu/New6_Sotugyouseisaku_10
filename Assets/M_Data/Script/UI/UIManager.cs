using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerControl playerControl;
    public GameCursor gameCursor;
    public TalkNPC talkNPC;

    public WarpPoint warpPoint;
    public EquimentManager equimentManager;
    public EquipmentEnhance equipmentEnhance;


    [Header("UIオブジェクト")]
    public GameObject bgUi;
    public GameObject cursorObj;
    public GameObject controlCanvas;
    public GameObject warpCanvas;
    public GameObject textCanvas;
    public GameObject equipmentCanvas;

    private int uiNum = 0;


    public Image DamegeImage;
    public Image SplaterImage;
    public Image ChengeSceneImage;


    [SerializeField] private bool blackout = false;

    private float imageCount;
    private float maxHp;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SceneChange();
    }

    // Start is called before the first frame update
    void Start()
    {
        imageCount = 0;
        maxHp = gameManager.playerManager.GetMaxHp;
        ChengeSceneImage.gameObject.SetActive(true);

        if (gameManager.GetSetXRMode)
        {
            controlCanvas.SetActive(false);
            cursorObj.SetActive(false);
        }
        else
        {
            controlCanvas.SetActive(true);
            cursorObj.SetActive(true);
        }

        uiClose();
    }

    private void FixedUpdate()
    {
        if (imageCount <= 1) { imageCount -= 0.01f; }
        SplaterImage.color = new Vector4(SplaterImage.color.r, SplaterImage.color.g, SplaterImage.color.b, imageCount);
        
        Hp();
        BlackOut();

    }

    private void Hp()
    {
        float hp = gameManager.playerManager.GetHp;
        float f = 1 - hp / maxHp;
        DamegeImage.color = new Vector4(DamegeImage.color.r, DamegeImage.color.g,
                                        DamegeImage.color.b, f);
    }

    public void HitDamege()
    {
        imageCount = 1;
    }

    // 会話開始
    public void talkStart(GameObject obj)
    {
        textCanvas.SetActive(true);
        talkNPC.SetText(obj);
        if (gameManager.GetSetXRMode == false) gameCursor.SetiingCursor();
    }

    // ワープポイント
    public void WapePoint(int i)
    {
        uiNum = 1;
        warpCanvas.SetActive(true);
        warpPoint.SetFlag(i);
        warpPoint.WarpOpen();
        bgUi.SetActive(true);
    }

    // 装備強化
    public void Enhance()
    {
        equipmentCanvas.SetActive(true);
        equipmentEnhance.OpenUI();
        bgUi.SetActive(true);
        uiNum = 2;
    }

    // UI表示
    public void uiOpen()
    {
        controlCanvas.SetActive(false);
        cursorObj.SetActive(false);
    }

    // UI非表示
    public void uiClose()
    {
        if (gameManager.GetSetXRMode == false)
        {
            controlCanvas.SetActive(true);
            cursorObj.SetActive(true);
        }

        uiNum = 0;
        bgUi.SetActive(false);
        warpCanvas.SetActive(false);
        textCanvas.SetActive(false);
        equipmentCanvas.SetActive(false);
    }

    // UIスクロール関数
    public void SetUINum(bool b)
    {
        switch (uiNum)
        {
            case 1:
                warpPoint.NumUpDown(b);
                break;
            case 2:
                equipmentEnhance.NumUpDown(b);
                break;
        }
    }
    // 決定処理
    public void Click()
    {
        switch (uiNum)
        {
            case 1:
                gameManager.ringSound.RingSE(0);
                if (warpPoint.GetBool(warpPoint.GetSetSpriteNum) == true) gameManager.StartWrap(warpPoint);
                break;
            case 2:
                gameManager.ringSound.RingSE(0);
                equipmentEnhance.WeaponUpgrade();
                break;
        }
    }

    public void SceneChange()
    {
        ChengeSceneImage.color = Color.black;
    }

    public void BlackOut()
    {
        if (!blackout)
        {
            if (0 < ChengeSceneImage.color.a)
            {
                ChengeSceneImage.color = new Vector4(0, 0, 0, ChengeSceneImage.color.a - 0.05f);
            }
        }
        else
        {
            if (ChengeSceneImage.color.a < 1)
            {
                ChengeSceneImage.color = new Vector4(0, 0, 0, ChengeSceneImage.color.a + 0.05f);
            }
        }
    }

    /// <summary>
    /// True:黒くする  False:消す
    /// </summary>
    public bool SetBlackOut 
    {
        set { blackout = value; }
    }
}