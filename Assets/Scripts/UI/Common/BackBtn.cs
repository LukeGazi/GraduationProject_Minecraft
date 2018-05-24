using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackBtn : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler {

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
    /// 鼠标点击方法
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) {
        AudioManager.Instance.PlayMouseClickClip();
        //根据标签选择要调用的返回界面的协程动画播放
        if (gameObject.tag == StrManager.BUTTON_BACK_OPTION_PANEL) {
            //若正在有协程动画播放，则返回，取消本次的播放
            if (UIInfoManager.Instance.IsAnimationPlaying) {
                return;
            }
            UIInfoManager.Instance.IsAnimationPlaying = true;
            StartCoroutine( MoveOptionPanelBack() );
        }
        if (gameObject.tag == StrManager.BUTTON_BACK_SINGLE_GAME_PANEL) {
            if (UIInfoManager.Instance.IsAnimationPlaying) {
                return;
            }
            UIInfoManager.Instance.IsAnimationPlaying = true;
            StartCoroutine( MoveSingleGamePanelBack() );
        }
    }

    /// <summary>
    /// Option面板的移动回归协程动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveOptionPanelBack() {
        NoticeManager.Instance.SendNotice( StrManager.OPTION_PANEL_BACK_NOTICE );
        NoticeManager.Instance.SendNotice( StrManager.BTN_OPTION_BACK_NOT_REOVER_NOTICE );
        yield return new WaitForSeconds( UIInfoManager.Instance.ButtonMoveDelayTimeTwo );
        NoticeManager.Instance.SendNotice( StrManager.BTN_SINGLEGAME_BACK_AND_RECOVER_NOTICE );
        yield return new WaitForSeconds( UIInfoManager.Instance.ButtonMoveDelayTimeOne );
        NoticeManager.Instance.SendNotice( StrManager.BTN_QUIT_BACK_AND_RECOVER_NOTICE );
        yield return null;
    }

    /// <summary>
    /// SingleGame面板的回归的协程动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveSingleGamePanelBack() {
        NoticeManager.Instance.SendNotice( StrManager.SINGLEGAME_PANEL_BACK_NOTICE );
        NoticeManager.Instance.SendNotice( StrManager.BTN_SINGLEGAME_BACK_NOT_RECOVER_NOTICE );
        yield return new WaitForSeconds( UIInfoManager.Instance.ButtonMoveDelayTimeTwo );
        NoticeManager.Instance.SendNotice( StrManager.BTN_OPTION_BACK_AND_RECOVER_NOTICE );
        yield return new WaitForSeconds( UIInfoManager.Instance.ButtonMoveDelayTimeOne );
        NoticeManager.Instance.SendNotice( StrManager.BTN_QUIT_BACK_AND_RECOVER_NOTICE );
        yield return null;
    }

    /// <summary>
    /// 鼠标按下
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData) {
        //放大效果
        GetRectTransform.localScale *= UIInfoManager.Instance.ButtonHoverEnlargeTime;
    }

    /// <summary>
    /// 鼠标抬起
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData) {
        //返回原效果
        GetRectTransform.localScale = Vector3.one;
    }

    /// <summary>
    /// 鼠标悬浮
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) {
        AudioManager.Instance.PlayMouseHoverClip();
    }
}
