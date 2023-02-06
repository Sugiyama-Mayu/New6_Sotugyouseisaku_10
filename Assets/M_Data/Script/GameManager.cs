using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    private SwichMode swichMode;

    // データの読み込み
    public DataManager dataRead;
   // public SaveData saveData;

    public UIManager uiManager;
    public TerrainManager terrainManager;
    public PlayerManager playerManager;
    public InputPlayer inputPlayer;
    public InputPlayerVR inputPlayerVR;
    public CreateManager createManager;
    [SerializeField] private ConnectionFile connectionFile;
    

    [Header("ゲームオブジェクト")]
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject pcPhoneObj;
    [SerializeField] private GameObject xrObj;
    [SerializeField] private GameObject titleObj;

    [Header("モード状態")]
    [SerializeField] private bool phoneVrMode;
    [SerializeField] private bool XRMode;

    [Header("スタート座標")]
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Quaternion startRot;

    int frameCount;
    float prevTime;
    float fps;

    bool b = false;
    float timeCount = 0;
    [SerializeField] float refreshCount;
    [SerializeField] int[] materialNum;
    [SerializeField] int[] dragItemNum;

    private void Awake()
    {
        if (GameObject.Find("PlayerRoot") == true)
        {
            playerObj = GameObject.Find("PlayerRoot");
            inputPlayer = playerObj.GetComponentInChildren<InputPlayer>();
            XRMode = false;
        }
        else
        {
            playerObj = GameObject.Find("PlayerRootXR");
            inputPlayerVR = playerObj.GetComponentInChildren<InputPlayerVR>();
            XRMode = true;
        }

        playerManager = playerObj.GetComponent<PlayerManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        connectionFile = GameObject.Find("Connection").GetComponent<ConnectionFile>();
        swichMode = GetComponent<SwichMode>();
        materialNum = new int[8]; // 0:骨 1:皮 2:牙 3:毛皮 4:爪 5:銅 6:銀 7:金
        dragItemNum = new int[4];

    }

    // Start is called before the first frame update
    void Start()
    {
        frameCount = 0;
        prevTime = 0.0f;
        terrainManager.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        DragItemNumUpdate();

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (phoneVrMode)
        {
            FpsCount();
        }

        // モード変更
        if (Keyboard.current.vKey.wasPressedThisFrame && GetSetXRMode == false)
        {
            if (!XRMode) swichMode.StartVR();
            else swichMode.StopVR();
        }
        if(terrainManager.enabled == false && titleObj.activeSelf == false)
        {
            terrainManager.enabled = true;
        }


    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 100;
        if (phoneVrMode) GUILayout.Label(fps.ToString(), style);
    }


    private void FpsCount()
    {
        frameCount++;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= 0.5f)
        {
            fps = frameCount / time;

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }


    public void StartWrap(WarpPoint warpPoint)
    {
        StartCoroutine(Warp(warpPoint));
    }

    public IEnumerator Warp(WarpPoint warpPoint)
    {
        uiManager.SetBlackOut = true;
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
        yield return new WaitForSeconds(0.5f);
        warpPoint.PlayerWarp();
        terrainManager.DistanceComparison();
        uiManager.uiClose();
        yield return new WaitForSeconds(0.2f);
        uiManager.SetBlackOut = false;
        SetiingActionMap(0);

    }

    public IEnumerator Continue()
    {
        Debug.Log("startContinue");
        uiManager.SetBlackOut = true;
        yield return new WaitForSeconds(1.0f);
        playerObj.transform.SetPositionAndRotation(startPos, startRot);
        playerManager.SetMaxHp();
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
        yield return new WaitForSeconds(1.0f);
        uiManager.SetBlackOut = false;

    }

    /// <summary>
    /// カメラの設定
    /// </summary>
    /// <param name="b"></param>
    public void XrCamera(bool b)
    {
        if (b)
        {
            pcPhoneObj.SetActive(false);
            xrObj.SetActive(true);
        }
        else
        {
            pcPhoneObj.SetActive(true);
            xrObj.SetActive(false);
        }
    }

    // 採掘処理
    public void Mining(int level)
    {
        int rand = Random.Range(0, 100);
        int[] count = new int[3];
        Debug.Log(level);
        switch (level)
        {
            case 1:
                if (rand < 50)
                {
                    materialNum[5]++;
                    count[0]++;
                }
                else
                {
                    materialNum[6]++;
                    count[1]++;
                }
                break;
            case 2:
                if (rand < 50)
                {
                    materialNum[5]++;
                    count[0]++;
                }
                else
                {
                    materialNum[6]++;
                    count[1]++;
                }
                rand = Random.Range(0, 100);
                if (0 < rand || rand < 39)
                {
                    materialNum[5]++;
                    count[0]++;
                }
                else if (40 < rand || rand < 79)
                {
                    materialNum[6]++;
                    count[1]++;
                }
                break;
            case 3:
                for (int i = 0; i < 3; i++)
                {
                    rand = Random.Range(0, 100);

                    if (0 < rand || rand < 39)
                    {
                        materialNum[5]++;
                        count[0]++;
                    }
                    else if (40 < rand || rand < 79)
                    {
                        materialNum[6]++;
                        count[1]++;
                    }
                    else
                    {
                        materialNum[7]++;
                        count[2]++;
                    }
                }
                break;
        }
        Debug.Log("銅：" + count[0] + " 銀：" + count[1] + " 金：" + count[2]);
    }



    // 素材キュー
    public void SetMaterial(string itemName, int itemCount)
    {
        b = true;
        switch (itemName)
        {
            case "骨":
                materialNum[0] += itemCount;
                break;
            case "皮":
                materialNum[1] += itemCount;
                break;
            case "牙":
                materialNum[2] += itemCount;
                break;
            case "毛皮":
                materialNum[3] += itemCount;
                break;
            case "爪":
                materialNum[4] += itemCount;
                break;
            case "銅":
                materialNum[5] += itemCount;
                break;
            case "銀":
                materialNum[6] += itemCount;
                break;
            case "金":
                materialNum[7] += itemCount;
                break;
        }
        Debug.Log(itemName + "を" + itemCount + "個をキューに追加");
    }

    // つるはし
    public bool GetTruhasi()
    {
        connectionFile.TranslationDataArray(connectionFile.ReadFile(601, ""), 6);     // 更新
        int num = connectionFile.haveNum;
        bool b;

        if (0 < num)
        {
            b = true;
            Mining(num); // 採取処理
        }
        else b = false;
        return b;
    }

    public void UseDragItem(int num)
    {
        dragItemNum[num]--;
    } 


    // アクションマップ変更
    public void SetiingActionMap(int i)
    {
        if (!XRMode)
        {
            inputPlayer.SetActionMap(i);
        }
        else 
        {
            inputPlayerVR.SetActionMap(i);
        }
    }

    public int[] GetSetMaterial
    {
        get
        {
            return materialNum;
        }
        set
        {
            materialNum = new int[8];
        }
    }

    public int[] GetSetDragItem
    {
        get
        {
            return dragItemNum;
        }
        set
        {
            dragItemNum = new int[4];
        }
    }

    public void DragItemNumUpdate()
    {
        string array = "";
        connectionFile.TranslationDataArray(connectionFile.ReadFile(506, array), 5);
        dragItemNum[0] = connectionFile.haveNum;
        connectionFile.TranslationDataArray(connectionFile.ReadFile(507, array), 5);
        dragItemNum[1] = connectionFile.haveNum;
        connectionFile.TranslationDataArray(connectionFile.ReadFile(508, array), 5);
        dragItemNum[2] = connectionFile.haveNum;
        connectionFile.TranslationDataArray(connectionFile.ReadFile(509, array), 5);
        dragItemNum[3] = connectionFile.haveNum;

    }

    // メニュー用
    public int GetDragItemNum(int id)
    {
        switch (id)
        {
            case 506:
                return dragItemNum[0];
            case 507:
                return dragItemNum[1];
            case 508:
                return dragItemNum[2];
            case 509:
                return dragItemNum[3];
            default:
                return 0;
        }

    }

    public int[] GetDragItemList
    {
        get
        {
            return dragItemNum;
        }
    }



    public bool GetVRMode
    {
        get
        {
            return phoneVrMode;
        }
    }

    public bool GetSetXRMode
    {
        get
        {
            return XRMode;
        }
        set
        {
          //  XrCamera(value);
            XRMode = value;
        }
    }
    public GameObject GetPlayerObj()
    {
        return playerObj;
    }
}
