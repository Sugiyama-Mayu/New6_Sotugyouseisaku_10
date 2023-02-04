using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Compass : MonoBehaviour
{

    // プレイヤー情報
    [SerializeField] private Transform playerDirection;     // 座標
    private Vector3 playerForwrd;                           // 方向

    // 座標
    [SerializeField] private GameObject[] warpPos;          // ワープポイント
    [SerializeField] private GameObject targetWarpPoint;    // ターゲットワープポイント

    // UI
    private GameObject[] directionText = new GameObject[4];  // 方角
    public GameObject warpPointText;                        // ワープポイント

    [SerializeField] private float compassRange; // 表示角度
    [SerializeField] private float textOffset;

    // Start is called before the first frame update
    void Start()
    {
        targetWarpPoint = warpPos[0];
        directionText[0] = GameObject.Find("North");
        directionText[1] = GameObject.Find("East");
        directionText[2] = GameObject.Find("South");
        directionText[3] = GameObject.Find("West");
        warpPointText = GameObject.Find("Warp");
    }

    // Update is called once per frame
    void Update()
    {
        playerForwrd = playerDirection.forward;
        playerForwrd.y = 0;

        PlayerDirection(playerForwrd);                  // 方位処理
        if (targetWarpPoint != null)
        {
            WarpPoint(playerDirection.transform.position);  // ワープポイントアイコン処理
        }

    }

    // 方位処理
    private void PlayerDirection(Vector3 playerForw)
    {
        Vector3[] posDelta = {  playerForw + Vector3.forward - playerForw , // 北
                                playerForw + Vector3.right - playerForw ,   // 東
                                playerForw + Vector3.back - playerForw ,    // 南
                                playerForw + Vector3.left - playerForw , }; // 西

        for (int i =0; i < 4; i++)
        {
            float angle = Vector3.Angle(playerForw, posDelta[i]);       // 角度計算
            Vector3 axis = Vector3.Cross(playerForw, posDelta[i]);      // 数値が正か負か

            // UI座標設定
            if (axis.y <= 0) directionText[i].transform.localPosition = new Vector3(angle * -7 + textOffset, 0, 0);
            else　directionText[i].transform.localPosition = new Vector3(angle * 7 + textOffset, 0, 0);

            // 表示非表示
            if (angle <= compassRange) directionText[i].SetActive(true);
            else directionText[i].SetActive(false);
        }
    }

    // ワープポイントアイコン処理 
    private void WarpPoint(Vector3 playerPos)
    {
        Vector3 posDelta = targetWarpPoint.transform.position - playerPos;

        // 一番近いワープポイントを探す
        foreach (GameObject obj in warpPos)
        {
            Vector3 posDelta1 = obj.transform.position - playerPos; // 2つの距離を測る
            float targetDistance = Vector3.SqrMagnitude(posDelta);
            float distance = Vector3.SqrMagnitude(posDelta1);
//            Debug.Log(targetDistance + " : " + distance);

            if (distance < targetDistance)                          // 距離の比較・判定
            {
                targetWarpPoint = obj;
                posDelta = obj.transform.position - playerPos;
            }
        }

        float angle = Vector3.Angle(playerForwrd, posDelta);        // 角度計算
        Vector3 axis = Vector3.Cross(playerForwrd, posDelta);       // 数値が正か負か

        if (angle <= compassRange) warpPointText.SetActive(true);   // 表示非表示
        else warpPointText.SetActive(false);

        // UI座標設定
        if (axis.y <= 0) warpPointText.transform.localPosition = new Vector3(angle * -7 + textOffset, 0, 0);
        else warpPointText.transform.localPosition = new Vector3(angle * 7 + textOffset, 0, 0);
    }

}
