using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private GameCursor gameCursor;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private ControllScripts controllScripts;
    [SerializeField] private MainSceneObj mainSceneObj;
    [SerializeField] private AttackBlockManager attackBlockManager;


    [Header("Input")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputActionAsset[] inputActions;
    [SerializeField] private bool debugInput;

    [Header("InputUI")]
    [SerializeField] private GameObject cursorObj;
    [SerializeField] private GameObject control;

    [Header("VR")]
    //[SerializeField] private bool phoneVr;
    [SerializeField] private GameObject vrRot;
    private Quaternion currentGyro;

    [Header("カメラ")]
    [SerializeField] private GameObject pcCamera;
    [SerializeField] private GameObject phoneVrCamera;
    [SerializeField] private Transform lookObj;


    [Header("移動関係")]
    private float _moveSpeed = 10f;                             // 移動速度
    [SerializeField] private float[] setSpeed = { 8.0f, 0.5f };
    [SerializeField] private float _lookSpeed = 50f;            // カメラ回転速度
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private GameObject UI;

    [Header("その他")]
    [SerializeField] private Transform playerYPos;
    [SerializeField] private GameObject talkNpc;

    /// Player Input
    private PlayerInput _playerInput = null;

    InputActionMap playerMap;
    InputActionMap UIMap;
    InputActionMap MenuMap;
    InputActionMap talkMap;

    private Vector2 _currentMoveInputValue = Vector2.zero;      // 現在の移動入力値
    private Vector2 _currentCursorInputValue = Vector2.zero;    // 現在の移動入力値
    private Vector2 _currentLookInputValue = Vector2.zero;      // 現在のカメラ回転入力値

    /// 前回のカメラの向き
    private Vector3 _preRotation = Vector3.zero;

    // Ray
    private Ray ray;
    [SerializeField] private float maxDistance = 20;

    [SerializeField] private GameObject attackBlock1;    // 祠ギミックアタックブロック
    [SerializeField] private GameObject attackBlock2;
    [SerializeField] private ClerkOperation clearkOperation;
    //[SerializeField] private bool clerkMode;

    // M.Sヒットしたゲームオブジェクトの保存(運ぶ処理に使用)
    private GameObject hitObj;
    private bool hitObjFlag = false;
    private GameObject huntTarget;
    public bool awayProcessFlag; //店員とのトークフラグがfalseの時に1回のみ処理するフラグ
    [SerializeField] private GameObject baseQuestButton; //関数呼び出し用オブジェ
    [SerializeField] private GameObject crystalParent;   // 宝石ドロップオブジェの初期親(ドロップオブジェ祠)
    [SerializeField] private GameObject crystalParent_2;   // 宝石ドロップオブジェの初期親(オール祠)
    private float jewelPosY;      //宝石ドロップオブジェのY座標
    private bool huntSelectOrder;


    /// Input Actions
    /// All
    private const string ACTION_CURSORMOVE = "CursorMove";

    /// Player
    private const string PLAYER_MOVE = "Move";
    private const string PLAYER_LOOK = "Look";
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

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerInput = gameObject.GetComponent<PlayerInput>();
        
        playerRb = playerObj.GetComponent<Rigidbody>();
        if (gameManager.GetVRMode)
        {
            debugInput = false;
            Input.gyro.enabled = true;
            pcCamera.SetActive(false);
            phoneVrCamera.SetActive(true);
        }
        else
        {
            Input.gyro.enabled = false;
            pcCamera.SetActive(true);
            phoneVrCamera.SetActive(false);
        }

        if (!debugInput)
        {
            playerInput.actions = inputActions[0];
            cursorObj.SetActive(true);
            control.SetActive(true);
        }
        else
        {
            playerInput.actions = inputActions[1];
            cursorObj.SetActive(false);
            control.SetActive(false);
        }
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
    }

    private void Update()
    {
        move();
        look();
        RayUpdate();
    }

    // 移動処理
    private void move()
    {
        if(!debugInput) MoveVextor();

        float cameraForward;
        cameraForward = transform.eulerAngles.y;

        var moveForward = Quaternion.Euler(0, cameraForward, 0) * new Vector3(_currentMoveInputValue.x, 0, _currentMoveInputValue.y);
        Vector3 vector = moveForward * _moveSpeed;
        playerRb.velocity =new Vector3(vector.x ,playerRb.velocity.y ,vector.z);

    }

    // 回転処理
    private void look()
    {
        if (phoneVrCamera.activeSelf)
        {
            currentGyro = Input.gyro.attitude;
            vrRot.transform.localRotation = Quaternion.Euler(90, 90, 0) * new Quaternion(-currentGyro.x, -currentGyro.y, currentGyro.z, currentGyro.w);
        }
        else 
        {
            _preRotation.y += _currentLookInputValue.x * _lookSpeed * Time.deltaTime;
            _preRotation.x -= _currentLookInputValue.y * _lookSpeed * Time.deltaTime;
            _preRotation.x = Mathf.Clamp(_preRotation.x, -89, 89);
            transform.localEulerAngles = _preRotation;            
        }
    }

    // ray
    private void RayUpdate()
    {
        ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        // Ray表示
        if (Physics.Raycast(ray, out var hit, maxDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 0.001f, false);
            // M.S 物を運ぶ処理(MoveObjのタグがつくオブジェクト限定)
            // hitObjFlagで一つずつしか運ばないようにしている
            // 持ち上げる処理(中央ボタンクリック)
            var current = Mouse.current;
            if (current.middleButton.wasPressedThisFrame && hitObjFlag == false)
            {
                if (hit.collider.gameObject.tag == "MoveObj" || hit.collider.gameObject.tag == "MoveObj_2")
                {
                    hitObjFlag = true; // 持ち上げるフラグを立てる
                    hitObj = hit.collider.gameObject; //持ったオブジェクトの保存
                    hitObj.transform.parent = playerCamera.transform;   //持ったオブジェクトの親をカメラにする
                    jewelPosY = hitObj.transform.position.y;            //持ったオブジェクトのY座標の保存
                    hitObj.GetComponent<BoxCollider>().enabled = false; //もったオブジェクトのコライダーを無効化
                }
            }
            if (hitObjFlag == true)
            {
                Debug.Log("運び中");
            }
            // 落とす処理(中央ボタン離す)
            if (current.middleButton.wasReleasedThisFrame && hitObjFlag == true)
            {
                hitObj.transform.parent = null; //親を戻す
                hitObjFlag = false; // 持ち上げるフラグをfalse
                hitObj.GetComponent<BoxCollider>().enabled = true; //コライダーの有効か
                hitObj.transform.localEulerAngles = new Vector3(-90.0f, 0.0f, 0.0f); //回転角度を戻す
                hitObj.transform.position =
                    new Vector3(hitObj.transform.position.x, jewelPosY, hitObj.transform.position.z); //Y座標を戻す
                if (hitObj.gameObject.tag == "MoveObj")
                {
                    hitObj.transform.parent = crystalParent.transform; //親を戻す
                }
                else if (hitObj.gameObject.tag == "MoveObj_2")
                {
                    hitObj.transform.parent = crystalParent_2.transform; //親を戻す
                }

            }
            // M.S クエストボードの処理
            //会話モードならば
            if (clearkOperation.talkMode == true)
            {
                // 選択したボタンがクエスト受注ボタンならば
                if (hit.collider.gameObject.tag == "HuntSelectButton" && hit.collider.gameObject.name == "DecideButton" && Input.GetMouseButtonDown(0))
                {
                    // 受注処理が終わったら
                    if (huntTarget.GetComponent<QuestData>().ClickOrderReceived() == true)
                    {
                        huntTarget.GetComponent<QuestData>().ClickBoardBack(); //クエスト選択画面に戻る
                    }
                }
                // 選択したボタンがクエスト選択画面に戻るならば
                else if (hit.collider.gameObject.tag == "HuntSelectButton" && hit.collider.gameObject.name == "BackButton" && Input.GetMouseButtonDown(0))
                {
                    huntTarget.GetComponent<QuestData>().ClickBoardBack(); //クエスト選択画面に戻る
                }
                // 選択したボタンがクエスト内容確認ボタンならば
                else if (hit.collider.gameObject.tag == "QuestConfirmationButton" && Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.GetComponent<QuestData>().ClickConfirmation(); //クエストの内容確認表示
                    // 選択したボタンの更新
                    huntTarget = hit.collider.gameObject;
                }
            }
            else
            {
                //まだトークモードが外れてから処理をしていなかったら
                if (awayProcessFlag == true)
                {
                    // トークモードが外れたらすぐにクエスト選択画面に戻る
                    baseQuestButton.GetComponent<QuestData>().ClickBoardBack();
                    awayProcessFlag = false;
                }
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green, 0.001f, false);
        }
    }


    // ActionMap切り替え関数
    public void ToPlayerMode()
    {
        _playerInput.SwitchCurrentActionMap("Player");
        weaponManager.WeaponChange();
        if (debugInput)gameCursor.gameObject.SetActive(false);
    }
    public void ToUIMode()
    {
        _playerInput.SwitchCurrentActionMap("UI");
        playerRb.velocity = Vector3.zero;
        if (debugInput) gameCursor.gameObject.SetActive(true);
        weaponManager.wearSword.SetActive(false);
        weaponManager.wearBow.SetActive(false);
        weaponManager.handObj.SetActive(false);
        gameManager.uiManager.uiOpen();
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
        _currentMoveInputValue = context.ReadValue<Vector2>();
    //    Debug.Log(_currentMoveInputValue);
    }

    // カーソルプレイヤー移動用
    public void MoveVextor()
    {
        _currentMoveInputValue = playerControl.GetMoveVector();
    }

    //　カーソル移動
    public void OnCursorMove(InputAction.CallbackContext context)
    {
        _currentCursorInputValue = context.ReadValue<Vector2>();
        // Debug.Log(_currentCursorInputValue);
    }

    // カメラ回転処理
    public void OnLook(InputAction.CallbackContext context)
    {
        _currentLookInputValue = context.ReadValue<Vector2>();
    }

    // クリック
    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        // Rayが特定のオブジェクトに当たった時
        if (context.performed && Physics.Raycast(ray, out var hit, maxDistance))
        {
            // Debug.Log(hit.collider.gameObject.tag + "だよ");
            // M.S 剣の時のみ祠のアタックブロックのクリックフラグをたてる
            if (hit.collider.gameObject.tag == "Shrine_AttackBlock" && weaponManager.wearSword.activeSelf == true)
            {
                ParticleSystem effect_c = Instantiate(attackBlockManager.hitEffect);
                effect_c.transform.position = hit.transform.position;
                effect_c.Play();
                Destroy(effect_c.gameObject, 5.0f);
                hit.collider.gameObject.GetComponent<AttackChangeMaterial>().clickFlag = true;
            }

            // NPC
            if (hit.collider.gameObject.tag == "NPC")
            {
                Debug.Log("会話開始");
                SetActionMap(2);
                gameManager.uiManager.talkStart(hit.collider.gameObject);
                hit.collider.gameObject.GetComponent<NPCManager>().npcNav.TalkStart(); // Npcがプレイヤーに向かう
                // playerCamera.transform.LookAt(hit.collider.gameObject.GetComponent<NPCManager>().GetNpcFace);
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
                    oreStoneObj.GetSetCanPick = false;
                    weaponManager.PickStart(gameManager.GetTruhasi());
                    //
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
            var mouseDelta = Input.mouseScrollDelta;
            if (mouseDelta != Vector2.zero)
            {
                // Debug.Log($"[frame: {Time.frameCount}] Input.mouseScrollDelta = {mouseDelta.ToString("F3")}");
                if (0 < mouseDelta.y)
                {
                    weaponManager.WeaponChange(true);
                }
                else
                {
                    weaponManager.WeaponChange(false);
                }
            }
        }
    }

    // 攻撃（右クリック）
    public void OnLAttack(InputAction.CallbackContext context)
    {
        if (context.performed)   // 押す（通常）
        {
            if (weaponManager.wearSword.activeSelf == true) // 剣_弱攻撃
            {
                weaponManager.SwordAttack();
            }
            else if(weaponManager.wearBow.activeSelf == true) // 弓攻撃
            {
                _moveSpeed = setSpeed[1];
                weaponManager.bow.SwitchDrawBow(true);
            }
        }
        // 離す（弓のみ）
        if (context.canceled && weaponManager.wearBow.activeSelf == true)
        {
            _moveSpeed = setSpeed[0];
            weaponManager.bow.ArrowShot();
        }
    }
    // 攻撃（左クリック）
    public void OnRAttack(InputAction.CallbackContext context)
    {
        if (context.performed)   // 押す（通常）
        {
            if (weaponManager.wearSword.activeSelf == true) // 剣_強攻撃
            {
                weaponManager.StrengthAttack();
            }
            else if(weaponManager.wearBow.activeSelf == true)　//　弓_矢切り替え
            {
                weaponManager.bow.ChengeArrow();
            }
        }
    }

    // ホイールクリック  祠外のみ
    public void OnOpenCloseMenu(InputAction.CallbackContext context)
    {
        if (mainSceneObj.menuOffMode == false)
        {
            if (context.performed)
            {
                SetActionMap(3);
                controllScripts.OpenCloseMenu();
            }
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

        var mouseDelta = Input.mouseScrollDelta;
        if (mouseDelta != Vector2.zero)
        {
            // Debug.Log($"[frame: {Time.frameCount}] Input.mouseScrollDelta = {mouseDelta.ToString("F3")}");
            if (0 < mouseDelta.y)
            { //+
                gameManager.uiManager.SetUINum(false);
            }
            else
            { //-
                gameManager.uiManager.SetUINum(true);
            }
        }
    }

    public void OnPageDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //if () { }
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
           // Debug.Log("menuclose");
        }
    }
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controllScripts.MouseLeftClick();
           // Debug.Log("leftclick");
        }
    }
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controllScripts.MouseRightClick();
           // Debug.Log("rightclick");
        }
    }
    public void OnMouseScroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var mouseDelta = Input.mouseScrollDelta;
            controllScripts.MouseScroll(mouseDelta.y);
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
            if (debugInput)
            {
                gameCursor.gameObject.SetActive(false);
                playerMap[PLAYER_MOVE].started += OnMove;
                playerMap[PLAYER_MOVE].performed += OnMove;
                playerMap[PLAYER_MOVE].canceled += OnMove;
            }
            else
            {
                playerMap[ACTION_CURSORMOVE].started += OnCursorMove;
                playerMap[ACTION_CURSORMOVE].performed += OnCursorMove;
                playerMap[ACTION_CURSORMOVE].canceled += OnCursorMove;

            }

            playerMap[PLAYER_LOOK].started += OnLook;
            playerMap[PLAYER_LOOK].performed += OnLook;
            playerMap[PLAYER_LOOK].canceled += OnLook;

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
            /*
            UIMap[ACTION_CURSORMOVE].started += OnCursorMove;
            UIMap[ACTION_CURSORMOVE].performed += OnCursorMove;
            UIMap[ACTION_CURSORMOVE].canceled += OnCursorMove;
            */
            UIMap[UI_CLICK].started += OnClick;
            UIMap[UI_CLICK].performed += OnClick;
            UIMap[UI_CLICK].canceled += OnClick;

            UIMap[UI_SCROLL].started += OnScroll;
            UIMap[UI_SCROLL].performed += OnScroll;
            UIMap[UI_SCROLL].canceled += OnScroll;

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


    public void SetHuntSelectOrder(bool flag)
    {
        huntSelectOrder = flag;
    }

    public bool GetDebug()
    {
        return debugInput;
    }

    public Vector2 GetCurosorMove
    {
        get { return _currentCursorInputValue; }
    }

}
