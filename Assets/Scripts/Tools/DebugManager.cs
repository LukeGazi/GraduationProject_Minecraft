using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager {

    static public bool EnableLog = true; //控制是否开启Debug

    static public void Log(object _message) {
        if (EnableLog) {
            Log( _message, null );
        }
    }
    static public void Log(object _message, Object _context) {
        if (EnableLog) {
            Debug.Log( _message, _context );
        }
    }

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
