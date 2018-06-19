using UnityEngine;


public static class AppUtil
{
    private static Vector3 TouchPosition = Vector3.zero;
    private static Vector3 PreviousPosition = Vector3.zero;

    private static Vector2 Touch2DPosition = Vector2.zero;
    private static Vector2 Previous2DPosition = Vector2.zero;

    /// <summary>
    /// タッチ情報を取得(エディタと実機を考慮)
    /// </summary>
    /// <returns>タッチ情報。タッチされていない場合は null</returns>
    public static TouchInfo GetTouch()
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0)) { return TouchInfo.Began; }
            if (Input.GetMouseButton(0)) { return TouchInfo.Moved; }
            if (Input.GetMouseButtonUp(0)) { return TouchInfo.Ended; }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                return (TouchInfo)((int)Input.GetTouch(0).phase);
            }
        }
        return TouchInfo.None;
    }

    /// <summary>
    /// タッチポジションを取得(エディタと実機を考慮)
    /// </summary>
    /// <returns>タッチポジション。タッチされていない場合は (0, 0, 0)</returns>
    public static Vector3 GetTouchPosition()
    {
        if (Application.isEditor)
        {
            TouchInfo touch = AppUtil.GetTouch();
            if (touch != TouchInfo.None)
            {
                PreviousPosition = Input.mousePosition;
                return PreviousPosition;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                TouchPosition.x = touch.position.x;
                TouchPosition.y = touch.position.y;
                return TouchPosition;
            }
        }
        return Vector3.zero;
    }

    public static Vector3 GetDeltaPosition()
    {
        if (Application.isEditor)
        {
            TouchInfo info = AppUtil.GetTouch();
            if (info != TouchInfo.None)
            {
                Vector3 currentPosition = Input.mousePosition;
                Vector3 delta = currentPosition - PreviousPosition;
                PreviousPosition = currentPosition;
                return delta;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                PreviousPosition.x = touch.deltaPosition.x;
                PreviousPosition.y = touch.deltaPosition.y;
                return PreviousPosition;
            }
        }
        return Vector3.zero;
    }
    
    /// <summary>
    /// タッチワールドポジションを取得(エディタと実機を考慮)
    /// </summary>
    /// <param name='camera'>カメラ</param>
    /// <returns>タッチワールドポジション。タッチされていない場合は (0, 0, 0)</returns>
    public static Vector3 GetTouchWorldPosition(Camera camera)
    {
        return camera.ScreenToWorldPoint(GetTouchPosition());
    }



    /// <summary>
    /// スクリーン座標をワールド座標（2D）へ変換
    /// </summary>
    /// <param name='camera'>カメラ</param>
    /// <returns>タッチワールドポジション。タッチされていない場合は (0, 0)</returns>
    public static Vector2 Get2DTouchPosition()
    {
        if (Application.isEditor)
        {
            TouchInfo touch = AppUtil.GetTouch();
            if (touch != TouchInfo.None)
            {
                // 前の2D値
                Previous2DPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                return Previous2DPosition;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                PreviousPosition.x = touch.position.x;
                PreviousPosition.y = touch.position.y;
                // ワールド座標に変換
                Touch2DPosition = Camera.main.ScreenToWorldPoint(PreviousPosition);
                return Touch2DPosition;
            }
        }
        return Vector2.zero;
    }
}

