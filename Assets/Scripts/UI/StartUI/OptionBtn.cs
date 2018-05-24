using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionBtn : StartPanelButtonUI {

    private void Awake() {
        //注册消息：开始播放移出按钮的两种方法、开始播放移回按钮动作的两种方法
        NoticeManager.Instance.Register( StrManager.BTN_OPTION_MOVE_AND_FADE_NOTICE, PlayMoveAwayAndFade );
        NoticeManager.Instance.Register( StrManager.BTN_OPTION_BACK_AND_RECOVER_NOTICE, PlayMoveBackAndRecover );

        NoticeManager.Instance.Register( StrManager.BTN_OPTION_MOVE_NOT_FADE_NOTICE, PlayMoveAwayNotFade );
        NoticeManager.Instance.Register( StrManager.BTN_OPTION_BACK_NOT_REOVER_NOTICE, PlayMoveBackNotRecover );
    }

    /// <summary>
    /// 鼠标点击
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerClick(PointerEventData eventData) {
        if (!GetInteractable) {
            return;
        }
        if (UIInfoManager.Instance.IsAnimationPlaying) {
            return;
        }
        base.OnPointerClick( eventData );
        UIInfoManager.Instance.IsAnimationPlaying = true;
        StartCoroutine( ButtonMoveAway() );
    }

    /// <summary>
    /// 点击OptionButton后所加载的协程动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator ButtonMoveAway() {
        NoticeManager.Instance.SendNotice( StrManager.BTN_SINGLEGAME_MOVE_AND_FADE_NOTICE );
        yield return new WaitForSeconds( UIInfoManager.Instance.ButtonMoveDelayTimeOne );
        NoticeManager.Instance.SendNotice( StrManager.BTN_QUIT_MOVE_AND_FADE_NOTICE );
        yield return new WaitForSeconds( UIInfoManager.Instance.ButtonMoveDelayTimeTwo );
        NoticeManager.Instance.SendNotice( StrManager.BTN_OPTION_MOVE_NOT_FADE_NOTICE );
        NoticeManager.Instance.SendNotice( StrManager.OPTION_PANEL_AWAY_NOTICE );
        yield return null;
    }

}
