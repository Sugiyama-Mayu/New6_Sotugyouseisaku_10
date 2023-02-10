using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollRingSwitchSound : MonoBehaviour
{
    [SerializeField] private RingSound ringSound;
    [SerializeField] private int switchBgmNum;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (switchBgmNum == 0)
            {
                ringSound.RingBGM(3);
            }
            else if (switchBgmNum == 1)
            {
                ringSound.RingBGM(1);
            }
        }
    }
}
