using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MiniJSON;
using System.Collections;
using System.Collections.Generic;

public class JsonPacker : MonoBehaviour {

    SaveData _data;
    
    // 保存するClass
    public GameObject r_TimeTriger;
    public AgeCount r_Aeg;
    public TimeCount r_TimeCont;

    //保存するディレクトリ名
    private const string DIRECTORY_NAME = "Data";
    private const string JSON_FILENAME = "savedata.json";

    private void Start()
    {
        

        r_TimeTriger = GameObject.Find("Canvas/StatusCanvas/StatusAge/TimeTrigger");
        r_Aeg = r_TimeTriger.GetComponent<AgeCount>();
        r_TimeCont = r_TimeTriger.GetComponent<TimeCount>();

        _data = LoadFromJson(GetFilePath());
        Debug.Log(GetFilePath());

        if (_data.s_nStartTime == null)
        {
            _data.s_nStartTime = r_TimeCont.StartTime.ToString();
        }
        else
        {
            r_TimeCont.StartTime = System.DateTime.Parse(_data.s_nStartTime);

        }
    }

    private void Update()
    {
        if (_data == null) return;//とりあえずSaveDataに何もなかったら何もやらない.
        if (_data.s_Aeg != r_Aeg.Age_Sec)
        {//エディタ上でid,s_nameを変更したらデータを書き換える.
            _data.s_Aeg = r_Aeg.Age_Sec;
        }

    }

    /// <summary>
    /// ファイルパスの暗号化復号化
    /// </summary>
    private static string GetFilePath()
    {

        string directoryPath = Application.persistentDataPath + "/" + DIRECTORY_NAME;

        //ディレクトリが無ければ作成
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        //ファイル名は暗号化する
        string encryptedFlieName = Encryption.EncryptString(JSON_FILENAME);
        string filePath = directoryPath + "/" + encryptedFlieName;

        return filePath;
    }


    /// <summary>
    /// アプリケーション終了時に呼ばれる
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveToJson(GetFilePath(), _data);
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveToJson(GetFilePath(), _data);
        }
    }

    /// <summary>
    /// ファイル書き込み
    /// </summary>
    /// <param s_name="filePath">ファイルのある場所</param>
    public static void SaveToJson(string filePath, SaveData data)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                string jsonStr = Encryption.EncryptString(JsonUtility.ToJson(data));
                Debug.Log(jsonStr);
                sw.WriteLine(jsonStr);
            }
        }
    }

    /// <summary>
    /// ファイル読み込みする
    /// </summary>
    /// <param s_name="filePath">ファイルのある場所</param>
    /// <returns></returns>
    public static SaveData LoadFromJson(string filePath)
    {
        if (!File.Exists(filePath))
        {//ファイルがない場合FALSE.
            Debug.Log("FileEmpty!");
            //Debug.Log("//////////// -> " + filePath);
            return new SaveData();//ファイルが無いときは適当に処す.
        }

        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fs))
            {
                
                string jsonStr = Encryption.DecryptString(sr.ReadToEnd());

                SaveData sd = JsonUtility.FromJson<SaveData>(jsonStr);

                if (sd == null)
                {
                    return new SaveData();
                }

                return sd;
            }
        }
    }

        
}

