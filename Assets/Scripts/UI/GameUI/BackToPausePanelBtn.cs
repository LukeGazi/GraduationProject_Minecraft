using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackToPausePanelBtn : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {
        NoticeManager.Instance.SendNotice( StrManager.PAUSE_PANEL_SHOW_NOTICE );
        NoticeManager.Instance.SendNotice( StrManager.GAME_OPTION_PANEL_HIDE_NOTICE );
    }
}
