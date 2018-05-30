using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private bool m_isBagOpen = false; //背包打开
    private bool m_isGameOn = true; //游戏界面在最上方
    private bool m_isGamePause = false; //游戏是否暂停
    private bool m_isOptionPanelOn = false; //选项界面是否在上方

    // Update is called once per frame
    private void Update() {

        //按下E键
        if (Input.GetKeyDown( KeyCode.E )) {
            //背包开启状态则关闭，关闭状态则开启
            if (m_isBagOpen) {
                NoticeManager.Instance.SendNotice( StrManager.BAG_PANEL_HIDE_NOTICE );
                NoticeManager.Instance.SendNotice( StrManager.ITEMS_TABS_PANEL_SHOW_NOTICE );
                NoticeManager.Instance.SendNotice( StrManager.SIGHT_IMAGE_SHOW_NOTICE );
                CursorManager.Instance.LockCursor();
                m_isBagOpen = false;
                m_isGameOn = true;
                m_isGamePause = false;
            } else {
                NoticeManager.Instance.SendNotice( StrManager.BAG_PANEL_SHOW_NOTICE );
                NoticeManager.Instance.SendNotice( StrManager.ITEMS_TABS_PANEL_HIDE_NOTICE );
                NoticeManager.Instance.SendNotice( StrManager.SIGHT_IMAGE_HIDE_NOTICE );
                CursorManager.Instance.UnlockCursor();
                m_isBagOpen = true;
                m_isGameOn = false;
                m_isGamePause = false;
            }
        }

        //按下ESC
        if (Input.GetKeyDown( KeyCode.Escape )) {
            //如果实在游戏界面中，则打开暂停面板
            if (m_isGameOn) {
                NoticeManager.Instance.SendNotice( StrManager.PAUSE_PANEL_SHOW_NOTICE );
                CursorManager.Instance.UnlockCursor();
                m_isGameOn = false;
                m_isGamePause = true;
                m_isBagOpen = false;
            } else if (m_isGamePause) { //如果实在暂停界面中，则取消暂停
                RetuenGame();
            }

            //背包开启状态则关闭，关闭状态则开启
            if (m_isBagOpen) {
                NoticeManager.Instance.SendNotice( StrManager.BAG_PANEL_HIDE_NOTICE );
                NoticeManager.Instance.SendNotice( StrManager.ITEMS_TABS_PANEL_SHOW_NOTICE );
                NoticeManager.Instance.SendNotice( StrManager.SIGHT_IMAGE_SHOW_NOTICE );
                CursorManager.Instance.LockCursor();
                m_isBagOpen = false;
                m_isGameOn = true;
                m_isGamePause = false;
            }


        }

        //如果在游戏界面
        if (m_isGameOn) {
            #region 物品栏被选中切换
            float scrollWhellRoll = Input.GetAxis( "Mouse ScrollWheel" );
            if (scrollWhellRoll >= 0.1f) {
                NoticeManager.Instance.SendNotice( StrManager.ITEM_SELECTED_MOVE_DOWN_NOTICE );
            }
            if (scrollWhellRoll <= -0.1f) {
                NoticeManager.Instance.SendNotice( StrManager.ITEM_SELECTED_MOVE_UP_NOTICE );
            }

            //如果按下大键盘1
            if (Input.GetKeyDown( KeyCode.Alpha1 )) {
                NoticeManager.Instance.SendNotice( StrManager.ITEM_SELECTED_MOVE_BY_NUM_NOTICE, new object[1] { 0 } );
            }
            //如果按下大键盘2
            if (Input.GetKeyDown( KeyCode.Alpha2 )) {
                NoticeManager.Instance.SendNotice( StrManager.ITEM_SELECTED_MOVE_BY_NUM_NOTICE, new object[1] { 1 } );
            }
            //如果按下大键盘3
            if (Input.GetKeyDown( KeyCode.Alpha3 )) {
                NoticeManager.Instance.SendNotice( StrManager.ITEM_SELECTED_MOVE_BY_NUM_NOTICE, new object[1] { 2 } );
            }
            //如果按下大键盘4
            if (Input.GetKeyDown( KeyCode.Alpha4 )) {
                NoticeManager.Instance.SendNotice( StrManager.ITEM_SELECTED_MOVE_BY_NUM_NOTICE, new object[1] { 3 } );
            }
            //如果按下大键盘5
            if (Input.GetKeyDown( KeyCode.Alpha5 )) {
                NoticeManager.Instance.SendNotice( StrManager.ITEM_SELECTED_MOVE_BY_NUM_NOTICE, new object[1] { 4 } );
            }
            //如果按下大键盘6
            if (Input.GetKeyDown( KeyCode.Alpha6 )) {
                NoticeManager.Instance.SendNotice( StrManager.ITEM_SELECTED_MOVE_BY_NUM_NOTICE, new object[1] { 5 } );
            }
            //如果按下大键盘7
            if (Input.GetKeyDown( KeyCode.Alpha7 )) {
                NoticeManager.Instance.SendNotice( StrManager.ITEM_SELECTED_MOVE_BY_NUM_NOTICE, new object[1] { 6 } );
            }
            //如果按下大键盘8
            if (Input.GetKeyDown( KeyCode.Alpha8 )) {
                NoticeManager.Instance.SendNotice( StrManager.ITEM_SELECTED_MOVE_BY_NUM_NOTICE, new object[1] { 7 } );
            }
            //如果按下大键盘9
            if (Input.GetKeyDown( KeyCode.Alpha9 )) {
                NoticeManager.Instance.SendNotice( StrManager.ITEM_SELECTED_MOVE_BY_NUM_NOTICE, new object[1] { 8 } );
            }
            #endregion

            #region  玩家左右键放置、销毁方块
            //从屏幕正中心发射射线，检测在一定距离内碰到的方块，进行计算后摆放手中方块
            //左键点击
            if (Input.GetMouseButtonDown( 0 )) {
                RaycastHit hitInfo;
                if (CreateRayIsHited( out hitInfo )) {
                    GameObject gameObject = hitInfo.transform.gameObject;
                    Destroy( gameObject );
                }
            }
            if (Input.GetMouseButtonDown( 1 )) {
                RaycastHit hitInfo;
                if (CreateRayIsHited( out hitInfo )) {
                    NoticeManager.Instance.SendNotice( StrManager.PLAYER_CREATE_BLOCK_TO_MAP, new object[] { hitInfo } );
                }
            }
            #endregion

        }


    }

    /// <summary>
    /// 返回游戏
    /// </summary>
    private void RetuenGame() {
        NoticeManager.Instance.SendNotice( StrManager.PAUSE_PANEL_HIDE_NOTICE );
        NoticeManager.Instance.SendNotice( StrManager.GAME_OPTION_PANEL_HIDE_NOTICE );

        m_isGameOn = true;
        m_isGamePause = false;
        m_isBagOpen = false;
        CursorManager.Instance.LockCursor();

    }

    public LayerMask PlayerLayerMask;
    /// <summary>
    /// 创建射线
    /// </summary>
    private bool CreateRayIsHited(out RaycastHit _hitInfo) {
        Ray ray = new Ray( Camera.main.transform.position, Camera.main.transform.forward ); //照相机位置和屏幕中心在世界坐标中的位置相同
        RaycastHit hit;
        bool isHited = Physics.Raycast( ray, out hit, GameInfoManager.Instance.RayDetectDistance, ~PlayerLayerMask ); //碰撞除了玩家
        _hitInfo = hit;
        return isHited;
    }

    private void Awake() {
        NoticeManager.Instance.Register( StrManager.RETURN_GAME, RetuenGame );
    }

}
