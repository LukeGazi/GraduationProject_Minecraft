using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleImageUI : MonoBehaviour {

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
    /// Title的Text，随机文字
    /// </summary>
    public Text TitleText;

    // Update is called once per frame
    void Update() {

        LookAtMouse();
        SetTextRandom();

    }

    private float m_movedX = 0;  //保存记录鼠标移动信息
    /// <summary>
    /// 注视鼠标而旋转
    /// </summary>
    private void LookAtMouse() {

        float mouseMoveX = Input.GetAxis( "Mouse X" ) * UIInfoManager.Instance.RotateTitleSpeed;

        //旋转限制
        m_movedX += mouseMoveX;
        m_movedX = Mathf.Clamp( m_movedX, UIInfoManager.Instance.RotateTitleLimitX, 0 );

        if (m_movedX < 0 && m_movedX > UIInfoManager.Instance.RotateTitleLimitX) {
            GetRectTransform.eulerAngles += Vector3.up * mouseMoveX;
        }

    }

    /// <summary>
    /// 文字随机内容显示
    /// </summary>
    private void SetTextRandom() {
        TitleText.text = TransformString( Time.time.ToString() );
    }

    /// <summary>
    /// 将string转化为乱字符
    /// </summary>
    private string TransformString(string _string) {
        char[] sympols = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '|' };
        char[] str = ( _string + _string ).ToCharArray();
        for (int i = 0; i < str.Length; i++) {
            int index = Random.Range( 0, sympols.Length );
            str[i] = sympols[index];
        }
        return new string( str );
    }

}
