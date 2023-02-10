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
    /// CSV�ǂݍ��� List��n���Ƃ���List�Ƀf�[�^��n��
    /// </summary>
    /// <returns></returns>
    public List<string[]> GetNPCTextRead()
    {
        // NPCTextFile = Resources.Load("testCSV") as TextAsset; // Resouces����CSV�ǂݍ���
        StringReader reader = new StringReader(NPCTextFile.text);

        List<string[]> list = new List<string[]>();

        if (NPCTextFile == null) return null;

        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine(); // ��s���ǂݍ���
            list.Add(line.Split(',')); // , ��؂�Ń��X�g�ɒǉ�
        }
        return list;
    }

    // �e�L�X�g�ǂݍ���
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
