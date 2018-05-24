using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInfos : MonoBehaviour {

    public static CanvasInfos Instance;

    private void Awake() {
        Instance = this;
    }

    private RectTransform m_rectTransform;
    private RectTransform GetRectTransform {
        get {
            if (m_rectTransform == null) {
                m_rectTransform = GetComponent<RectTransform>();
            }
            return m_rectTransform;
        }
    }

    public float GetCanvasWidth {
        get {
            return GetRectTransform.sizeDelta.x;
        }
    }
    public float GetCanvasHeight {
        get {
            return GetRectTransform.sizeDelta.y;
        }
    }

}
