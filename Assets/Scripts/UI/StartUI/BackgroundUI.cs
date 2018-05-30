using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundUI : MonoBehaviour {
    /// <summary>
    /// 获取图片宽度
    /// </summary>
    private float GetCurrentImageWidth {
        get {
            //画布跨度乘以倍数
            return CanvasInfos.Instance.GetCanvasWidth * UIInfoManager.Instance.BackgroundImageEnlargeTime;
        }
    }
    /// <summary>
    /// 获取图面高度
    /// </summary>
    private float GetCurrentImageHeight {
        get {
            //根据画布和背景图片宽度的比例获取
            float rate = CanvasInfos.Instance.GetCanvasWidth / UIInfoManager.Instance.BackgroundImageWidth;
            float height = UIInfoManager.Instance.BackgroundImageHeight * rate;
            return height * UIInfoManager.Instance.BackgroundImageEnlargeTime;
        }
    }

    /// <summary>
    /// 获取RectTransorm
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
    /// 初始化图片大小
    /// </summary>
    private void InitImageSize() {
        GetRectTransform.sizeDelta = new Vector2( GetCurrentImageWidth, GetCurrentImageHeight );
    }

    private void Start() {
        InitImageSize();
        AudioManager.Instance.PlayBGMClip();
    }

    /// <summary>
    /// 鼠标移动X轴方向的范围控制
    /// </summary>
    private float? m_moveLimitX;
    private float? m_moveLimitY;
    private float GetMoveLimitX {
        get {
            //根据画布信息控制
            if (m_moveLimitX == null) {
                m_moveLimitX = ( GetCurrentImageHeight - CanvasInfos.Instance.GetCanvasHeight ) / 2;
            }
            return (float)m_moveLimitX;
        }
    }
    /// <summary>
    /// 鼠标移动Y轴方向的范围控制
    /// </summary>
    private float GetMoveLimitY {
        get {
            if (m_moveLimitY == null) {
                m_moveLimitY = ( GetCurrentImageWidth - CanvasInfos.Instance.GetCanvasWidth ) / 2;
            }
            return (float)m_moveLimitY;
        }
    }
    private float m_movedX = 0; //记录鼠标移动距离
    private float m_movedY = 0; //记录鼠标移动距离
    /// <summary>
    /// 移动背景
    /// </summary>
    private void MoveImageByMovingMouse() {
        float mouseMoveX = Input.GetAxis( "Mouse X" ) * UIInfoManager.Instance.BackgroundMoveImageSpeed;
        float mouseMoveY = Input.GetAxis( "Mouse Y" ) * UIInfoManager.Instance.BackgroundMoveImageSpeed;

        //移动限制
        m_movedX += mouseMoveX;
        m_movedY += mouseMoveY;
        m_movedX = Mathf.Clamp( m_movedX, -GetMoveLimitX, GetMoveLimitX );
        m_movedY = Mathf.Clamp( m_movedY, -GetMoveLimitY, GetMoveLimitY );
        if (Mathf.Abs( m_movedX ) < GetMoveLimitX) {
            GetRectTransform.position -= mouseMoveX * Vector3.right;
        }
        if (Mathf.Abs( m_movedY ) < GetMoveLimitY) {
            GetRectTransform.position -= mouseMoveY * Vector3.up;
        }
    }

    private void Update() {
        MoveImageByMovingMouse();
    }

}
