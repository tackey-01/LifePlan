using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataTimeCount : MonoBehaviour {

    public TimeCount DataTime;
    public int DataCount = 0;
    public int old_DataCount = 0;
    public int TimeCount = 0;
    public int old_TimeCount = 0;
    private Text DataTimeText;
    // 3600
    private const int TimeSec = 6;
    // 86400 1日
    private const int TimeDay = 60;

    // Use this for initialization
    void Start () {
        DataTimeText = GetComponentInChildren<Text>();

        DataTime = GameObject.Find("TimeTrigger").GetComponent<TimeCount>();
    }
	
	// Update is called once per frame
	void Update () {
        TimeCount = ((int)DataTime.sec / TimeSec) % 24;

        DataCount = ((int)DataTime.sec / TimeDay);

        if (TimeCount != old_TimeCount)
        {
            DataTimeText.text = DataCount.ToString("0") + "日目" + TimeCount.ToString("0") + "時間";
            old_TimeCount = TimeCount;
        }
        if (DataCount != old_DataCount)
        {
            DataTimeText.text = DataCount.ToString("0") + "日目" + TimeCount.ToString("0") + "時間";
            old_DataCount = DataCount;
        }
    }
}
