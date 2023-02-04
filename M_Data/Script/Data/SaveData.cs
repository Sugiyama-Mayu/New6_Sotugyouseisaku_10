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
     //   public int money = 0;
        public int[] weaponNum = {0 ,0 ,0 ,0};
        public bool[] warp = { true, true, true, true, true, true };
        //public int[] enemyNum = { 0, 0, 0, 0, 0 };
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
        SaveLoad();
       // SavePlayerData(playData);
    }

    private void Update()
    {
        
    }

    // json‘‚«‚İˆ—
    public void SavePlayerData(PlayerData data)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(data);

        writer = new StreamWriter(filePath, false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();

    }

    // json“Ç‚İ‚İˆ—
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

    private void SaveLoad()
    {
        int i = 0;
        gameManager.playerManager.PlayerPos = playData.pos;  // À•W
        for (i = 0; i < 4; i++) // •Šíî•ñ
        {
            gameManager.playerManager.SetEquipmentNum(i, playData.weaponNum[i]);    
        }
        i = 0;
        foreach(bool b in playData.warp)
        {
            if(b == true)
            {
                gameManager.uiManager.warpPoint.SetFlag(i);
            }
            i++;
        }

    }

}
