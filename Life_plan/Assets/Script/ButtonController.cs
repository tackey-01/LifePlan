using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public CanvasGroup mainCanvas;
    private GameObject Gameobj;
    public string flagName;

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

    public void MenuListTrue()
    {
        GameObject.Find("Canvas").transform.Find("CharacterCanvas").transform.Find("MenuPanel").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("CharacterCanvas").transform.Find("Menubutton").gameObject.SetActive(false);
    }

    public void ListUp()
    {
        if (flagName == null)
        {
            return;
        }

        Gameobj = GameObject.Find("Canvas").transform.Find(flagName).gameObject;

        if (Gameobj == null)
        {
            return;
        }
        GameObject.Find("Canvas").transform.Find("CharacterCanvas").transform.Find("MenuPanel").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("CharacterCanvas").transform.Find("Menubutton").gameObject.SetActive(true);
        Gameobj.SetActive(true);
        // Canvas lock
        mainCanvas = GameObject.Find("Canvas").gameObject.GetComponent<CanvasGroup>();
        mainCanvas.interactable = false;
    }

    public void ListClose()
    {
        if(flagName == null)
        {
            return;
        }

        Gameobj = GameObject.Find("Canvas").transform.Find(flagName).gameObject;

        if (Gameobj == null)
        {
            return;
        }

        Gameobj.SetActive(false);
        mainCanvas = GameObject.Find("Canvas").gameObject.GetComponent<CanvasGroup>();
        mainCanvas.interactable = true;
    }
    
}