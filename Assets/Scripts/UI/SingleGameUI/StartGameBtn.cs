using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameBtn : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler {

    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) {
        AudioManager.Instance.PlayMouseClickClip();
        LoadGame();
    }

    /// <summary>
    /// 鼠标悬浮事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) {
        AudioManager.Instance.PlayMouseHoverClip();
    }

    /// <summary>
    /// 加载游戏
    /// </summary>
    private void LoadGame() {
        SceneManager.LoadScene( 1 );
    }

    private void Awake() {
        NoticeManager.Instance.Register( StrManager.LOAD_SCENE_NOTICE, LoadGame );
    }

}
