using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfoManager : Singletons<UIInfoManager> {

    #region BackgroundUI
    public float BackgroundImageWidth = 714; //背景图片本身宽度
    public float BackgroundImageHeight = 416; //背景图片本身高度

    public float BackgroundImageEnlargeTime = 1.2f; //背景图片扩大倍率
    public float BackgroundMoveImageSpeed = 2.0f; //鼠标移动带动背景移动的速率

    #endregion

    #region StartPanelButtonUI
    /* Button注视旋转部分 */
    public float RotateButtonSpeed = 0.5f; //Button注视旋转的速率
    public float RotateButtonLimitX = 20; //Button注视旋转范围限制

    /* Button悬浮部分 */
    public float ButtonHoverEnlargeTime = 1.1f; //Button悬浮放大倍数

    /* Button移动褪色部分 */
    public float ButtonMoveSpeed = 5.5f; //Button移动速度
    public float ButtonFadeSpeed = 7.5f; //Button褪色速度
    #endregion

    #region TitleImageUI
    /* Title注视旋转部分 */
    public float RotateTitleSpeed = 0.5f; //Button注视旋转的速率
    public float RotateTitleLimitX = -20; //Button注视旋转范围限制
    #endregion

    #region ButtonEvent
    public float ButtonMoveDelayTimeOne = 0.1f; //按钮移动延时
    public float ButtonMoveDelayTimeTwo = 0.3f; //按钮移动延时
    #endregion

    #region HidenPanelUI
    public float HidenPanelWidth = 400; //隐藏Panel的宽度
    #endregion

    public float ButtonMoveError = 1.0f; //按钮移动误差
    public bool IsAnimationPlaying = false; //是否正在播放协程动画

    #region AudioScrollbar
    public float ScrollbarMoveError = 0.2f; //静音键移动误差
    public float ScrollbarMoveSpeed = 0.5f; //静音键动画移动速度
    #endregion

    #region SpecifyAudioPanelUI
    public float SpecifyAudioPanelShrinkError = 0.2f; //音量详细面板收缩误差
    public float SpecifyAudioPanelShrinkSpeed = 0.2f; //音量详细面板收缩速度
    #endregion

    #region ViewFieldSlider
    public float ViewFieldMin = 40; //视野最低值
    public float ViewFieldMax = 80; //视野最高值
    #endregion
}
