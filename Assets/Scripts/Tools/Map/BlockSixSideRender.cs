using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BlockSixSideRender : MonoBehaviour {

    #region Mesh
    private Mesh m_mesh;
    private MeshFilter m_meshFilter;

    /// <summary>
    /// 获取存储顶点信息，相对坐标，为渲染的顶点
    /// </summary>
    private Vector3[] m_vertices;
    private Vector3[] GetVertices {
        get {

            if (m_vertices == null) {
                List<Vector3> tempVerticesList = new List<Vector3>( 32 ) {
                    //FRONT:
                    new Vector3( -0.5f, -0.5f, -0.5f ),
                    new Vector3( -0.5f, 0.5f, -0.5f ),
                    new Vector3( 0.5f, 0.5f, -0.5f ),
                    new Vector3( 0.5f, -0.5f, -0.5f ),

                    //TOP:
                    new Vector3( -0.5f, 0.5f, -0.5f ),
                    new Vector3( -0.5f, 0.5f, 0.5f ),
                    new Vector3( 0.5f, 0.5f, 0.5f ),
                    new Vector3( 0.5f, 0.5f, -0.5f ),

                    //BACK:
                    new Vector3( 0.5f, -0.5f, 0.5f ),
                    new Vector3( 0.5f, 0.5f, 0.5f ),
                    new Vector3( -0.5f, 0.5f, 0.5f ),
                    new Vector3( -0.5f, -0.5f, 0.5f ),

                    //BOTTOM:
                    new Vector3( 0.5f, -0.5f, -0.5f ),
                    new Vector3( 0.5f, -0.5f, 0.5f ),
                    new Vector3( -0.5f, -0.5f, 0.5f ),
                    new Vector3( -0.5f, -0.5f, -0.5f ),

                    //LEFT:
                    new Vector3( -0.5f, -0.5f, 0.5f ),
                    new Vector3( -0.5f, 0.5f, 0.5f ),
                    new Vector3( -0.5f, 0.5f, -0.5f ),
                    new Vector3( -0.5f, -0.5f, -0.5f ),

                    //RIGHT:
                    new Vector3( 0.5f, -0.5f, -0.5f ),
                    new Vector3( 0.5f, 0.5f, -0.5f ),
                    new Vector3( 0.5f, 0.5f, 0.5f ),
                    new Vector3( 0.5f, -0.5f, 0.5f ),

                };
                m_vertices = tempVerticesList.ToArray();
            }
            return m_vertices;
        }
    }

    /// <summary>
    /// 设置渲染三角形顶点顺序，数字坐标即为设置顶点索引对应
    /// </summary>
    private int[] m_triangles;
    private int[] GetTriangles {
        get {
            if (m_triangles == null) {
                List<int> tempTrianglesList = new List<int>( 36 ) {
                    //FRONT
                    0,1,2, 0,2,3,
                    //TOP
                    4,5,6, 4,6,7,
                    //BACK
                    8,9,10, 8,10,11,
                    //BOTTOM
                    12,13,14, 12,14,15,
                    //LEFT
                    16,17,18, 16,18,19,
                    //RIGHT
                    20,21,22, 20,22,23
                };
                m_triangles = tempTrianglesList.ToArray();
            }
            return m_triangles;
        }
    }

    /// <summary>
    /// 获取物体Mesh
    /// </summary>
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
    /// 更新Mesh
    /// </summary>
    private void UpdateMesh() {
        //更新一次，减少资源消耗
        if (!m_mesh) { 
            GetMesh();

            RenderBlock();

            // 开始重新渲染
            m_meshFilter.mesh.RecalculateNormals();
        }
    }

    /// <summary>
    /// 渲染方块，设定好前面所设置的参数
    /// </summary>
    private void RenderBlock() {
        // 渲染参数设定
        m_meshFilter.mesh.Clear();
        m_meshFilter.mesh.vertices = GetVertices;  //设置顶点信息
        m_meshFilter.mesh.SetTriangles( GetTriangles, 0 );   //设置三角网格
        m_meshFilter.mesh.uv = GetNewUV; //设置uv，贴图对应的顶点
    }

    /// <summary>
    /// 获取自定义贴图中没面所对应贴图的坐标点
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    private Vector2[] m_newUV;
    private Vector2[] GetNewUV {
        get {
            if (m_newUV == null) {
                List<Vector2> tempNewUVList = new List<Vector2>( 24 );
                float pointX = 0;
                float pointY = 0;
                for (int row = 0; row < 2; row++) {
                    pointY = row;
                    for (int colum = 0; colum < 3; colum++) {
                        pointX = colum;
                        tempNewUVList.Add( new Vector2( pointX / 3.0f, pointY / 3.0f ) );
                        tempNewUVList.Add( new Vector2( pointX / 3.0f, ( pointY + 1 ) / 3.0f ) );
                        tempNewUVList.Add( new Vector2( ( pointX + 1 ) / 3.0f, ( pointY + 1 ) / 3.0f ) );
                        tempNewUVList.Add( new Vector2( ( pointX + 1 ) / 3.0f, pointY / 3.0f ) );
                    }
                }
                m_newUV = tempNewUVList.ToArray();
            }
            return m_newUV;
        }
    }
    #endregion

    void Update() {
#if UNITY_EDITOR
        UpdateMesh();
#endif
    }

}
