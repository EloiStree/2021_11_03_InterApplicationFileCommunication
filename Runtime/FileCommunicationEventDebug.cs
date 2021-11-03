using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileCommunicationEventDebug : MonoBehaviour
{

    public int m_historyCount;
    public List<FileCommunicationData> m_found;
    public List<string> m_deleted;

    public void PushNewFile(FileCommunicationData data)
    {
        Eloi.E_GeneralUtility.ListAsQueueInsert(in data, m_historyCount, ref m_found);
    }
    public void PushDeletedfile(string path)
    {

        Eloi.E_GeneralUtility.ListAsQueueInsert(in path, m_historyCount, ref m_deleted);
    }
}
