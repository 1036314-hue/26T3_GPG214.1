using System.IO;
using UnityEngine;

public class AssetBundles : MonoBehaviour
{
    string folderPath = "AssetBundles";
    string fileName = "AssetBundle";
    string combinedPath;
    private AssetBundle bundle;
    public SpriteRenderer playerSprite;

    private void Start()
    {
        LoadAssetBundle();
        LoadPlayer();
        LoadPlayerSprite();
    }

    void LoadPlayer()
    {
        if(bundle == null)
        {
            return;
        }

        //Find the Gameobject in the bundle called player and save it
        GameObject playerPrefab = bundle.LoadAsset<GameObject>("player");
        if(playerPrefab != null)
        {
            Instantiate(playerPrefab);
        }
        
    }

    void LoadPlayerSprite()
    {
        if(bundle == null)
        {
            return;
        }
        Texture2D player = bundle.LoadAsset<Texture2D>("player");
        if (player != null)
        {
            playerSprite.sprite = Sprite.Create(player, new Rect(0, 0, player.width, player.height), Vector2.zero);
        }
    }

    void LoadAssetBundle()
    {
        combinedPath = Path.Combine(Application.streamingAssetsPath, folderPath, fileName);

        if (File.Exists(combinedPath))
        {
            bundle = AssetBundle.LoadFromFile(combinedPath);
            Debug.Log("Asset Bundle Loaded");
        }
        else
        {
            Debug.Log("File does not exist in " + combinedPath);
        }
    }
}
