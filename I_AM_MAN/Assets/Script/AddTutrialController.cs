using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTutrialController : MonoBehaviour {
    public ParticleSystem hitParticle;
    public ParticleSystem ElectParticle;

    SteamVR_Controller.Device device;
    SteamVR_TrackedObject trackedObject;
    Vector3 prePos;
    Vector3 nowPos;
    float distance;

    // Use this for initialization
    void Start() {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObject.index);
        nowPos = transform.position;
        prePos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        prePos = nowPos;
        nowPos = transform.position;
        distance = Vector3.Distance(nowPos, prePos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Obstaclemove>() && distance > 3 * Time.deltaTime)
        {
            GameObject.Find("breakSound").GetComponent<AudioSource>().Play();
            if (collision.gameObject.tag == "Elect")
            {
                BreakElect();
            }
            else
            {
                StartCoroutine("Vive", (ushort)500);
            }

            hitParticle.Play();
            Destroy(collision.gameObject);
            FindObjectOfType<TutrialCtrl>().ReceivePantchHit();
        }
    }

    void BreakElect()
    {
        StartCoroutine("Vive", (ushort)3999);
        ElectParticle.Play();
    }

    IEnumerator Vive(ushort power)
    {

        float wait = 0.01f;
        for (int i = 0; i < (int)(0.1f / wait); i++)
        {
            device.TriggerHapticPulse(power);
            yield return new WaitForSeconds(wait);
        }
    }

    public IEnumerator BeemVive()
    {
        float wait = 0.1f;
        for (int i = 0; i < (int)(2.0f / wait); i++)
        {
            device.TriggerHapticPulse(500);
            yield return new WaitForSeconds(wait);
        }
    }

}

    

