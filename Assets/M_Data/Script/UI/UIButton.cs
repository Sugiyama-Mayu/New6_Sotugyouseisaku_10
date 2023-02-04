using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private int num;

    public int GetNum
    {
        get
        {
            return num;
        }
    }
}
