using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTabsUI : MonoBehaviour {

    #region Panel
    /// <summary>
    /// 获取RectTransform
    /// </summary>
    private RectTransform m_rectTransform;
    private RectTransform GetRectTransform {
        get {
            if (m_rectTransform == null) {
                m_rectTransform = GetComponent<RectTransform>();
            }
            return m_rectTransform;
        }
    }

    /// <summary>
    /// 展示面板
    /// </summary>
    private void ShowPanel() {
        GetRectTransform.localScale = Vector3.one;
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    private void HidePanel() {
        GetRectTransform.localScale = Vector3.zero;
    }
    #endregion

    #region 物品选中框移动
    public Image[] ItemTabsImages; //物品栏每栏
    public Image SelectedTabImage; //被选中栏外框

    private int m_currentSelectedIndex = 0; //当前备选索引
    /// <summary>
    /// 移动被选框
    /// </summary>
    private void MoveIndexSelectedTabImage(int _moveIndex) {
        if (_moveIndex < 0 || _moveIndex > 8) {
            return;
        }
        MoveSelectedTabImage( _moveIndex );
    }
    /// <summary>
    /// 用于外部消息机制调用
    /// </summary>
    /// <param name="_moveIndex"></param>
    private void MoveIndexSelectedTabImage(object[] _moveIndex) {
        int moveIndex = (int)_moveIndex[0];
        if (moveIndex < 0 || moveIndex > 8) {
            return;
        }
        MoveSelectedTabImage( moveIndex );
    }
    /// <summary>
    /// 上移
    /// </summary>
    private void MoveUpSelectedTabImage() {
        m_currentSelectedIndex++;
        if (m_currentSelectedIndex >= 9) {
            m_currentSelectedIndex = m_currentSelectedIndex - 9;
        }
        MoveIndexSelectedTabImage( m_currentSelectedIndex );
    }
    /// <summary>
    /// 下移
    /// </summary>
    private void MoveDownSelectedTabImage() {
        m_currentSelectedIndex--;
        if (m_currentSelectedIndex < 0) {
            m_currentSelectedIndex = m_currentSelectedIndex + 9;
        }
        MoveIndexSelectedTabImage( m_currentSelectedIndex );
    }
    /// <summary>
    /// 根据传递的参数移动
    /// </summary>
    /// <param name="_index"></param>
    private void MoveSelectedTabImage(int _index) {
        SelectedTabImage.GetComponent<RectTransform>().localPosition = ItemTabsImages[_index].GetComponent<RectTransform>().localPosition;
        m_currentSelectedIndex = _index;
        //消息机制调用更新玩家手中方块
        NoticeManager.Instance.SendNotice( StrManager.UPDATE_PLYER_HAND_BLOCK, new object[] { ItemTabsImages[_index].GetComponent<BagBlockImageUI>().BlockInfo } );
    }
    #endregion

    #region 物品框显示新物品
    public Transform[] BagItemImageTrans; //背包中item关联栏
    /// <summary>
    /// 更新物品栏
    /// </summary>
    private void UpdateItemInTabs() {
        for (int i = 0; i < BagItemImageTrans.Length; i++) {
            if (BagItemImageTrans[i].childCount != 0) {
                BagBlockImageUI bagBlockImageUI = BagItemImageTrans[i].GetChild( 0 ).GetComponent<BagBlockImageUI>();
                Block block = bagBlockImageUI.BlockInfo;
                ItemTabsImages[i].GetComponent<BagBlockImageUI>().BlockInfo = bagBlockImageUI.BlockInfo; //统一block类型
                ItemTabsImages[i].GetComponent<Image>().sprite = block.GetSprite;
                ItemTabsImages[i].GetComponent<Image>().color = Vector4.one;
            } else {
                ItemTabsImages[i].GetComponent<BagBlockImageUI>().BlockInfo = null;
                ItemTabsImages[i].GetComponent<Image>().color = Vector4.zero;
            }
            if (i == m_currentSelectedIndex) {
                NoticeManager.Instance.SendNotice( StrManager.UPDATE_PLYER_HAND_BLOCK, new object[] { ItemTabsImages[i].GetComponent<BagBlockImageUI>().BlockInfo } );
            }
        }
    }

    #endregion

    private void Awake() {

        NoticeManager.Instance.Register( StrManager.ITEMS_TABS_PANEL_SHOW_NOTICE, ShowPanel );
        NoticeManager.Instance.Register( StrManager.ITEMS_TABS_PANEL_HIDE_NOTICE, HidePanel );

        NoticeManager.Instance.Register( StrManager.ITEM_SELECTED_MOVE_UP_NOTICE, MoveUpSelectedTabImage );
        NoticeManager.Instance.Register( StrManager.ITEM_SELECTED_MOVE_DOWN_NOTICE, MoveDownSelectedTabImage );

        NoticeManager.Instance.Register( StrManager.ITEM_SELECTED_MOVE_BY_NUM_NOTICE, MoveIndexSelectedTabImage );
        NoticeManager.Instance.Register( StrManager.ITEM_PANEL_UPDATE_SHOW_NEW_IMAGE_NOTICE, UpdateItemInTabs );
    }

}
