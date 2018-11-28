using UnityEngine;
using System.Collections;


public class MY_TrackedController : MonoBehaviour
{
    SteamVR_TrackedController trackedController;
    public string whichHand;
    public ParticleSystem hitParticle;
    public ParticleSystem ElectParticle;
    public GameObject beemObject;
    public ParticleSystem Beem;
    bool isPanch = false;
    UICtrl uiCtrl;
    SteamVR_TrackedObject trackedObject;
    SteamVR_Controller.Device device;
    DemoScript UH;
    bool isStart = false;
    GameCtrl gameCtrl;
    bool beemCharge = false;
    float time = 0;
    float beemChargeStartTime;
    bool isBeemFire = false;
    public GameObject chargeEff;
    bool isBeemVive = false;
    public bool isCharge;

    Vector3 prePos;
    Vector3 nowPos;
    float distance;
    int panchSpeed;
    int beemPower;
    bool isOnlyBeemMode;

    SerialIO serial;

    void Start()
    {
        trackedController = gameObject.GetComponent<SteamVR_TrackedController>();
        uiCtrl = FindObjectOfType<UICtrl>();
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObject.index);
        UH = FindObjectOfType<DemoScript>();
        gameCtrl = FindObjectOfType<GameCtrl>();
        serial = FindObjectOfType<SerialIO>();
        chargeEff.SetActive(false);

        nowPos = transform.position;
        prePos = transform.position;

        if (trackedController == null)
        {
            trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
        }

        trackedController.MenuButtonClicked += new ClickedEventHandler(DoMenuButtonClicked);
        trackedController.MenuButtonUnclicked += new ClickedEventHandler(DoMenuButtonUnClicked);
        trackedController.TriggerClicked += new ClickedEventHandler(DoTriggerClicked);
        trackedController.TriggerUnclicked += new ClickedEventHandler(DoTriggerUnclicked);
        trackedController.SteamClicked += new ClickedEventHandler(DoSteamClicked);
        trackedController.PadClicked += new ClickedEventHandler(DoPadClicked);
        trackedController.PadUnclicked += new ClickedEventHandler(DoPadClicked);
        trackedController.PadTouched += new ClickedEventHandler(DoPadTouched);
        trackedController.PadUntouched += new ClickedEventHandler(DoPadTouched);
        trackedController.Gripped += new ClickedEventHandler(DoGripped);
        trackedController.Ungripped += new ClickedEventHandler(DoUngripped);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (beemCharge && (time-beemChargeStartTime)>2.0f)
        {
            isBeemFire = true;
        }

        prePos = nowPos;
        nowPos = transform.position;
        distance = Vector3.Distance(nowPos, prePos);
    }


    public void DoMenuButtonClicked(object sender, ClickedEventArgs e)
    {
        //Debug.Log(whichHand+": "+"DoMenuButtonClicked");
    }

    public void DoMenuButtonUnClicked(object sender, ClickedEventArgs e)
    {
        //Debug.Log(whichHand + ": " + "DoMenuButtonUnClicked");
    }

    public void DoTriggerClicked(object sender, ClickedEventArgs e)
    {
        //Debug.Log(whichHand + ": " + "DoTriggerClicked");
        if (gameCtrl.GetAbleBeem())
        {
            beemCharge = true;
            beemChargeStartTime = time;
            chargeEff.SetActive(true);
            GameObject.Find("beemCharge").GetComponent<AudioSource>().Play();
        }
    }

    public void DoTriggerUnclicked(object sender, ClickedEventArgs e)
    {
        //Debug.Log(whichHand + ": " + "DoTriggerUnclicked");
        if(isStart && gameCtrl.GetAbleBeem() && isBeemFire)BeemStart();
        isStart = true;
        beemCharge = false;
        chargeEff.SetActive(false);
        GameObject.Find("beemCharge").GetComponent<AudioSource>().Stop();
    }

    public void DoSteamClicked(object sender, ClickedEventArgs e)
    {
        //Debug.Log(whichHand + ": " + "DoSteamClicked");
    }

    public void DoPadClicked(object sender, ClickedEventArgs e)
    {
        //Debug.Log(whichHand + ": " + "DoPadClicked");
    }

    public void DoPadUnclicked(object sender, ClickedEventArgs e)
    {
        //Debug.Log(whichHand + ": " + "DoPadUnclicked");
    }

    public void DoPadTouched(object sender, ClickedEventArgs e)
    {
        //Debug.Log(whichHand + ": " + "DoPadTouched");
    }

    public void DoPadUntouched(object sender, ClickedEventArgs e)
    {
        //Debug.Log(whichHand + ": " + "DoPadUntouched");
    }

    public void DoGripped(object sender, ClickedEventArgs e)
    {
        //Debug.Log(whichHand + ": " + "DoGripped");
        isPanch = true;
    }

    public void DoUngripped(object sender, ClickedEventArgs e)
    {
        //Debug.Log(whichHand + ": " + "DoUngripped");
        isPanch = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Obstaclemove>()  && distance > panchSpeed * Time.deltaTime)
        {
            if (!isOnlyBeemMode || (isOnlyBeemMode && !(collision.gameObject.GetComponent<Obstaclemove>().isOnlyBeem)))
            {
                GameObject.Find("breakSound").GetComponent<AudioSource>().Play();
                if (collision.gameObject.tag == "Elect" && isCharge)
                {
                    BreakElect();
                }
                else
                {
                    StartCoroutine("Vive", (ushort)500);
                }

                hitParticle.Play();
                Destroy(collision.gameObject);
                gameCtrl.breaknum++;
            }
        }

    }

    void BeemStart()
    {
        chargeEff.SetActive(false);
        beemObject.SetActive(true);
        Beem.Play();
        GameObject.Find("beemSound").GetComponent<AudioSource>().Play();
        StartCoroutine("BeemVive");
        uiCtrl.DeltaBattery(-beemPower);
        gameCtrl.FireBeemEnd();
        isBeemFire = false;
    }

    void BreakElect()
    {
        uiCtrl.DeltaBattery(15);
        if (serial.isMasle) { FindObjectOfType<SerialIO>().BatteryUp_Masle(); }
        if (serial.isZisyaku) { FindObjectOfType<SerialIO>().BatteryUP_Zisyaku((int)FindObjectOfType<UICtrl>().GetBatteryValue()); }
        StartCoroutine("Vive", (ushort)3999);
        ElectParticle.Play();

        //UnlimitedHand作動
        if (serial.isUH) { UH.biribiri_(); }
    }

    IEnumerator Vive(ushort power)
    {
        
        float wait = 0.01f;
        for(int i=0; i<(int)(0.1f/wait); i++)
        {
            device.TriggerHapticPulse(power);
            yield return new WaitForSeconds(wait);
        }
    }

    public IEnumerator BeemVive()
    {
        isBeemVive = true;
        float wait = 0.1f;
        for (int i = 0; i < (int)(2.0f / wait); i++)
        {
            if(isBeemVive)device.TriggerHapticPulse(500);
            yield return new WaitForSeconds(wait);
        }
    }

    public void ViveStop()
    {
        isBeemVive = false;
    }

    public void SetPanchSpeed(int s)
    {
        panchSpeed = s;
    }

    public void SetBeemPower(int p)
    {
        beemPower = p;
    }
    public void SetOnlyBeemMode(bool b)
    {
        isOnlyBeemMode = b;
    }
}
