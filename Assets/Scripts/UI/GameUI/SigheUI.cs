using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigheUI : MonoBehaviour {
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

    private void Awake() {

        NoticeManager.Instance.Register( StrManager.SIGHT_IMAGE_SHOW_NOTICE, ShowPanel );
        NoticeManager.Instance.Register( StrManager.SIGHT_IMAGE_HIDE_NOTICE, HidePanel );

    }

}
