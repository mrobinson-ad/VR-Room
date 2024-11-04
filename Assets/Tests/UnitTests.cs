using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEngine.Video;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SkyboxManagerTests
{
    private SkyboxManager skyboxManager;
    private ToggleMenu menuSphere;

    [UnitySetUp]
     public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Visit Room", LoadSceneMode.Single);
        yield return new WaitForSeconds(0.1f);
    }

    [TearDown]
    public void TearDown()
    {
        SceneManager.UnloadSceneAsync("Visit Room");
    }

    [UnityTest]
    public IEnumerator VideoToRoom()
    {
        skyboxManager = GameObject.FindObjectOfType<SkyboxManager>();
        yield return new WaitUntil(() => skyboxManager != null);

        VideoClip testClip = Resources.Load<VideoClip>("TestVideo");

        yield return skyboxManager.FadeToVideo(testClip);

        Assert.IsTrue(skyboxManager.videoPlayer.isPlaying);
        Assert.AreEqual(skyboxManager.videoMat, RenderSettings.skybox);
        Assert.IsFalse(skyboxManager.room.activeSelf);
        Assert.IsFalse(skyboxManager.dirLight.activeSelf);

        yield return skyboxManager.FadeToRoom();

        Assert.IsTrue(skyboxManager.room.activeSelf);
        Assert.IsTrue(skyboxManager.dirLight.activeSelf);

        foreach (var item in skyboxManager.socketedItems)
        {
            Assert.IsTrue(item.activeSelf);
        }

       
        Assert.AreEqual("Chambre", skyboxManager.currentText.text);
        Assert.AreEqual(skyboxManager.pointNames[0], skyboxManager.nextText.text);
        Assert.AreEqual(skyboxManager.pointNames[skyboxManager.photos.Count -1], skyboxManager.previousText.text);
    }

    [UnityTest]
    public IEnumerator OnNextClicked()
    {
        skyboxManager = GameObject.FindObjectOfType<SkyboxManager>();
        yield return new WaitUntil(() => skyboxManager != null);

        skyboxManager.OnNextClicked();

        yield return new WaitForSeconds(1.5f);

        Assert.IsFalse(skyboxManager.room.activeInHierarchy);
        var index = skyboxManager.currentIndex;
        Assert.AreEqual(index, 0);
        Assert.AreEqual(skyboxManager.skyMaterials[index], RenderSettings.skybox);
        Assert.AreEqual(skyboxManager.pointNames[index], skyboxManager.currentText.text);
        Assert.AreEqual(skyboxManager.pointNames[skyboxManager.GetWrappedIndex(index + 1)], skyboxManager.nextText.text);
        Assert.AreEqual(skyboxManager.photos[skyboxManager.GetWrappedIndex(index + 1)], skyboxManager.nextSphere.GetComponent<Renderer>().material.GetTexture("_BaseMap"));
    }

    [UnityTest]
    public IEnumerator OnPreviousClicked()
    {
        skyboxManager = GameObject.FindObjectOfType<SkyboxManager>();
        yield return new WaitUntil(() => skyboxManager != null);

        skyboxManager.OnNextClicked();
        yield return new WaitForSeconds(1.5f);
        skyboxManager.OnNextClicked();
        yield return new WaitForSeconds(1.5f);

        Assert.AreEqual(skyboxManager.currentIndex, 1);

        skyboxManager.OnPreviousClicked();
        yield return new WaitForSeconds(1.5f);

        Assert.IsFalse(skyboxManager.room.activeInHierarchy);
        var index = skyboxManager.currentIndex;

        Assert.AreEqual(index, 0);
        Assert.AreEqual(skyboxManager.skyMaterials[index], RenderSettings.skybox);
        Assert.AreEqual(skyboxManager.pointNames[index], skyboxManager.currentText.text);
        Assert.AreEqual(skyboxManager.pointNames[skyboxManager.GetWrappedIndex(index - 1)], skyboxManager.previousText.text);
        Assert.AreEqual(skyboxManager.photos[skyboxManager.GetWrappedIndex(index - 1)], skyboxManager.previousSphere.GetComponent<Renderer>().material.GetTexture("_BaseMap"));
    }

    [UnityTest]
    public IEnumerator ToggleMenu()
    {
        menuSphere = GameObject.FindObjectOfType<ToggleMenu>();

        yield return new WaitUntil(() => menuSphere != null);

        menuSphere.Toggle();

        GameObject menu = GameObject.Find("MenuCanvas"); 

        Assert.IsTrue(menu.activeInHierarchy);

        menuSphere.Toggle();

        Assert.IsFalse(menu.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator ToggleDev()
    {
        DevConsole devConsole = GameObject.FindObjectOfType<DevConsole>();

        yield return new WaitUntil(() => devConsole != null);

        devConsole.ToggleConsole();

        GameObject console = GameObject.Find("Dev Console");

        Assert.IsTrue(console.activeInHierarchy);

        devConsole.ToggleConsole();

        Assert.IsFalse(console.activeInHierarchy);
    }
}