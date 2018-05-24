using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ViewFieldSlider : MonoBehaviour, IPointerUpHandler {

    /// <summary>
    /// 获取Slider
    /// </summary>
    private Slider m_viewFeildSlider;
    private Slider GetViewFieldSlider {
        get {
            if (m_viewFeildSlider == null) {
                m_viewFeildSlider = GetComponent<Slider>();
            }
            return m_viewFeildSlider;
        }
    }
    public Text ViewValueText;

    /// <summary>
    /// 添加事件绑定
    /// </summary>
    private void AddSliderValueChangeEvents() {
        GetViewFieldSlider.onValueChanged.AddListener( ChangeViewSliderValue );
    }
    /// <summary>
    /// SliderValue事件绑定
    /// </summary>
    /// <param name="_value"></param>
    private void ChangeViewSliderValue(float _value) {
        ChageTextValue( _value );
    }

    /// <summary>
    /// 通过slidervalue获取View的value
    /// </summary>
    /// <param name="_sliderValue"></param>
    /// <returns></returns>
    private int GetViewValue(float _sliderValue) {
        //40 - 80 / 60
        return (int)( UIInfoManager.Instance.ViewFieldMin + _sliderValue * ( UIInfoManager.Instance.ViewFieldMax - UIInfoManager.Instance.ViewFieldMin ) );
    }

    /// <summary>
    /// 修改Text显示值
    /// </summary>
    private void ChageTextValue(float _sliderValue) {
        ViewValueText.text = GetViewValue( _sliderValue ).ToString();
    }

    /// <summary>
    /// 修改照相机视野 
    /// </summary>
    private void ChangeCameraView(float _sliderValue) {
        WorldManager.Instance.CamerViewOfField = _sliderValue;
    }

    private void Awake() {
        AddSliderValueChangeEvents();
        ViewValueText.text = "";
    }

    public void OnPointerUp(PointerEventData eventData) {
        ViewValueText.text = "";
    }
}
