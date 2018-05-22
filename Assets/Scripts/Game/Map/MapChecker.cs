using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChecker : MonoBehaviour {

    public Transform PlayerTrans; //玩家
    private int m_checkDistance = 50; //玩家探测范围
    private int m_loadCount = 100; //预加载方块一行数量
    private Player player;

    MapGenerator mapGenerator;

    /// <summary>
    /// 检测地图是否需要再生成并返回需要再生成的方向
    /// </summary>
    /// <returns></returns>
    private MapHorizontalDirection MapGenerateDirection() {
        //玩家方向判断需要生成地图的方向
        MapHorizontalDirection playerDirection = player.GetPlayerDirection();

        MapManager.ModifyGenerateRange( playerDirection, PlayerTrans.position, m_checkDistance );

        return playerDirection;
    }

    private void Start() {
        player = PlayerTrans.GetComponent<Player>();
        mapGenerator = GetComponent<MapGenerator>();
    }

    public void StartCheck() {
        StartCoroutine( CheckAndGenerateMap() );
    }

    private IEnumerator CheckAndGenerateMap() {
        while (Application.isPlaying) {

            if (MapGenerateDirection() != MapHorizontalDirection.None) {
                mapGenerator.GenerateMapOneLine( MapGenerateDirection(), m_loadCount );
            }

            yield return null;
        }
        yield return null;
    }

}
