using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecifyAudioPanelUI : MonoBehaviour {

    #region Panel
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
    /// 播放展示动画
    /// </summary>
    private void PlayShowPanel() {
        StartCoroutine( ShowPanel() );
    }
    /// <summary>
    /// 展示面板动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowPanel() {
        while (GetRectTransform.localScale.x < 1 - UIInfoManager.Instance.SpecifyAudioPanelShrinkError) {
            GetRectTransform.localScale = Vector3.Lerp( GetRectTransform.localScale, Vector3.one, UIInfoManager.Instance.SpecifyAudioPanelShrinkSpeed );
            yield return null;
        }
        GetRectTransform.localScale = Vector3.one;
        yield return null;
    }

    /// <summary>
    /// 播放隐藏动画
    /// </summary>
    private void PlayHidePanel() {
        StartCoroutine( HidePanel() );
    }
    /// <summary>
    /// 隐藏面板动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator HidePanel() {
        while (GetRectTransform.localScale.x > UIInfoManager.Instance.SpecifyAudioPanelShrinkError) {
            GetRectTransform.localScale = Vector3.Lerp( GetRectTransform.localScale, Vector3.zero, UIInfoManager.Instance.SpecifyAudioPanelShrinkSpeed );
            yield return null;
        }
        GetRectTransform.localScale = Vector3.zero;
        yield return null;
    }

    #endregion

    #region Slider
    public Slider VolumeSlider;
    public Slider BGMSlider;
    public Slider EffetSlider;

    /// <summary>
    /// 添加Slider值变化事件
    /// </summary>
    private void AddSliderValueChangeEvents() {
        VolumeSlider.onValueChanged.AddListener( ChangeVolumeSliderValue );
        BGMSlider.onValueChanged.AddListener( ChangeBGMSliderValue );
        EffetSlider.onValueChanged.AddListener( ChangeEffectSliderValue );
    }
    /// <summary>
    /// VolumeSlider变化事件
    /// </summary>
    /// <param name="_volume"></param>
    private void ChangeVolumeSliderValue(float _volume) {
        AudioManager.Instance.AlterAllAudioVolume( _volume );
    }
    /// <summary>
    /// VolumeSlider变化事件
    /// </summary>
    /// <param name="_volume"></param>
    private void ChangeBGMSliderValue(float _volume) {
        AudioManager.Instance.AlterAudioSourceVolume( AudioSourceType.BGM, _volume );
    }
    /// <summary>
    /// VolumeSlider变化事件
    /// </summary>
    /// <param name="_volume"></param>
    private void ChangeEffectSliderValue(float _volume) {
        AudioManager.Instance.AlterAudioSourceVolume( AudioSourceType.MOUSE_CLICK, _volume );
        AudioManager.Instance.AlterAudioSourceVolume( AudioSourceType.MOUSE_HOVER, _volume );
    }

    #endregion

    private void Awake() {
        //注册消息
        NoticeManager.Instance.Register( StrManager.SPECIFY_AUDIO_PANEL_SHOW_NOTICE, PlayShowPanel );
        NoticeManager.Instance.Register( StrManager.SPECIFY_AUDIO_PANEL_HIDE_NOTICE, PlayHidePanel );

        AddSliderValueChangeEvents();
    }

    private void OnDestroy() {
        //销毁消息
        NoticeManager.Instance.Unregister( StrManager.SPECIFY_AUDIO_PANEL_SHOW_NOTICE, PlayShowPanel );
        NoticeManager.Instance.Unregister( StrManager.SPECIFY_AUDIO_PANEL_HIDE_NOTICE, PlayHidePanel );
    }

}
