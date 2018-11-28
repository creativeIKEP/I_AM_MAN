using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutrialUICtrl : MonoBehaviour {
    public Image battery;
    HeadMoveCalc headMoveCalc;
    HandMoveCalc[] handMovecalc;
    float batteryValue;
    float preMove;
    float batteryUI_W;
    float batteryUI_H;

    // Use this for initialization
    void Start () {
        headMoveCalc = FindObjectOfType<HeadMoveCalc>();
        handMovecalc = FindObjectsOfType<HandMoveCalc>();
        batteryUI_W = battery.rectTransform.rect.width;
        batteryUI_H = battery.rectTransform.rect.height;

        batteryValue = batteryUI_W;
        preMove = 0;
    }
	
	// Update is called once per frame
	void Update () {
        float headMove = headMoveCalc.GetHeadMoveDis();
        for (int i = 0; i < handMovecalc.Length; i++) { headMove += handMovecalc[i].GetHandMoveDis(); }
        batteryValue -= headMove - preMove;
        preMove = headMove;
        if (batteryValue > 0 && batteryValue <= batteryUI_W) battery.rectTransform.sizeDelta = new Vector2(batteryValue, batteryUI_H);
        else if (batteryValue > batteryUI_W)
        {
            battery.rectTransform.sizeDelta = new Vector2(batteryUI_W, batteryUI_H);
            batteryValue = batteryUI_W;
        }
        else
        {
            batteryValue = 0;
            battery.rectTransform.sizeDelta = new Vector2(0, batteryUI_H);
        }
    }

    public float GetBatteryValue()
    {
        return batteryValue;
    }
}
