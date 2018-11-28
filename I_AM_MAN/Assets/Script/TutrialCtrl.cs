using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutrialCtrl : MonoBehaviour {
    public GameObject title;
    public GameObject tutrial;
    public Text extext;
    public Image batteryPointImage;
    public ParticleSystem leftpartile;
    public ParticleSystem rightpartile;
    public AudioSource directionAudio;
    public GameObject[] obstaclePos;
    public GameObject obst;
    public GameObject[] electObst;
    public GameObject beemLightPoint;
    public ParticleSystem beemStatus;
    public GameObject beemTestobst;

    int state = 0;
    float preBatteryDown;
    GameObject g = null;
    int hitCount = 0;
    Color buff;
    bool ableUseBeem = false;
    bool isEnglish;

    public AudioClip[] japaneseExplainAudio;
    public AudioClip[] englishExplainAudio;
    public AudioSource expainAudio;
    bool isPlayAudioOnce = false;



    // Use this for initialization
    void Start () {
        state=0;
        batteryPointImage.gameObject.SetActive(false);
        GetComponent<WhichObstacle>().enabled = false;
        for (int i = 0; i < electObst.Length; i++)
        {
            electObst[i].SetActive(false);
        }
        buff = extext.color;
        beemLightPoint.SetActive(false);
        beemTestobst.SetActive(false);

        isEnglish = FindObjectOfType<JapaneseToEng>().GetIsEnglish();
    }
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case 0:
                if (!isEnglish)
                {
                    extext.text = "トラックパッドのクリックで進みます";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "Go to next step by clicking track pad.";
                    expainAudio.clip = englishExplainAudio[state];
                }

                    if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 1:
                if (!isEnglish)
                {
                    extext.text = "パワードスーツはバッテリーがないと使えません";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "The powered suit can't move without the battery.";
                    expainAudio.clip = englishExplainAudio[state];
                }
                batteryPointImage.gameObject.SetActive(true);

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 2:
                if (!isEnglish)
                {
                    extext.text = "バッテリーはパワードスーツの運動量に応じて消費されます";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "Battery was decreased according to your movement";
                    expainAudio.clip = englishExplainAudio[state];
                }

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 3:
                batteryPointImage.gameObject.SetActive(false);
                if (!isEnglish)
                {
                    extext.text = "コントローラを振ってバッテリーを減らしてみてください!";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "Try reduce the Battery by swinging your controllers!";
                    expainAudio.clip = englishExplainAudio[state];
                }

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }

                if (FindObjectOfType<TutrialUICtrl>().GetBatteryValue() - preBatteryDown < -10)
                {
                    state++;
                    isPlayAudioOnce = false;
                }

                break;
            case 4:
                if (!isEnglish)
                {
                    extext.text = "バッテリーが減ったのが確認できましたか？";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "Could you confirm that the battery has decreased?";
                    expainAudio.clip = englishExplainAudio[state];
                }
                extext.color = Color.green;

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 5:
                extext.color = buff;
                if (!isEnglish)
                {
                    extext.text = "ゲームを始めると障害物が飛んできます";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "Obstacles will fly to you when you start the game mode.";
                    expainAudio.clip = englishExplainAudio[state];
                }

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 6:
                if (!isEnglish)
                {
                    extext.text = "飛んでくる障害物の方向はパワードスーツが感知して教えてくれます";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "Powered suit tells you to the direction of obstacle.";
                    expainAudio.clip = englishExplainAudio[state];
                }

                leftpartile.Play();
                rightpartile.Play();

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }

                break;
            case 7:
                if (!isEnglish)
                {
                    extext.text = "障害物はパンチで破壊できます。実際にやってみましょう";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "You can destroy obstacles with your punch action. Let's try it!";
                    expainAudio.clip = englishExplainAudio[state];
                }
                rightpartile.Clear();
                leftpartile.Clear();
                rightpartile.Stop();
                leftpartile.Stop();

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }

                break;
            case 8:
                if (!isEnglish)
                {
                    extext.text = "パンチで障害物を破壊してください！";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "Destroy Obstacles with your actual panches!";
                    expainAudio.clip = englishExplainAudio[state];
                }
                extext.color = Color.green;
                GetComponent<WhichObstacle>().enabled = true;
                GetComponent<WhichObstacle>().SetIsWhich(true);
                PantchTest();

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }

                if (hitCount >= 2) { state++; isPlayAudioOnce = false; }
                break;
            case 9:
                if (!isEnglish)
                {
                    extext.text = "素晴らしい！";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "Fantastic!";
                    expainAudio.clip = englishExplainAudio[state];
                }
                extext.color = buff;
                Obstaclemove[] g = FindObjectsOfType<Obstaclemove>();
                for(int i=0; i<g.Length; i++)
                {
                    Destroy(g[i].gameObject);
                }
                GetComponent<WhichObstacle>().enabled = false;
                rightpartile.Clear();
                leftpartile.Clear();
                rightpartile.gameObject.SetActive(false);
                leftpartile.gameObject.SetActive(false);
                directionAudio.Stop();

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 10:
                if (!isEnglish)
                {
                    extext.text = "ゲームモードでは障害物が自分にあたると、バッテリーが減るので気を付けて！";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "The battery will decrease, when obstacles hit you in the game mode. Be careful!";
                    expainAudio.clip = englishExplainAudio[state];
                }

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 11:
                if (!isEnglish)
                {
                    extext.text = "障害物の中には電気をまとったものがあります";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "Some of obstacle is electrical appliance.";
                    expainAudio.clip = englishExplainAudio[state];
                }
                for (int i=0; i<electObst.Length; i++)
                {
                    electObst[i].SetActive(true);
                }

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 12:
                if (!isEnglish)
                {
                    extext.text = "これを破壊するとバッテリーが回復します！";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "The battery will recover when you destroy obstacles!";
                    expainAudio.clip = englishExplainAudio[state];
                }
                extext.color = Color.green;

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 13:
                if (!isEnglish)
                {
                    extext.text = "最後にビームの使い方を説明します";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "Finally, I will explain how to use the beem.";
                    expainAudio.clip = englishExplainAudio[state];
                }
                extext.color = buff;
                for (int i = 0; i < electObst.Length; i++)
                {
                    electObst[i].SetActive(false);
                }

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 14:
                if (!isEnglish)
                {
                    extext.text = "遠くにある障害物もビームで破壊できます";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "You can also destroy obstacles with beem.";
                    expainAudio.clip = englishExplainAudio[state];
                }


                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 15:
                if (!isEnglish)
                {
                    extext.text = "コントローラのトリガーボタンを押し込み、約2秒後に離すと発射できます";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "To fire the beem, push the trigger button of controllers and release it after 2 seconds.";
                    expainAudio.clip = englishExplainAudio[state];
                }


                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 16:
                if (!isEnglish)
                {
                    extext.text = "ただし、ビームエネルギーがたまらないと使えません";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "But beem can't use without energy.";
                    expainAudio.clip = englishExplainAudio[state];
                }

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 17:
                if (!isEnglish)
                {
                    extext.text = "ビームエネルギーがたまるとここが光ります";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "When you can use beem, here it glows.";
                    expainAudio.clip = englishExplainAudio[state];
                }

                beemLightPoint.SetActive(true);

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 18:
                if (!isEnglish)
                {
                    extext.text = "ではビームで障害物を破壊してみましょう";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "Let's try destroy obstacles with beem!";
                    expainAudio.clip = englishExplainAudio[state];
                }

                beemLightPoint.SetActive(false);

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 19:
                extext.color = Color.green;
                if (!isEnglish)
                {
                    extext.text = "ビームの光線が障害物にあたるように発射してください！";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "Fire so that the beem hits the obstacle!";
                    expainAudio.clip = englishExplainAudio[state];
                }

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 20:
                if (!isEnglish)
                {
                    extext.text = "素晴らしい！";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "Fantastic!";
                    expainAudio.clip = englishExplainAudio[state];
                }
                extext.color = buff;
                ableUseBeem = false;
                beemStatus.Clear();
                beemStatus.Stop();

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 21:
                if (!isEnglish)
                {
                    extext.text = "以上でチュートリアルは終了です";
                    expainAudio.clip = japaneseExplainAudio[state];
                }
                else
                {
                    extext.text = "That concludes the tutorial.";
                    expainAudio.clip = englishExplainAudio[state];
                }

                if (!isPlayAudioOnce)
                {
                    expainAudio.Play();
                    isPlayAudioOnce = true;
                }
                break;
            case 22:
                title.SetActive(true);
                tutrial.SetActive(false);
                break;
            default:
                break;
        }
	}

    public void ReceivePadEvent()
    {
        if (state == 2)
        {
            preBatteryDown = FindObjectOfType<TutrialUICtrl>().GetBatteryValue();
        }
        else if (state == 3) { return; }
        else if (state == 5) { directionAudio.Play(); }
        else if (state == 6) { directionAudio.Stop(); }
        else if (state == 8) return;
        else if (state == 18)
        {
            BeemPractice();
            beemTestobst.SetActive(true);
        }
        else if (state == 19) return;
        state++;
        Debug.Log("stateNO: " + state);
        isPlayAudioOnce = false;
    }

    public void ReceiveBeemHit()
    {
        state++;
        isPlayAudioOnce = false;
    }

    void createObstacle()
    {
        int r = Random.Range(0, 3);
        Instantiate(obst, obstaclePos[r].transform.position, Quaternion.identity);
    }

    void PantchTest()
    {
        if (g == null)
        {
            int r = Random.Range(0, obstaclePos.Length);
            float x = obstaclePos[r].transform.position.x;
            float z = obstaclePos[r].transform.position.z;
            float ymax = Mathf.Tan(Mathf.PI / 6) * 30;
            float y = Random.Range(0, ymax);

            g = Instantiate(obst, new Vector3(x, y, z), Quaternion.identity);
            g.GetComponent<Obstaclemove>().SetMoveSpeed(0.05f);
        }
     }

    void BeemPractice()
    {
        beemStatus.Play();
        ableUseBeem = true;
    }
    
    public void ReceivePantchHit()
    {
        hitCount++;
    }
    public bool GetAbleIsBeem()
    {
        return ableUseBeem;
    }
}
