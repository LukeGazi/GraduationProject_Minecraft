using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuitBtn : StartPanelButtonUI {

    private void Awake() {
        //注册移动协程动画
        NoticeManager.Instance.Register( StrManager.BTN_QUIT_MOVE_AND_FADE_NOTICE, PlayMoveAwayAndFade );
        NoticeManager.Instance.Register( StrManager.BTN_QUIT_BACK_AND_RECOVER_NOTICE, PlayMoveBackAndRecover );
    }

    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerClick(PointerEventData eventData) {
        base.OnPointerClick( eventData );
        //Debug模式下无法退出，Debug文字进行测试
        Debug.Log( StrManager.DEBUG_QUIT_APPLICATION );
        Application.Quit();
    }

}
