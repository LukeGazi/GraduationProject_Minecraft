using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoManager : Singletons<GameInfoManager> {

    #region InputManager
    public float RayDetectDistance = 5.5f; //射线探测距离，能触碰到的Block的间隔个数
    #endregion

    #region CameraFollow
    public float CameraRotateSensitivityX = 5.0f;
    public float CameraRotateSensitivityY = 5.0f;
    public float CameraRotateMaxLimitRY = 90;
    public float CameraRotateMinLimitRY = -90;
    public  float CameraMoveHeight = 1.5f;
    #endregion

}
