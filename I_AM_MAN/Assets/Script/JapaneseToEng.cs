using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JapaneseToEng : MonoBehaviour
{

    public bool isEnglish = false;

    public Text ExText1;
    public Text ExText2;
    public Text ExText3;

    string t1j = "Level1\n\n・パンチスピード : 普通\n・障害物のスピード : 遅い\n・ビーム使用のバッテリー減少 : 大";
    string t2j = "Level2\n\n・パンチスピード : 少し速く\n・障害物のスピード : 普通\n・ビーム使用のバッテリー減少 : 中\n\n・ビル破壊はビームのみ";
    string t3j = "Level3\n\n・パンチスピード : 速く\n・障害物のスピード : 速い\n・ビーム使用のバッテリー減少 : 少\n\n・ビル破壊はビームのみ";

    string t1e = "Level1\n\n・Require panch speed: Normal\n・Obstacles speed: Slow\n・Beem use battery : High";
    string t2e = "Level2\n\n・Require panch speed: Bit faster\n・Obstacles speed: Normal\n・Beem use battery : Normal\n\n・Only beem can break building";
    string t3e = "Level3\n\n・Require panch speed: Faster\n・Obstacles speed: Fast\n・Beem use battery : Low\n\n・Only beem can break building";


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //print(isEnglish);
        if (isEnglish)
        {
            ExText1.text = t1e;
            ExText2.text = t2e;
            ExText3.text = t3e;
            
        }
        else
        {
            
            ExText1.text = t1j;
            ExText2.text = t2j;
            ExText3.text = t3j;
        }
    }

    public void EnglishMode()
    {
        isEnglish = !isEnglish;
        
    }

}
