using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyValueList<K, V> {
    //键名列表
    private List<K> m_keys;
    //值列表
    private List<V> m_values;

    /// <summary>
    /// 初始化
    /// </summary>
    public KeyValueList() {
        m_keys = new List<K>();
        m_values = new List<V>();
    }

    /// <summary>
    /// 添加键值对
    /// </summary>
    /// <param name="_key"></param>
    /// <param name="_target"></param>
    /// <returns></returns>
    public V Add(K _key, V _target) {
        V result = default( V );
        int index = m_keys.IndexOf( _key );
        if (index == -1) {
            m_keys.Add( _key );
            m_values.Add( _target );
            result = _target;
        } else {
            result = m_values[index];
            m_values[index] = _target;
        }
        return result;
    }

    /// <summary>
    /// 移出键值对
    /// </summary>
    /// <param name="_key"></param>
    /// <returns></returns>
    public V Remove(K _key) {
        V result = default( V );
        int index = m_keys.IndexOf( _key );
        if (index == -1) {
            return default( V );
        } else {
            result = m_values[index];
            m_keys.RemoveAt( index );
            m_values.RemoveAt( index );
        }
        return result;
    }

    /// <summary>
    /// 获取键值对
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

    public bool ContainsKey(K key) {
        return m_keys.Contains( key );
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
            Add( _key, value );
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