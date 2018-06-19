using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void LoadSetting()
    {
        GameObject.Find("Canvas").transform.Find("ContractPanel").gameObject.SetActive(true);
        Debug.Log("クリックOK");
    }

    public void close()
    {
        GameObject.Find("Canvas").transform.Find("ContractPanel").gameObject.SetActive(false);
    }
}