using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    /// <summary>
    /// 地图生成方式类型
    /// </summary>
    public enum MapGenerateType {
        FLAT,
        RANDOM,
        PERLIN,
    }

    #region 地图参数信息
    public GameObject[] Block; //方块预制体数组
    [SerializeField]
    private int m_mapWidth = 100; //地图宽度
    [SerializeField]
    private int m_mapHeight = 5; //地图高度
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
    private float? m_seedY; //种子Y
    private float GetSeedY {
        get {
            if (m_seedY == null) {
                m_seedY = Random.value * m_seedTime;
            }
            return (float)m_seedY;
        }
    }

    [SerializeField]
    private int m_relief = 20; //地图平滑度

    private delegate void GenderateMapDelegate(Transform _transform);
    #endregion

    // Use this for initialization
    void Start() {
        InitiateMap();
    }

    /// <summary>
    /// 初始化地图
    /// </summary>
    private void InitiateMap() {
        switch (GenerateType) {
            case MapGenerateType.FLAT:
                GenerateFlatMap();
                break;
            case MapGenerateType.RANDOM:
                GenerateRandomMap();
                break;
            case MapGenerateType.PERLIN:
                GeneratePerlinMap();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 生成平坦地图
    /// </summary>
    private void GenerateFlatMap() {
        GenerateMap( null );
    }

    /// <summary>
    /// 生成随机地图
    /// </summary>
    private void GenerateRandomMap() {
        GenerateMap( SetRandomY );
    }

    /// <summary>
    /// 利用柏林噪声生成地图
    /// </summary>
    private void GeneratePerlinMap() {
        GenerateMap( SetPerlinY );
    }

    /// <summary>
    /// 生成地图
    /// </summary>
    private void GenerateMap(GenderateMapDelegate _genderateMapDelegate) {
        //生成基层平面地图
        for (int row = 0; row < m_mapWidth; row++) {
            for (int cloum = 0; cloum < m_mapWidth; cloum++) {
                GameObject block = GameObject.Instantiate( Block[0], transform );
                block.transform.position = new Vector3( -m_mapWidth / 2 + row, 0, -m_mapWidth / 2 + cloum );

                if (_genderateMapDelegate != null) {
                    _genderateMapDelegate( block.transform );
                }
            }
        }
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
        float zSample = ( _blockTrans.localPosition.z + GetSeedY ) / m_relief;
        //float noise = Mathf.PerlinNoise( xSample, zSample );
        float noise = (float)PerlinNoiseGenerator.PerlinNoise( xSample, zSample );
        float y = m_mapHeight * noise;
        y = Mathf.Round( y ); //取整
        _blockTrans.localPosition += Vector3.up * y;
    }

    #region 调试使用
    /// <summary>
    /// inspector面板修改后自动调用
    /// </summary>
    private void OnValidate() {
        if (!Application.isPlaying) {
            return;
        }
        ModifyBlockY();
    }

    /// <summary>
    /// 调整方块高度
    /// </summary>
    private void ModifyBlockY() {
        switch (GenerateType) {
            case MapGenerateType.FLAT:
                foreach (Transform blockTrans in transform) {
                    blockTrans.transform.localPosition -= Vector3.up * blockTrans.transform.localPosition.y;
                }
                break;
            case MapGenerateType.RANDOM:
                foreach (Transform blockTrans in transform) {
                    SetRandomY( blockTrans );
                }
                break;
            case MapGenerateType.PERLIN:
                foreach (Transform child in transform) {
                    SetPerlinY( child );
                }
                break;
            default:
                break;
        }
    }
    #endregion

}
