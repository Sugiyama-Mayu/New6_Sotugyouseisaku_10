using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S
// 選択中の弓に合わせたSEの再生
public class ArrowSoundManager : MonoBehaviour
{
    [SerializeField] private Bow bow;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private RingSound ringSound;
    private int oldArrowNum;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //弓選択状態かどうか
        if (weaponManager.weaopn == 2)
        {
            //0 通常矢  1 炎矢  2 電気矢
            switch (bow.arrowNum)
            {
                case 0:
                    ringSound.StopSE(0);
                    break;
                case 1:
                    if(oldArrowNum != bow.arrowNum)
                    {
                        ringSound.StopSE(0);
                        oldArrowNum = bow.arrowNum;
                    }
                    if (!ringSound.audioSorceArrow.isPlaying)
                    {
                        ringSound.RingSE(11);
                    }
                    break;
                case 2:
                    if (oldArrowNum != bow.arrowNum)
                    {
                        ringSound.StopSE(0);
                        oldArrowNum = bow.arrowNum;
                    }
                    if (!ringSound.audioSorceArrow.isPlaying)
                    {
                        ringSound.RingSE(12);
                    }
                    break;
            }

        }
        else
        {
            ringSound.StopSE(0);

        }
    }
}
