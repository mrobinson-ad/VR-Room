using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class SkyboxManager : MonoBehaviour
{

    public int currentIndex = -1;
    public List<Image> fadeImages;

    public GameObject nextSphere;
    public GameObject previousSphere;
    public TextMeshProUGUI nextText;
    public TextMeshProUGUI previousText;
    public TextMeshProUGUI currentText;
    public List<GameObject> socketedItems;
    public List<Texture2D> photos;
    public List<Material> skyMaterials;
    public List<string> pointNames;
    public GameObject room;
    public Material roomBG;
    public Material videoMat;
    public GameObject dirLight;
    public VideoPlayer videoPlayer;
    public PlayMusic musicPlayer;

    [SerializeField] float fadeTime = 0.5f;

    /// <summary>
    /// Blinks to the specified video
    /// </summary>
    /// <param name="video"></param>
    public void SwapVideo(VideoClip video)
    {
        StartCoroutine(FadeToVideo(video));
    }

    /// <summary>
    /// Returns to the room hub
    /// </summary>
    public void ReturnRoom()
    {
        StartCoroutine(FadeToRoom());
    }

    /// <summary>
    /// Blinks and sets the room gameObjects and the directional light to active
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeToRoom()
    {
        yield return Blink(0, 0.3f, fadeTime);

        RenderSettings.skybox = roomBG;
        room.SetActive(true);
        videoPlayer.Stop();
        dirLight.SetActive(true);
        foreach (GameObject go in socketedItems)
            go.SetActive(true);
        currentText.text = "Chambre";
        currentIndex = -1;
        nextSphere.GetComponent<Renderer>().material.SetTexture("_BaseMap", photos[GetWrappedIndex(currentIndex + 1)]);
        nextText.text = pointNames[GetWrappedIndex(currentIndex + 1)];
        previousSphere.GetComponent<Renderer>().material.SetTexture("_BaseMap", photos[GetWrappedIndex(currentIndex - 1)]);
        previousText.text = pointNames[GetWrappedIndex(currentIndex - 1)];

        yield return Blink(0.3f, 0, fadeTime);

        if (AudioManager.Instance.IsPlaying() != musicPlayer.isPlaying)
        {
                musicPlayer.Toggle();
        }
    }

    /// <summary>
    /// Blinks and sets the clip to the video player, the skybox to the video material and plays
    /// </summary>
    /// <param name="clip"></param>
    /// <returns></returns>
    public IEnumerator FadeToVideo(VideoClip clip)
    {
        videoPlayer.clip = clip;
        yield return Blink(0, 0.3f, fadeTime);

        RenderSettings.skybox = videoMat;
        dirLight.SetActive(false);
        foreach (GameObject go in socketedItems)
            go.SetActive(false);
        room.SetActive(false);
        videoPlayer.Play();

        yield return Blink(0.3f, 0, fadeTime);
    }

    /// <summary>
    /// Lerps the rectTransform of the fade images to simulate a blink
    /// </summary>
    /// <param name="startHeight"></param>
    /// <param name="targetHeight"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator Blink(float startHeight, float targetHeight, float duration)
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

    /// <summary>
    /// Advances the index and calls FadeInOut
    /// </summary>
    public void OnNextClicked()
    {
        currentIndex = GetWrappedIndex(currentIndex + 1);
        StartCoroutine(FadeInOut());
    }

    /// <summary>
    /// Reduces the index and calls FadeInOut
    /// </summary>
    public void OnPreviousClicked()
    {
        currentIndex = GetWrappedIndex(currentIndex - 1);
        StartCoroutine(FadeInOut());
    }

    /// <summary>
    /// Blinks and sets the skybox and the sphere's material and text based on the current index
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeInOut()
    {
        yield return Blink(0, 0.3f, fadeTime);

        RenderSettings.skybox = skyMaterials[currentIndex];
        videoPlayer.Stop();

        currentText.text = pointNames[currentIndex];

        nextSphere.GetComponent<Renderer>().material.SetTexture("_BaseMap", photos[GetWrappedIndex(currentIndex + 1)]);
        nextText.text = pointNames[GetWrappedIndex(currentIndex + 1)];
        previousSphere.GetComponent<Renderer>().material.SetTexture("_BaseMap", photos[GetWrappedIndex(currentIndex - 1)]);
        previousText.text = pointNames[GetWrappedIndex(currentIndex - 1)];

        foreach (GameObject go in socketedItems)
            go.SetActive(false);
        room.SetActive(false);

        yield return Blink(0.3f, 0, fadeTime);
    }

    /// <summary>
    /// Wraps around the given index to prevent out of bounds errors
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetWrappedIndex(int index)
    {
        if (index < 0) return photos.Count - 1;
        if (index >= photos.Count) return 0;
        return index;
    }
}
