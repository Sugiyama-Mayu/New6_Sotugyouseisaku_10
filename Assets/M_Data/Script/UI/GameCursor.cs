using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCursor : MonoBehaviour
{
    public GameManager gameManager;

    public Transform cursorObj;

    private float screenLimit;


    [SerializeField] private float cursorSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cursorObj = GameObject.Find("CursorObj").GetComponent<Transform>();
        if (gameManager.GetVRMode != true)
        {
            screenLimit = 2.0f;
        }
        else
        {
            screenLimit = 1.5f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = cursorObj.localPosition;
        cursorPos += gameManager.inputPlayer.GetCurosorMove * cursorSensitivity * Time.deltaTime;
        cursorPos.x = Mathf.Clamp(cursorPos.x, Screen.width / screenLimit * -1, Screen.width / screenLimit);
        cursorPos.y = Mathf.Clamp(cursorPos.y, Screen.height / screenLimit * -1, Screen.height / screenLimit);
        cursorObj.localPosition = new Vector3(cursorPos.x, cursorPos.y, 0.0f);
    }
}
