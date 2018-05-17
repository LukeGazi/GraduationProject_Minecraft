using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    //观察目标  
    public Transform PlayerTrans;

    #region Rotation
    private float m_sensitivityX = 5.0f;
    private float m_sensitivityY = 5.0f;
    private float m_maxLimitRY = 45;
    private float m_minLimitRY = -45;
    float m_rotationY = 0f;
    private Quaternion m_quaternion;

    private void RotateCamera() {
        float m_rotationX = transform.localEulerAngles.y + Input.GetAxis( "Mouse X" ) * m_sensitivityX;
        m_rotationY += Input.GetAxis( "Mouse Y" ) * m_sensitivityY;
        m_rotationY = Mathf.Clamp( m_rotationY, m_minLimitRY, m_maxLimitRY );

        transform.localEulerAngles = new Vector3( -m_rotationY, m_rotationX, 0 );
    }

    #endregion

    #region Move
    private float m_height = 1.5f;

    private void MoveCamera() {
        transform.position = PlayerTrans.position + Vector3.up * m_height;
    }
    #endregion


    private void LateUpdate() {
        if (!PlayerTrans) {
            DebugManager.LogError( StrManager.PLAYER_NOT_FOUND_ERROR );
            return;
        }
        MoveCamera();
        RotateCamera();
    }





}
