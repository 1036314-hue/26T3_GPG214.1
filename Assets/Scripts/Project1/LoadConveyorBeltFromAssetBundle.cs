using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadConveyorBeltFromAssetBundle : MonoBehaviour
{
    string folderPath = "AssetBundles";
    string fileName = "assetbundle";
    string combinedPath;
    private AssetBundle bundle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        //Using Yield Return to prevent Load Conveyor Belt from happening before loading in the asset bundle
        yield return StartCoroutine(LoadAssetBundle());
        yield return StartCoroutine(LoadConveyorBelt());
        yield return StartCoroutine(LoadBoxSpawner());
    }

   
    //Loading Access to the contents of the bundle
    IEnumerator LoadAssetBundle()
    {
        //Creating the path to the bundle
        combinedPath = Path.Combine(Application.streamingAssetsPath, folderPath, fileName);

        //Providing the path to the request
        UnityWebRequest assetBundleRequest = UnityWebRequestAssetBundle.GetAssetBundle(combinedPath);

        //requesting download of the asset bundle
        AsyncOperation downloadOperation = assetBundleRequest.SendWebRequest();

        //Checking if the file exists to prevent errors
        if (File.Exists(combinedPath))
        {
            while (!downloadOperation.isDone)
            {
                Debug.Log("Download" + ((downloadOperation.progress / 1f) * 100)+ "%");
                yield return null;
            }
            
            //Check if errors in download
            if (assetBundleRequest.result == UnityWebRequest.Result.ConnectionError || assetBundleRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error with Download at " + combinedPath);
                //Break out
                yield break;
            }

            Debug.Log("Download Complete");

            bundle = DownloadHandlerAssetBundle.GetContent(assetBundleRequest);

            assetBundleRequest.Dispose();

            yield return null;
        }
        else
        {
            Debug.LogError("File does not exist in " + combinedPath);
        }
        yield return null;
    }

    //Adding ConveyorBelt to scene
    IEnumerator LoadConveyorBelt()
    {
        //Checking that the bundle exists
        if(bundle == null)
        {
            //Breaking out if it doesnt
            yield return null;
        }

        //Creating a reference to the object inside the bundle
        GameObject conveyorBelt = bundle.LoadAsset<GameObject>("ConveyorBelt");
        //Error handling for if object doesnt exist within the bundle
        if(conveyorBelt != null )
        {
            //Creating the object
            Instantiate(conveyorBelt);
        }
        yield return null;
    }

    //Adding Box Spawner to scene
    IEnumerator LoadBoxSpawner()
    {
        //Checking that the bundle exists
        if (bundle == null)
        {
            //Breaking out if it doesnt
            yield return null;
        }

        //Creating a reference to the object inside the bundle
        GameObject boxSpawner = bundle.LoadAsset<GameObject>("BoxSpawner");
        //Error handling for if object doesnt exist within the bundle
        if (boxSpawner != null)
        {
            //Creating the object
            Instantiate(boxSpawner);
        }
        yield return null;
    }
}
