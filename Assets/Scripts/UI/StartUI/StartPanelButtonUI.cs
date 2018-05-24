using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartPanelButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    #region 参数
    /// <summary>
    /// 获取RextTransform
    /// </summary>
    private RectTransform m_rectTransform;
    private RectTransform GetRectTransform {
        get {
            if (m_rectTransform == null) {
                m_rectTransform = GetComponent<RectTransform>();
            }
            return m_rectTransform;
        }
    }

    /// <summary>
    /// 获取Button组件
    /// </summary>
    private Button m_button;
    private Button GetButton {
        get {
            if (m_button == null) {
                m_button = GetComponent<Button>();
            }
            return m_button;
        }
    }

    /// <summary>
    /// 获得按钮的可用性
    /// </summary>
    protected bool GetInteractable {
        get {
            return GetButton.interactable;
        }
    }
    #endregion

    // Update is called once per frame
    void Update() {

        LookAtMouse();

    }

    #region 鼠标注视旋转
    private float m_movedX = 0;
    /// <summary>
    /// 注视鼠标而旋转
    /// </summary>
    private void LookAtMouse() {

        float mouseMoveX = Input.GetAxis( "Mouse X" ) * UIInfoManager.Instance.RotateButtonSpeed;

        //旋转限制
        m_movedX += mouseMoveX;
        m_movedX = Mathf.Clamp( m_movedX, 0, UIInfoManager.Instance.RotateButtonLimitX );

        if (m_movedX > 0 && m_movedX < UIInfoManager.Instance.RotateButtonLimitX) {
            GetRectTransform.eulerAngles += Vector3.up * mouseMoveX;
        }

    }
    #endregion

    #region 接口实现
    /// <summary>
    /// 鼠标悬浮进入
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) {
        if (!GetInteractable) {
            return;
        }
        AudioManager.Instance.PlayMouseHoverClip();
        EnlargeScale();
    }

    /// <summary>
    /// 鼠标悬浮退出
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) {
        if (!GetInteractable) {
            return;
        }
        RecoverScale();
    }

    /// <summary>
    /// 鼠标点击方法，需要被重写
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerClick(PointerEventData eventData) {
        if (!GetInteractable) {
            return;
        }
        AudioManager.Instance.PlayMouseClickClip();
    }
    #endregion

    #region button大小
    /// <summary>
    /// 扩大按钮大小
    /// </summary>
    private void EnlargeScale() {
        GetRectTransform.localScale *= UIInfoManager.Instance.ButtonHoverEnlargeTime;
    }

    private void RecoverScale() {
        GetRectTransform.localScale = Vector3.one;
    }
    #endregion

    #region button动画
    /// <summary>
    /// 播放移开褪色动画
    /// </summary>
    protected void PlayMoveAwayAndFade() {
        StartCoroutine( MoveAwayAndFade() );
    }
    /// <summary>
    /// 播放移开不褪色动画
    /// </summary>
    protected void PlayMoveAwayNotFade() {
        StartCoroutine( MoveAwayNotFade() );
    }
    /// <summary>
    /// 播放移回上色动画
    /// </summary>
    protected void PlayMoveBackAndRecover() {
        StartCoroutine( MoveBackAndRecover() );
    }
    /// <summary>
    /// 播放移回不上色动画
    /// </summary>
    protected void PlayMoveBackNotRecover() {
        StartCoroutine( MoveBackNotRecover() );
    }

    /// <summary>
    /// 控制移动且褪色的组件的移动距离，并利用可空值类型存储
    /// </summary>
    private float? m_moveFadeDistance; //移动距离
    private float GetMoveFadeDistance {
        get {
            if (m_moveFadeDistance == null) {
                m_moveFadeDistance = Screen.width;
            }
            return (float)m_moveFadeDistance;
        }
    }
    /// <summary>
    /// 控制移动不褪色的组件的移动距离，为点击的按钮
    /// </summary>
    private float? m_notFadeDistance; //移动距离
    private float GetNotFadeDistance {
        get {
            if (m_notFadeDistance == null) {
                m_notFadeDistance = UIInfoManager.Instance.HidenPanelWidth;
            }
            return (float)m_notFadeDistance;
        }
    }
    /// <summary>
    /// 按钮移动前原本的位置
    /// </summary>
    private Vector3? m_oldPosition;
    private Vector3 GetOldPosition {
        get {
            if (m_oldPosition == null) {
                m_oldPosition = GetRectTransform.localPosition;
            }
            return (Vector3)m_oldPosition;
        }
    }
    /// <summary>
    /// 按钮的Image组件，用于褪色
    /// </summary>
    private Image m_buttonImage;
    private Image GetButtonImage {
        get {
            if (m_buttonImage == null) {
                m_buttonImage = GetComponent<Image>();
            }
            return m_buttonImage;
        }
    }
    /// <summary>
    /// 图片原始颜色，用于恢复
    /// </summary>
    private Color? m_imgOldColor;
    private Color GetImgOldColor {
        get {
            if (m_imgOldColor == null) {
                m_imgOldColor = GetButtonImage.color;
            }
            return (Color)m_imgOldColor;
        }
    }
    /// <summary>
    /// 字体颜色，字体跟随消失和显示
    /// </summary>
    private Color? m_txtOldColor;
    private Color GetTxtOldColor {
        get {
            if (m_txtOldColor == null) {
                m_txtOldColor = BtnContextText.color;
            }
            return (Color)m_txtOldColor;
        }
    }
    /// <summary>
    /// Button下的Text组件
    /// </summary>
    public Text BtnContextText;

    /// <summary>
    /// 按钮的移动且褪色协程动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveAwayAndFade() {
        Vector3 endPosition = GetOldPosition - Vector3.right * GetMoveFadeDistance;
        while (GetRectTransform.localPosition.x > endPosition.x + UIInfoManager.Instance.ButtonMoveError) {
            GetRectTransform.localPosition = Vector3.Lerp( GetRectTransform.localPosition, endPosition, UIInfoManager.Instance.ButtonMoveSpeed * Time.deltaTime );
            GetButtonImage.color = Color.Lerp( GetButtonImage.color, new Color( GetImgOldColor.r, GetImgOldColor.g, GetImgOldColor.b, 0 ), UIInfoManager.Instance.ButtonFadeSpeed * Time.deltaTime );
            BtnContextText.color = Color.Lerp( BtnContextText.color, new Color( GetTxtOldColor.r, GetTxtOldColor.g, GetTxtOldColor.b, 0 ), UIInfoManager.Instance.ButtonFadeSpeed * Time.deltaTime );
            yield return null;
        }
        GetRectTransform.localPosition = endPosition;
        yield return null;
    }
    /// <summary>
    /// 按钮的移动但不褪色的协程动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveAwayNotFade() {
        DisableButton();
        Vector3 endPosition = GetOldPosition - Vector3.right * GetNotFadeDistance;
        while (GetRectTransform.localPosition.x > endPosition.x + UIInfoManager.Instance.ButtonMoveError) {
            GetRectTransform.localPosition = Vector3.Lerp( GetRectTransform.localPosition, endPosition, UIInfoManager.Instance.ButtonMoveSpeed * Time.deltaTime );
            yield return null;
        }
        GetRectTransform.localPosition = endPosition;
        yield return null;
    }

    /// <summary>
    /// 取消按钮的可用性，在点击后调用
    /// </summary>
    private void DisableButton() {
        GetRectTransform.localScale *= UIInfoManager.Instance.ButtonHoverEnlargeTime;
        GetComponent<Button>().interactable = false;
        CancelAnimationPlayingFlag();
    }
    /// <summary>
    /// 恢复按钮的可用性，动画运行结束后调用
    /// </summary>
    private void EnableButton() {
        GetRectTransform.localScale = Vector3.one;
        GetComponent<Button>().interactable = true;
        CancelAnimationPlayingFlag();
    }
    /// <summary>
    /// 取消协程动画播放标记，即可以进行下次协程动画的播放
    /// </summary>
    private void CancelAnimationPlayingFlag() {
        UIInfoManager.Instance.IsAnimationPlaying = false;
    }

    /// <summary>
    /// 按钮移动回归且恢复颜色
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveBackAndRecover() {
        while (GetRectTransform.localPosition.x < GetOldPosition.x - UIInfoManager.Instance.ButtonMoveError) { //> GetOldPosition.x - GetDistance
            GetRectTransform.localPosition = Vector3.Lerp( GetRectTransform.localPosition, GetOldPosition, UIInfoManager.Instance.ButtonMoveSpeed * Time.deltaTime );
            GetButtonImage.color = Color.Lerp( GetButtonImage.color, new Color( GetImgOldColor.r, GetImgOldColor.g, GetImgOldColor.b, 1 ), UIInfoManager.Instance.ButtonFadeSpeed * Time.deltaTime );
            BtnContextText.color = Color.Lerp( BtnContextText.color, new Color( GetTxtOldColor.r, GetTxtOldColor.g, GetTxtOldColor.b, 1 ), UIInfoManager.Instance.ButtonFadeSpeed * Time.deltaTime );
            yield return null;
        }
        ModifyAfterBack();
        yield return null;
    }
    /// <summary>
    /// 按钮的移动回归，针对没有褪色的按钮，恢复可用性
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveBackNotRecover() {
        while (GetRectTransform.localPosition.x < GetOldPosition.x - UIInfoManager.Instance.ButtonMoveError) { //> GetOldPosition.x - GetDistance
            GetRectTransform.localPosition = Vector3.Lerp( GetRectTransform.localPosition, GetOldPosition, UIInfoManager.Instance.ButtonMoveSpeed * Time.deltaTime );
            yield return null;
        }
        ModifyAfterBack();
        yield return null;
    }
    /// <summary>
    /// 移回后修正
    /// </summary>
    private void ModifyAfterBack() {
        GetRectTransform.localPosition = GetOldPosition; //位置修正
        EnableButton();
        m_notFadeDistance = null;
        m_oldPosition = null;
    }

    #endregion

}
