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
    [SerializeField] private ConnectionFile connectionFile;

    public string secneName;

    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject pcPhoneObj;
    [SerializeField] private GameObject xrObj;
    [SerializeField] private GameObject titleObj;

    [SerializeField] private bool phoneVrMode;
    [SerializeField] private bool XRMode;

    int frameCount;
    float prevTime;
    float fps;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        frameCount = 0;
        prevTime = 0.0f;
        terrainManager.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        secneName = SceneManager.GetActiveScene().name;
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

    // 採掘処理
    public IEnumerator Mining(int i)
    {
        if(0 <= i)
        {

        }
        yield return new WaitForSeconds(1f);

        if (1 <= i)
        {

            yield return new WaitForSeconds(1f);
        }


        if (2 <= i)
        {

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


    public string GetSceneName
    {
        get
        {
            return secneName;
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

    public bool GetTruhasi()
    {
        string array ="";
        int haveItemID = 601;
        int haveKindID = 6;
        connectionFile.TranslationDataArray(connectionFile.ReadFile(haveItemID, array), haveKindID);     // 更新
        int i = connectionFile.haveNum;
        bool b;
        if (0<i)
        {
            b = true;
            getOre();
        }
        else
        {
            b = false;
        }
        Debug.Log("Have:" + i + " Pick:" + b);
        return b;
    }

    // 鉱石採取
    public void getOre()
    {

    }

    public GameObject GetPlayerObj()
    {
        return playerObj;
    }
}
