using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for List<>
using UnityEngine.UI;
using System.Threading;

public class DemoScript : MonoBehaviour {
	public Toggle toggle;
	public Toggle toggleQuaternion;
	public UH unlimitedhand;
	public int padNum;
    

	void Start () { }
	
	void Update () {}

	
	public void biribiri_(){
        Thread thread = new Thread(biriThread);
        thread.Start();
	}
	
    void biriThread()
    {
        unlimitedhand.vibrate_Start();
        unlimitedhand.stimulateSetSleepTime(5, 500);
        Debug.Log("biri4");
        unlimitedhand.vibrate_End();
    }
}