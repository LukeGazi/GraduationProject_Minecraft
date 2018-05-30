using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyValueParamList<K, V, P> {
    //键名列表
    private List<K> m_keys;
    //值列表
    private List<V> m_values;
    //参数列表
    private List<P> m_params;

    /// <summary>
    /// 初始化
    /// </summary>
    public KeyValueParamList() {
        m_keys = new List<K>();
        m_values = new List<V>();
        m_params = new List<P>();
    }

    /// <summary>
    /// 添加键值参
    /// </summary>
    /// <param name="_key"></param>
    /// <param name="_target"></param>
    /// <returns></returns>
    public V Add(K _key, V _target, P _pram) {
        V result = default( V );
        if (m_keys.IndexOf( _key ) == -1) {
            m_keys.Add( _key );
            m_values.Add( _target );
            m_params.Add( _pram );
            result = _target;
        } else {
            int index = m_keys.IndexOf( _key );
            result = m_values[index];
            m_values[index] = _target;
            m_params[index] = _pram;
        }
        return result;
    }

    /// <summary>
    /// 移出键值参
    /// </summary>
    /// <param name="_key"></param>
    /// <returns></returns>
    public V Remove(K _key) {
        V result = default( V );
        if (m_keys.IndexOf( _key ) == -1) {
            return default( V );
        } else {
            int index = m_keys.IndexOf( _key );
            result = m_values[index];
            m_keys.RemoveAt( index );
            m_values.RemoveAt( index );
            m_params.RemoveAt( index );
        }
        return result;
    }

    /// <summary>
    /// 获取键值参
    /// </summary>
    /// <param name="_key"></param>
    /// <returns></returns>
    public V GetValue(K _key) {
        V result = default( V );
        int index = m_keys.IndexOf( _key );
        if (index != -1) {
            result = m_values[index];
        }
        return result;
    }

    public bool ContainsKey(K _key) {
        return m_keys.Contains( _key );
    }

    /// <summary>
    /// 获取键值参
    /// </summary>
    /// <param name="_key"></param>
    /// <returns></returns>
    public P GetParam(K _key) {
        P result = default( P );
        int index = m_keys.IndexOf( _key );
        if (index != -1) {
            result = m_params[index];
        }
        return result;
    }

    /// <summary>
    /// 索引器
    /// </summary>
    /// <param name="_key"></param>
    /// <returns></returns>
    public V this[K _key] {
        get {
            return GetValue( _key );
        }
        set {
            Add( _key, value, GetParam( _key ) );
        }
    }

    /// <summary>
    /// 获取长度
    /// </summary>
    public int GetSize {
        get {
            return ( m_keys != null ) ? m_keys.Count : 0;
        }
    }
}