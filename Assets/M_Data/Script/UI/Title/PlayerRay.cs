using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRay : MonoBehaviour
{
    [SerializeField] VRTitle VRTitle;
    [SerializeField] Ray[] rays;
    [SerializeField] Transform[] rayTrans;
    private const string PLAYER_FIRE = "Fire";
    InputActionMap TitleMap;
    public RingSound ringSound;


     public PlayerInput _playerInput;
    float maxDistance = 10;
    bool b = false;

    private void Start()
    {
        TitleMap = _playerInput.actions.FindActionMap("Title");

        TitleMap[PLAYER_FIRE].started += OnFire;
        TitleMap[PLAYER_FIRE].performed += OnFire;
        TitleMap[PLAYER_FIRE].canceled += OnFire;
        rays = new Ray[2];

    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        rays[0] = new Ray(rayTrans[0].transform.position, rayTrans[0].transform.forward);
        rays[1] = new Ray(rayTrans[1].transform.position, rayTrans[1].transform.forward);

        RayHit(0);      
        if(!b)RayHit(1);
        Debug.DrawRay(rays[0].origin, rays[0].direction * maxDistance, Color.green, 0.001f, false);
        Debug.DrawRay(rays[1].origin, rays[1].direction * maxDistance, Color.green, 0.001f, false);

    }

    public void RayHit(int i)
    {
        if (Physics.Raycast(rays[i], out var hit, maxDistance))
        {
            Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.name == "Start")
            {
                ringSound.RingSE(0);
                StartCoroutine(VRTitle.StartGame());
                b = true;
            }
        }

    }

}
