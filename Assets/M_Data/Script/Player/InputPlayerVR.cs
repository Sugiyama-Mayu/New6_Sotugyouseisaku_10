using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputPlayerVR : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private WeaponManagerVR weaponManagerVR;
    [SerializeField] private ControllScripts controllScripts;
    [SerializeField] private RingSound ringSound;

    [Header("Input")]
    [SerializeField] private PlayerInput playerInput;

    [Header("移動関係")]
    private float _moveSpeed = 10f;                             // 移動速度
    [SerializeField] private float[] setSpeed = { 8.0f, 0.5f };
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private Rigidbody playerRb;

    [Header("Ray")]
    Ray[] handRay = new Ray[2];
    bool once = true;
    [SerializeField] private GameObject[] HandObj;
    [SerializeField] private LineRenderer[] lineRenderers;
    [SerializeField] private float maxDistance;

    [SerializeField] private GameObject attackBlock1;    // 祠ギミックアタックブロック
    [SerializeField] private GameObject attackBlock2;
    [SerializeField] private ClerkOperation clearkOperation;


    [Header("その他")]
    [SerializeField] private Transform playerYPos;
    [SerializeField] private float rotCamera;
    bool bowString = false;

    /// Player Input
    private PlayerInput _playerInput = null;

    InputActionMap playerMap;
    InputActionMap UIMap;
    InputActionMap talkMap;
    InputActionMap MenuMap;

    private Vector2 _currentMoveInputValue;      // 現在の移動入力値
    private float _currentRoteInputValue = 0;      // 現在の移動入力値
    private Vector2 mouseDelta;
    private int inputCount;



    /// Input Actions
    /// Player
    private const string PLAYER_MOVE = "Move";
    private const string PLAYER_ROTE = "Rote";
    private const string PLAYER_FIRE = "Fire";
    private const string PLAYER_WEAPON = "WeaponChange";
    private const string PLAYER_LATTACK = "LAttack";
    private const string PLAYER_RATTACK = "RAttack";
    private const string PLAYER_OPENCLOSEMENU = "OpenCloseMenu";

    /// UI
    private const string UI_CLICK = "Click";
    private const string UI_PAGEDOWN = "PageDown";
    private const string UI_SCROLL = "Scroll";


    /// Talk
    private const string TALK_PAGEUP = "PageUp";

    /// Menu
    private const string MENU_CLOSE = "MenuClose";
    private const string MENU_LEFTCLICK = "LeftClick";
    private const string MENU_RIGHTCLICK = "RightClick";
    private const string MENU_MOUSESCROLL = "MouseScroll";
 
    /*
    // M.Sヒットしたゲームオブジェクトの保存(運ぶ処理に使用)
    private GameObject hitObj;
    private bool hitObjFlag = false;
    private bool huntSelectOrder;
    private GameObject huntTarget;
    */


    private void Awake()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        playerRb = playerObj.GetComponent<Rigidbody>();
        _moveSpeed = setSpeed[0];
    }

    // Start
    private void Start()
    {
        Invoke("ReSetRot",0.2f);
        playerMap = playerInput.actions.FindActionMap("Player");
        UIMap = playerInput.actions.FindActionMap("UI");
        talkMap = playerInput.actions.FindActionMap("Talk");
        MenuMap = playerInput.actions.FindActionMap("Menu");


        // action設定
        InputActionSetting();
        
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            SetActionMap(1);
        }
        else
        {
            SetActionMap(0);
        }
    }

    private void Update()
    {
        move();
        RayUpdate();
    }

    // 移動処理
    private void move()
    {
        float cameraForward;
        cameraForward = playerCamera.transform.eulerAngles.y;

        var moveForward = Quaternion.Euler(0, cameraForward, 0) * new Vector3(_currentMoveInputValue.x, 0, _currentMoveInputValue.y);
        Vector3 vector = moveForward * _moveSpeed;
        playerRb.velocity = new Vector3(vector.x ,playerRb.velocity.y ,vector.z);
        _currentMoveInputValue = Vector2.zero;
       
    }

    public void RayUpdate()
    {
        handRay[0] = new Ray(HandObj[0].transform.position, HandObj[0].transform.forward);
        handRay[1] = new Ray(HandObj[1].transform.position, HandObj[1].transform.forward);
        RayHitCheck(true);
        RayHitCheck(false);
    }

    private void RayHitCheck(bool b)
    {
        int i;
        Ray ray;
        if (b) 
        {
            ray = handRay[0];
            i = 0;
        }
        else 
        { 
            ray = handRay[1];
            i = 1;
        }

        if (Physics.Raycast(ray, out var hit, maxDistance))
        {
            if (lineRenderers[i].enabled == false && hit.collider.tag == "WarpPoint" || hit.collider.tag == "Ore" ||hit.collider.tag == "NPC" ) 
            {
                lineRenderers[i].enabled = true; 
            }
        }
        else if (lineRenderers[i].enabled == true) lineRenderers[i].enabled = false;
    }

    // ActionMap切り替え関数
    public void ToPlayerMode()
    {
        _playerInput.SwitchCurrentActionMap("Player");
        weaponManagerVR.WeaponChange();
    }
    public void ToUIMode()
    {
        _playerInput.SwitchCurrentActionMap("UI");
        playerRb.velocity = Vector3.zero;
        weaponManagerVR.wearSword.SetActive(false);
        weaponManagerVR.wearBow.SetActive(false);
    }
    public void ToTalkMode()
    {
        _playerInput.SwitchCurrentActionMap("Talk");
    }
    public void ToMenuMode()
    {
        _playerInput.SwitchCurrentActionMap("Menu");
    }


    //--------------------------------------------------------------------------------------------------------------------------
    // InputEvent用関数
    //--------------------------------------------------------------------------------------------------------------------------
    // Player
    // 移動処理
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)_currentMoveInputValue = context.ReadValue<Vector2>();
    }

    public void OnRote(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        _currentRoteInputValue = context.ReadValue<Vector2>().x;

        Quaternion q = transform.rotation;
        Quaternion rot = Quaternion.identity;

        if (_currentRoteInputValue > 0.5f) rot = Quaternion.AngleAxis(rotCamera, Vector3.up);
        else if(_currentRoteInputValue < -0.5f) rot = Quaternion.AngleAxis(-rotCamera, Vector3.up);
        transform.rotation = q * rot;

    }

    // クリック
    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        RayHit(handRay[0]);
        if(once)RayHit(handRay[1]);
    }

    // Rayヒット関数
    private void RayHit(Ray ray)
    {
        once = false;
        if (Physics.Raycast(ray, out var hit, maxDistance))
        {
            // NPC
            if (hit.collider.gameObject.tag == "NPC")
            {
                Debug.Log("会話開始");
                SetActionMap(2);
                gameManager.uiManager.talkStart(hit.collider.gameObject);
                hit.collider.gameObject.GetComponent<NPCManager>().npcNav.TalkStart(); // Npcがプレイヤーに向かう
            }

            if (hit.collider.gameObject.tag == "Smithy") // 武器強化
            {
                SetActionMap(1);
                gameManager.uiManager.Enhance();
            }

            if (hit.collider.gameObject.tag == "WarpPoint") // ワープポイント
            {
                string str = hit.collider.gameObject.name;
                str = str.Substring(5);
                int i = int.Parse(str);

                gameManager.uiManager.WapePoint(i);
                SetActionMap(1);
            }

            if (hit.collider.gameObject.tag == "Ore")
            {
                OreStoneObj oreStoneObj = hit.collider.gameObject.GetComponent<OreStoneObj>();
                if (oreStoneObj.GetSetCanPick && gameManager.GetTruhasi())
                {
                    ringSound.RingSE(1);

                    oreStoneObj.GetSetCanPick = false;
                    gameManager.GetTruhasi();                    //
                    Debug.Log("Pick");
                }
                else
                {
                    Debug.Log("NowCoolTime");
                }

            }
        }

        }

    // 武器切り替え（マウスホイール）
    public void OnWeaponChange(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weaponManagerVR.WeaponChange(true);
        }
    }

    // 攻撃（左クリック）
    public void OnLAttack(InputAction.CallbackContext context)
    {
        if (weaponManagerVR.wearBow.activeSelf == false) return;   // 押す（通常）
        if (context.performed)   // 押す（通常）
        {
            weaponManagerVR.bow.ChengeArrow();

        }

    }
    // 攻撃（右クリック）
    public void OnRAttack(InputAction.CallbackContext context)
    {
        if (weaponManagerVR.wearBow.activeSelf == false) return;   // 押す（通常）
        var value = context.ReadValue<float>();

        if (context.performed) 
        {
            //     Debug.Log(value);
            if (!bowString)
            {
                if (0.9f <= value)
                {
                    _moveSpeed = setSpeed[1];
                    weaponManagerVR.bow.SwitchDrawBow(true);
                    ringSound.RingSE(14);
                    bowString = true;
                    return;
                }

            }
            else
            {
                if (value < 0.9f)
                {
                    // 離す（弓のみ）
                    _moveSpeed = setSpeed[0];
                    weaponManagerVR.bow.ArrowShot();
                    ringSound.StopSE(1);
                    ringSound.RingSE(13);
                    bowString = false;
                }

            }

        }

    }

    // ホイールクリック
    public void OnOpenCloseMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SetActionMap(3);
            controllScripts.OpenCloseMenu();
        }
    }


    //--------------------------------------------------------------------------------------------------------------------------
    // UI
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameManager.uiManager.Click();
        }
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var mouseDelta = context.ReadValue<Vector2>();
        //       Debug.Log(mouseDelta);
        if (mouseDelta != Vector2.zero)
        {
            // Debug.Log($"[frame: {Time.frameCount}] Input.mouseScrollDelta = {mouseDelta.ToString("F3")}");
            if (mouseDelta.y < -0.7f)
            { // +
                inputCount++;
                if (3 < inputCount)
                {
                    gameManager.uiManager.SetUINum(true);
                    inputCount = 0;

                }
            }
            else if (0.7f < mouseDelta.y)
            { // -
                inputCount++;
                if (3 < inputCount)
                {
                    gameManager.uiManager.SetUINum(false);
                    inputCount = 0;

                }
            }
        }
    }

    public void OnPageDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SetActionMap(0);
            gameManager.uiManager.uiClose();

        }
    }

