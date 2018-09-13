using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMoveCalc : MonoBehaviour
{
    float MoveDis = 0;

    Vector3 prePos;

    // Use this for initialization
    void Start()
    {
        prePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float diff = Vector3.Distance(prePos, transform.position);
        prePos = transform.position;
        MoveDis += diff;
    }

    public float GetHandMoveDis()
    {
        return MoveDis;
    }

    public void ResetHandMoveDis()
    {
        MoveDis = 0;
    }

}
