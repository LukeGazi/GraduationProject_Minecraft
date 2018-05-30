using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrManager {

    #region ERROR
    public static string MESH_FILTER_FAIL_FOUND_ERROR = "Fail to get component MeshFilter!";
    public static string MESH_FAIL_GET_ERROR = "Fail to get Mesh!";
    public static string MESH_NOT_CUBE_ERROR = "The mesh is not the cube mesh!";
    public static string PLAYER_NOT_FOUND_ERROR = "Player is not attached!";
    public static string MAP_POINTED_QUADRANT_ERROR = "Quadrant is out of range!";
    #endregion

    #region NOTICE
    public static string START_CHECK_NOTICE = "Start check";
    //Button Move And Fade
    public static string BTN_OPTION_MOVE_AND_FADE_NOTICE = "Btn option move away and fade";
    public static string BTN_SINGLEGAME_MOVE_AND_FADE_NOTICE = "Btn single move away and fade";
    public static string BTN_QUIT_MOVE_AND_FADE_NOTICE = "Btn quit move away and fade";
    public static string BTN_OPTION_BACK_AND_RECOVER_NOTICE = "Btn option move back and recover";
    public static string BTN_SINGLEGAME_BACK_AND_RECOVER_NOTICE = "Btn single move back and recover";
    public static string BTN_QUIT_BACK_AND_RECOVER_NOTICE = "Btn quit move back and recover";
    //Panel Move
    public static string OPTION_PANEL_AWAY_NOTICE = "Option panel move away";
    public static string SINGLEGAME_PANEL_AWAY_NOTICE = "Singlegame panel move away";
    public static string OPTION_PANEL_BACK_NOTICE = "Option panel move back";
    public static string SINGLEGAME_PANEL_BACK_NOTICE = "Singlegame panel move back";
    //Button Move Not Fade
    public static string BTN_OPTION_MOVE_NOT_FADE_NOTICE = "Btn option move away not fade";
    public static string BTN_SINGLEGAME_MOVE_NOT_FADE_NOTICE = "Btn single move away not fade";
    public static string BTN_OPTION_BACK_NOT_REOVER_NOTICE = "Btn option move back not recover";
    public static string BTN_SINGLEGAME_BACK_NOT_RECOVER_NOTICE = "Btn single move back not recover";
    //Panel Shrink
    public static string SPECIFY_AUDIO_PANEL_SHOW_NOTICE = "Specify audio panel show";
    public static string SPECIFY_AUDIO_PANEL_HIDE_NOTICE = "Specify audio panel hide";
    //Panel Hide
    public static string BAG_PANEL_SHOW_NOTICE = "Bag panel show";
    public static string BAG_PANEL_HIDE_NOTICE = "Bag panel hide";
    public static string PAUSE_PANEL_SHOW_NOTICE = "Pause panel show";
    public static string PAUSE_PANEL_HIDE_NOTICE = "Pause panel hide";
    public static string GAME_OPTION_PANEL_SHOW_NOTICE = "Game option panel show";
    public static string GAME_OPTION_PANEL_HIDE_NOTICE = "Game option panel hide";
    public static string ITEMS_TABS_PANEL_SHOW_NOTICE = "Items tabs panel show";
    public static string ITEMS_TABS_PANEL_HIDE_NOTICE = "Items tabs panel hide";
    //Image Hide
    public static string SIGHT_IMAGE_SHOW_NOTICE = "Sight image show";
    public static string SIGHT_IMAGE_HIDE_NOTICE = "Sight image hide";
    //Load Scene
    public static string LOAD_SCENE_NOTICE = "Load Scene";
    //Item Selected Image Move
    public static string ITEM_SELECTED_MOVE_UP_NOTICE = "Item selected move up";
    public static string ITEM_SELECTED_MOVE_DOWN_NOTICE = "Item selected move down";
    public static string ITEM_SELECTED_MOVE_BY_NUM_NOTICE = "Item selected move by num";
    //Item Tabs Show Image
    public static string ITEM_PANEL_UPDATE_SHOW_NEW_IMAGE_NOTICE = "Item tabs update show new item";
    //Update Player Hand
    public static string UPDATE_PLYER_HAND_BLOCK = "Update block in hand of player";
    //Create Block
    public static string PLAYER_CREATE_BLOCK_TO_MAP = "Player create new block to map";
    //Input Manager
    public static string RETURN_GAME = "Return game";
    //Camera
    public static string ALTER_CAMERA_VIEW = "Alter view of main camera";
    public static string ALTER_VIEW_SLIDER_VALUE = "Alter value of slider about view";
    #endregion

    #region DEBUG
    public static string DEBUG_QUIT_APPLICATION = "Quit Game!";
    public static string DEBUG_VIEW_CHANGE = "View change!";
    #endregion

    #region TAG
    public static string BUTTON_BACK_OPTION_PANEL = "OptionPanel";
    public static string BUTTON_BACK_SINGLE_GAME_PANEL = "SinglePanel";
    public static string BAG_BLOCK = "BagBlock";
    public static string ITEM_BLOCK = "ItemBlock";
    #endregion
}
