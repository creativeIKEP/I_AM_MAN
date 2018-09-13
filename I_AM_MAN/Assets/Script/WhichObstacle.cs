using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhichObstacle : MonoBehaviour {
    public AudioSource whichSound;
    Obstaclemove[] obstacles;
    Camera camera;
    public ParticleSystem rightp;
    public ParticleSystem leftp;
    int index;
    float minDis;
    bool isWhich;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
        rightp.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(rightp.main.startColor.color.r, rightp.main.startColor.color.g, rightp.main.startColor.color.b, 0));
        leftp.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(leftp.main.startColor.color.r, leftp.main.startColor.color.g, leftp.main.startColor.color.b, 0));

        leftp.Play();
        rightp.Play();
        
    }

    // Update is called once per frame
    void Update() {
        if (isWhich)
        {
            obstacles = FindObjectsOfType<Obstaclemove>();
            index = -1;
            minDis = float.MaxValue;
            for (int i = 0; i < obstacles.Length; i++)
            {
                float diff = Vector3.Distance(camera.gameObject.transform.position, obstacles[i].gameObject.transform.position);
                if (minDis > diff)
                {
                    minDis = diff;
                    index = i;
                }
            }

            rightp.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(rightp.main.startColor.color.r, rightp.main.startColor.color.g, rightp.main.startColor.color.b, 0));
            leftp.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(leftp.main.startColor.color.r, leftp.main.startColor.color.g, leftp.main.startColor.color.b, 0));
            whichSound.Pause();


            if (index >= 0 && !obstacles[index].GetIsVisibleCenter())
            {
                Vector3 obstPos = new Vector3(obstacles[index].gameObject.transform.position.x, 0, obstacles[index].gameObject.transform.position.z);
                Vector3 camePos = new Vector3(camera.gameObject.transform.position.x, 0, camera.gameObject.transform.position.z);
                Vector3 whichDirection = (obstPos - camePos).normalized;

                Vector3 cameRight = new Vector3(camera.transform.right.x, 0, camera.transform.right.z);
                float angle = Vector3.Angle(cameRight.normalized, whichDirection);
                if (angle <= 90)
                {
                    //right
                    rightp.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(rightp.main.startColor.color.r, rightp.main.startColor.color.g, rightp.main.startColor.color.b, 255));
                    whichSound.Play();
                }
                else
                {
                    //left
                    leftp.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(leftp.main.startColor.color.r, leftp.main.startColor.color.g, leftp.main.startColor.color.b, 255));
                    whichSound.Play();
                }
            }
        }
	}

    public void SetIsWhich(bool b)
    {
        isWhich = b;
    }
}
