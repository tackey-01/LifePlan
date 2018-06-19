using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TouchInfo info = AppUtil.GetTouch();

#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject())
        {
            
            Debug.Log("PC");

            return;

        }
#else 
    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
        Debug.Log("Android");
        return;
    }
#endif

        switch (info)
        {
            case TouchInfo.Began:
                Vector3 delta = AppUtil.GetTouchPosition();
                Debug.Log("////// x -> " + delta.x + "/////// y -> " + delta.y);
                SceneManager.LoadScene("GameScene");
                break;
            case TouchInfo.Moved:
                break;
            case TouchInfo.Ended:
                break;
        }
    }
}
