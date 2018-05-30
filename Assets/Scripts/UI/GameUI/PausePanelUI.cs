using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelUI : MonoBehaviour {


    #region Panel
    /// <summary>
    /// 获取RectTransform
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
    /// 展示面板
    /// </summary>
    private void ShowPanel() {
        GetRectTransform.localScale = Vector3.one;
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    private void HidePanel() {
        GetRectTransform.localScale = Vector3.zero;
    }
    #endregion

    private void Awake() {
        GetRectTransform.localScale = Vector3.zero; //开始隐藏

        NoticeManager.Instance.Register( StrManager.PAUSE_PANEL_SHOW_NOTICE, ShowPanel );
        NoticeManager.Instance.Register( StrManager.PAUSE_PANEL_HIDE_NOTICE, HidePanel );

    }
}
