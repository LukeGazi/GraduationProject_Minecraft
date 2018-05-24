using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioScrollbar : MonoBehaviour, IPointerUpHandler {

    /// <summary>
    /// 获取Scrollbar
    /// </summary>
    private Scrollbar m_muteSrollbar;
    private Scrollbar GetMuteScrollbar {
        get {
            if (m_muteSrollbar == null) {
                m_muteSrollbar = GetComponent<Scrollbar>();
            }
            return m_muteSrollbar;
        }
    }

    private bool m_isMute = false;
    /// <summary>
    /// 修改静音状态
    /// </summary>
    private void ChangeMuteStatus() {
        //如果是满格则变为0
        if (m_isMute) {
            StartCoroutine( ChangeToUnmuteAnimation() );
        } else {
            StartCoroutine( ChangeToMuteAnimation() );
        }
    }

    /// <summary>
    /// 修改静音状态动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeToMuteAnimation() {
        while (GetMuteScrollbar.value > UIInfoManager.Instance.ScrollbarMoveError) {
            GetMuteScrollbar.value = Mathf.Lerp( GetMuteScrollbar.value, 0, UIInfoManager.Instance.ScrollbarMoveSpeed );
            yield return null;
        }
        AfterChangeMuteStatusAnimation( 0, Color.grey, true );
        yield return null;
    }

    /// <summary>
    /// 修改为非静音状态动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeToUnmuteAnimation() {
        while (GetMuteScrollbar.value < 1 - UIInfoManager.Instance.ScrollbarMoveError) {
            GetMuteScrollbar.value = Mathf.Lerp( GetMuteScrollbar.value, 1, UIInfoManager.Instance.ScrollbarMoveSpeed );
            yield return null;
        }
        AfterChangeMuteStatusAnimation( 1, Color.green, false );
        yield return null;
    }

    /// <summary>
    /// 静音动画后修改背景色以及位置修正
    /// </summary>
    private void AfterChangeMuteStatusAnimation(float _finalValue, Color _finalColor, bool _isMute) {
        GetMuteScrollbar.value = _finalValue;
        GetComponent<Image>().color = _finalColor;
        m_isMute = _isMute;
        AudioManager.Instance.SwitchMuteGame( _isMute );
        if (_isMute) {
            NoticeManager.Instance.SendNotice( StrManager.SPECIFY_AUDIO_PANEL_HIDE_NOTICE );
        } else {
            NoticeManager.Instance.SendNotice( StrManager.SPECIFY_AUDIO_PANEL_SHOW_NOTICE );
        }
    }

    /// <summary>
    /// 鼠标抬起时调用状态改变方法
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData) {
        ChangeMuteStatus();
    }
}
