using UnityEngine;

public class JSONFiles : MonoBehaviour
{
   

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

    }

    void LoadJSON()
    {

    }
}
