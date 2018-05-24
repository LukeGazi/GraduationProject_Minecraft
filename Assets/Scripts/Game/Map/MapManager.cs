using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager {

    public struct GenerateBorder {
        public int Front; //地图Z正半轴
        public int Right; //地图X正半轴
        public int Back; //地图Z负半轴
        public int Left; //地图X负半轴
    }

    public static GenerateBorder MapGenerateBorder;

    /// <summary>
    /// 记录探测距离
    /// </summary>
    /// <param name="_mapHorizontalDirection">方向</param>
    /// <param name="_distance">增加距离</param>
    public static void ModifyDetectRange(MapHorizontalDirection _mapHorizontalDirection, Vector3 _position, int _distance) {
        switch (_mapHorizontalDirection) {
            case MapHorizontalDirection.FRONT:
                MapGenerateBorder.Front = (int)_position.z + _distance;
                break;
            case MapHorizontalDirection.RIGHT:
                MapGenerateBorder.Right = (int)_position.x + _distance;
                break;
            case MapHorizontalDirection.BACK:
                MapGenerateBorder.Back = (int)_position.z - _distance;
                break;
            case MapHorizontalDirection.LEFT:
                MapGenerateBorder.Left = (int)_position.x - _distance;
                break;
            case MapHorizontalDirection.FRONT_AND_RIGHT:
                MapGenerateBorder.Front = (int)_position.z + _distance;
                MapGenerateBorder.Right = (int)_position.x + _distance;
                break;
            case MapHorizontalDirection.FRONT_AND_LEFT:
                MapGenerateBorder.Front = (int)_position.z + _distance;
                MapGenerateBorder.Left = (int)_position.x - _distance;
                break;
            case MapHorizontalDirection.BACK_AND_RIGHT:
                MapGenerateBorder.Back = (int)_position.z - _distance;
                MapGenerateBorder.Right = (int)_position.x + _distance;
                break;
            case MapHorizontalDirection.BACK_AND_LEFT:
                MapGenerateBorder.Back = (int)_position.z - _distance;
                MapGenerateBorder.Left = (int)_position.x - _distance;
                break;
            case MapHorizontalDirection.None:
                break;
            default:
                break;
        }
    }


    #region 地图生成点存储，四个象限
    public enum Quadrant {
        FIRST,
        SECOND,
        THRID,
        FORTH,
        FAULT,
    }

    private const int DEFAULT_LENGTH = 4096; //默认长度
    private static List<int[,]> m_mapPointList; //4个象限
    private static List<int> m_mapPointCountList; //4个象限的mapPoint

    /// <summary>
    /// 通过象限获取对应的mapPoint
    /// </summary>
    /// <param name="_quadrant"></param>
    /// <returns></returns>
    private static int[,] GetMapPointMatrix(Quadrant _quadrant) {
        int index = (int)_quadrant;

        if (m_mapPointList == null) {
            int[,] mapPointOne = new int[DEFAULT_LENGTH, DEFAULT_LENGTH];
            int[,] mapPointTwo = new int[DEFAULT_LENGTH, DEFAULT_LENGTH];
            int[,] mapPointThree = new int[DEFAULT_LENGTH, DEFAULT_LENGTH];
            int[,] mapPointFour = new int[DEFAULT_LENGTH, DEFAULT_LENGTH];

            m_mapPointList = new List<int[,]>();
            m_mapPointList.Add( mapPointOne );
            m_mapPointList.Add( mapPointTwo );
            m_mapPointList.Add( mapPointThree );
            m_mapPointList.Add( mapPointFour );

            m_mapPointCountList = new List<int>();
            m_mapPointCountList.Add( DEFAULT_LENGTH );
            m_mapPointCountList.Add( DEFAULT_LENGTH );
            m_mapPointCountList.Add( DEFAULT_LENGTH );
            m_mapPointCountList.Add( DEFAULT_LENGTH );
        }

        return m_mapPointList[index];
    }

    /// <summary>
    /// 根据坐标添加mapPoint
    /// </summary>
    /// <param name="_mapX"></param>
    /// <param name="_mapZ"></param>
    public static void AddMappedPoint(int _mapX, int _mapZ) {
        Quadrant quadrant = GetQuadrant( ref _mapX, ref _mapZ );
        if (quadrant == Quadrant.FAULT) {
            DebugManager.LogError( StrManager.MAP_POINTED_QUADRANT_ERROR );
            return;
        }

        //赋值1
        m_mapPointList[(int)quadrant][_mapX, _mapZ] = 1;
    }

    /// <summary>
    /// 根据坐标判断是都已经生成过
    /// </summary>
    /// <param name="_mapX"></param>
    /// <param name="_mapZ"></param>
    /// <returns></returns>
    public static bool IsMapped(int _mapX, int _mapZ) {
        Quadrant quadrant = GetQuadrant( ref _mapX, ref _mapZ );
        if (quadrant == Quadrant.FAULT) {
            DebugManager.LogError( StrManager.MAP_POINTED_QUADRANT_ERROR );
            return true;
        }
        int[,] map = GetMapPointMatrix( quadrant );
        ////扩容
        //int count = m_mapPointCountList[(int)quadrant];
        //if (_mapX >= count || _mapZ >= count) {
        //    int[,] copyMapPoint = new int[count * 2, count * 2];
        //    m_mapPointList[(int)quadrant].CopyTo( copyMapPoint, 0 );
        //    copyMapPoint = m_mapPointList[(int)quadrant];
        //    m_mapPointList[(int)quadrant] = new int[count * 2, count * 2];
        //    m_mapPointList[(int)quadrant] = copyMapPoint;
        //    Debug.Log( copyMapPoint.Length );
        //    Debug.Log( m_mapPointList[(int)quadrant].Length );
        //    m_mapPointCountList[(int)quadrant] = count * 2;
        //}
        return map[_mapX, _mapZ] == 1;
    }
    /// <summary>
    /// 获取象限
    /// </summary>
    /// <param name="_mapX"></param>
    /// <param name="_mapZ"></param>
    /// <returns></returns>
    public static Quadrant GetQuadrant(ref int _mapX, ref int _mapZ) {
        if (_mapX >= 0 && _mapZ > 0) {
            return Quadrant.FIRST;
        }
        if (_mapX < 0 && _mapZ >= 0) {
            _mapX *= -1;
            return Quadrant.SECOND;
        }
        if (_mapX <= 0 && _mapZ < 0) {
            _mapX *= -1;
            _mapZ *= -1;
            return Quadrant.THRID;
        }
        if (_mapX > 0 && _mapZ <= 0) {
            _mapZ *= -1;
            return Quadrant.FORTH;
        }
        if (_mapX == 0 && _mapZ == 0) {
            return Quadrant.FIRST;
        }
        return Quadrant.FAULT;
    }

    #endregion
}
