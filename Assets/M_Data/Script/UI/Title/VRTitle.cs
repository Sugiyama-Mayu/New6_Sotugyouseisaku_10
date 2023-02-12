using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VRTitle : MonoBehaviour
{
    [SerializeField] private Image image;
    float a = 0;

    private void Start()
    {
        image.color = new Vector4(image.color.r, image.color.g, image.color.b, 0);
    }
    public IEnumerator StartGame()
    {
        for (a = 0; a < 1; a += 0.05f)
        {
            image.color = new Vector4(image.color.r, image.color.g, image.color.b, a);
            yield return new WaitForSeconds(0.1f);


        }
        image.color = new Vector4(image.color.r, image.color.g, image.color.b, 1);
        SceneManager.LoadScene("FieldSceneVR");

    }

}
