using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeManager : Singletons<NoticeManager> {

    public delegate void NoticeDelegate();
    public delegate void NoticeWithParamDelegate(object[] _params);

    //消息处理器集合。消息名，消息处理器，一个消息名对应多个消息处理器
    private KeyValueList<string, NoticeDelegate> m_noticesList;
    private KeyValueParamList<string, NoticeWithParamDelegate, object[]> m_noticesWithParmsList;

    public NoticeManager() {
        m_noticesList = new KeyValueList<string, NoticeDelegate>();
        m_noticesWithParmsList = new KeyValueParamList<string, NoticeWithParamDelegate, object[]>();
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
    public void Register(string _name, NoticeWithParamDelegate _delegate) {
        if (m_noticesWithParmsList == null || _delegate == null) {
            return;
        }

        if (m_noticesWithParmsList.ContainsKey( _name ) && ( m_noticesWithParmsList[_name] != null )) {
            m_noticesWithParmsList[_name] += _delegate;
        } else {
            NoticeWithParamDelegate method = null;
            method += _delegate;
            m_noticesWithParmsList[_name] = method;
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
    public void Unregister(string _name, NoticeWithParamDelegate _delegate) {
        if (m_noticesWithParmsList.ContainsKey( _name )) {
            m_noticesWithParmsList[_name] -= _delegate;
            if (m_noticesWithParmsList[_name] == null) {
                m_noticesWithParmsList.Remove( _name );
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
    public void SendNotice(string _name, object[] _params) {
        if (m_noticesWithParmsList.ContainsKey( _name )) {
            m_noticesWithParmsList[_name]( _params );
        }
    }
}
