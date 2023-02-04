using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public RectTransform content_;
    private float itemHight_;

    public GameObject ListObject;
    public List<GameObject> ObjectList = new List<GameObject>();
    private int ObjectCount;


    // 空のオブジェクトを作成
    private GameObject Item0;
    private GameObject Item1;
    private GameObject Item2;
    private GameObject Item3;
    private GameObject Item4;
    private GameObject Item5;
    private GameObject Item6;
    private GameObject Item7;
    private GameObject Item8;
    private GameObject Item9;

    void Start()
    {
        Create();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Create();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(ObjectCount);
        }
    }

    public void Create()
    {
        while (ObjectCount < 10)
        {
            switch (ObjectCount)
            {
                case 0:
                    Item0 = GameObject.Instantiate(ListObject) as GameObject;
                    Item0.name = "aaa";
                    ObjectList.Add(Item0);
                    break;
                case 1:
                    Item1 = GameObject.Instantiate(ListObject) as GameObject;
                    Item1.name = "bbb";
                    ObjectList.Add(Item1);
                    break;
                case 2:
                    Item2 = GameObject.Instantiate(ListObject) as GameObject;
                    Item2.name = "ccc";
                    ObjectList.Add(Item2);
                    break;
                case 3:
                    Item3 = GameObject.Instantiate(ListObject) as GameObject;
                    Item3.name = "ddd";
                    ObjectList.Add(Item3);
                    break;
                case 4:
                    Item4 = GameObject.Instantiate(ListObject) as GameObject;
                    Item4.name = "eee";
                    ObjectList.Add(Item4);
                    break;
                case 5:
                    Item5 = GameObject.Instantiate(ListObject) as GameObject;
                    Item5.name = "fff";
                    ObjectList.Add(Item5);
                    break;
                case 6:
                    Item6 = GameObject.Instantiate(ListObject) as GameObject;
                    Item6.name = "ggg";
                    ObjectList.Add(Item6);
                    break;
                case 7:
                    Item7 = GameObject.Instantiate(ListObject) as GameObject;
                    Item7.name = "hhh";
                    ObjectList.Add(Item7);
                    break;
                case 8:
                    Item8 = GameObject.Instantiate(ListObject) as GameObject;
                    Item8.name = "iii";
                    ObjectList.Add(Item8);
                    break;
                case 9:
                    Item9 = GameObject.Instantiate(ListObject) as GameObject;
                    Item9.name = "jjj";
                    ObjectList.Add(Item9);
                    break;
            }

            if (ObjectCount < 10)
            {
                ObjectCount++;
            }
        }

    }

}
