using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// M.S 音の管理
public class RingSound : MonoBehaviour
{
    //BGM BGM番号0～5
    [SerializeField] private AudioClip titleBGM;     //タイトル
    [SerializeField] private AudioClip fieldBGM;     //フィールド
    [SerializeField] private AudioClip shrineBGM;    //祠
    [SerializeField] private AudioClip villageBGM;   //村
    [SerializeField] private AudioClip gameClearBGM; //ゲームクリア
    [SerializeField] private AudioClip gameOverBGM;  //ゲームオーバー
    //SE
     //操作 SE番号上から1～18
    [SerializeField] private AudioClip clickButton; //ボタンクリック(ゲームモード選択)
    [SerializeField] private AudioClip digOreSE;    //鉱石を掘る
    [SerializeField] private AudioClip gameClearSE; //ゲームクリア音
    //アイテム
    [SerializeField] private AudioClip useItemSE;       //アイテム使用
    [SerializeField] private AudioClip enterTheStoreSE; //入店音
    [SerializeField] private AudioClip buyOrSellSE;     //売買       
     //クエスト
    [SerializeField] private AudioClip clearQuestSE;         //クエストクリア
    [SerializeField] private AudioClip talkQuestSE;          //クエスト会話
    [SerializeField] private AudioClip orderReceivedQuestSE; //クエスト受注
    //状態変化
    [SerializeField] private AudioClip damegeSE; //ダメージ
    [SerializeField] private AudioClip diedSE;   //死亡  
     //武器
    [SerializeField] private AudioClip fireArrowSE;   //炎の矢
    [SerializeField] private AudioClip electArrowSE;  //電気の矢
    [SerializeField] private AudioClip arrowAttackSE; //弓攻撃
    [SerializeField] private AudioClip arrowDrawSE;   //弓を絞る
    [SerializeField] private AudioClip swordAttackSE; //剣攻撃
     //祠
    [SerializeField] private AudioClip shrineElectGimmickSE; //祠の電気ギミック
    [SerializeField] private AudioClip shrineFireGimmickSE;  //祠の炎ギミック
    [SerializeField] private AudioClip ShrineGimmickClearSE; //ギミッククリア
    [SerializeField] private AudioClip ShrineGetJewelSE;     //証を手に入れた音

    public AudioSource audioSorceArrow;
    public AudioSource audioSordeDrawShootArrow;
    AudioSource audioSorce;
    private void Start()
    {
        audioSorce = this.GetComponent<AudioSource>();
        audioSorceArrow = audioSorceArrow.GetComponent<AudioSource>();
        audioSordeDrawShootArrow = audioSordeDrawShootArrow.GetComponent<AudioSource>();
    }
    public void RingBGM(int audioNumber)
    {
        switch (audioNumber)
        {
            case 0:
                audioSorce.clip = titleBGM;
                break;
            case 1:
                audioSorce.clip = fieldBGM;
                break;
            case 2:
                audioSorce.clip = shrineBGM;
                break;
            case 3:
                audioSorce.clip = villageBGM;
                break;
            case 5:
                audioSorce.clip = gameOverBGM;
                break;
        }
        audioSorce.Play();
    }
    public void RingSE(int audioNumber)
    {
        switch (audioNumber)
        {
            case 0: //ボタンクリック
                audioSorce.PlayOneShot(clickButton);
                break;
            case 1: //採掘
                audioSorce.PlayOneShot(digOreSE);
                break;
            case 2: //ゲームクリア音
                audioSorce.PlayOneShot(gameClearSE);
                break;
            case 3: //アイテム使用音
                audioSorce.PlayOneShot(useItemSE);
                break;
            case 4: //ショップ入店音
                audioSorce.PlayOneShot(enterTheStoreSE);
                break;
            case 5: //売買音
                audioSorce.PlayOneShot(buyOrSellSE);
                break;
            case 6: //クエストクリア
                audioSorce.PlayOneShot(clearQuestSE);
                break;
            case 7: //クエスト会話
                audioSorce.PlayOneShot(talkQuestSE);
                break;
            case 8: //クエスト受注
                audioSorce.PlayOneShot(orderReceivedQuestSE);
                break;
            case 9: //敵自分ダメージ
                audioSorce.PlayOneShot(damegeSE);
                break;
            case 10: //敵自分死亡
                audioSorce.PlayOneShot(diedSE);
                break;
            case 11: // 炎の矢
                audioSorceArrow.PlayOneShot(fireArrowSE);
                break;
            case 12: // 電気の矢
                audioSorceArrow.PlayOneShot(electArrowSE);
                break;
            case 13: // 弓攻撃
                audioSordeDrawShootArrow.PlayOneShot(arrowAttackSE);
                break;
            case 14: // 弓を絞る
                audioSordeDrawShootArrow.PlayOneShot(arrowDrawSE);
                break;
            case 15: // 剣攻撃
                audioSorce.PlayOneShot(swordAttackSE);
                break;
            case 16: // 祠(電気ギミック)
                audioSorce.PlayOneShot(shrineElectGimmickSE);
                break;
            case 17: // 祠(燃えるギミック)
                audioSorce.PlayOneShot(shrineFireGimmickSE);
                break;
            case 18: // ギミッククリア
                audioSorce.PlayOneShot(ShrineGimmickClearSE);
                break;
            case 19: // 証を手に入れた音
                audioSorce.PlayOneShot(ShrineGetJewelSE);
                break;
        }
    }

    public void StopSE(int selectAudioSorce)
    {
        switch (selectAudioSorce)
        {
            case 0:
                audioSorceArrow.Stop();
                break;
            case 1:
                audioSordeDrawShootArrow.Stop();
                break;
        }
        /*switch (audioNumber)
        {
            case 11: // 炎の矢
                audioSorceArrow.PlayOneShot(fireArrowSE);
                break;
        }*/
    }
}
