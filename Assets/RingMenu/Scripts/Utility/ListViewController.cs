using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListViewController : MonoBehaviour
{
    public RectTransform content_;
    public GameObject item_prefab_;
    public string[] itemList_;
    private float itemHight_;

    void Start()
    {
        GameObject item = GameObject.Instantiate(item_prefab_) as GameObject;
        RectTransform rect = item.GetComponent<RectTransform>();
        itemHight_ = rect.rect.height;
        GameObject.Destroy(item);
    }

    void Update()
    {
     //   UpdateListView();
    }

    private void UpdateListView()
    {
        // item���ɍ��킹��Content�̍�����ύX����.
        int setting_count = itemList_.Length;
        float newHeight = setting_count * itemHight_;
        content_.sizeDelta = new Vector2(content_.sizeDelta.x, newHeight); // ������ύX����.

        // Content�̎q�v�f��ListViewItem��ǉ����Ă���.
        foreach (string itemStr in itemList_)
        {
            GameObject item = GameObject.Instantiate(item_prefab_) as GameObject; // ListViewItem �̃C���X�^���X�쐬.
            Text itemText = item.GetComponentInChildren<Text>(); // Text�R���|�[�l���g���擾.
            itemText.text = itemStr;

            RectTransform itemTransform = (RectTransform)item.transform;
            itemTransform.SetParent(content_, false); // �쐬����Item��Content�̎q�v�f�ɐݒ�.
        }
    }
    //private void RemoveAllListViewItem()
    //{
    //    foreach (Transform child in content_.transform)
    //    {
    //        GameObject.Destroy(child.gameObject);
    //    }
    //}
}
