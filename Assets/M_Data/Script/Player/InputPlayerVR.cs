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

    [Header("�ړ��֌W")]
    private float _moveSpeed = 9f;                             // �ړ����x
    [SerializeField] private float[] setSpeed = { 8.0f, 0.5f };
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private Rigidbody playerRb;

    [Header("Ray")]
    Ray[] handRay = new Ray[2];
    bool once = true;
    bool updateRay = true;
 
    [SerializeField] private GameObject[] HandObj;
    [SerializeField] private LineRenderer[] lineRenderers;
    [SerializeField] private float maxDistance;

    [Header("��ՃM�~�b�N")]
    [SerializeField] private GameObject attackBlock1;    // �K�M�~�b�N�A�^�b�N�u���b�N
    [SerializeField] private GameObject attackBlock2;
    [SerializeField] private RotateBall rotateBall;


    [Header("�V���b�v�E�N�G�X�g�{�[�h")]
    [SerializeField] private ClerkOperation clearkOperation;
    [SerializeField] private ShopClerkOperation shopClerkOperation;
    [SerializeField] private WheelShopProcess wheelShopProcess;


    [Header("���̑�")]
    [SerializeField] private Transform playerYPos;
    [SerializeField] private float rotCamera;
    bool bowString = false;

    // M.S�q�b�g�����Q�[���I�u�W�F�N�g�̕ۑ�(�^�ԏ����Ɏg�p)
    private GameObject hitObj;
    private bool hitObjFlag = false;
    private GameObject huntTarget;
    public bool awayProcessFlag; //�X���Ƃ̃g�[�N�t���O��false�̎���1��̂ݏ�������t���O
    [SerializeField] private GameObject baseQuestButton; //�֐��Ăяo���p�I�u�W�F
    [SerializeField] private GameObject crystalParent;   // ��΃h���b�v�I�u�W�F�̏����e(�h���b�v�I�u�W�F�K)
    [SerializeField] private GameObject crystalParent_2;   // ��΃h���b�v�I�u�W�F�̏����e(�I�[���K)
    private float jewelPosY;      //��΃h���b�v�I�u�W�F��Y���W
    private bool huntSelectOrder;


    /// Player Input
    private PlayerInput _playerInput = null;

    InputActionMap playerMap;
    InputActionMap UIMap;
    InputActionMap talkMap;
    InputActionMap MenuMap;
    InputActionMap questMap;
    InputActionMap shopMap;

    private Vector2 _currentMoveInputValue;      // ���݂̈ړ����͒l
    private float _currentRoteInputValue = 0;      // ���݂̈ړ����͒l
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

    /// Quest
    private const string QUEST_CLICK = "QuestClick";
    private const string QUEST_CLOSE = "QuestClose";

    /// Shop
    private const string SHOP_CLICK = "ShopClick";
    private const string SHOP_PAGEDOWN = "ShopPageDown";
    private const string SHOP_SCROLL = "ShopScroll";
    private const string SHOP_MODECHANGE = "ModeChange";

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
        questMap = playerInput.actions.FindActionMap("Quest");
        shopMap = playerInput.actions.FindActionMap("Shop");


        // action�ݒ�
        InputActionSetting();
        SetActionMap(0);

    }

    private void Update()
    {
        move();
        RayUpdate();
    }

    // �ړ�����
    private void move()
    {
        float cameraForward;
        cameraForward = playerCamera.transform.eulerAngles.y;

        var moveForward = Quaternion.Euler(0, cameraForward, 0) * new Vector3(_currentMoveInputValue.x, 0, _currentMoveInputValue.y);
        Vector3 vector = moveForward * _moveSpeed ;
        playerRb.velocity = new Vector3(vector.x ,playerRb.velocity.y ,vector.z);
        _currentMoveInputValue = Vector2.zero;
       
    }

    public void RayUpdate()
    {
        handRay[0] = new Ray(HandObj[0].transform.position, HandObj[0].transform.forward);
        handRay[1] = new Ray(HandObj[1].transform.position, HandObj[1].transform.forward);
        if (updateRay)
        {
            RayHitCheck(0);
            RayHitCheck(1);
        }
    }

    private void RayHitCheck(int i)
    {
        Ray ray;
        if (i == 0) 
        {
            ray = handRay[i];
        }
        else 
        { 
            ray = handRay[i];
        }

        if (Physics.Raycast(ray, out var hit, maxDistance))
        {
            if (lineRenderers[i].enabled == false && hit.collider.tag != "Untagged" && hit.collider.tag != "Enemy") 
            {
                lineRenderers[i].enabled = true; 
            }

        }
        else if (lineRenderers[i].enabled == true) lineRenderers[i].enabled = false;
    }

    // ActionMap�؂�ւ��֐�
    public void ToPlayerMode()
    {
        _playerInput.SwitchCurrentActionMap("Player");
        updateRay = true;
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

    public void ToQuestMode()
    {
        _playerInput.SwitchCurrentActionMap("Quest");
        updateRay = false;
        lineRenderers[0].enabled = true;
        lineRenderers[1].enabled = true;

    }

    public void ToShopMode()
    {
        _playerInput.SwitchCurrentActionMap("Shop");
        gameManager.uiManager.uiClose();
    }



    //--------------------------------------------------------------------------------------------------------------------------
    // InputEvent�p�֐�
    //--------------------------------------------------------------------------------------------------------------------------
    // Player
    // �ړ�����
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

    // �N���b�N
    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        RayHit(handRay[0]);
        if(once)RayHit(handRay[1]);
    }

    // Ray�q�b�g�֐�
    private void RayHit(Ray ray)
    {
        once = false;
        if (Physics.Raycast(ray, out var hit, maxDistance))
        {
            // NPC
            if (hit.collider.gameObject.tag == "NPC")
            {
                Debug.Log("��b�J�n");
                SetActionMap(2);
                gameManager.uiManager.talkStart(hit.collider.gameObject);
                hit.collider.gameObject.GetComponent<NPCManager>().npcNav.TalkStart(); // Npc���v���C���[�Ɍ�����
            }

            if (hit.collider.gameObject.tag == "Smithy") // ���틭��
            {
                SetActionMap(1);
                gameManager.uiManager.Enhance();
            }

            if (hit.collider.gameObject.tag == "WarpPoint") // ���[�v�|�C���g
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
                if (oreStoneObj.GetSetCanPick)
                {
                    ringSound.RingSE(1);

                    oreStoneObj.GetSetCanPick = false;
                    gameManager.GetTruhasi();                    //
                    Debug.Log("Pick");
                }
                else
                {
                    Debug.Log("NowCoolTime or NoHavePick");
                }
            }

            //�V���b�v
            if (hit.collider.gameObject.tag == "Shop" && shopClerkOperation.StartEndShop())
            {
                SetActionMap(5);
                playerRb.isKinematic = true;
                playerRb.useGravity = false;
            }


            // �N�G�X�g�{�[�h
            if (hit.collider.gameObject.tag == "QuestBoard")
            {
                clearkOperation.QuestBoardStartEnd();
            }

            // �W���G���M�~�b�N
            if (hit.collider.gameObject.tag == "MoveObj" || hit.collider.gameObject.tag == "MoveObj_2")
            {
                if (hitObjFlag == false)
                {
                    hitObjFlag = true; // �����グ��t���O�𗧂Ă�
                    hitObj = hit.collider.gameObject; //�������I�u�W�F�N�g�̕ۑ�
                    hitObj.transform.parent = HandObj[1].transform;   //�������I�u�W�F�N�g�̐e���J�����ɂ���
                    jewelPosY = hitObj.transform.position.y;            //�������I�u�W�F�N�g��Y���W�̕ۑ�
                    hitObj.GetComponent<BoxCollider>().enabled = false; //�������I�u�W�F�N�g�̃R���C�_�[�𖳌���
                    return;
                }
            }
        }

        // ���Ƃ�����(�����{�^������)
        if (hitObjFlag == true)
        {
            hitObj.transform.parent = null; //�e��߂�
            hitObjFlag = false; // �����グ��t���O��false
            hitObj.GetComponent<BoxCollider>().enabled = true; //�R���C�_�[�̗L����
            hitObj.transform.localEulerAngles = new Vector3(-90.0f, 0.0f, 0.0f); //��]�p�x��߂�
            hitObj.transform.position =
                new Vector3(hitObj.transform.position.x, jewelPosY, hitObj.transform.position.z); //Y���W��߂�
            if (hitObj.gameObject.tag == "MoveObj")
            {
                hitObj.transform.parent = crystalParent.transform; //�e��߂�
            }
            else if (hitObj.gameObject.tag == "MoveObj_2")
            {
                hitObj.transform.parent = crystalParent_2.transform; //�e��߂�
            }
        }

        if (rotateBall.GetRotateModeAllow)
        {
            playerInput.enabled = false;
            rotateBall.StartRotateControll();
        }

    }

    // ����؂�ւ��i�}�E�X�z�C�[���j
    public void OnWeaponChange(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weaponManagerVR.WeaponChange(true);
        }
    }

    // �U���i���N���b�N�j
    public void OnLAttack(InputAction.CallbackContext context)
    {
        if (weaponManagerVR.wearBow.activeSelf == false) return;   // �����i�ʏ�j
        if (context.performed)   // �����i�ʏ�j
        {
            weaponManagerVR.bow.ChengeArrow();

        }

    }
    // �U���i�E�N���b�N�j
    public void OnRAttack(InputAction.CallbackContext context)
    {
        if (weaponManagerVR.wearBow.activeSelf == false) return;   // �����i�ʏ�j
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
                    // �����i�|�̂݁j
                    _moveSpeed = setSpeed[0];
                    weaponManagerVR.bow.ArrowShot();
                    ringSound.StopSE(1);
                    ringSound.RingSE(13);
                    bowString = false;
                }

            }

        }

    }

    // �z�C�[���N���b�N
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
        if (context.performed)   // �����i�ʏ�j
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

    //----------------------------------------------------------------------------------------------------------------------------------------------------
    // Quest
    public void OnQuestClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            for(int i= 0; i<2; i++) 
            {
                Ray ray = handRay[i];
                Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green, 0.001f, false);
                if (Physics.Raycast(ray, out var hit, maxDistance))
                {
                    // M.S �N�G�X�g�{�[�h�̏���
                    //��b���[�h�Ȃ��
                    if (clearkOperation.talkMode == true)
                    {
                        Debug.Log(hit.collider.gameObject.tag);
                        Debug.Log(hit.collider.gameObject.name);
                        // �I�������{�^�����N�G�X�g�󒍃{�^���Ȃ��
                        if (hit.collider.gameObject.tag == "HuntSelectButton" && hit.collider.gameObject.name == "DecideButton")
                        {
                            Debug.Log("Talkmode");
                            // �󒍏������I�������
                            if (huntTarget.GetComponent<QuestData>().ClickOrderReceived() == true)
                            {
                                huntTarget.GetComponent<QuestData>().ClickBoardBack(); //�N�G�X�g�I����ʂɖ߂�
                            }
                        }
                        // �I�������{�^�����N�G�X�g�I����ʂɖ߂�Ȃ��
                        else if (hit.collider.gameObject.tag == "HuntSelectButton" && hit.collider.gameObject.name == "BackButton")
                        {
                            huntTarget.GetComponent<QuestData>().ClickBoardBack(); //�N�G�X�g�I����ʂɖ߂�
                        }
                        // �I�������{�^�����N�G�X�g���e�m�F�{�^���Ȃ��
                        else if (hit.collider.gameObject.tag == "QuestConfirmationButton")
                        {
                            hit.collider.gameObject.GetComponent<QuestData>().ClickConfirmation(); //�N�G�X�g�̓��e�m�F�\��
                                                                                                   // �I�������{�^���̍X�V
                            huntTarget = hit.collider.gameObject;
                        }
                    }
                    else
                    {
                        //�܂��g�[�N���[�h���O��Ă��珈�������Ă��Ȃ�������
                        if (awayProcessFlag == true)
                        {
                            // �g�[�N���[�h���O�ꂽ�炷���ɃN�G�X�g�I����ʂɖ߂�
                            baseQuestButton.GetComponent<QuestData>().ClickBoardBack();
                            awayProcessFlag = false;
                        }
                    }
                }

            }
        }
    }

    public void OnQuestClose(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            clearkOperation.QuestBoardStartEnd();
            ToPlayerMode();
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------
    // Shop
    public void OnShopClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("shopclick");
            wheelShopProcess.ShopClick();
        }
    }
    public void OnShopPageDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (shopClerkOperation.StartEndShop())
            {
                Debug.Log("shopClose");
                SetActionMap(0);
                playerRb.isKinematic = false;
                playerRb.useGravity = true;
            }
        }
    }
    public void OnShopScroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var mouseDelta = context.ReadValue<Vector2>();
            if (mouseDelta != Vector2.zero)
            {
                if (mouseDelta.y < -0.7f)
                { // +
                    inputCount++;
                    if (3 < inputCount)
                    {
                        wheelShopProcess.WheelScroll(mouseDelta.y);
                        inputCount = 0;

                    }
                }
                else if (0.7f < mouseDelta.y)
                { // -
                    inputCount++;
                    if (3 < inputCount)
                    {
                        wheelShopProcess.WheelScroll(mouseDelta.y);
                        inputCount = 0;

                    }
                }
            }
        }
    }
    public void OnModeChange(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("modechage");
            wheelShopProcess.ShopMode();
        }
    }


    //--------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// ActionMap�؂�ւ� 0:Player 1:UI 2:Talk
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
            case 4:
                ToQuestMode();
                break;
            case 5:
                ToShopMode();
                break;
            default:
                Debug.LogWarning("�w��͈͊O");
                break;
        }
    }

    // action�̐ݒ�
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

            questMap[QUEST_CLICK].started += OnQuestClick;
            questMap[QUEST_CLICK].performed += OnQuestClick;
            questMap[QUEST_CLICK].canceled += OnQuestClick;

            questMap[QUEST_CLOSE].started += OnQuestClose;
            questMap[QUEST_CLOSE].performed += OnQuestClose;
            questMap[QUEST_CLOSE].canceled += OnQuestClose;


            shopMap[SHOP_CLICK].started += OnShopClick;
            shopMap[SHOP_CLICK].performed += OnShopClick;
            shopMap[SHOP_CLICK].canceled += OnShopClick;

            shopMap[SHOP_PAGEDOWN].started += OnShopPageDown;
            shopMap[SHOP_PAGEDOWN].performed += OnShopPageDown;
            shopMap[SHOP_PAGEDOWN].canceled += OnShopPageDown;

            shopMap[SHOP_SCROLL].started += OnShopScroll;
            shopMap[SHOP_SCROLL].performed += OnShopScroll;
            shopMap[SHOP_SCROLL].canceled += OnShopScroll;

            shopMap[SHOP_MODECHANGE].started += OnModeChange;
            shopMap[SHOP_MODECHANGE].performed += OnModeChange;
            shopMap[SHOP_MODECHANGE].canceled += OnModeChange;


        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // �Z�b�^�[�E�Q�b�^�[
    public void ReSetRot()
    {
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void SetHuntSelectOrder(bool flag)
    {
        huntSelectOrder = flag;
    }

}

