using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports;


//http://monolizm.com/sab/pdf/%E7%AC%AC14%E5%9B%9E_%E3%83%97%E3%83%AC%E3%82%BC%E3%83%B3%E8%B3%87%E6%96%99(%E3%82%B7%E3%83%AA%E3%82%A2%E3%83%AB%E9%80%9A%E4%BF%A1%E7%B7%A8).pdf


public class SerialIO : MonoBehaviour {
    public string comMasle;
    public string comZisyaku;
    public int SerialSpeed;
    SerialPort portMasle;
    SerialPort portZisyaku;


    // Use this for initialization
    void Start () {
        if (FindObjectOfType<GameCtrl>().isMasle)
        {
            portMasle = new SerialPort(comMasle, SerialSpeed);
            
            if (portMasle.IsOpen)
            {
                portMasle.Close();
            }
            else
            {
                portMasle.Open();
                portMasle.ReadTimeout = 1000;
                Battery10_OverDown(200);
            }
            
        }

        if (FindObjectOfType<GameCtrl>().isZisyaku)
        {
            portZisyaku = new SerialPort(comZisyaku, SerialSpeed);

            if (portZisyaku.IsOpen)
            {
                portZisyaku.Close();
            }
            else
            {
                portZisyaku.Open();
                portZisyaku.ReadTimeout = 1000;
                Battery25_OverDown(200);
                
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    public void Battery10_OverDown(int battery)
    {
        int data = battery;
        string dataStr="";
        if (data >= 100) { dataStr = data.ToString(); }
        else if(data>=10 && data<100)
        {
            dataStr="0"+ data.ToString();
        }
        else { dataStr = "00" + data.ToString(); }

        byte[] byte1 = new byte[3];
        for (int i = 0; i < 3; i++)
        {
            byte1[i] = (byte)dataStr[i];
        }

        portMasle.Write(byte1, 0, byte1.Length);
        Debug.Log("10down筋肉, "+battery);
    }

    public void BatteryUp_Masle()
    {
        byte[] byte1 = new byte[1];
        byte1[0] = (byte)'A';
        portMasle.Write(byte1, 0, byte1.Length);
        Debug.Log("masle回復");
    }

    public void Battery25_OverDown(int battery)
    {
        
        int data = battery;
        string dataStr = "";
        if (data >= 100) { dataStr = data.ToString(); }
        else if (data >= 10 && data < 100)
        {
            dataStr = "0" + data.ToString();
        }
        else { dataStr = "00" + data.ToString(); }

        byte[] byte1 = new byte[3];
        for (int i = 0; i < 3; i++)
        {
            byte1[i] = (byte)dataStr[i];
        }

        portZisyaku.Write(byte1, 0, byte1.Length);
        Debug.Log("25down電磁石, "+dataStr);
    }

    public void BatteryUP_Zisyaku(int battery)
    {
        
        int data = battery;
        string dataStr = "";
        if (data >= 100) { dataStr = data.ToString(); }
        else if (data >= 10 && data < 100)
        {
            dataStr = "0" + data.ToString();
        }
        else { dataStr = "00" + data.ToString(); }

        byte[] byte1 = new byte[3];
        for (int i = 0; i < 3; i++)
        {
            byte1[i] = (byte)dataStr[i];
        }

        portZisyaku.Write(byte1, 0, byte1.Length);
        Debug.Log("回復電磁石, "+dataStr);
    }
}
