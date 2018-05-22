using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BlockSixSideRenderOne : MonoBehaviour {

    #region six side parameters
    private enum BlockSide {
        FRONT,
        TOP,
        BACK,
        BOTTOM,
        LEFT,
        RIGHT
    }

    //the index of uv of cube
    private static int[][] uvIndex;
    private int[][] GetUVIndex {
        get {
            if (uvIndex == null) {
                uvIndex = new int[6][];
                InitUVIndex();
            }
            return uvIndex;
        }
    }
    private void InitUVIndex() {
        uvIndex[(int)BlockSide.FRONT] = new int[] { 0, 1, 2, 3 };
        uvIndex[(int)BlockSide.TOP] = new int[] { 8, 9, 4, 5 };
        uvIndex[(int)BlockSide.BACK] = new int[] { 7, 6, 11, 10 };
        uvIndex[(int)BlockSide.BOTTOM] = new int[] { 12, 15, 13, 14 };
        uvIndex[(int)BlockSide.LEFT] = new int[] { 20, 23, 21, 22 };
        uvIndex[(int)BlockSide.RIGHT] = new int[] { 16, 19, 17, 18 };
    }

    //the index of six side texture in one integrated texutre
    private Vector2[] sidePoints;
    private Vector2[] GetSidePoints {
        get {
            if (sidePoints == null) {
                sidePoints = new Vector2[6];
                InitSidePoints();
            }
            return sidePoints;
        }
    }
    private void InitSidePoints() {
        sidePoints[(int)BlockSide.FRONT] = new Vector2( 0, 0 );
        sidePoints[(int)BlockSide.TOP] = new Vector2( 1, 0 );
        sidePoints[(int)BlockSide.BACK] = new Vector2( 2, 0 );
        sidePoints[(int)BlockSide.BOTTOM] = new Vector2( 0, 1 );
        sidePoints[(int)BlockSide.LEFT] = new Vector2( 1, 1 );
        sidePoints[(int)BlockSide.RIGHT] = new Vector2( 2, 1 );
    }
    #endregion

    #region Mesh
    private Mesh m_mesh;
    private MeshFilter m_meshFilter;

    private Vector3[] m_vertices;
    private Vector3[] GetVertices(BlockSide blockSide) {
        // Render Range : x,y,z
        if (m_vertices == null) {
            m_vertices = new Vector3[4];
        }
        switch (blockSide) {
            case BlockSide.FRONT:
                m_vertices[0] = new Vector3( -0.5f, -0.5f, -0.5f );
                m_vertices[1] = new Vector3( -0.5f, 0.5f, -0.5f );
                m_vertices[2] = new Vector3( 0.5f, 0.5f, -0.5f );
                m_vertices[3] = new Vector3( 0.5f, -0.5f, -0.5f );
                break;
            case BlockSide.TOP:
                m_vertices[0] = new Vector3( -0.5f, 0.5f, -0.5f );
                m_vertices[1] = new Vector3( -0.5f, 0.5f, 0.5f );
                m_vertices[2] = new Vector3( 0.5f, 0.5f, 0.5f );
                m_vertices[3] = new Vector3( 0.5f, 0.5f, -0.5f );
                break;
            case BlockSide.BACK:
                m_vertices[0] = new Vector3( -0.5f, -0.5f, 0.5f );
                m_vertices[1] = new Vector3( -0.5f, 0.5f, 0.5f );
                m_vertices[2] = new Vector3( 0.5f, 0.5f, 0.5f );
                m_vertices[3] = new Vector3( 0.5f, -0.5f, 0.5f );
                break;
            case BlockSide.BOTTOM:
                m_vertices[0] = new Vector3( -0.5f, -0.5f, -0.5f );
                m_vertices[1] = new Vector3( -0.5f, -0.5f, 0.5f );
                m_vertices[2] = new Vector3( 0.5f, -0.5f, 0.5f );
                m_vertices[3] = new Vector3( 0.5f, -0.5f, -0.5f );
                break;
            case BlockSide.LEFT:
                m_vertices[0] = new Vector3( -0.5f, -0.5f, 0.5f );
                m_vertices[1] = new Vector3( -0.5f, 0.5f, 0.5f );
                m_vertices[2] = new Vector3( -0.5f, 0.5f, -0.5f );
                m_vertices[3] = new Vector3( -0.5f, -0.5f, -0.5f );
                break;
            case BlockSide.RIGHT:
                m_vertices[0] = new Vector3( 0.5f, -0.5f, -0.5f );
                m_vertices[1] = new Vector3( 0.5f, 0.5f, -0.5f );
                m_vertices[2] = new Vector3( 0.5f, 0.5f, 0.5f );
                m_vertices[3] = new Vector3( 0.5f, -0.5f, 0.5f );
                break;
            default:
                break;
        }
        return m_vertices;
    }

    private int[] m_triangles;
    private int[] GetTriangles {
        get {
            // render order in triangle mesh
            if (m_triangles == null) {
                m_triangles = new int[6];
                m_triangles[0] = 0;
                m_triangles[1] = 1;
                m_triangles[2] = 2;
                m_triangles[3] = 0;
                m_triangles[4] = 2;
                m_triangles[5] = 3;
            }
            return m_triangles;
        }
    }

    private void GetMesh() {
        m_meshFilter = GetComponent<MeshFilter>();
        if (!m_meshFilter) {
            DebugManager.LogError( StrManager.MESH_FILTER_FAIL_FOUND_ERROR );
            return;
        }

        Mesh meshCopy = Mesh.Instantiate( m_meshFilter.sharedMesh ) as Mesh;
        m_mesh = m_meshFilter.mesh = meshCopy;

        if (!m_mesh) {
            DebugManager.LogError( StrManager.MESH_FAIL_GET_ERROR );
        }

        if (m_mesh.uv.Length != 24) {
            DebugManager.LogError( StrManager.MESH_NOT_CUBE_ERROR );
            return;
        }
    }

    /// <summary>
    /// Update the mesh
    /// </summary>
    private void UpdateMesh() {
        if (!m_mesh) { //Only excute once
            GetMesh();

            RenderSide( BlockSide.TOP );
            //RenderSide( BlockSide.BACK );
            //RenderSide( BlockSide.BOTTOM );
            //RenderSide( BlockSide.FRONT );
            //RenderSide( BlockSide.LEFT );
            //RenderSide( BlockSide.RIGHT );

            // 开始重新渲染
            m_meshFilter.mesh.RecalculateNormals();
        }
    }

    private void RenderSide(BlockSide blockSide) {
        // 渲染参数设定
        m_meshFilter.mesh.Clear();
        m_meshFilter.mesh.subMeshCount = 1;   //贴图数量只设定为一张
        m_meshFilter.mesh.vertices = GetVertices( blockSide );  //设置顶点信息
        m_meshFilter.mesh.SetTriangles( GetTriangles, 0 );   //设置三角网格
        m_meshFilter.mesh.uv = GetNewUV( GetSidePoints[(int)blockSide] ); //设置uv，贴图对应的顶点
    }

    /// <summary>
    /// Get the new position of four angle of the each small texture in the integrated texture
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    private Vector2[] GetNewUV(Vector2 point) {
        float pointX = point.x;
        float pointY = point.y;
        Vector2[] newUV = new Vector2[4];
        newUV[0] = new Vector2( pointX / 3.0f, pointY / 3.0f );
        newUV[1] = new Vector2( pointX / 3.0f, ( pointY + 1 ) / 3.0f );
        newUV[2] = new Vector2( ( pointX + 1 ) / 3.0f, ( pointY + 1 ) / 3.0f );
        newUV[3] = new Vector2( ( pointX + 1 ) / 3.0f, pointY / 3.0f );
        return newUV;
    }
    #endregion

    void Update() {
#if UNITY_EDITOR
        UpdateMesh();
#endif 
    }

}
