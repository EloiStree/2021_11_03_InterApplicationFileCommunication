using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileCommunicationDeleter : MonoBehaviour
{
    public List<FileCommunicationData> toDelete= new List<FileCommunicationData>();

    public void Delete(FileCommunicationData target)
    {
        toDelete.Add(target) ;
        
        
        
    }

    void Update()
    {
        if (toDelete.Count > 0) {
            for (int i = toDelete.Count - 1; i >= 0; i--)
            {
                string p = toDelete[i].m_path;
                if (!File.Exists(p))
                    toDelete.RemoveAt(i);
                else
                {
                    try
                    {
                        File.Delete(p);

                    }
                    catch { }
                }

            }
        }
    }
}
