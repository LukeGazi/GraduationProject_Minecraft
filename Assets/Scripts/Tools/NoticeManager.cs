using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeManager {

    #region 单例
    private static NoticeManager m_instance;
    public static NoticeManager Instance {
        get {
            if (m_instance == null) {
                m_instance = new NoticeManager();
            }
            return m_instance;
        }
    }
    private NoticeManager() { }
    #endregion

     

}
