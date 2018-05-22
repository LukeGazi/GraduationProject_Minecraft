using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Camera m_mainCamera;
    private Camera GetCamera {
        get {
            if (!m_mainCamera) {
                m_mainCamera = Camera.main;
            }
            return m_mainCamera;
        }
    }

    #region CharacterController parameters and methods
    [SerializeField]
    private float speed = 6.0F;
    [SerializeField]
    private float jumpSpeed = 8.0F;
    [SerializeField]
    private float gravity = 20.0F;
    private Vector3 m_moveDirection = Vector3.zero;

    private CharacterController m_characterController;
    private CharacterController GetCharacterController {
        get {
            if (!m_characterController) {
                m_characterController = GetComponent<CharacterController>();
            }
            return m_characterController;
        }
    }

    /// <summary>
    /// Player move by CharacterConroller
    /// </summary>
    private void MovePlayer() {
        if (GetCharacterController.isGrounded) {
            m_moveDirection = new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );
            m_moveDirection = transform.TransformDirection( m_moveDirection );
            m_moveDirection *= speed;
            if (Input.GetButton( "Jump" ))
                m_moveDirection.y = jumpSpeed;
        }
        m_moveDirection.y -= gravity * Time.deltaTime;
        GetCharacterController.Move( m_moveDirection * Time.deltaTime );
    }


    #endregion

    #region Rotation
    [SerializeField]
    private float m_sensitivity = 5.0f;

    /// <summary>
    /// Player rotate by move mouse
    /// </summary>
    private void RotatePlayer() {
        Quaternion quaternion = GetCamera.transform.rotation;
        quaternion.x = 0;
        quaternion.z = 0;
        transform.rotation = quaternion;
    }
    #endregion

    public MapHorizontalDirection GetPlayerDirection() {

        if (m_direction.z >= 1) {
            if (m_direction.x >= 1) {
                return MapHorizontalDirection.FRONT_AND_RIGHT;
            }
            if (m_direction.x <= -1) {
                return MapHorizontalDirection.FRONT_AND_LEFT;
            }
            return MapHorizontalDirection.FRONT;
        }
        if (m_direction.z <= -1) {
            if (m_direction.x >= 1) {
                return MapHorizontalDirection.BACK_AND_RIGHT;
            }
            if (m_direction.x <= -1) {
                return MapHorizontalDirection.BACK_AND_LEFT;
            }
            return MapHorizontalDirection.BACK;
        }
        if (m_direction.x >= 1) {
            return MapHorizontalDirection.RIGHT;
        }
        if (m_direction.x <= -1) {
            return MapHorizontalDirection.LEFT;
        }

        return MapHorizontalDirection.None;
    }

    private Vector3 m_oldPosition = Vector3.zero;
    private Vector3 m_direction;

    /// <summary>
    /// 计算方向，以一个block为单位
    /// </summary>
    private void CalcDirection() {
        m_direction = transform.position - m_oldPosition;
        if (m_direction.x - m_oldPosition.x >= 1 || m_direction.x - m_oldPosition.x <= -1) {
            m_oldPosition.x = m_direction.x;
        }
        if (m_direction.z - m_oldPosition.z >= 1 || m_direction.z - m_oldPosition.z <= 1) {
            m_oldPosition.z = m_direction.z;
        }
    }

    // Update is called once per frame
    private void Update() {
        MovePlayer();
        RotatePlayer();
    }

    private void LateUpdate() {
        CalcDirection();
    }
}
