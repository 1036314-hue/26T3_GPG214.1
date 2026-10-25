using System.IO;
using UnityEngine;

public class FileSystems : MonoBehaviour
{
    private void Start()
    {
        string assetPath = Application.dataPath;
        Debug.Log("Asset path: " + assetPath);

        string persistantPath = Application.persistentDataPath;
        Debug.Log("Persistant Data Path: " + persistantPath);

        string streamingAssetsPath = Application.streamingAssetsPath;
        if (Directory.Exists(streamingAssetsPath))
        {
            Debug.Log("StreamingAssetsPath" + streamingAssetsPath);
        }
        else
        {
            Directory.CreateDirectory(Path.Combine(Application.dataPath, "StreamingAssets"));
        }
        Debug.Log("Streaming Assets Path: " +  streamingAssetsPath);

        if (File.Exists(Path.Combine(Application.streamingAssetsPath, "PlayerInfo.txt"))) 
        {
            Debug.Log("The File Exists");
        }
        else
        {
            File.CreateText(Path.Combine(Application.streamingAssetsPath, "PlayerInfo.txt"));
        }

        string[] allFiles = Directory.GetFiles(Application.streamingAssetsPath);
        for(int i = 0; i < allFiles.Length; i++)
        {
            Debug.Log(allFiles[i]);
        }

            string temporaryCachePath = Application.temporaryCachePath;
        Debug.Log("Temporary Cache Path: " +  temporaryCachePath);

        string editorPath = "Assets/Editor/";
        Debug.Log("Editor Path: " + editorPath);

        string resourcesPath = "Assets/Resourses/";
        Debug.Log("Resources Path: " +  resourcesPath);
        Object prefab = Resources.Load("prefab.prefab");

        string filePath = Path.Combine(Application.dataPath, "Example.txt");
    }

    [ContextMenu("Open Persistent Datapath")]
    public void OpenPersistantDataPath()
    {
        string persistentPath = Application.persistentDataPath;
        Application.OpenURL(persistentPath);
    }

   // [MenuItem("Window/Open Persistant Datapath")]
    public static void WindowPersistantDataPath()
    {
        string persistantPath = Application.persistentDataPath;
        Application.OpenURL(persistantPath);
    }

}
