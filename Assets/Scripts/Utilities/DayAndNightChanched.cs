using UnityEngine;
using System.Collections;

public class CameraColorChanger : MonoBehaviour
{
    private Camera mainCamera; 
    private string colorHexNight = "#1E131F";
    private string colorHexDay = "#9F988A";
    private float deltaTime;
    [SerializeField, Range(10,60*7)]private float timeDay;
    [SerializeField, Range(10,60*6)]private float timeNight;
    private bool isNight = false;


    void Start()
    {
        mainCamera = Camera.main;

        if (ColorUtility.TryParseHtmlString(colorHexDay, out Color targetColor))
        {
            mainCamera.backgroundColor = targetColor;
        }
        else
        {
            Debug.LogError("Failed to parse color from HEX string");
        }
        deltaTime = timeDay;
    }

    void Update()
    {
        deltaTime -= Time.deltaTime;
        Debug.Log(deltaTime);
        if (deltaTime < 0 && !isNight)
        {
            StartCoroutine(ChangeCameraColor(colorHexNight, 6f));
            deltaTime = timeNight;
            isNight = true;
        }
        if(deltaTime < 0 && isNight) 
        {
            StartCoroutine(ChangeCameraColor(colorHexDay, 5f));
            deltaTime = timeDay;
            isNight = false;
        }
        
    }

    IEnumerator ChangeCameraColor(string newColor, float duration)
    {
        if (ColorUtility.TryParseHtmlString(newColor, out Color targetColor))
        {
            Color currentColor = mainCamera.backgroundColor;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                mainCamera.backgroundColor = Color.Lerp(currentColor, targetColor, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            mainCamera.backgroundColor = targetColor; 
        }
        else
        {
            Debug.LogError("Failed to parse color from HEX string");
        }

        
    }
}
