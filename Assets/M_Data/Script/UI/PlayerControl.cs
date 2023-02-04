using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [SerializeField] private Transform cursorObj;
    [SerializeField] private Transform canvasPos;
    [SerializeField] private float stopVector;
    [SerializeField] private float[] stopVector1 = new float[] { 30, 200 };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetMoveVector();
    }

    public Vector2 GetMoveVector()
    {
        Vector2 corsorVector = cursorObj.localPosition - canvasPos.localPosition;


        /*
        Debug.Log(canvasPos.localPosition + " : " + cursorObj.localPosition);
        Debug.Log(corsorVector.magnitude);
        */
        if (stopVector1[0] <= corsorVector.magnitude && corsorVector.magnitude <= stopVector1[1])
        {
            corsorVector = corsorVector.normalized;
            return corsorVector;
        }
        else
        {
            return Vector2.zero;
        }
    }


}
