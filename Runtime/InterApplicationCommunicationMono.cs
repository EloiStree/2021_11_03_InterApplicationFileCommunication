using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InterApplicationCommunicationMono : MonoBehaviour
{

    public string m_extensionName = ".filecommunication";
    public string m_subFolderPath = "FileAction";
    public string m_creationFolderPath = "";


    public void PushAction(string actionStringMessage) {

        string root = m_creationFolderPath;
        if (string.IsNullOrEmpty(m_creationFolderPath))
            root = Directory.GetCurrentDirectory();

        Eloi.E_FilePathUnityUtility.MeltPathTogether(out string path, root, m_subFolderPath, DateTime.Now.ToString("yyyy_mm_dd_hh_mm_ss_fff")+ m_extensionName);
        string folderPath = Path.GetDirectoryName(path);
        Directory.CreateDirectory(folderPath);
        File.WriteAllText(path, actionStringMessage);

    }
    [ContextMenu("Push Random To 1-100")]
    public void PushRandomAction()
    {
        PushAction(UnityEngine.Random.Range(0, 100).ToString());
    }
    [ContextMenu("Push Random To R G B")]
    public void PushRandomRGB()
    {
        string rgb = "rgb";
        Eloi.E_UnityRandomUtility.GetRandomOf(in rgb, out char c);
        PushAction(""+c);
    }
}
