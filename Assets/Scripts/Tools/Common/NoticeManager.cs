using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeManager : Singletons<NoticeManager> {

    public delegate void NoticeDelegate();

    /// <summary>消息处理器集合。消息名，消息处理器，一个消息名对应多个消息处理器</summary>
    private KeyValueList<string, NoticeDelegate> m_noticesList;

    public NoticeManager() {
        m_noticesList = new KeyValueList<string, NoticeDelegate>();
    }

    /// <summary>
    /// 注册消息
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_delegate"></param>
    public void Register(string _name, NoticeDelegate _delegate) {
        if (m_noticesList == null || _delegate == null) {
            return;
        }

        if (m_noticesList.ContainsKey( _name ) && ( m_noticesList[_name] != null )) {
            m_noticesList[_name] += _delegate;
        } else {
            NoticeDelegate method = null;
            method += _delegate;
            m_noticesList[_name] = method;
        }
    }

    /// <summary>
    /// 注销消息
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_delegate"></param>
    public void Unregister(string _name, NoticeDelegate _delegate) {
        if (m_noticesList.ContainsKey( _name )) {
            m_noticesList[_name] -= _delegate;
            if (m_noticesList[_name] == null) {
                m_noticesList.Remove( _name );
            }
        }
    }

    /// <summary>
    /// 发送消息，执行
    /// </summary>
    /// <param name="_name"></param>
    public void SendNotice(string _name) {
        if (m_noticesList.ContainsKey( _name )) {
            m_noticesList[_name]();
        }

    }


}
