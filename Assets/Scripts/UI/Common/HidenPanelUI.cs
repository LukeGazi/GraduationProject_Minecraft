using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidenPanelUI : MonoBehaviour {

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
    /// 隐藏Panel界面
    /// </summary>
    private void HidenPanel() {
        GetRectTransform.localPosition = new Vector3( ( CanvasInfos.Instance.GetCanvasWidth + UIInfoManager.Instance.HidenPanelWidth ) / 2, 0, 0 );
    }

    private void Start() {
        HidenPanel();
    }

    /// <summary>
    /// 播放移开动画
    /// </summary>
    protected void PlayMoveAway() {
        StartCoroutine( MoveAway() );
    }


    /// <summary>
    /// 播放移回动画
    /// </summary>
    protected void PlayMoveBack() {
        StartCoroutine( MoveBack() );
    }

    #region 显示Panel的协程动画
    /// <summary>
    /// 需要移动的距离
    /// </summary>
    private float? m_distance; //移动距离
    private float GetDistance {
        get {
            if (m_distance == null) {
                //根据面板本身的宽度移动
                m_distance = UIInfoManager.Instance.HidenPanelWidth;
            }
            return (float)m_distance;
        }
    }
    /// <summary>
    /// 面板原始位置
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
    /// 面板移出显示
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveAway() {
        Vector3 endPosition = GetOldPosition - Vector3.right * GetDistance;
        while (GetRectTransform.localPosition.x > endPosition.x + UIInfoManager.Instance.ButtonMoveError) {
            GetRectTransform.localPosition = Vector3.Lerp( GetRectTransform.localPosition, endPosition, UIInfoManager.Instance.ButtonMoveSpeed * Time.deltaTime );
            yield return null;
        }
        yield return null;
    }

    /// <summary>
    /// 面板移回
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveBack() {
        while (GetRectTransform.localPosition.x < GetOldPosition.x - UIInfoManager.Instance.ButtonMoveError) {
            GetRectTransform.localPosition = Vector3.Lerp( GetRectTransform.localPosition, GetOldPosition, UIInfoManager.Instance.ButtonMoveSpeed * Time.deltaTime );
            yield return null;
        }
        m_distance = null;
        m_oldPosition = null;
        yield return null;
    }
    #endregion

}
