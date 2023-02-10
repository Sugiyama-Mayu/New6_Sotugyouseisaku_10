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

    public bool SetListBool
    {
        set
        {
            Anim.SetBool("ListBool", value);
        }
    } 

}
