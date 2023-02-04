using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllShrineManager : MonoBehaviour
{
    //[SerializeField] private AttackBlockManager attackBlockManager;
    //[SerializeField] private MoveBlockManager moveBlockManager;
    //[SerializeField] private TargetManager targetManager;
    [SerializeField] private ShrineClearScript shrineClearScript;
    public bool attackClearFlag;
    public bool moveCrystalClearFlag;
    public bool targetClearFlag;
    private bool clearProcess;
    private void Start()
    {
        attackClearFlag = false;
        moveCrystalClearFlag = false;
        targetClearFlag = false;
        clearProcess = false;
    }
    private void Update()
    {
        if(attackClearFlag == true && moveCrystalClearFlag == true && targetClearFlag == true
            && clearProcess == false)
        {
            shrineClearScript.shrineClearFlag = true;
            clearProcess = true;
        }
    }
}
