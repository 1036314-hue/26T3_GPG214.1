using System.IO;
using UnityEngine;

public class JSONFiles : MonoBehaviour
{
    public DataToSave saveData = new DataToSave();
    public string playerJSONFileName = "Save.json";
    public string folderPath = Application.streamingAssetsPath;
    private string fullFilePath = string.Empty;

    private BoxSpawner boxSpawner;
    private ConveyorBelt conveyorBelt;

    private void Start()
    {
       fullFilePath = Path.Combine(folderPath, playerJSONFileName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveJSON();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            LoadJSON();
        }
    }

    void SaveJSON()
    {
        boxSpawner = Object.FindFirstObjectByType<BoxSpawner>();
        conveyorBelt = Object.FindFirstObjectByType<ConveyorBelt>();
        if (boxSpawner != null && conveyorBelt != null)
        {
            saveData.SetSavedData(boxSpawner, conveyorBelt);
            string jsonData = JsonUtility.ToJson(saveData);
            File.WriteAllText(fullFilePath, jsonData);
        }
        else
        {
            Debug.LogError("Unable to find reference for box spawner or conveyorbelt");
        }
    }

    void LoadJSON()
    {
        if(File.Exists(fullFilePath))
        {


            string jsonData = File.ReadAllText(fullFilePath);

            saveData = JsonUtility.FromJson<DataToSave>(jsonData);
            boxSpawner = Object.FindFirstObjectByType<BoxSpawner>();
            conveyorBelt = Object.FindFirstObjectByType<ConveyorBelt>();
            if (boxSpawner != null && conveyorBelt != null)
            {


                saveData.LoadSavedData(boxSpawner, conveyorBelt);
            }
            else
            {
                Debug.LogError("Unable to find reference for box spawner or conveyorbelt");
            }
        }
        else
        {
            Debug.LogError("No Save Data Found");
        }
    }
}
