using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TextureLoader : MonoBehaviour
{
    public string fileName = "Textures/Charmander.jpg";

    public string spriteName = "Textures/Charmander.jpg";

    public string folderPath = Application.streamingAssetsPath;

    private string combinedFilePathLocation;

    private Image charmanderSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Combine the path
        combinedFilePathLocation = Path.Combine(folderPath, fileName);

        LoadTexture();
    }

    private void LoadTexture()
    {
        if (File.Exists(combinedFilePathLocation))
        {
            //Read in all the bytes of data
            byte[] imageBytes = File.ReadAllBytes(combinedFilePathLocation);

            //Temporary Texture to hold
            Texture2D texture = new Texture2D(2, 2);
            //Take bytes and converts to image
            texture.LoadImage(imageBytes);

            GetComponent<Renderer>().material.mainTexture = texture;
        }
        else
        {
            Debug.Log("Texture File Not Found at Path " + combinedFilePathLocation);
        }
    }

    private void LoadSprite()
    {
        if (File.Exists(combinedFilePathLocation))
        {
            //Read in all the bytes of data
            byte[] spriteBytes = File.ReadAllBytes(combinedFilePathLocation);

            //Temporary Texture to hold
            Texture2D texture = new Texture2D(2, 2);
            //Take bytes and converts to image
            texture.LoadImage(spriteBytes);

            charmanderSprite.sprite = Sprite.Create(texture, new Rect(0,0, texture.width, texture.height), Vector2.zero);
        }
        else
        {
            Debug.Log("Texture File Not Found at Path " + combinedFilePathLocation);
        }
    }
}
