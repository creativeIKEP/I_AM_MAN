using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMoveCalc : MonoBehaviour
{
    float headMoveDis = 0;

    Vector3 preHeadPos;

    // Use this for initialization
    void Start()
    {
        preHeadPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float diff = Vector3.Distance(preHeadPos, transform.position);
        preHeadPos = transform.position;
        headMoveDis += diff;
    }

    public float GetHeadMoveDis()
    {
        return headMoveDis;
    }

    public void ResetHeadMoveDis()
    {
        headMoveDis = 0;
    }

}
