using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCtrl : MonoBehaviour {
    /*
    public bool isZisyaku;
    public bool isMasle;
    public bool isUH;
    */
    public int gametime;
    public float obstacle_Rate;
    public GameObject[] genarratePos;
    public GameObject[] obstacles;
    public GameObject titleObjects;
    public GameObject GameObjects;
    public GameObject gameoverImage;
    public Image beemUI;
    public GameObject beemOKEff;
    public GameObject gameScene;
    public GameObject scoreScene;
    public GameObject scoreCanvaus;
    

    float time = 0;
    int reminingTime;
    float nextGenerateTime;

    public int beemChargeTime;
    int nextbeemTime;
    bool ableBeem = false;

    float obstacleSpeed;

    public int breaknum = 0;

    bool RankCalc = false;
    int gameLevel;


	// Use this for initialization
	void Start () {
        nextGenerateTime = obstacle_Rate;
        gameoverImage.SetActive(false);
        //Debug.Log("Masle is " + isMasle);
        //Debug.Log("UH is " + isUH);

        nextbeemTime = beemChargeTime;
        beemOKEff.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        reminingTime = (int)((float)gametime - time);
        if (reminingTime < 0)
        {
            reminingTime = 0;
            GameEnd();
        }

        if(nextGenerateTime<= time && reminingTime>0)
        {
            nextGenerateTime += obstacle_Rate;
            int r = Random.Range(0, genarratePos.Length);
            float x = genarratePos[r].transform.position.x;
            float z = genarratePos[r].transform.position.z;
            float ymax = Mathf.Tan(Mathf.PI / 6) * 30;
            float y = Random.Range(0, ymax);
            int i = Random.Range(0, obstacles.Length);
            GameObject g=Instantiate(obstacles[i], new Vector3(x, y, z), Quaternion.identity);
            g.GetComponent<Obstaclemove>().SetMoveSpeed(obstacleSpeed);
        }

        if (nextbeemTime <= time && reminingTime > 0)
        {
            ableBeem = true;
            beemOKEff.SetActive(true);
        }
	}

    public void FireBeemEnd()
    {
        ableBeem = false;
        nextbeemTime = (int)time+beemChargeTime;
        beemOKEff.SetActive(false);
    }

    public bool GetAbleBeem()
    {
        return ableBeem;
    }

    public int GetReminingTime()
    {
        return reminingTime;
    }

    public void GameEnd()
    {
        gameoverImage.SetActive(true);
        if (!RankCalc) { FindObjectOfType<Ranking>().SetPram(breaknum, FindObjectOfType<UICtrl>().GetBatteryValue(), reminingTime, gameLevel); RankCalc = true; }
        StartCoroutine("End");
        Obstaclemove[] obst = FindObjectsOfType<Obstaclemove>();
        for (int i = 0; i < obst.Length; i++) { Destroy(obst[i].gameObject); }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(5.0f);
        scoreScene.SetActive(true);
        gameScene.SetActive(false);
        Instantiate(scoreCanvaus, new Vector3(0.0f, 1.5f, -5.0f), new Quaternion(0.0f, 180.0f, 0.0f, 1.0f));
        Instantiate(scoreCanvaus, new Vector3(5.0f, 1.5f, 0.0f), Quaternion.identity).transform.LookAt(new Vector3(10, 1.5f, 0));
        Instantiate(scoreCanvaus, new Vector3(-5.0f, 1.5f, 0.0f), Quaternion.identity).transform.LookAt(new Vector3(-10, 1.5f, 0));
    }

    public void CashMoveSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
    public void SetGameLevel(int l)
    {
        gameLevel = l;
    }
}
