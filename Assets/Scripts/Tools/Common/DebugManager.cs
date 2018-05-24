using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager {

    static public bool EnableLog = true; //控制是否开启Debug

    /// <summary>
    /// 打印日志信息
    /// </summary>
    /// <param name="_message"></param>
    static public void Log(object _message) {
        if (EnableLog) {
            Log( _message, null );
        }
    }
    /// <summary>
    /// 打印日志信息
    /// </summary>
    /// <param name="_message"></param>
    /// <param name="_context">上下文信息，方便找到Debug物体</param>
    static public void Log(object _message, Object _context) {
        if (EnableLog) {
            Debug.Log( _message, _context );
        }
    }

    /// <summary>
    /// 打印错误日志信息
    /// </summary>
    /// <param name="_message"></param>
    static public void LogError(object _message) {
        if (EnableLog) {
            LogError( _message, null );
        }
    }
    static public void LogError(object _message, Object _context) {
        if (EnableLog) {
            Debug.LogError( _message, _context );
        }
    }

    /// <summary>
    /// 打印警告日志信息
    /// </summary>
    /// <param name="_message"></param>
    static public void LogWarning(object _message) {
        if (EnableLog) {
            LogWarning( _message, null );
        }
    }
    static public void LogWarning(object _message, Object _context) {
        if (EnableLog) {
            Debug.LogWarning( _message, _context );
        }
    }

}
