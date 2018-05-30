using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    /// <summary>
    /// 获取主照相机
    /// </summary>
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

    /// <summary>
    /// 控制玩家旋转
    /// </summary>
    private void RotatePlayer() {
        Quaternion quaternion = GetCamera.transform.rotation;
        quaternion.x = 0;
        quaternion.z = 0;
        transform.rotation = quaternion;
    }
    #endregion

    /// <summary>
    /// 获取玩家移动方向
    /// </summary>
    /// <returns></returns>
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
        if (m_direction.z - m_oldPosition.z >= 1 || m_direction.z - m_oldPosition.z <= -1) {
            m_oldPosition.z = m_direction.z;
        }
    }

    #region 玩家手中物品
    public GameObject PlayerHand; //玩家手
    private GameObject m_oldBlock = null;
    private Block m_oldBlockInfo;
    /// <summary>
    /// 更新玩家手中物品
    /// </summary>
    private void UpdatePlayerHandItem(object[] _objectsInHand) {
        Block blockInfo = _objectsInHand[0] as Block;
        m_oldBlockInfo = blockInfo;
        if (blockInfo == null) {
            if (m_oldBlock) {
                Destroy( m_oldBlock.gameObject );
            }
            PlayerHand.SetActive( true ); //胳膊可见
            m_oldBlock = null;
        } else {
            if (m_oldBlock) {
                Destroy( m_oldBlock.gameObject );
            }
            PlayerHand.SetActive( false ); //胳膊隐藏
            m_oldBlock = GameObject.Instantiate( blockInfo.GetBlockPrefab );
            m_oldBlock.transform.parent = transform;
            m_oldBlock.transform.localPosition = PlayerHand.transform.localPosition;
        }
    }
    /// <summary>
    /// 创建新方块
    /// </summary>
    private void CreateNewBlcokToMap(object[] _infos) {
        if (m_oldBlock == null) {
            return;
        }
        RaycastHit hitInfo = (RaycastHit)( _infos[0] );
        //Vector3 hitPoint = hitInfo.point;
        //Vector3 hitTransPosition = hitInfo.transform.position;
        //Vector3 subPosition = hitTransPosition - hitPoint;
        ////原坐标+差坐标->四舍五入取整得坐标
        //Vector3 newPosition = subPosition - hitTransPosition;
        //综合上述
        //..............
        Vector3 hitPoint = hitInfo.point;
        Vector3 hitTransPosition = hitInfo.transform.position;
        Vector3 subPosition = hitPoint - hitTransPosition;
        Vector3 newPosition = hitInfo.point;
        ////将坐标四舍五入取整，差值正负作为新坐标的加减的符号
        newPosition = new Vector3( GetNextPoint( newPosition.x, subPosition.x ), GetNextPoint( newPosition.y, subPosition.y ), GetNextPoint( newPosition.z, subPosition.z ) );
        GameObject block = GameObject.Instantiate( m_oldBlockInfo.GetBlockPrefab );
        //坐标计算二选一
        //block.transform.position = ProcessPosition( hitTransPosition, subPosition );
        block.transform.position = newPosition;
        ////临时操作*****************************************************************
        block.transform.parent = GameObject.Find( "Map" ).transform;
        //************************************************************************
    }
    /// <summary>
    /// 取整，根据实际情况总结计算
    /// </summary>
    /// <returns></returns>
    private int GetNextPoint(float _float, float _sign) {
        float fraction = _float - (int)_float; //计算小数部分
        if (Mathf.Abs( fraction ) == 0.5f) { //如果小数部分为0.5，根据sign计算
            _float += _sign >= 0 ? 0.5f : -0.5f;
        } else { //否则就四舍六入
            _float = Mathf.Round( _float ); 
        }
        return (int)_float;
    }
    private Vector3 ProcessPosition(Vector3 _oldPosition, Vector3 _subPosition) {
        if (_subPosition.x == 0.5f) {
            _oldPosition.x += 1;
        } else if (_subPosition.x == -0.5f) {
            _oldPosition.x -= 1;
        } else if (_subPosition.y == 0.5f) {
            _oldPosition.y += 1;
        } else if (_subPosition.y == -0.5f) {
            _oldPosition.y -= 1;
        } else if (_subPosition.z == 0.5f) {
            _oldPosition.z += 1;
        } else if (_subPosition.z == -0.5f) {
            _oldPosition.z -= 1;
        }
        return _oldPosition;
    }
    #endregion

    private void Awake() {
        NoticeManager.Instance.Register( StrManager.UPDATE_PLYER_HAND_BLOCK, UpdatePlayerHandItem );
        NoticeManager.Instance.Register( StrManager.PLAYER_CREATE_BLOCK_TO_MAP, CreateNewBlcokToMap );
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
