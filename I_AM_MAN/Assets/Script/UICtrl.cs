﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour {
    public Text timer;
    public Image battery;
    GameCtrl gameCtrl;
    HeadMoveCalc headMoveCalc;
    HandMoveCalc[] handMovecalc;
    
    float batteryUI_W;
    float batteryUI_H;
    float batteryValue;
    float preMove;

    float preBatteryvalue_Masle;
    float preBatteryvalue_Zisyaku;

    public SerialIO serial;

    // Use this for initialization
    void Start () {
        gameCtrl = FindObjectOfType<GameCtrl>();
        headMoveCalc = FindObjectOfType<HeadMoveCalc>();
        handMovecalc = FindObjectsOfType<HandMoveCalc>();
        
        batteryUI_W = battery.rectTransform.rect.width;
        batteryUI_H = battery.rectTransform.rect.height;

        batteryValue = batteryUI_W;
        preBatteryvalue_Masle = batteryValue;
        preBatteryvalue_Zisyaku = batteryValue;

        preMove = 0;
    }
	
	// Update is called once per frame
	void Update () {
        int time = gameCtrl.GetReminingTime();
        timer.text = "Time:"+time.ToString();

        float headMove = headMoveCalc.GetHeadMoveDis();
        for(int i=0; i<handMovecalc.Length; i++) { headMove += handMovecalc[i].GetHandMoveDis(); }
        batteryValue -= headMove - preMove;
        preMove = headMove;
        if (batteryValue > 0 && batteryValue <= batteryUI_W) battery.rectTransform.sizeDelta = new Vector2(batteryValue, batteryUI_H);
        else if (batteryValue > batteryUI_W) {
            battery.rectTransform.sizeDelta = new Vector2(batteryUI_W, batteryUI_H);
            batteryValue = batteryUI_W;
        }
        else
        {
            batteryValue = 0;
            battery.rectTransform.sizeDelta = new Vector2(0, batteryUI_H);
            gameCtrl.GameEnd();
        }

        //Debug.Log(preBatteryvalue_Masle- batteryValue);
        if ((preBatteryvalue_Masle - batteryValue) >= 10 && gameCtrl.isMasle)
        {
            FindObjectOfType<SerialIO>().Battery10_OverDown((int)batteryValue);
            preBatteryvalue_Masle = batteryValue;
        }

        
        //Debug.Log(preBatteryvalue_Zisyaku- batteryValue);
        if ((preBatteryvalue_Zisyaku-batteryValue) >= 25 && gameCtrl.isZisyaku)
        {
            
            serial.Battery25_OverDown((int)batteryValue);
            preBatteryvalue_Zisyaku = batteryValue;
        }
    }

    public void DeltaBattery(float delta)
    {
        batteryValue += delta;
    }

    public void ResetBattery()
    {
        batteryValue = batteryUI_W;
    }

    private void OnApplicationQuit()
    {
        if (gameCtrl.isMasle)
        {
            FindObjectOfType<SerialIO>().Battery10_OverDown(200);
        }

        if (gameCtrl.isZisyaku)
        {
            FindObjectOfType<SerialIO>().Battery25_OverDown(200);
        }
    }

    public float GetBatteryValue()
    {
        return batteryValue;
    }
}
