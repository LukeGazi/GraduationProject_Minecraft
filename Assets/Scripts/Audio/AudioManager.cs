using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioSourceType {
    BGM,
    MOUSE_HOVER,
    MOUSE_CLICK,
}

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    public AudioClip[] MouseHoverClips; //鼠标悬浮音效
    public AudioClip[] MouseClickClips; //鼠标点击音效
    public AudioClip[] BGMClip; //游戏背景音乐

    private AudioSource[] m_audioSource;
    private AudioSource GetAudioSource(AudioSourceType _audioSourceType) {
        //根据枚举类型的个数生成需要的空间个数
        if (m_audioSource == null) {
            m_audioSource = new AudioSource[3];
            for (int i = 0; i < 3; i++) {
                m_audioSource[i] = gameObject.AddComponent<AudioSource>();
            }
        }
        return m_audioSource[(int)_audioSourceType];
    }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        //切换场景时不销毁
        DontDestroyOnLoad( this );
    }

    /// <summary>
    /// 播放鼠标悬浮音效
    /// </summary>
    public void PlayMouseHoverClip() {
        if (MouseHoverClips == null) {
            return;
        }
        if (MouseHoverClips[0] == null) {
            return;
        }
        GetAudioSource( AudioSourceType.MOUSE_HOVER ).PlayOneShot( MouseHoverClips[0] );
    }


    /// <summary>
    /// 播放鼠标点击音效
    /// </summary>
    public void PlayMouseClickClip() {
        if (MouseClickClips == null) {
            return;
        }
        if (MouseClickClips[0] == null) {
            return;
        }
        GetAudioSource( AudioSourceType.MOUSE_CLICK ).PlayOneShot( MouseClickClips[0] );
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    public void PlayBGMClip() {
        if (BGMClip == null) {
            return;
        }
        if (BGMClip[0] == null) {
            return;
        }
        GetAudioSource( AudioSourceType.BGM ).clip = BGMClip[Random.Range( 0, BGMClip.Length )];
        GetAudioSource( AudioSourceType.BGM ).loop = true;
        GetAudioSource( AudioSourceType.BGM ).Play();
    }


    /// <summary>
    /// 修改所有音源音量
    /// </summary>
    public void AlterAllAudioVolume(float _volume) {
        AudioListener.volume = _volume;
    }

    /// <summary>
    /// 静音游戏
    /// </summary>
    public void SwitchMuteGame(bool _isMute) {
        AudioListener.pause = _isMute;
    }

    /// <summary>
    /// 修改音源音量
    /// </summary>
    /// <param name="_audioSourceType"></param>
    /// <param name="_volume"></param>
    public void AlterAudioSourceVolume(AudioSourceType _audioSourceType, float _volume) {
        GetAudioSource( _audioSourceType ).volume = _volume;
    }

}
