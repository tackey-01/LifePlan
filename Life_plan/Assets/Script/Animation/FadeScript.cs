using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    float alfa;
    float speed = 0.01f;
    float red, green, blue;

    public Text Image_Text;
    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
    public bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ

    void Start()
    {
        Image_Text = this.gameObject.GetComponent<Text>();
        red = Image_Text.color.r;
        green = Image_Text.color.g;
        blue = Image_Text.color.b;
    }

    void Update()
    {
        if (isFadeIn)
        {
            StartFadeIn();
        }

        if (isFadeOut)
        {
            StartFadeOut();
        }
    }

    void StartFadeIn()
    {
        alfa -= speed;                //a)不透明度を徐々に下げる
        SetAlpha();                      //b)変更した不透明度パネルに反映する
        if (alfa <= 0)
        {                    //c)完全に透明になったら処理を抜ける
            isFadeIn = false;
            isFadeOut = true;
            Image_Text.enabled = false;    //d)パネルの表示をオフにする
        }
    }

    void StartFadeOut()
    {
        Image_Text.enabled = true;  // a)パネルの表示をオンにする
        alfa += speed;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (alfa >= 1)
        {             // d)完全に不透明になったら処理を抜ける
            isFadeOut = false;
            isFadeIn = true;
        }
    }

    void SetAlpha()
    {
        Image_Text.color = new Color(red, green, blue, alfa);
    }
}
