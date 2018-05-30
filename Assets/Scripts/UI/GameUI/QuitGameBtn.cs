using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuitGameBtn : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {
        DebugManager.Log( StrManager.DEBUG_QUIT_APPLICATION );
        Application.Quit();
    }
}
