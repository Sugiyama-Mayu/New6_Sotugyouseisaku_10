using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    [SerializeField] private TextAsset NPCTextFile;
    [SerializeField] private TextAsset weaponDataFile;


    public List<string[]> NPCText;

    // Start is called before the first frame update
    void Start()
    {
        if (NPCTextFile) NPCText = GetNPCTextRead();

    }


    /// <summary>
    /// CSV読み込み Listを渡すとそのListにデータを渡す
    /// </summary>
    /// <returns></returns>
    public List<string[]> GetNPCTextRead()
    {
        // NPCTextFile = Resources.Load("testCSV") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(NPCTextFile.text);

        List<string[]> list = new List<string[]>();

        if (NPCTextFile == null) return null;

        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            list.Add(line.Split(',')); // , 区切りでリストに追加
        }
        return list;
    }

    // テキスト読み込み
    public List<string> GetNpcText(int id)
    {
        List<string> index = new List<string> { };

        for (int i = 0; NPCText[id][i] != "End"; i++)
        {
            index.Add(NPCText[id][i]);
        }
        index.Add("End");
        return index;
    }
}
