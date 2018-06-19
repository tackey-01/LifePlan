using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidStatus : MonoBehaviour {

    
    public bool IsExistAlert { get; set; }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        // プラットフォームがアンドロイドかチェック
        if (Application.platform == RuntimePlatform.Android)
        {
            // エスケープキー取得
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //すでにアラートが表示されていたらリターン	
                if (IsExistAlert == true)
                {
                    return;
                }
                IsExistAlert = true;
                AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject activity = unity.GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                    //ここでAlertDialogを作成
                    AndroidJavaObject alertDialogBuilder = new AndroidJavaObject("android.app.AlertDialog$Builder", activity);
                    alertDialogBuilder.Call<AndroidJavaObject>("setTitle", "確認");
                    alertDialogBuilder.Call<AndroidJavaObject>("setMessage", "message");
                    alertDialogBuilder.Call<AndroidJavaObject>("setCancelable", true);
                    alertDialogBuilder.Call<AndroidJavaObject>("setNegativeButton", "CANCEL", new NegativeButtonListner(this));
                    alertDialogBuilder.Call<AndroidJavaObject>("setPositiveButton", "OK", new PositiveButtonListner(this));
                    AndroidJavaObject dialog = alertDialogBuilder.Call<AndroidJavaObject>("create");
                    dialog.Call("show");
                 }));
                return;
            }
        }
    }

    // Game Finish
    private class PositiveButtonListner : AndroidJavaProxy
    {

        private AndroidStatus _parent;

        public PositiveButtonListner(AndroidStatus d) : base("android.content.DialogInterface$OnClickListener")
        {
            //リスナーを作成した時に呼び出される
            _parent = d;
        }

        public void onClick(AndroidJavaObject obj, int value)
        {
            //ボタンが押された時に呼び出される
            // リストに残さないための処理
            Application.runInBackground = false;
            // アプリケーション終了
            Application.Quit();
            _parent.IsExistAlert = false;
        }
    }

    // CANCEL button
    private class NegativeButtonListner : AndroidJavaProxy
    {
        /// <summary>
        /// The parent.
        /// </summary>
        private AndroidStatus _parent;

        public NegativeButtonListner(AndroidStatus d) : base("android.content.DialogInterface$OnClickListener")
        {
            //リスナーを作成した時に呼び出される
            _parent = d;
        }

        public void onClick(AndroidJavaObject obj, int value)
        {
            //ボタンが押された時に呼び出される
            _parent.IsExistAlert = false;
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        Debug.Log("OnApplicationFocus:" + hasFocus);
        if (hasFocus)
        {
            IsExistAlert = false;
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        Debug.Log("OnApplicationPause:" + pauseStatus);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
    }
}
