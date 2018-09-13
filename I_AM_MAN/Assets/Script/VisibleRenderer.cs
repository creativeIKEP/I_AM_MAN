using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleRenderer : MonoBehaviour {
    Obstaclemove parent;

	// Use this for initialization
	void Start () {
        parent = gameObject.GetComponentInParent<Obstaclemove>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnBecameVisible()
    {
        if (Camera.current.tag == "MainCamera")
        {
            //Debug.Log("Visible!!!!     " + parent.name);
            parent.SetIsVisible(true);
        }
    }

    private void OnBecameInvisible()
    {
        
            //Debug.Log("Npt Visible.     " + parent.name);
            parent.SetIsVisible(false);
        

    }
}
