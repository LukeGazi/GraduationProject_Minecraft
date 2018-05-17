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

    // Update is called once per frame
    private void Update() {
        MovePlayer();
        RotatePlayer();
    }
}
