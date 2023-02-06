using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    private SwichMode swichMode;

    // �f�[�^�̓ǂݍ���
    public DataManager dataRead;
   // public SaveData saveData;

    public UIManager uiManager;
    public TerrainManager terrainManager;
    public PlayerManager playerManager;
    public InputPlayer inputPlayer;
    public InputPlayerVR inputPlayerVR;
    public CreateManager createManager;
    [SerializeField] private ConnectionFile connectionFile;
    

    [Header("�Q�[���I�u�W�F�N�g")]
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject pcPhoneObj;
    [SerializeField] private GameObject xrObj;
    [SerializeField] private GameObject titleObj;

    [Header("���[�h���")]
    [SerializeField] private bool phoneVrMode;
    [SerializeField] private bool XRMode;

    [Header("�X�^�[�g���W")]
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
        materialNum = new int[8]; // 0:�� 1:�� 2:�� 3:�є� 4:�� 5:�� 6:�� 7:��
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

        // ���[�h�ύX
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
    /// �J�����̐ݒ�
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

    // �̌@����
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
        Debug.Log("���F" + count[0] + " ��F" + count[1] + " ���F" + count[2]);
    }



    // �f�ރL���[
    public void SetMaterial(string itemName, int itemCount)
    {
        b = true;
        switch (itemName)
        {
            case "��":
                materialNum[0] += itemCount;
                break;
            case "��":
                materialNum[1] += itemCount;
                break;
            case "��":
                materialNum[2] += itemCount;
                break;
            case "�є�":
                materialNum[3] += itemCount;
                break;
            case "��":
                materialNum[4] += itemCount;
                break;
            case "��":
                materialNum[5] += itemCount;
                break;
            case "��":
                materialNum[6] += itemCount;
                break;
            case "��":
                materialNum[7] += itemCount;
                break;
        }
        Debug.Log(itemName + "��" + itemCount + "���L���[�ɒǉ�");
    }

    // ��͂�
    public bool GetTruhasi()
    {
        connectionFile.TranslationDataArray(connectionFile.ReadFile(601, ""), 6);     // �X�V
        int num = connectionFile.haveNum;
        bool b;

        if (0 < num)
        {
            b = true;
            Mining(num); // �̎揈��
        }
        else b = false;
        return b;
    }

    public void UseDragItem(int num)
    {
        dragItemNum[num]--;
    } 


    // �A�N�V�����}�b�v�ύX
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

    // ���j���[�p
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
