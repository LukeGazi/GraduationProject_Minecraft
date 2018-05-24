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
    /// <param name="key"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public V Add(K key, V target) {
        V result = default( V );
        if (m_keys.IndexOf( key ) == -1) {
            m_keys.Add( key );
            m_values.Add( target );
            result = target;
        } else {
            int index = m_keys.IndexOf( key );
            result = m_values[index];
            m_values[index] = target;
        }
        return result;
    }

    /// <summary>
    /// 移出键值对
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public V Remove(K key) {
        V result = default( V );
        if (m_keys.IndexOf( key ) == -1) {
            return default( V );
        } else {
            int index = m_keys.IndexOf( key );
            result = m_values[index];
            m_keys.RemoveAt( index );
            m_values.RemoveAt( index );
        }
        return result;
    }

    /// <summary>
    /// 获取键值对
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public V GetValue(K key) {
        V result = default( V );
        int index = m_keys.IndexOf( key );
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
    /// <param name="key"></param>
    /// <returns></returns>
    public V this[K key] {
        get {
            return GetValue( key );
        }
        set {
            Add( key, value );
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