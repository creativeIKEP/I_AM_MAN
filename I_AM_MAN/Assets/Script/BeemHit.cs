using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeemHit : MonoBehaviour {
    public GameObject beem;
    public GameObject destroyEff;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Obstaclemove>())
        {
            Vector3 pos = other.transform.position;
            Instantiate(destroyEff, pos, Quaternion.identity);
            GameObject.Find("breakSound").GetComponent<AudioSource>().Play();
            Destroy(other);
            beem.SetActive(false);
            MY_TrackedController[] controller = FindObjectsOfType<MY_TrackedController>();
            for (int i = 0; i < controller.Length; i++) { controller[i].ViveStop(); }
            try
            {
                FindObjectOfType<GameCtrl>().breaknum++;
            }
            catch
            {
                FindObjectOfType<TutrialCtrl>().ReceiveBeemHit();
            }
        }
    }
}
