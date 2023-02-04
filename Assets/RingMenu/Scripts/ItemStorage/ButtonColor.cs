using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{
    public SelectionItem selection;

    GameObject buttonObj;

    public int ButtonId;

    void Start()
    {
        
    }

    void Update()
    {
        switch (ButtonId)
        {
            case 0:
                if(selection.n_PosNum == 0)
                {
                    this.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                }
                if(selection.n_PosNum != 0)
                {
                    this.GetComponent<Image>().color = new Color(1, 1, 1);
                }
                break;
            case 1:
                if (selection.n_PosNum == 1)
                {
                    this.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                }
                if (selection.n_PosNum != 1)
                {
                    this.GetComponent<Image>().color = new Color(1, 1, 1);
                }
                break;
            case 2:
                if (selection.n_PosNum == 2)
                {
                    this.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                }
                if (selection.n_PosNum != 2)
                {
                    this.GetComponent<Image>().color = new Color(1, 1, 1);
                }
                break;
            case 3:
                if (selection.n_PosNum == 3)
                {
                    this.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                }
                if (selection.n_PosNum != 3)
                {
                    this.GetComponent<Image>().color = new Color(1, 1, 1);
                }
                break;
            case 4:
                if (selection.n_PosNum == 4)
                {
                    this.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                }
                if (selection.n_PosNum != 4)
                {
                    this.GetComponent<Image>().color = new Color(1, 1, 1);
                }
                break;
            case 5:
                if (selection.n_PosNum == 5)
                {
                    this.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                }
                if (selection.n_PosNum != 5)
                {
                    this.GetComponent<Image>().color = new Color(1, 1, 1);
                }
                break;
            case 6:
                if (selection.n_PosNum == 6)
                {
                    this.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                }
                if (selection.n_PosNum != 6)
                {
                    this.GetComponent<Image>().color = new Color(1, 1, 1);
                }
                break;
            case 7:
                if (selection.n_PosNum == 7)
                {
                    this.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                }
                if (selection.n_PosNum != 7)
                {
                    this.GetComponent<Image>().color = new Color(1, 1, 1);
                }
                break;
            case 8:
                if (selection.n_PosNum == 8)
                {
                    this.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                }
                if (selection.n_PosNum != 8)
                {
                    this.GetComponent<Image>().color = new Color(1, 1, 1);
                }
                break;
            case 9:
                if (selection.n_PosNum == 9)
                {
                    this.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                }
                if (selection.n_PosNum != 9)
                {
                    this.GetComponent<Image>().color = new Color(1, 1, 1);
                }
                break;

        }
    }


}
