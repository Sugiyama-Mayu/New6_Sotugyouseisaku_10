using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anime : MonoBehaviour
{
    private Animator Anim;
    public Takap.Samples.RingCmdControl RingCmd;


    void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {/*
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Anim.SetBool("ListBool", false);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Anim.SetBool("ListBool", true);
        }
*/
    }

    public bool SetListBool
    {
        set
        {
            Anim.SetBool("ListBool", value);
        }
    } 

}
