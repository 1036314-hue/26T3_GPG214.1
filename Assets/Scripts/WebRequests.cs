using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WebRequests : MonoBehaviour
{
    [SerializeField]
    private string url;

    public Image webTexture;
    private Sprite spriteFromWeb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        //Show loading screen
        yield return StartCoroutine(LoadTextureFromWeb());

        webTexture.sprite = spriteFromWeb;

        //hide the loading screen and show the image
        //wait until the above is done// then do more
        yield return null;
    }

     
    IEnumerator LoadTextureFromWeb()
    {
        UnityWebRequest imageRequest = UnityWebRequest.Get(url);

        AsyncOperation downloadOperation = imageRequest.SendWebRequest();

        while(!downloadOperation.isDone)
        {
            Debug.Log("Download progress " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }

        if (imageRequest.result == UnityWebRequest.Result.ConnectionError || imageRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error with download");
            yield break;
        }

        Debug.Log("Download Complete");

        byte[] allDataDownloaded = imageRequest.downloadHandler.data;

        Texture2D myTexture = new Texture2D(2, 2);
        myTexture.LoadImage(allDataDownloaded);
        spriteFromWeb = Sprite.Create(myTexture, new Rect(0,0, myTexture.width, myTexture.height), Vector2.zero);


        yield return null;
    }


    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);


        while (asyncLoad != null && !asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            Debug.Log("Loading Progress" + (progress * 100) + "%");
            yield return null; //yeild control back to the main thread
        }

        //Loading is completed
        Debug.Log("Scene Loaded Successfully");
        //Now we load the scene
        asyncLoad.allowSceneActivation = true;

        yield return null;
    }
}
