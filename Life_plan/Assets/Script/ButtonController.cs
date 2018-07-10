using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public CanvasGroup mainCanvas;
    public void LoadSetting()
    {
        GameObject.Find("Canvas").transform.Find("ContractPanel").gameObject.SetActive(true);
        mainCanvas = GameObject.Find("Canvas").gameObject.GetComponent<CanvasGroup>();
        mainCanvas.interactable = false;
    }

    public void close()
    {
        GameObject.Find("Canvas").transform.Find("ContractPanel").gameObject.SetActive(false);
        mainCanvas = GameObject.Find("Canvas").gameObject.GetComponent<CanvasGroup>();
        mainCanvas.interactable = true;
    }
    
    public void TitleReturn()
    {
        SceneManager.LoadScene("TitleScene");
    }
}