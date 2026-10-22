using Codice.Client.Common;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FileSystems))]
public class FileSystemsCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        FileSystems fileSystemsScript = (FileSystems)target;

        if(GUILayout.Button("Open Persistant Datapath"))
        {
            fileSystemsScript.OpenPersistantDataPath();
        }
    }
}
