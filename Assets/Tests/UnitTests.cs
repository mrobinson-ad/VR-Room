using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEngine.Video;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SkyboxManagerTests
{
    private SkyboxManager skyboxManager;
    private GameObject skyboxManagerObject;

    private VideoClip testClip;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject to attach SkyboxManager
        skyboxManagerObject = new GameObject();
        skyboxManager = skyboxManagerObject.AddComponent<SkyboxManager>();

        // Mock and assign necessary dependencies
        skyboxManager.fadeImages = new List<Image> { new GameObject().AddComponent<Image>() };

        // Setup nextSphere and previousSphere with MeshRenderer components
        skyboxManager.nextSphere = new GameObject();
        skyboxManager.nextSphere.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));

        skyboxManager.previousSphere = new GameObject();
        skyboxManager.previousSphere.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));

        // Setup text components
        skyboxManager.nextText = new GameObject().AddComponent<TextMeshProUGUI>();
        skyboxManager.previousText = new GameObject().AddComponent<TextMeshProUGUI>();
        skyboxManager.currentText = new GameObject().AddComponent<TextMeshProUGUI>();

        // Setup additional GameObjects and references
        skyboxManager.room = new GameObject();
        skyboxManager.dirLight = new GameObject();
        skyboxManager.videoPlayer = skyboxManagerObject.AddComponent<VideoPlayer>();
        skyboxManager.musicPlayer = skyboxManagerObject.AddComponent<PlayMusic>();

        // Mock lists with basic data for the test
        skyboxManager.photos = new List<Texture2D> { Texture2D.blackTexture, Texture2D.whiteTexture };
        skyboxManager.skyMaterials = new List<Material> { new Material(Shader.Find("Standard")) };
        skyboxManager.pointNames = new List<string> { "Point1", "Point2" };
        skyboxManager.socketedItems = new List<GameObject> { new GameObject(), new GameObject() };

        // Assign a material to roomBG
        skyboxManager.roomBG = new Material(Shader.Find("Standard"));

        // Create and set up the AudioManager singleton instance
        var audioManagerObject = new GameObject();
        var audioManager = audioManagerObject.AddComponent<AudioManager>();
        AudioManager.Instance = audioManager; // Set the singleton instance

        // Add an AudioSource component and assign it to AudioManager
        audioManager.audioSource = audioManagerObject.AddComponent<AudioSource>();
        testClip = Resources.Load<VideoClip>("TestVideo");
        skyboxManager.videoPlayer.clip = testClip;
    }

    [TearDown]
    public void TearDown()
    {
        // Cleanup GameObjects after each test
        GameObject.Destroy(skyboxManagerObject);
    }

    [UnityTest]
    public IEnumerator ReturnRoom()
    {
        yield return skyboxManager.FadeToRoom();

        Assert.IsTrue(skyboxManager.room.activeSelf);
        Assert.IsTrue(skyboxManager.dirLight.activeSelf);

        foreach (var item in skyboxManager.socketedItems)
        {
            Assert.IsTrue(item.activeSelf);
        }

        // Validate UI text updates
        Assert.AreEqual("Chambre", skyboxManager.currentText.text);
        Assert.AreEqual(skyboxManager.pointNames[0], skyboxManager.nextText.text);
        Assert.AreEqual(skyboxManager.pointNames[skyboxManager.photos.Count - 1], skyboxManager.previousText.text);
    }

    [UnityTest]
    public IEnumerator SwapVideo()
    {
        yield return skyboxManager.FadeToVideo(testClip);

        Assert.IsFalse(skyboxManager.room.activeSelf);
        Assert.IsFalse(skyboxManager.dirLight.activeSelf);
        Assert.AreEqual(testClip, skyboxManager.videoPlayer.clip);
        Assert.IsTrue(skyboxManager.videoPlayer.isPlaying);
    }

    [UnityTest]
    public IEnumerator OnNextClicked()
    {
        skyboxManager.currentIndex = -1;
        skyboxManager.OnNextClicked();

        yield return new WaitForSeconds(2f);

        Assert.AreEqual(0, skyboxManager.currentIndex);
        Assert.AreEqual(skyboxManager.pointNames[0], skyboxManager.currentText.text);
        Assert.AreEqual(skyboxManager.pointNames[1], skyboxManager.nextText.text); 
    }

    [UnityTest]
    public IEnumerator OnPreviousClicked()
    {
        skyboxManager.currentIndex = 1;
        skyboxManager.OnPreviousClicked();

        yield return new WaitForSeconds(2f);

        Assert.AreEqual(0, skyboxManager.currentIndex);
        Assert.AreEqual(skyboxManager.pointNames[0], skyboxManager.currentText.text);
        Assert.AreEqual(skyboxManager.pointNames[1], skyboxManager.nextText.text);
    }
}