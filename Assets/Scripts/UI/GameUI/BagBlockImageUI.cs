using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BagBlockImageUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {

    public BlockType ImageBlockType;
    public Block BlockInfo;

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
    /// 鼠标按下
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData) {
        GetComponent<Image>().raycastTarget = false; //取消射线接受，检测到下方物体
    }

    /// <summary>
    /// 鼠标松开
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData) {

        GameObject backGb = eventData.pointerCurrentRaycast.gameObject; //获取检测到的对象
        if (backGb != null) {
            if (backGb.tag == StrManager.BAG_BLOCK) {
                transform.SetParent( backGb.transform );//设置父对象
            }
            if (backGb.tag == StrManager.ITEM_BLOCK) {
                transform.SetParent( backGb.transform );//设置父对象
                NoticeManager.Instance.SendNotice( StrManager.ITEM_PANEL_UPDATE_SHOW_NEW_IMAGE_NOTICE ); //更新物品栏
            }
        }
        GetRectTransform.localPosition = Vector3.zero; //位置修正
        GetComponent<Image>().raycastTarget = true; //恢复射线检测，可以进行下次点击
    }

    // Use this for initialization
    void Start() {
        switch (ImageBlockType) {
            case BlockType.GRASS:
                BlockInfo = BlockInfoManager.Instance.GrassBlock;
                break;
            case BlockType.STONE:
                BlockInfo = BlockInfoManager.Instance.StoneBlock;
                break;
            case BlockType.DIRT:
                BlockInfo = BlockInfoManager.Instance.DirtBlock;
                break;
            case BlockType.TREE:
            default:
                break;
        }
        if (BlockInfo != null) {
            GetComponent<Image>().sprite = BlockInfo.GetSprite;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        GetRectTransform.position = Input.mousePosition;
    }
}