//--------------------------------------------------------------------------------------------------------------------------
// Talk
    public void OnPageUp(InputAction.CallbackContext context)
    {
        if (context.performed)   // 押す（通常）
        {
            gameManager.uiManager.talkNPC.TextCountUp();
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------
    // Menu
    public void OnMenuClose(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controllScripts.OpenCloseMenu();
            Debug.Log("menuclose");
            SetActionMap(0);
        }
    }
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controllScripts.MouseLeftClick();
            Debug.Log("leftclick");
        }
    }
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controllScripts.MouseRightClick();
            Debug.Log("rightclick");
        }
    }
    public void OnMouseScroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mouseDelta = context.ReadValue<Vector2>();
          //  Debug.Log(mouseDelta);
            controllScripts.StickScroll(mouseDelta);

        }
    }

    //--------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// ActionMap切り替え 0:Player 1:UI 2:Talk
    /// </summary>
    /// <param name="i"> </param>
    public void SetActionMap(int i)
    {
        switch (i)
        {
            case 0:
                ToPlayerMode();
                break;
            case 1:
                ToUIMode();
                break;
            case 2:
                ToTalkMode();
                break;
            case 3:
                ToMenuMode();
                break;
            default:
                Debug.LogWarning("指定範囲外");
                break;
        }
    }

    // actionの設定
    private void InputActionSetting()
    {
        if (TryGetComponent(out _playerInput))
        {
            playerMap[PLAYER_MOVE].started += OnMove;
            playerMap[PLAYER_MOVE].performed += OnMove;
            playerMap[PLAYER_MOVE].canceled += OnMove;

            playerMap[PLAYER_ROTE].started += OnRote;
            playerMap[PLAYER_ROTE].performed += OnRote;
            playerMap[PLAYER_ROTE].canceled += OnRote;

            playerMap[PLAYER_FIRE].started += OnFire;
            playerMap[PLAYER_FIRE].performed += OnFire;
            playerMap[PLAYER_FIRE].canceled += OnFire;

            playerMap[PLAYER_WEAPON].started += OnWeaponChange;
            playerMap[PLAYER_WEAPON].performed += OnWeaponChange;
            playerMap[PLAYER_WEAPON].canceled += OnWeaponChange;

            playerMap[PLAYER_LATTACK].started += OnLAttack;
            playerMap[PLAYER_LATTACK].performed += OnLAttack;
            playerMap[PLAYER_LATTACK].canceled += OnLAttack;

            playerMap[PLAYER_RATTACK].started += OnRAttack;
            playerMap[PLAYER_RATTACK].performed += OnRAttack;
            playerMap[PLAYER_RATTACK].canceled += OnRAttack;

            playerMap[PLAYER_OPENCLOSEMENU].started += OnOpenCloseMenu;
            playerMap[PLAYER_OPENCLOSEMENU].performed += OnOpenCloseMenu;
            playerMap[PLAYER_OPENCLOSEMENU].canceled += OnOpenCloseMenu;

            UIMap[UI_CLICK].started += OnClick;
            UIMap[UI_CLICK].performed += OnClick;
            UIMap[UI_CLICK].canceled += OnClick;

            UIMap[UI_SCROLL].started += OnScroll;
            UIMap[UI_SCROLL].performed += OnScroll;
            UIMap[UI_SCROLL].canceled += OnScroll;

            UIMap[UI_CLICK].started += OnClick;
            UIMap[UI_CLICK].performed += OnClick;
            UIMap[UI_CLICK].canceled += OnClick;

            UIMap[UI_PAGEDOWN].started += OnPageDown;
            UIMap[UI_PAGEDOWN].performed += OnPageDown;
            UIMap[UI_PAGEDOWN].canceled += OnPageDown;

            talkMap[TALK_PAGEUP].started += OnPageUp;
            talkMap[TALK_PAGEUP].performed += OnPageUp;
            talkMap[TALK_PAGEUP].canceled += OnPageUp;

            MenuMap[MENU_CLOSE].started += OnMenuClose;
            MenuMap[MENU_CLOSE].performed += OnMenuClose;
            MenuMap[MENU_CLOSE].canceled += OnMenuClose;

            MenuMap[MENU_LEFTCLICK].started += OnLeftClick;
            MenuMap[MENU_LEFTCLICK].performed += OnLeftClick;
            MenuMap[MENU_LEFTCLICK].canceled += OnLeftClick;

            MenuMap[MENU_RIGHTCLICK].started += OnRightClick;
            MenuMap[MENU_RIGHTCLICK].performed += OnRightClick;
            MenuMap[MENU_RIGHTCLICK].canceled += OnRightClick;

            MenuMap[MENU_MOUSESCROLL].started += OnMouseScroll;
            MenuMap[MENU_MOUSESCROLL].performed += OnMouseScroll;
            MenuMap[MENU_MOUSESCROLL].canceled += OnMouseScroll;


        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // セッター・ゲッター
    public void ReSetRot()
    {
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

}

