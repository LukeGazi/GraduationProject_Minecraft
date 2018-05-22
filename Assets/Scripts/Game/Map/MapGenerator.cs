using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 方块类型
/// </summary>
public enum BlockType {
    GRASS,
    STONE,
    TREE,
    DIRT,
}

/// <summary>
/// 地图水平方向
/// </summary>
public enum MapHorizontalDirection {
    FRONT,
    RIGHT,
    BACK,
    LEFT,
    FRONT_AND_RIGHT,
    FRONT_AND_LEFT,
    BACK_AND_RIGHT,
    BACK_AND_LEFT,
    None,
}

/// <summary>
/// 地图生成方式类型
/// </summary>
public enum MapGenerateType {
    FLAT,
    RANDOM,
    PERLIN,
}

public class MapGenerator : MonoBehaviour {

    public Transform PlayerTrans;

    #region 地图参数信息
    public GameObject[] Block; //方块预制体数组
    private int m_mapWidth = 150; //地图宽度
    private int m_mapHeight = 10; //地图高度
    public MapGenerateType GenerateType = MapGenerateType.FLAT; //地图生成类型，默认平坦类型

    private float m_seedTime = 100; //种子信息倍数 
    private float? m_seedX; //种子X
    private float GetSeedX {
        get {
            if (m_seedX == null) {
                m_seedX = Random.value * m_seedTime;
            }
            return (float)m_seedX;
        }
    }
    private float? m_seedZ; //种子Z
    private float GetSeedZ {
        get {
            if (m_seedZ == null) {
                m_seedZ = Random.value * m_seedTime;
            }
            return (float)m_seedZ;
        }
    }

    [SerializeField]
    private int m_relief = 20; //地图平滑度

    public delegate void SetBlockHeightYDelegate(Transform _transform);
    private SetBlockHeightYDelegate m_setBlockHeightDelegate;
    #endregion

