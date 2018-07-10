using UnityEngine;
using System.Collections;
using System;

public class TimeCount : MonoBehaviour {

    protected double m_nSec = 0;    //!< アプリ起動からの経過時間(秒)
    protected double m_nDeltaTime = 0;  //!< 前フレームとの差分時間(秒)
    protected static TimeCount m_Instance;               //!< インスタンス

    private DateTime m_nStartTime;          //!< アプリ起動時の時刻を記録する

    // ----------------------------------------------------------------------------
    /*! 
	 @brief このクラスのインスタンスを取得（未生成なら生成）
	 @return	インスタンス
	*/
    // ----------------------------------------------------------------------------
    public static TimeCount Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (TimeCount)FindObjectOfType(typeof(TimeCount));
                if (m_Instance == null)
                {
                    Debug.LogError("TimeCount:Instance Error[m_Instance == null]");
                }
            }
            return m_Instance;
        }
    }


    // ----------------------------------------------------------------------------
    /*! 
	 @brief アプリ起動からの経過時間(秒)を取得
	 @return	アプリ起動からの経過時間(秒)
	*/
    // ----------------------------------------------------------------------------
    public double sec
    {
        get
        {
            return m_nSec;
        }
    }


    // ----------------------------------------------------------------------------
    /*! 
	 @brief 前フレームとの差分時間(秒)を取得
	 @return	前フレームとの差分時間(秒)
	*/
    // ----------------------------------------------------------------------------
    public double deltaTime
    {
        get
        {
            return m_nDeltaTime;
        }
    }

    public DateTime StartTime
    {
        get
        {
            return m_nStartTime;
        }
        set
        {
            m_nStartTime = value;            
        }
    }

    // ----------------------------------------------------------------------------
    // Use this for initialization
    // ----------------------------------------------------------------------------
    void Start()
    {
        StartTime = DateTime.Now;
        m_nDeltaTime = 0;
    }


    // ----------------------------------------------------------------------------
    // Update is called once per frame
    // ----------------------------------------------------------------------------
    void Update()
    {
        // 前フレームの時間を記録
        double nLastSec = m_nSec;

        // 起動時からの経過時間を計算
        m_nSec = (DateTime.Now - m_nStartTime).TotalSeconds;

        // 前フレームとの差分を求める
        m_nDeltaTime = m_nSec - nLastSec;
    }
}
