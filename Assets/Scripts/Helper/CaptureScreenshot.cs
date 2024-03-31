using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CaptureScreenshot : MonoBehaviour
{
    private string path;

    public void Capture(RawImage image)
    {
        path = Application.persistentDataPath + $"Screenshot + {UnbiasedTime.Instance.Now.ToShortDateString()}";
        
        ScreenCapture.CaptureScreenshot(path);

        StartCoroutine(ShowImage(image));
    }

    IEnumerator ShowImage(RawImage image)
    {
        byte[] data = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(64,64, TextureFormat.ARGB32, false);
        texture.LoadImage(data);
        
        yield return new WaitForEndOfFrame();
        image.texture = texture;
    }
}