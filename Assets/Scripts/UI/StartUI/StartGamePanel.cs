using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartGamePanel : MonoBehaviour {
    #region 获取参数
    public Image TitleImage;
    public Text TitleText;
    public Button SingleGameBtn;
    public Button OptionBtn;
    public Button QuitgameBtn;

    public Texture2D CursorTexture;
    #endregion

    private void Start() {
        AlterCursorTexture();
        AudioManager.Instance.PlayBGMClip();
    }

    /// <summary>
    /// 修改鼠标样式
    /// </summary>
    private void AlterCursorTexture() {
        Cursor.SetCursor( CursorTexture, Vector2.zero, CursorMode.Auto );
    }

}
