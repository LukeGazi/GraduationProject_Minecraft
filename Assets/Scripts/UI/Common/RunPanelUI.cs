using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPanelUI : MonoBehaviour {

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


}
