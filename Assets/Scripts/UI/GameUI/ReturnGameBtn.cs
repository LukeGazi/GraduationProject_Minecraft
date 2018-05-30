using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnGameBtn : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {
        NoticeManager.Instance.SendNotice( StrManager.RETURN_GAME );
    }
}
