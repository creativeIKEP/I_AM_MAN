using UnityEngine;
using System.Collections;


public class MY_TrackedController2 : MonoBehaviour
{
    SteamVR_TrackedController trackedController;
    SteamVR_TrackedObject trackedObject;
    SteamVR_Controller.Device device;

    TutrialCtrl tutrialCtrl;

    bool beemCharge=false;
    float beemChargeStartTime;
    public GameObject chargeEff;
    float time = 0;
    bool isBeemFire = false;
    public ParticleSystem Beem;



    void Start()
    {
        trackedController = gameObject.GetComponent<SteamVR_TrackedController>();
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObject.index);
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
        trackedController.PadUnclicked += new ClickedEventHandler(DoPadUnclicked);
        trackedController.PadTouched += new ClickedEventHandler(DoPadTouched);
        trackedController.PadUntouched += new ClickedEventHandler(DoPadUntouched);
        trackedController.Gripped += new ClickedEventHandler(DoGripped);
        trackedController.Ungripped += new ClickedEventHandler(DoUngripped);

        tutrialCtrl = FindObjectOfType<TutrialCtrl>();

        chargeEff.gameObject.SetActive(false);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (beemCharge && (time - beemChargeStartTime) > 2.0f)
        {
            isBeemFire = true;
        }
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
        if (tutrialCtrl.GetAbleIsBeem())
        {
            beemCharge = true;
            beemChargeStartTime = time;
            chargeEff.gameObject.SetActive(true);
            GameObject.Find("beemCharge").GetComponent<AudioSource>().Play();
        }
    }

    public void DoTriggerUnclicked(object sender, ClickedEventArgs e)
    {
        //Debug.Log(whichHand + ": " + "DoTriggerUnclicked");
        if (tutrialCtrl.GetAbleIsBeem() && isBeemFire) BeemStart();
        beemCharge = false;
        chargeEff.gameObject.SetActive(false);
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
        Vector2 padPos = device.GetAxis();
        //Debug.Log(padPos);
        tutrialCtrl.ReceivePadEvent();
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
        
    }

    public void DoUngripped(object sender, ClickedEventArgs e)
    {
        //Debug.Log(whichHand + ": " + "DoUngripped");
        
    }

    void BeemStart()
    {
        chargeEff.gameObject.SetActive(false);
        Beem.Play();
        GameObject.Find("beemSound").GetComponent<AudioSource>().Play();
        isBeemFire = false;
    }
}
