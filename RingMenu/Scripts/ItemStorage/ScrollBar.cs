using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBar : MonoBehaviour
{
    [SerializeField] Scrollbar scrollBar;

    void Start()
    {
        scrollBar.value = 1;
    }

    void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            if(scrollBar.value <= 1)
            {
                scrollBar.value += 0.1f;
            }
        }
        if (scroll < 0)
        {
            if (scrollBar.value >= 0)
            {
                scrollBar.value -= 0.1f;
            }
        }
    }
}
