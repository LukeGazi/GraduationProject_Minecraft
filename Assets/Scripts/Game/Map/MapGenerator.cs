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
    NONE,
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
    private GameObject GetBlock {
        get {
            switch (WorldManager.Instance.WorldType) {
                case "GRASS":
                    return Block[0];
                case "STONE":
                    return Block[1];
                case "DIRT":
                    return Block[2];
                case "MIXED":
                    return Block[Random.Range( 0, 3 )];
                default:
                    return Block[0];
            }
        }
    }
    private int m_mapWidth = 150; //地图宽度
    [SerializeField]
    //地图高度
    private int GetMapHeight {
        get {
            switch (WorldManager.Instance.TerrainType) {
                case "FLAT":
                    return 0;
                case "HILL":
                    return 15;
                case "MOUNTAIN":
                    return 35;
                case "RANDOM":
                    return 15;
                default:
                    return 0;
            }
        }
    }
    //地图生成类型，默认平坦类型
    public MapGenerateType GetGenerateType {
        get {
            switch (WorldManager.Instance.TerrainType) {
                case "FLAT":
                    return MapGenerateType.FLAT;
                case "HILL":
                case "MOUNTAIN":
                    return MapGenerateType.PERLIN;
                case "RANDOM":
                    return MapGenerateType.RANDOM;
                default:
                    return MapGenerateType.FLAT;
            }
        }
    }

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
    private int m_relief = 10; //地图平滑度

    //设置高度委托
    public delegate int SetBlockHeightYDelegate(Transform _transform);
    private SetBlockHeightYDelegate m_setBlockHeightDelegate;
    #endregion

    // Use this for initialization
    private void Start() {
        InitiateMap();
        CursorManager.Instance.LockCursor();
    }

    /// <summary>
    /// 初始化地图
    /// </summary>
    private void InitiateMap() {
        switch (GetGenerateType) {
            case MapGenerateType.FLAT:
                m_setBlockHeightDelegate = null;
                break;
            case MapGenerateType.RANDOM:
                m_setBlockHeightDelegate = GetRandomY;
                break;
            case MapGenerateType.PERLIN:
                m_setBlockHeightDelegate = GetPerlinY;
                break;
            default:
                break;
        }
        StartCoroutine( InitOrignalMap() );
    }

    /// <summary>
    /// 初始化最初地图
    /// </summary>
    private IEnumerator InitOrignalMap() {
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
                StartCoroutine( GenerateMapRegionFront( _count ) );
                break;
            case MapHorizontalDirection.RIGHT:
                StartCoroutine( GenerateMapRegionRight( _count ) );
                break;
            case MapHorizontalDirection.BACK:
                StartCoroutine( GenerateMapRegionBack( _count ) );
                break;
            case MapHorizontalDirection.LEFT:
                StartCoroutine( GenerateMapRegionLeft( _count ) );
                break;
            case MapHorizontalDirection.FRONT_AND_RIGHT:
                StartCoroutine( GenerateMapRegionFront( _count ) );
                StartCoroutine( GenerateMapRegionRight( _count ) );
                break;
            case MapHorizontalDirection.FRONT_AND_LEFT:
                StartCoroutine( GenerateMapRegionFront( _count ) );
                StartCoroutine( GenerateMapRegionLeft( _count ) );
                break;
            case MapHorizontalDirection.BACK_AND_RIGHT:
                StartCoroutine( GenerateMapRegionBack( _count ) );
                StartCoroutine( GenerateMapRegionRight( _count ) );
                break;
            case MapHorizontalDirection.BACK_AND_LEFT:
                StartCoroutine( GenerateMapRegionBack( _count ) );
                StartCoroutine( GenerateMapRegionLeft( _count ) );
                break;
            case MapHorizontalDirection.None:
                return;
            default:
                break;
        }
    }

    /// <summary>
    /// 协程动态生成玩家前方地图块
    /// </summary>
    /// <param name="_count"></param>
    /// <returns></returns>
    private IEnumerator GenerateMapRegionFront(int _count) {
        for (int i = (int)PlayerTrans.position.z; i < MapManager.MapGenerateBorder.Front; i++) {
            StartCoroutine( GenerateMapLineBF( _count, i ) );
            yield return null;
        }
        yield return null;
    }

    /// <summary>
    /// 协程动态生成玩家后方地图块
    /// </summary>
    /// <param name="_count"></param>
    /// <returns></returns>
    private IEnumerator GenerateMapRegionBack(int _count) {
        //根据玩家的位置信息，从玩家坐标开始往外探测生成
        for (int i = (int)PlayerTrans.position.z; i > MapManager.MapGenerateBorder.Back; i--) {
            StartCoroutine( GenerateMapLineBF( _count, i ) );
            yield return null;
        }
        yield return null;
    }

    /// <summary>
    /// 协程动态生成玩家右方地图块
    /// </summary>
    /// <param name="_count"></param>
    /// <returns></returns>
    private IEnumerator GenerateMapRegionRight(int _count) {
        for (int i = (int)PlayerTrans.position.x; i < MapManager.MapGenerateBorder.Right; i++) {
            StartCoroutine( GenerateMapLineRL( _count, i ) );
            yield return null;
        }
        yield return null;
    }

    /// <summary>
    /// 协程动态生成玩家左方地图块
    /// </summary>
    /// <param name="_count"></param>
    /// <returns></returns>
    private IEnumerator GenerateMapRegionLeft(int _count) {
        for (int i = (int)PlayerTrans.position.x; i > MapManager.MapGenerateBorder.Left; i--) {
            StartCoroutine( GenerateMapLineRL( _count, i ) );
            yield return null;
        }
        yield return null;
    }


    /// <summary>
    /// 协程生成前后两个方向的方块
    /// </summary>
    /// <param name="_count"></param>
    /// <param name="_mapZ"></param>
    /// <returns></returns>
    private IEnumerator GenerateMapLineBF(int _count, int _mapZ) {
        float startX = PlayerTrans.position.x; //起始地图X坐标
        //从中间开始生成
        for (int i = 0; i < _count / 2; i++) {
            GenerateOneColumBlock( (int)( startX + i ), _mapZ );
            GenerateOneColumBlock( (int)( startX - i ), _mapZ );
            yield return null;
        }
        yield return null;
    }

    /// <summary>
    /// 协程生成左右两个方向的方块
    /// </summary>
    /// <param name="_count"></param>
    /// <param name="_mapZ"></param>
    /// <returns></returns>
    private IEnumerator GenerateMapLineRL(int _count, int _mapX) {
        float startZ = PlayerTrans.position.z; //起始地图X坐标
        //从中间开始生成
        for (int i = 0; i < _count / 2; i++) {
            GenerateOneColumBlock( _mapX, (int)( startZ + i ) );
            GenerateOneColumBlock( _mapX, (int)( startZ - i ) );
            yield return null;
        }
        yield return null;
    }

    /// <summary>
    /// 生成一列方块
    /// </summary>
    /// <param name="_mapX"></param>
    /// <param name="_mapZ"></param>
    private void GenerateOneColumBlock(int _mapX, int _mapZ) {
        if (!MapManager.IsMapped( _mapX, _mapZ )) {
            MapManager.AddMappedPoint( _mapX, _mapZ );
            GameObject block = GameObject.Instantiate( GetBlock, transform );
            block.transform.position = new Vector3( _mapX, 0, _mapZ );


            int height = 0;
            if (m_setBlockHeightDelegate != null) {
                height = m_setBlockHeightDelegate( block.transform );
                if (height > 0) {
                    for (int h = 0; h < height; h++) {
                        block = GameObject.Instantiate( GetBlock, transform );
                        block.transform.position = new Vector3( _mapX, h + 1, _mapZ );
                    }
                } else {
                    for (int h = 0; h > height; h--) {
                        block = GameObject.Instantiate( GetBlock, transform );
                        block.transform.position = new Vector3( _mapX, h, _mapZ );
                    }
                }
            }
        }
    }

    /// <summary>
    /// 加载地图协程完成后的处理操作
    /// </summary>
    private void AferGenerateMap() {
        PlayerTrans.position = Vector3.up * 10; //玩家位置归零

        NoticeManager.Instance.SendNotice( StrManager.START_CHECK_NOTICE );
    }

    /// <summary>
    /// 随机赋值方块高度
    /// </summary>
    /// <param name="_blockTrans"></param>
    private int GetRandomY(Transform _blockTrans) {
        float y = Random.Range( 0, GetMapHeight );
        return (int)y;
        //_blockTrans.localPosition += Vector3.up * y;
    }

    /// <summary>
    /// 依据柏林噪声算法赋值方块高度
    /// </summary>
    private int GetPerlinY(Transform _blockTrans) {
        float xSample = ( _blockTrans.localPosition.x + GetSeedX ) / m_relief;
        float zSample = ( _blockTrans.localPosition.z + GetSeedZ ) / m_relief;
        //float noise = Mathf.PerlinNoise( xSample, zSample );
        float noise = (float)PerlinNoiseGenerator.PerlinNoise( xSample, zSample );
        float y = GetMapHeight * noise;
        y = Mathf.Round( y ); //取整
        return (int)y;
        //_blockTrans.localPosition += Vector3.up * y;
    }

}
