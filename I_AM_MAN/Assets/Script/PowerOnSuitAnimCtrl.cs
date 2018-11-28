using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOnSuitAnimCtrl : MonoBehaviour {
    public void SetPowerOnAnimEnabled(bool isEnabled)
    {
        gameObject.SetActive(isEnabled);
    }
}
