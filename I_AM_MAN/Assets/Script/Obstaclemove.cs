using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaclemove : MonoBehaviour {
    float moveSpeed;
    public float hitDamage;
    Rigidbody rigidbody;
    GameObject player;
    public bool isOnlyBeem;

    bool isVisible = false;
    bool isVisibleCenter = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        rigidbody = GetComponent<Rigidbody>();
	}
	
    

	// Update is called once per frame
	void Update () {
        rigidbody.AddForce((player.transform.position-transform.position).normalized*moveSpeed, ForceMode.VelocityChange);
        transform.Rotate(new Vector3(1, 1, 1));

        Vector3 v1 = (transform.position - Camera.main.transform.position).normalized;
        Vector3 v2 = Camera.main.transform.forward.normalized;
        //内積計算
        float dot = Vector3.Dot(v1, v2);
        //角度差計算(rad)
        float angle= Mathf.Acos(dot);
        if (angle < Mathf.PI / 10)
        {
            isVisibleCenter = true;
        }
        else { isVisibleCenter = false; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            ParticleSystem damage = GameObject.Find("damageEff").GetComponent<ParticleSystem>();
            UICtrl uiCtrl = FindObjectOfType<UICtrl>();
            uiCtrl.DeltaBattery(-hitDamage);
            damage.Play();
            GameObject.Find("damageSound").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }

    /*
    private void OnBecameVisible()
    {
        Debug.Log("Visible!!!!     " + gameObject.name);
        isVisible = true;
    }

    

    private void OnBecameInvisible()
    {
        Debug.Log("Npt Visible.     " + gameObject.name);
        isVisible = false;
    }
    */

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public bool GetIsVisible()
    {
        return isVisible;
    }

    public void SetIsVisible(bool value)
    {
        isVisible = value;
    }

    public bool GetIsVisibleCenter()
    {
        return isVisibleCenter;
    }
}

