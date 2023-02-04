using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
//*****
public class Player : MonoBehaviour
{
    [SerializeField] private SwichMode swichMode;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Transform playerYPos;
    [SerializeField] private bool playerOperate = true; // 操作できるか

    [SerializeField] private float _moveSpeed = 10f;        /// 移動速度
    [SerializeField] private float _lookSpeed = 50f;        /// カメラ回転速度
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject playerObj;

    [SerializeField] private GameObject attackBlock1;    // 祠ギミックアタックブロック
    [SerializeField] private GameObject attackBlock2;
    [SerializeField] private ClerkOperation clearkOperation;
    [SerializeField] private bool clerkMode;
    /// Player Input
    private PlayerInput _playerInput = null;

    private Vector2 _currentMoveInputValue = Vector2.zero;  /// 現在の移動入力値
    private Vector2 _currentLookInputValue = Vector2.zero;  /// 現在のカメラ回転入力値

    /// 前回のカメラの向き
    private Vector3 _preRotation = Vector3.zero;

    // Ray
    private Ray ray;
    [SerializeField] private float maxDistance = 20;
    // bool hitRay = false;

    /// Input Actions
    private const string ACTION_MOVE = "Move";
    private const string ACTION_LOOK = "Look";
    private const string ACTION_FIRE = "Fire";
    private const string ACTION_WEAPON = "Weapon";
    private const string ACTION_ATTACK = "Attack";

    /// Device - ゲームパッド
    private const string DEVICE_GAMEPAD = "Gamepad";

