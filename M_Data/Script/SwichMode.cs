using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class SwichMode : MonoBehaviour
{
    public GameManager gameManager;
    public bool playVR = false;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    // VR���[�h�J�n
    public void StartVR()
    {
        var manualXRControl = new ManualXRControl();
        StartCoroutine(manualXRControl.StartXRCoroutine(gameManager));
        Cursor.lockState = CursorLockMode.Locked;
    }

    // VR���[�h�I��
    public void StopVR()
    {
        var manualXRControl = new ManualXRControl();
        manualXRControl.StopXR(gameManager);
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("StopVR");
    }

    // �A�v���I����
    private void OnApplicationQuit()
    {
        if (gameManager.GetSetXRMode == true)
        {
            StopVR();
        }
        //   Debug.Log("OnApplicationQuit");
    }

    public bool getVRmode()
    {
        return playVR;
    }

}

// XR�N���I���֌W�̃N���X
public class ManualXRControl
{
    public IEnumerator StartXRCoroutine(GameManager gameManager)
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
        }
        else
        {
            Debug.Log("Starting XR...");
            
            gameManager.GetSetXRMode = true;
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }
    }

    public void StopXR(GameManager gameManager)
    {
        Debug.Log("Stopping XR...");

        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        gameManager.GetSetXRMode = false;
        Debug.Log("XR stopped completely.");
    }
}
