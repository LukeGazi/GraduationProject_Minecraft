using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SingleGameBtn : StartPanelButtonUI {

    private void Awake() {
        //注册消息：开始播放移出按钮的两种方法、开始播放移回按钮动作的两种方法
        NoticeManager.Instance.Register( StrManager.BTN_SINGLEGAME_MOVE_AND_FADE_NOTICE, PlayMoveAwayAndFade );
        NoticeManager.Instance.Register( StrManager.BTN_SINGLEGAME_BACK_AND_RECOVER_NOTICE, PlayMoveBackAndRecover );

        NoticeManager.Instance.Register( StrManager.BTN_SINGLEGAME_MOVE_NOT_FADE_NOTICE, PlayMoveAwayNotFade );
        NoticeManager.Instance.Register( StrManager.BTN_SINGLEGAME_BACK_NOT_RECOVER_NOTICE, PlayMoveBackNotRecover );
    }

    /// <summary>
    /// 鼠标点击
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerClick(PointerEventData eventData) {
        //若鼠标不可用或正在由动画播放，则不加载动画
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
    /// 点击SingleGameButton所加载的协程动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator ButtonMoveAway() {
        NoticeManager.Instance.SendNotice( StrManager.BTN_OPTION_MOVE_AND_FADE_NOTICE );
        yield return new WaitForSeconds( UIInfoManager.Instance.ButtonMoveDelayTimeOne );
        NoticeManager.Instance.SendNotice( StrManager.BTN_QUIT_MOVE_AND_FADE_NOTICE );
        yield return new WaitForSeconds( UIInfoManager.Instance.ButtonMoveDelayTimeTwo );
        NoticeManager.Instance.SendNotice( StrManager.BTN_SINGLEGAME_MOVE_NOT_FADE_NOTICE );
        NoticeManager.Instance.SendNotice( StrManager.SINGLEGAME_PANEL_AWAY_NOTICE );
        yield return null;
    }

}
