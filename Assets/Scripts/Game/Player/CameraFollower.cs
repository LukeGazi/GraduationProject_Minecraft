using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    //观察目标  
    public Transform PlayerTrans;

    #region Rotation
    float m_rotationY = 0f;
    private Quaternion m_quaternion;

    /// <summary>
    /// 旋转照相机角度
    /// </summary>
    private void RotateCamera() {
        float m_rotationX = transform.localEulerAngles.y + Input.GetAxis( "Mouse X" ) * GameInfoManager.Instance.CameraRotateSensitivityX;
        m_rotationY += Input.GetAxis( "Mouse Y" ) * GameInfoManager.Instance.CameraRotateSensitivityY;
        m_rotationY = Mathf.Clamp( m_rotationY, GameInfoManager.Instance.CameraRotateMinLimitRY, GameInfoManager.Instance.CameraRotateMaxLimitRY );

        transform.localEulerAngles = new Vector3( -m_rotationY, m_rotationX, 0 );
    }

    #endregion

    #region Move
    /// <summary>
    /// 移动照相机
    /// </summary>
    private void MoveCamera() {
        transform.position = PlayerTrans.position + Vector3.up * GameInfoManager.Instance.CameraMoveHeight;
    }
    #endregion

    private void Awake() {
        NoticeManager.Instance.Register( StrManager.ALTER_CAMERA_VIEW, AlterFieldOView );
    }

    private void Start() {
        GetComponent<Camera>().fieldOfView = WorldManager.Instance.CamerViewOfField;
        NoticeManager.Instance.SendNotice( StrManager.ALTER_VIEW_SLIDER_VALUE, new object[] { GetComponent<Camera>().fieldOfView } );
    }

    /// <summary>
    /// 修改照相机视野
    /// </summary>
    private void AlterFieldOView(object[] _view) {
        float view = (float)_view[0];
        GetComponent<Camera>().fieldOfView = view;
    }

    private void LateUpdate() {
        if (!PlayerTrans) {
            DebugManager.LogError( StrManager.PLAYER_NOT_FOUND_ERROR );
            return;
        }
        MoveCamera();
        RotateCamera();
    }





}
