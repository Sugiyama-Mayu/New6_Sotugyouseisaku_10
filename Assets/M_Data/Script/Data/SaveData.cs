using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public string name = "";
        public Vector3 pos = Vector3.zero;
        public int[] weaponNum = {0 ,0 ,0 , 0, 0};
        public bool[] warp = { true, true, true, true, true, true };
    }
    [SerializeField] private GameManager gameManager;

    [SerializeField] private PlayerData playData;
    private string filePath;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.dataPath + "/"+ "SaveData" + "/"+ "savedata.json";
        playData = LoadPlayerData();
       // DataLoad();
    }

    private void OnApplicationQuit()
    {
        SavePlayerData(playData);
    }

    // json書き込み処理
    public void SavePlayerData(PlayerData data)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(data);

        writer = new StreamWriter(filePath, false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();

    }

    // json読み込み処理
    public PlayerData LoadPlayerData()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(filePath);
        datastr = reader.ReadToEnd();
        reader.Close();
        Debug.Log(datastr);

        return JsonUtility.FromJson<PlayerData>(datastr);
    }

    // 初めから
    public void ResetGameData()
    {
        playData.weaponNum = new int[5];
        SavePlayerData(playData);
        Debug.Log("データリセット");
    }

    // 続きから
    public void DataLoad()
    {
        Debug.Log("データロード");

        int i = 0;
        gameManager.playerManager.PlayerPos = playData.pos;  // 座標
        i = 0;
        foreach(bool b in playData.warp)
        {
            if(b == true)
            {
                gameManager.uiManager.warpPoint.SetFlag(i);
            }
            i++;
        }
        gameManager.equimentManager.SetEquimentLevel = playData.weaponNum;
    }

    public int[] SaveEquimentLevel
    {
        set
        {
            for(int i=0; i<playData.weaponNum.Length; i++)
            {
                playData.weaponNum[i] = value[i];
            }
            Debug.Log(playData.weaponNum[0]+""+playData.weaponNum[1] + "" + playData.weaponNum[2] + "" + playData.weaponNum[3] + "" + playData.weaponNum[4]);
        }
    }


}
