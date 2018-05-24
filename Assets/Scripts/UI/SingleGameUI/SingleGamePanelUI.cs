using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleGamePanelUI : HidenPanelUI {

    public InputField WorldNameIF; //世界名称输入框
    public Dropdown TerrainTypeDropdown; //地形类型下拉列表
    public Dropdown WorldTypeDropdown; //世界类型下拉列表
    public Dropdown SpeciesRichnesDropdown; //物种丰富度下拉列表

    /// <summary>
    /// 添加下拉列表选项
    /// </summary>
    private void AddDropdownOption() {
        TerrainTypeDropdown.AddOptions( UIInfoManager.Instance.TerrainTypeList );
        WorldTypeDropdown.AddOptions( UIInfoManager.Instance.WorldTypeList );
        SpeciesRichnesDropdown.AddOptions( UIInfoManager.Instance.SpeciesRichnesList );
    }
    /// <summary>
    /// 添加下拉列表事件
    /// </summary>
    private void AddDropdownEvents() {
        WorldNameIF.onValueChanged.AddListener( OnWorldNameChenged );
        TerrainTypeDropdown.onValueChanged.AddListener( OnTerrainTypeDropdownValueChanged );
        WorldTypeDropdown.onValueChanged.AddListener( OnWorldTypeDropdownValueChanged );
        SpeciesRichnesDropdown.onValueChanged.AddListener( OnSpeciesRichnessDropdownValueChanged );
    }

    /// <summary>
    /// WorldName值变化后
    /// </summary>
    private void OnWorldNameChenged(string _worldName) {
        WorldManager.Instance.WorldName = WorldNameIF.text;
    }
    /// <summary>
    /// TerrainTypeDropdown值变化
    /// </summary>
    private void OnTerrainTypeDropdownValueChanged(int _index) {
        WorldManager.Instance.TerrainType = UIInfoManager.Instance.TerrainTypeList[_index];
    }
    /// <summary>
    /// WorldTypeDropdown值变化
    /// </summary>
    private void OnWorldTypeDropdownValueChanged(int _index) {
        WorldManager.Instance.WorldType = UIInfoManager.Instance.WorldTypeList[_index];
    }
    /// <summary>
    /// SpeciesRichnessDropdown值变化
    /// </summary>
    private void OnSpeciesRichnessDropdownValueChanged(int _index) {
        WorldManager.Instance.SpeciesRichness = UIInfoManager.Instance.SpeciesRichnesList[_index];
    }

    private void Awake() {

        //注册消息：移入移出动画
        NoticeManager.Instance.Register( StrManager.SINGLEGAME_PANEL_AWAY_NOTICE, PlayMoveAway );
        NoticeManager.Instance.Register( StrManager.SINGLEGAME_PANEL_BACK_NOTICE, PlayMoveBack );

        AddDropdownOption();
        AddDropdownEvents();
    }



}