    // M.Sヒットしたゲームオブジェクトの保存(運ぶ処理に使用)
    private GameObject hitObj;
    private bool hitObjFlag = false;
    private bool huntSelectOrder;
    private GameObject huntTarget;
    /// Start
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        huntSelectOrder = false;
        if (TryGetComponent(out _playerInput))
        {
            _playerInput.actions[ACTION_MOVE].started += OnMove;
            _playerInput.actions[ACTION_MOVE].performed += OnMove;
            _playerInput.actions[ACTION_MOVE].canceled += OnMove;

            _playerInput.actions[ACTION_LOOK].started += OnLook;
            _playerInput.actions[ACTION_LOOK].performed += OnLook;
            _playerInput.actions[ACTION_LOOK].canceled += OnLook;
        }
    }

    private void Update()
    {
        if (playerOperate)
        {
            move();
            look();
            RayUpdate();
        }
        /*
        if (Input.GetKeyDown(KeyCode.K))
        {
            VRPlay(true);

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            VRPlay(false);

        }
        */
    }

    // 移動処理
    private void move()
    {
        float cameraForward;
        if (swichMode.getVRmode() == true)
        {
            cameraForward = playerCamera.transform.eulerAngles.y;
        }
        else
        {
            cameraForward = transform.eulerAngles.y;

        }
        var moveForward = Quaternion.Euler(0, cameraForward, 0) * new Vector3(_currentMoveInputValue.x, 0, _currentMoveInputValue.y);
        playerObj.transform.position += moveForward * _moveSpeed * Time.deltaTime;
    }

    // 回転処理
    private void look()
    {
        _preRotation.y += _currentLookInputValue.x * _lookSpeed * Time.deltaTime;
        _preRotation.x -= _currentLookInputValue.y * _lookSpeed * Time.deltaTime;
        _preRotation.x = Mathf.Clamp(_preRotation.x, -89, 89);
        transform.localEulerAngles = _preRotation;
    }

    // ray
    private void RayUpdate()
    {
        ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        // Ray表示
        if (Physics.Raycast(ray, out var hit, maxDistance))
        {
            if (Input.GetKey(KeyCode.L))
            {
                Debug.Log(hit.collider.gameObject.tag);
                Debug.Log(hit.collider.gameObject);

            }
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 0.001f, false);

            var current = Mouse.current;
            // M.S 物を運ぶ処理(MoveObjのタグがつくオブジェクト限定)
            // hitObjFlagで一つずつしか運ばないようにしている
            if(current.middleButton.wasPressedThisFrame && hit.collider.gameObject.tag == "MoveObj"
                && hitObjFlag == false)
            {
                hitObjFlag = true;
                //hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                hitObj = hit.collider.gameObject;
                //hitObj.transform.position = 
                // new Vector3 (playerCamera.transform.localPosition.x, hitObj.transform.localPosition.y, playerCamera.transform.localPosition.z - 0.5f) ;
                hitObj.transform.parent = playerCamera.transform;

            }
            if (hitObjFlag == true)
            {
                Debug.Log("運び中");
                //hitObj.transform.position = playerCamera.transform.position;
                

            }
            if(current.middleButton.wasReleasedThisFrame && hitObjFlag == true)
            {
                //hitObj.GetComponent<Rigidbody>().isKinematic = false;
                hitObjFlag = false;
                //Vector3 dropPos = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y, playerCamera.transform.position.z - 5.0f);
                // hitObj.transform.position = playerObj.transform.forward;
                hitObj.transform.parent = null;
            }
            // 2022.12.18
            if (clerkMode == true)
            {
                if (clearkOperation.talkMode == true)
                {
                   if(huntSelectOrder == true)
                    {
                        if (hit.collider.gameObject.tag == "HuntSelectButton" && hit.collider.gameObject.name == "DecideButton" && Input.GetMouseButtonDown(0))
                        {
                            if (huntTarget.GetComponent<QuestData>().ClickOrderReceived() == true)
                            {
                                huntSelectOrder = false;
                                huntTarget.GetComponent<QuestData>().ClickBoardBack();
                            }
                            else
                            {
                                huntSelectOrder = true;
                            }
                        }
                        else if(hit.collider.gameObject.tag == "HuntSelectButton" && hit.collider.gameObject.name == "BackButton" && Input.GetMouseButtonDown(0))
                        {
                            huntTarget.GetComponent<QuestData>().ClickBoardBack();
                            huntSelectOrder = false;
                        }
                    }
                    else if (hit.collider.gameObject.tag == "QuestConfirmationButton" && Input.GetMouseButtonDown(0))
                    {
                        hit.collider.gameObject.GetComponent<QuestData>().ClickConfirmation();
                        huntTarget = hit.collider.gameObject;
                        huntSelectOrder = true;
                    }
                }
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green, 0.001f, false);
        }
    }

    // InputEvent用関数
    /// 移動処理
    public void OnMove(InputAction.CallbackContext context)
    {
        _currentMoveInputValue = context.ReadValue<Vector2>();
    }

    /// カメラ回転処理
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
            //Debug.Log(hit.collider.gameObject.tag + "だよ");
            // M.S 剣の時のみ祠のアタックブロックのクリックフラグをたてる
            if (hit.collider.gameObject.tag == "Shrine_AttackBlock" && weaponManager.wearSword.activeSelf == true)
            {
                hit.collider.gameObject.GetComponent<AttackChangeMaterial>().clickFlag = true;
            }

            if (hit.collider.gameObject.tag != "Untagged")
            {
                Debug.Log(hit.collider.gameObject.tag);
            }

            // NPC
            if (hit.collider.gameObject.tag == "NPC")
            {
                Debug.Log("会話開始");
                //  playerOperate = false;

            }
        }
    }

    // 武器切り替え・納刀処理
    public void OnWeaponSwitting(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (context.interaction is HoldInteraction)          // 長押し
        {
            weaponManager.WeaponChange(true);
            Debug.Log("武器納刀");
        }
        else if (context.interaction is PressInteraction)   // 押す（通常）
        {
            weaponManager.WeaponChange(true);
            Debug.Log("武器切り替え");
        }
    }

    // 攻撃処理
    public void OnAttack(InputAction.CallbackContext context)
    {
        //if (!weaponManager.GetHaveWeapon()) return;


        if (context.performed)   // 押す（通常）
        {
            if (weaponManager.wearSword.activeSelf == true) // 剣攻撃
            {
                Debug.Log("剣攻撃");
            }
            else if (weaponManager.wearBow.activeSelf == true) // 弓攻撃
            {
                weaponManager.bow.SwitchDrawBow(true);
            }

        }

        // 離す（弓のみ）
        if (context.canceled && weaponManager.wearBow.activeSelf == true)
        {
            weaponManager.bow.ArrowShot();
        }


    }
    // 
    public void SetPlayer(bool b)
    {
        playerOperate = b;
    }

    public void SetHuntSelectOrder(bool flag)
    {
        huntSelectOrder = flag;
    }
}
