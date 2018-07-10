using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class AgeCount : MonoBehaviour {

    public TimeCount Age;
    public int Age_Sec = 0;
    public int oldAge_Sec = 0;
    private Text AgeText;

    //public SaveData _data;

    // Use this for initialization
    void Start () {
        AgeText = GetComponentInChildren<Text>();
        Age = GetComponent<TimeCount>();
    }
	
	// Update is called once per frame
	void Update () {
        Age_Sec = ((int)Age.sec / 60);

        if (Age_Sec != oldAge_Sec)
        {
            AgeText.text = Age_Sec.ToString("0") + "歳";
            oldAge_Sec = Age_Sec;
        }

	}
    
}
