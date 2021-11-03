using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenToFileCommunicationEventDemo : MonoBehaviour
{
    public FileCommunicationData m_lastReceived;
    public Color m_color;
    public void ChangeColor(FileCommunicationData data)
    {
        if (Eloi.E_StringUtility.AreEquals(data.m_fileContent, "r", true, true))
            m_color = Color.red;
       else if (Eloi.E_StringUtility.AreEquals(data.m_fileContent, "g", true, true))
            m_color = Color.green;
       else if (Eloi.E_StringUtility.AreEquals(data.m_fileContent, "b", true, true))
            m_color = Color.blue;
    }

    
}
