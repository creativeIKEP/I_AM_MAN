using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {
    public GameObject titleObjects;
    public GameObject GameObjects;

    
    // Use this for initialization
    private void Awake()
    {
        titleObjects.SetActive(true);
        GameObjects.SetActive(false);
    }
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    
    public void Level1()
    {
        GameStart();
        FindObjectOfType<GameCtrl>().CashMoveSpeed(0.05f);
        FindObjectOfType<GameCtrl>().SetGameLevel(1);
        FindObjectOfType<WhichObstacle>().SetIsWhich(true);
        MY_TrackedController[] controllers = FindObjectsOfType<MY_TrackedController>();
        for(int i=0; i<controllers.Length; i++)
        {
            controllers[i].SetPanchSpeed(3);
            controllers[i].SetBeemPower(15);
            controllers[i].SetOnlyBeemMode(false);
        }
    }

    public void Level2()
    {
        GameStart();
        FindObjectOfType<GameCtrl>().CashMoveSpeed(0.1f);
        FindObjectOfType<GameCtrl>().SetGameLevel(2);
        FindObjectOfType<WhichObstacle>().SetIsWhich(true);
        MY_TrackedController[] controllers = FindObjectsOfType<MY_TrackedController>();
        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].SetPanchSpeed(5);
            controllers[i].SetBeemPower(10);
            controllers[i].SetOnlyBeemMode(true);
        }
    }

    public void Level3()
    {
        GameStart();
        FindObjectOfType<GameCtrl>().CashMoveSpeed(0.3f);
        FindObjectOfType<GameCtrl>().SetGameLevel(3);
        FindObjectOfType<WhichObstacle>().SetIsWhich(true);
        MY_TrackedController[] controllers = FindObjectsOfType<MY_TrackedController>();
        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].SetPanchSpeed(7);
            controllers[i].SetBeemPower(5);
            controllers[i].SetOnlyBeemMode(true);
        }
    }

    void GameStart()
    {
        GameObjects.SetActive(true);
        titleObjects.SetActive(false);
    }
}
