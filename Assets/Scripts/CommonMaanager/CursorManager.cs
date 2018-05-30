using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour {

    public static CursorManager Instance;

    public Texture2D CursorTexture;

    private void Awake() {
        Instance = this;
        AlterCursorTexture();
    }

    private void Start() {
        DontDestroyOnLoad( this );
    }

    /// <summary>
    /// 修改鼠标样式
    /// </summary>
    public void AlterCursorTexture() {
        Cursor.SetCursor( CursorTexture, Vector2.zero, CursorMode.Auto );
    }

    /// <summary>
    /// 锁上鼠标
    /// </summary>
    public void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// 解锁鼠标
    /// </summary>
    public void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
    }

}
