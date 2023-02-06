using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public NPCNavMesh npcNav;

    [SerializeField] private bool shop;
    [SerializeField] private string npcID;

    // Start is called before the first frame update
    void Start()
    {
       // npcNav = gameObject.GetComponent<NPCNavMesh>();
    }

    public int GetNpcId
    {
        get { return int.Parse(npcID); }
    }

}