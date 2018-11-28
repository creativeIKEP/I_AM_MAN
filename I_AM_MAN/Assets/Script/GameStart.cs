using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {
    public GameObject tutrialObjects;
    public GameObject titleObjects;
    public GameObject GameObjects;
    public AudioSource countSound;
    public GameObject PowerAnimcanvas;
    public GameObject uipanel;
    Text countdown;

    // Use this for initialization
    void Start () {
        countdown = GameObject.Find("countdown").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Game_Start()
    {
        GameObjects.SetActive(true);
        titleObjects.SetActive(false);
    }

    public void Tutrial_Start()
    {
        Debug.Log("tutrial");
        tutrialObjects.SetActive(true);
        titleObjects.SetActive(false);
        PowerAnimcanvas.SetActive(false);
        uipanel.SetActive(true);
    }

    public IEnumerator Level1Start()
    {
        countdown.text = "3";
        countSound.Play();
        yield return new WaitForSeconds(1);
        countdown.text = "2";
        countSound.Play();
        yield return new WaitForSeconds(1);
        countdown.text = "1";
        countSound.Play();
        yield return new WaitForSeconds(1);

        Game_Start();
        FindObjectOfType<GameCtrl>().CashMoveSpeed(0.05f);
        FindObjectOfType<GameCtrl>().SetGameLevel(1);
        FindObjectOfType<WhichObstacle>().SetIsWhich(true);
        MY_TrackedController[] controllers = FindObjectsOfType<MY_TrackedController>();
        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].SetPanchSpeed(3);
            controllers[i].SetBeemPower(15);
            controllers[i].SetOnlyBeemMode(false);
        }
    }

    public IEnumerator Level2Start()
    {
        countdown.text = "3";
        countSound.Play();
        yield return new WaitForSeconds(1);
        countdown.text = "2";
        countSound.Play();
        yield return new WaitForSeconds(1);
        countdown.text = "1";
        countSound.Play();
        yield return new WaitForSeconds(1);

        Game_Start();
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

    public IEnumerator Level3Start()
    {
        countdown.text = "3";
        countSound.Play();
        yield return new WaitForSeconds(1);
        countdown.text = "2";
        countSound.Play();
        yield return new WaitForSeconds(1);
        countdown.text = "1";
        countSound.Play();
        yield return new WaitForSeconds(1);

        Game_Start();
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
}