    // Use this for initialization
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        InitiateMap();
    }

    /// <summary>
    /// 初始化地图
    /// </summary>
    private void InitiateMap() {
        switch (GenerateType) {
            case MapGenerateType.FLAT:
                m_setBlockHeightDelegate = null;
                break;
            case MapGenerateType.RANDOM:
                m_setBlockHeightDelegate = SetRandomY;
                break;
            case MapGenerateType.PERLIN:
                m_setBlockHeightDelegate = SetPerlinY;
                break;
            default:
                break;
        }
        StartCoroutine( GenerateMap() );
    }

    /// <summary>
    /// 生成地图
    /// </summary>
    private IEnumerator GenerateMap() {
        int generateCount = m_mapWidth / 2;
        for (int row = 0; row < generateCount; row++) {
            StartCoroutine( GenerateMapLineBF( m_mapWidth, row ) );
            StartCoroutine( GenerateMapLineBF( m_mapWidth, -row ) );
            yield return null;
        }
        yield return null;
        AferGenerateMap();
    }

    /// <summary>
    /// 生成一排Block
    /// </summary>
    /// <param name="_mapHorizontalDirection">水平方向</param>
    /// <param name="_count">生成数量</param>
    public void GenerateMapOneLine(MapHorizontalDirection _mapHorizontalDirection, int _count) {
        switch (_mapHorizontalDirection) {
            case MapHorizontalDirection.FRONT:
                StartCoroutine( GenerateMapLineBF( _count, MapManager.MapGenerateBorder.Front ) );
                break;
            case MapHorizontalDirection.RIGHT:
                StartCoroutine( GenerateMapLineRL( _count, MapManager.MapGenerateBorder.Right ) );
                break;
            case MapHorizontalDirection.BACK:
                StartCoroutine( GenerateMapLineBF( _count, MapManager.MapGenerateBorder.Back ) );
                break;
            case MapHorizontalDirection.LEFT:
                StartCoroutine( GenerateMapLineRL( _count, MapManager.MapGenerateBorder.Left ) );
                break;
            case MapHorizontalDirection.FRONT_AND_RIGHT:
                StartCoroutine( GenerateMapLineBF( _count, MapManager.MapGenerateBorder.Front ) );
                StartCoroutine( GenerateMapLineRL( _count, MapManager.MapGenerateBorder.Right ) );
                break;
            case MapHorizontalDirection.FRONT_AND_LEFT:
                StartCoroutine( GenerateMapLineBF( _count, MapManager.MapGenerateBorder.Front ) );
                StartCoroutine( GenerateMapLineRL( _count, MapManager.MapGenerateBorder.Left ) );
                break;
            case MapHorizontalDirection.BACK_AND_RIGHT:
                StartCoroutine( GenerateMapLineBF( _count, MapManager.MapGenerateBorder.Back ) );
                StartCoroutine( GenerateMapLineRL( _count, MapManager.MapGenerateBorder.Right ) );
                break;
            case MapHorizontalDirection.BACK_AND_LEFT:
                StartCoroutine( GenerateMapLineBF( _count, MapManager.MapGenerateBorder.Back ) );
                StartCoroutine( GenerateMapLineRL( _count, MapManager.MapGenerateBorder.Left ) );
                break;
            case MapHorizontalDirection.None:
                return;
            default:
                break;
        }
    }

    //地图前后行生成
    private IEnumerator GenerateMapLineBF(int _count, int _mapZ) {
        float startX = PlayerTrans.position.x; //起始地图X坐标
        //从中间开始生成
        for (int i = 0; i < _count / 2; i++) {
            GenerateOnBlock( (int)( startX + i ), _mapZ );
            GenerateOnBlock( (int)( startX - i ), _mapZ );
            yield return null;
        }
        yield return null;
    }

    //地图左右行生成
    private IEnumerator GenerateMapLineRL(int _count, int _mapX) {
        float startZ = PlayerTrans.position.z; //起始地图X坐标
        //从中间开始生成
        for (int i = 0; i < _count / 2; i++) {
            GenerateOnBlock( _mapX, (int)( startZ + i ) );
            GenerateOnBlock( _mapX, (int)( startZ - i ) );
            yield return null;
        }
        yield return null;
    }

    /// <summary>
    /// 生成一个方块
    /// </summary>
    /// <param name="_mapX"></param>
    /// <param name="_mapZ"></param>
    private void GenerateOnBlock(int _mapX, int _mapZ) {
        if (!MapManager.IsMapped( _mapX, _mapZ )) {
            MapManager.AddMappedPoint( _mapX, _mapZ );
            GameObject block = GameObject.Instantiate( Block[0], transform );
            block.transform.position = new Vector3( _mapX, 0, _mapZ );

            if (m_setBlockHeightDelegate != null) {
                m_setBlockHeightDelegate( block.transform );
            }
        }
    }

    /// <summary>
    /// 加载地图协程完成后的处理操作
    /// </summary>
    private void AferGenerateMap() {
        PlayerTrans.position = Vector3.up * 10; //玩家位置归零

        MapChecker mapChecker = GetComponent<MapChecker>();
        mapChecker.StartCheck();
    }

    /// <summary>
    /// 随机赋值方块高度
    /// </summary>
    /// <param name="_blockTrans"></param>
    private void SetRandomY(Transform _blockTrans) {
        float y = Random.Range( 0, m_mapHeight );
        _blockTrans.localPosition += Vector3.up * y;
    }

    /// <summary>
    /// 依据柏林噪声算法赋值方块高度
    /// </summary>
    private void SetPerlinY(Transform _blockTrans) {
        float xSample = ( _blockTrans.localPosition.x + GetSeedX ) / m_relief;
        float zSample = ( _blockTrans.localPosition.z + GetSeedZ ) / m_relief;
        //float noise = Mathf.PerlinNoise( xSample, zSample );
        float noise = (float)PerlinNoiseGenerator.PerlinNoise( xSample, zSample );
        float y = m_mapHeight * noise;
        y = Mathf.Round( y ); //取整
        _blockTrans.localPosition += Vector3.up * y;
    }

}
