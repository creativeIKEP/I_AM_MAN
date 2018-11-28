using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {
    GameObject levelPanel;
    GameStart gs;
    
    void Start ()
    {
        levelPanel = GameObject.Find("levelSerect");
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void TutrialStart()
    {
        GameObject.Find("tutrialButton").SetActive(false);
        FindObjectOfType<GameStart>().Tutrial_Start();
    }
    
    public void Level1()
    {
        gs = FindObjectOfType<GameStart>();
        StartCoroutine(gs.Level1Start());
        levelPanel.GetComponent<Canvas>().enabled = false;
    }

    public void Level2()
    {
        gs = FindObjectOfType<GameStart>();
        StartCoroutine(gs.Level2Start());
        levelPanel.GetComponent<Canvas>().enabled = false;
    }

    public void Level3()
    {
        gs = FindObjectOfType<GameStart>();
        StartCoroutine(gs.Level3Start());
        levelPanel.GetComponent<Canvas>().enabled = false;
    } 
}
