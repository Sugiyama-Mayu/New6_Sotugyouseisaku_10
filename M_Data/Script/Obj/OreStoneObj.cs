using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreStoneObj : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float reFleshCount;
    [SerializeField] private float nowCount;
    [SerializeField] private bool can;

    // Start is called before the first frame update
    void Start()
    {
        can = true;
    }

    private void FixedUpdate()
    {
        if (can) return;
        if( reFleshCount < nowCount)
        {
            nowCount = 0;
            GetSetCanPick = true;
        }
        else
        {
            nowCount += Time.deltaTime;
        }
    }

    public bool GetSetCanPick
    {
        set
        {
            can = value;
            if (can)
            {
                effect.Play();
                Debug.Log("Ä¶");
            }
            else
            {
                effect.Stop();
                Debug.Log("’âŽ~");
            }
        }
        get
        {
            return can;
        }
    }
}
