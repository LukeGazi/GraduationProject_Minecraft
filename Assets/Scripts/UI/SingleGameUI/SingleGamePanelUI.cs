using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGamePanelUI : HidenPanelUI {

    private void Awake() {
        //注册消息：移入移出动画
        NoticeManager.Instance.Register( StrManager.SINGLEGAME_PANEL_AWAY_NOTICE, PlayMoveAway );
        NoticeManager.Instance.Register( StrManager.SINGLEGAME_PANEL_BACK_NOTICE, PlayMoveBack );
    }

}
