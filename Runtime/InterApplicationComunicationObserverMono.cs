using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InterApplicationComunicationObserverMono : MonoBehaviour
{

    public string m_directoryObservedPath;
    public string m_fileExtensionObserved=".filecommunication";
    public bool m_observedChildren;
    public string[] m_fileObservedPrevious;
    public string[] m_fileObservedCurrent;
    public string[] m_newFiles;
    public string[] m_lostFiles;
    public FileCommunicationEvent m_onNewFile;
    public FileCommunicationPathEvent m_pathOfMoveOrDeletedFile;

    public List<string> readWhenPossible = new List<string>();
    public void CheckForNewFile() {
        m_fileObservedPrevious = m_fileObservedCurrent;
        if(Directory.Exists(m_directoryObservedPath))
        m_fileObservedCurrent = Directory.GetFiles(
            m_directoryObservedPath,
            "*" + m_fileExtensionObserved,
            m_observedChildren ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

        m_newFiles = m_fileObservedCurrent.Except(m_fileObservedPrevious).ToList().ToArray();
        m_lostFiles = m_fileObservedPrevious.Except(m_fileObservedCurrent).ToList().ToArray();
        for (int i = 0; i < m_newFiles.Length; i++)
        {
            PushInReadQueue(m_newFiles[i]);
        }
        for (int i = 0; i < m_lostFiles.Length; i++)
        {
            m_pathOfMoveOrDeletedFile.Invoke(m_lostFiles[i]);
        }
    }

    private void PushInReadQueue(string path)
    {
        readWhenPossible.Add(path);
    }

    private void Update()
    {
        for (int i = readWhenPossible.Count-1; i >=0 ; i--)
        {
            string p = readWhenPossible[i];
            if (!File.Exists(p))
                readWhenPossible.RemoveAt(i);
            else
            {
                try
                {
                   string t =  File.ReadAllText(p);
                   m_onNewFile.Invoke(new FileCommunicationData(p, t));
                    readWhenPossible.RemoveAt(i);

                }
                catch { }
            }

        }
    }
}


[System.Serializable]
public class FileCommunicationEvent : UnityEvent<FileCommunicationData> { }
[System.Serializable]
public class FileCommunicationPathEvent : UnityEvent<string> { }

[System.Serializable]
public class FileCommunicationData
{
    public string m_path;
    public string m_fileContent;

    public FileCommunicationData(string path, string fileContent)
    {
        m_path = path;
        m_fileContent = fileContent;
    }
}
