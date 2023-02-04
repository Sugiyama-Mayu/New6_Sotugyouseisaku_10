using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S タイトルやゲームオーバーなどのオブジェクトをまとめる
public class MainSceneObj : MonoBehaviour
{
    public Canvas gameOverCanvas;      // ゲームオーバー画面でのボタンなどのUI
    public Canvas titleCanvas;         // タイトル画面でのボタンなどのUI
    public GameObject player;          // 通常時使用しているカメラ
    public Camera switchTitleCamera;   // タイトル画面で使用するカメラ
    public GameObject questCanvas;
    public bool menuOffMode;           // 祠の中ではミドルボタンメニューオフ
}
