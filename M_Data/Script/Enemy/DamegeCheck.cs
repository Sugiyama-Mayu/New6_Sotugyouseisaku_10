using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamegeCheck : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI text;
    public float count;
    bool b;

    private void Start()
    {
        player = GameObject.Find("GameManager").GetComponent<GameManager>().GetPlayerObj();
    }

    private void FixedUpdate()
    {
        text.transform.LookAt(player.transform);

        if (b) 
        {
            if (0 < count) count--;
            else 
            {
                b = false;
                text.text = "";

            }

        } 
    }
    public float SetDamege
    {
        set
        {
            count = 60;
            b = true;
            text.text = value.ToString();
        }
    }

}
