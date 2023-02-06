using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCursor : MonoBehaviour
{
    public GameManager gameManager;

    public Transform cursorObj;
    public Transform controlObj;

    private float screenLimit;
    private Vector2 homePos;
    [SerializeField] private float maxMinOffset=180.0f; 


    [SerializeField] private float cursorSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cursorObj = GameObject.Find("CursorObj").GetComponent<Transform>();
        homePos = controlObj.localPosition;
        if (gameManager.GetVRMode != true)
        {
            screenLimit = 2.0f;
        }
        else
        {
            screenLimit = 1.5f;
        }
        SetiingCursor();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = cursorObj.localPosition;
        cursorPos += gameManager.inputPlayer.GetCurosorMove * cursorSensitivity * Time.deltaTime;
        cursorPos.x = Mathf.Clamp(cursorPos.x, homePos.x - 180.0f, homePos.x + 180.0f);
        cursorPos.y = Mathf.Clamp(cursorPos.y, homePos.y - 180.0f, homePos.y + 180.0f);
        cursorObj.localPosition = new Vector3(cursorPos.x, cursorPos.y, 0.0f);
    }

    public void SetiingCursor()
    {
        cursorObj.localPosition = controlObj.localPosition;
    }
}
