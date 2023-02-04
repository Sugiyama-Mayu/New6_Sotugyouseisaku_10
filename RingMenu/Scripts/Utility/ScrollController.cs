using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    [SerializeField] Scrollbar ScrollBar;

    void Start()
    {

    }

    void Update()
    {
        // �X�N���[���r���[�@�X�N���[�����x��
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            ScrollBar.value += 0.1f;
        }
        if (scroll < 0)
        {
            ScrollBar.value -= 0.1f;
        }
    }
}
