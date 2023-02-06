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

    [Header("�J����")]
    [SerializeField] private GameObject pcCamera;
    [SerializeField] private GameObject phoneVrCamera;
    [SerializeField] private Transform lookObj;


    [Header("�ړ��֌W")]
    private float _moveSpeed = 10f;                             // �ړ����x
    [SerializeField] private float[] setSpeed = { 8.0f, 0.5f };
    [SerializeField] private float _lookSpeed = 50f;            // �J������]���x
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private GameObject UI;

    [Header("���̑�")]
    [SerializeField] private Transform playerYPos;
    [SerializeField] private GameObject talkNpc;

    /// Player Input
    private PlayerInput _playerInput = null;

    InputActionMap playerMap;
    InputActionMap UIMap;
    InputActionMap MenuMap;
    InputActionMap talkMap;

    private Vector2 _currentMoveInputValue = Vector2.zero;      // ���݂̈ړ����͒l
    private Vector2 _currentCursorInputValue = Vector2.zero;    // ���݂̈ړ����͒l
    private Vector2 _currentLookInputValue = Vector2.zero;      // ���݂̃J������]���͒l

    /// �O��̃J�����̌���
    private Vector3 _preRotation = Vector3.zero;

    // Ray
    private Ray ray;
    [SerializeField] private float maxDistance = 20;

    [SerializeField] private GameObject attackBlock1;    // �K�M�~�b�N�A�^�b�N�u���b�N
    [SerializeField] private GameObject attackBlock2;
    [SerializeField] private ClerkOperation clearkOperation;
    //[SerializeField] private bool clerkMode;

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

        // action�ݒ�
        InputActionSetting();
    }

    private void Update()
    {
        move();
        look();
        RayUpdate();
    }

    // �ړ�����
    private void move()
    {
        if(!debugInput) MoveVextor();

        float cameraForward;
        cameraForward = transform.eulerAngles.y;

        var moveForward = Quaternion.Euler(0, cameraForward, 0) * new Vector3(_currentMoveInputValue.x, 0, _currentMoveInputValue.y);
        Vector3 vector = moveForward * _moveSpeed;
        playerRb.velocity =new Vector3(vector.x ,playerRb.velocity.y ,vector.z);

    }

    // ��]����
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

        // Ray�\��
        if (Physics.Raycast(ray, out var hit, maxDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 0.001f, false);
            // M.S �����^�ԏ���(MoveObj�̃^�O�����I�u�W�F�N�g����)
            // hitObjFlag�ň�������^�΂Ȃ��悤�ɂ��Ă���
            // �����グ�鏈��(�����{�^���N���b�N)
            var current = Mouse.current;
            if (current.middleButton.wasPressedThisFrame && hitObjFlag == false)
            {
                if (hit.collider.gameObject.tag == "MoveObj" || hit.collider.gameObject.tag == "MoveObj_2")
                {
                    hitObjFlag = true; // �����グ��t���O�𗧂Ă�
                    hitObj = hit.collider.gameObject; //�������I�u�W�F�N�g�̕ۑ�
                    hitObj.transform.parent = playerCamera.transform;   //�������I�u�W�F�N�g�̐e���J�����ɂ���
                    jewelPosY = hitObj.transform.position.y;            //�������I�u�W�F�N�g��Y���W�̕ۑ�
                    hitObj.GetComponent<BoxCollider>().enabled = false; //�������I�u�W�F�N�g�̃R���C�_�[�𖳌���
                }
            }
            if (hitObjFlag == true)
            {
                Debug.Log("�^�ђ�");
            }
            // ���Ƃ�����(�����{�^������)
            if (current.middleButton.wasReleasedThisFrame && hitObjFlag == true)
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
            // M.S �N�G�X�g�{�[�h�̏���
            //��b���[�h�Ȃ��
            if (clearkOperation.talkMode == true)
            {
                // �I�������{�^�����N�G�X�g�󒍃{�^���Ȃ��
                if (hit.collider.gameObject.tag == "HuntSelectButton" && hit.collider.gameObject.name == "DecideButton" && Input.GetMouseButtonDown(0))
                {
                    // �󒍏������I�������
                    if (huntTarget.GetComponent<QuestData>().ClickOrderReceived() == true)
                    {
                        huntTarget.GetComponent<QuestData>().ClickBoardBack(); //�N�G�X�g�I����ʂɖ߂�
                    }
                }
                // �I�������{�^�����N�G�X�g�I����ʂɖ߂�Ȃ��
                else if (hit.collider.gameObject.tag == "HuntSelectButton" && hit.collider.gameObject.name == "BackButton" && Input.GetMouseButtonDown(0))
                {
                    huntTarget.GetComponent<QuestData>().ClickBoardBack(); //�N�G�X�g�I����ʂɖ߂�
                }
                // �I�������{�^�����N�G�X�g���e�m�F�{�^���Ȃ��
                else if (hit.collider.gameObject.tag == "QuestConfirmationButton" && Input.GetMouseButtonDown(0))
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
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green, 0.001f, false);
        }
    }


    // ActionMap�؂�ւ��֐�
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
    // InputEvent�p�֐�
    //--------------------------------------------------------------------------------------------------------------------------
    // Player
    // �ړ�����
    public void OnMove(InputAction.CallbackContext context)
    {
        _currentMoveInputValue = context.ReadValue<Vector2>();
    //    Debug.Log(_currentMoveInputValue);
    }

    // �J�[�\���v���C���[�ړ��p
    public void MoveVextor()
    {
        _currentMoveInputValue = playerControl.GetMoveVector();
    }

    //�@�J�[�\���ړ�
    public void OnCursorMove(InputAction.CallbackContext context)
    {
        _currentCursorInputValue = context.ReadValue<Vector2>();
        // Debug.Log(_currentCursorInputValue);
    }

    // �J������]����
    public void OnLook(InputAction.CallbackContext context)
    {
        _currentLookInputValue = context.ReadValue<Vector2>();
    }

    // �N���b�N
    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        // Ray������̃I�u�W�F�N�g�ɓ���������
        if (context.performed && Physics.Raycast(ray, out var hit, maxDistance))
        {
            // Debug.Log(hit.collider.gameObject.tag + "����");
            // M.S ���̎��̂��K�̃A�^�b�N�u���b�N�̃N���b�N�t���O�����Ă�
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
                Debug.Log("��b�J�n");
                SetActionMap(2);
                gameManager.uiManager.talkStart(hit.collider.gameObject);
                hit.collider.gameObject.GetComponent<NPCManager>().npcNav.TalkStart(); // Npc���v���C���[�Ɍ�����
                // playerCamera.transform.LookAt(hit.collider.gameObject.GetComponent<NPCManager>().GetNpcFace);
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

    // ����؂�ւ��i�}�E�X�z�C�[���j
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

    // �U���i�E�N���b�N�j
    public void OnLAttack(InputAction.CallbackContext context)
    {
        if (context.performed)   // �����i�ʏ�j
        {
            if (weaponManager.wearSword.activeSelf == true) // ��_��U��
            {
                weaponManager.SwordAttack();
            }
            else if(weaponManager.wearBow.activeSelf == true) // �|�U��
            {
                _moveSpeed = setSpeed[1];
                weaponManager.bow.SwitchDrawBow(true);
            }
        }
        // �����i�|�̂݁j
        if (context.canceled && weaponManager.wearBow.activeSelf == true)
        {
            _moveSpeed = setSpeed[0];
            weaponManager.bow.ArrowShot();
        }
    }
    // �U���i���N���b�N�j
    public void OnRAttack(InputAction.CallbackContext context)
    {
        if (context.performed)   // �����i�ʏ�j
        {
            if (weaponManager.wearSword.activeSelf == true) // ��_���U��
            {
                weaponManager.StrengthAttack();
            }
            else if(weaponManager.wearBow.activeSelf == true)�@//�@�|_��؂�ւ�
            {
                weaponManager.bow.ChengeArrow();
            }
        }
    }

    // �z�C�[���N���b�N  �K�O�̂�
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
    // �Z�b�^�[�E�Q�b�^�[
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
