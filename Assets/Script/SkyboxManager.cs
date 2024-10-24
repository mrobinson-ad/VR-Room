using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SkyboxManager : MonoBehaviour
{

    public List<Image> fadeImages;
    public List<GameObject> socketedItems;
    public GameObject room;
    public Material roomBG;
    public Material videoMat;
    public GameObject dirLight;
    public VideoPlayer videoPlayer;

    [SerializeField] const float fadeTime = 0.5f;
    public void SwapMat(Sphere sphere)
    {
        StartCoroutine(FadeInOut(sphere.skyMaterial, sphere.sphereToShow));
    }

    public void SwapVideo(VideoClip video)
    {
        StartCoroutine(FadeToVideo(video));
    }

    public void ReturnRoom()
    {
        StartCoroutine(FadeToRoom());
    }

    private IEnumerator FadeInOut(Material mat, GameObject sphere)
    {

       yield return FadeImagesWithHeight(0, 0.3f, fadeTime);

        RenderSettings.skybox = mat;
        videoPlayer.Stop();
        sphere.SetActive(true);
        foreach (GameObject go in socketedItems)
            go.SetActive(false);
        room.SetActive(false);

        yield return FadeImagesWithHeight(0.3f, 0, fadeTime);
    }

    private IEnumerator FadeToRoom()
    {
        yield return FadeImagesWithHeight(0, 0.3f, fadeTime);
        
        RenderSettings.skybox = roomBG;
        room.SetActive(true);
        videoPlayer.Stop();
        dirLight.SetActive(true);
        foreach (GameObject go in socketedItems)
            go.SetActive(true);

        yield return FadeImagesWithHeight(0.3f, 0, fadeTime);
    
    }

    private IEnumerator FadeToVideo(VideoClip clip)
    {
        videoPlayer.clip = clip;
        yield return FadeImagesWithHeight(0, 0.3f, fadeTime);

        RenderSettings.skybox = videoMat;
        dirLight.SetActive(false);
        foreach (GameObject go in socketedItems)
            go.SetActive(false);
        room.SetActive(false);
        videoPlayer.Play();

        yield return FadeImagesWithHeight(0.3f, 0, fadeTime);
    }

    private IEnumerator FadeImagesWithHeight(float startHeight, float targetHeight, float duration)
    {
        RectTransform[] rectTransforms = new RectTransform[fadeImages.Count];
        for (int i = 0; i < fadeImages.Count; i++)
        {
            rectTransforms[i] = fadeImages[i].GetComponent<RectTransform>();
        }

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float blend = Mathf.Clamp01(t / duration);
            float newHeight = Mathf.Lerp(startHeight, targetHeight, blend);

            for (int i = 0; i < fadeImages.Count; i++)
            {
                Vector2 size = rectTransforms[i].sizeDelta;
                size.y = newHeight;
                rectTransforms[i].sizeDelta = size;
            }

            yield return null;
        }

        for (int i = 0; i < fadeImages.Count; i++)
        {
            Vector2 size = rectTransforms[i].sizeDelta;
            size.y = targetHeight;
            rectTransforms[i].sizeDelta = size;
        }
    }
}
