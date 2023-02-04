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
    [SerializeField] private bool playerOperate = true; // ����ł��邩

    [SerializeField] private float _moveSpeed = 10f;        /// �ړ����x
    [SerializeField] private float _lookSpeed = 50f;        /// �J������]���x
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject playerObj;

    [SerializeField] private GameObject attackBlock1;    // �K�M�~�b�N�A�^�b�N�u���b�N
    [SerializeField] private GameObject attackBlock2;
    [SerializeField] private ClerkOperation clearkOperation;
    [SerializeField] private bool clerkMode;
    /// Player Input
    private PlayerInput _playerInput = null;

    private Vector2 _currentMoveInputValue = Vector2.zero;  /// ���݂̈ړ����͒l
    private Vector2 _currentLookInputValue = Vector2.zero;  /// ���݂̃J������]���͒l

    /// �O��̃J�����̌���
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

    /// Device - �Q�[���p�b�h
    private const string DEVICE_GAMEPAD = "Gamepad";

    // M.S�q�b�g�����Q�[���I�u�W�F�N�g�̕ۑ�(�^�ԏ����Ɏg�p)
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

    // �ړ�����
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

    // ��]����
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

        // Ray�\��
        if (Physics.Raycast(ray, out var hit, maxDistance))
        {
            if (Input.GetKey(KeyCode.L))
            {
                Debug.Log(hit.collider.gameObject.tag);
                Debug.Log(hit.collider.gameObject);

            }
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 0.001f, false);

            var current = Mouse.current;
            // M.S �����^�ԏ���(MoveObj�̃^�O�����I�u�W�F�N�g����)
            // hitObjFlag�ň�������^�΂Ȃ��悤�ɂ��Ă���
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
                Debug.Log("�^�ђ�");
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

    // InputEvent�p�֐�
    /// �ړ�����
    public void OnMove(InputAction.CallbackContext context)
    {
        _currentMoveInputValue = context.ReadValue<Vector2>();
    }

    /// �J������]����
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
            //Debug.Log(hit.collider.gameObject.tag + "����");
            // M.S ���̎��̂��K�̃A�^�b�N�u���b�N�̃N���b�N�t���O�����Ă�
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
                Debug.Log("��b�J�n");
                //  playerOperate = false;

            }
        }
    }

    // ����؂�ւ��E�[������
    public void OnWeaponSwitting(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (context.interaction is HoldInteraction)          // ������
        {
            weaponManager.WeaponChange(true);
            Debug.Log("����[��");
        }
        else if (context.interaction is PressInteraction)   // �����i�ʏ�j
        {
            weaponManager.WeaponChange(true);
            Debug.Log("����؂�ւ�");
        }
    }

    // �U������
    public void OnAttack(InputAction.CallbackContext context)
    {
        //if (!weaponManager.GetHaveWeapon()) return;


        if (context.performed)   // �����i�ʏ�j
        {
            if (weaponManager.wearSword.activeSelf == true) // ���U��
            {
                Debug.Log("���U��");
            }
            else if (weaponManager.wearBow.activeSelf == true) // �|�U��
            {
                weaponManager.bow.SwitchDrawBow(true);
            }

        }

        // �����i�|�̂݁j
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
