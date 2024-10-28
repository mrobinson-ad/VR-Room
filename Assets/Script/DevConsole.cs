using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DevConsole : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fps_display;
    [SerializeField] private TextMeshProUGUI vert_display;
    [SerializeField] private TextMeshProUGUI tri_display;

    [SerializeField] private GameObject devConsole;

    private void Start()
    {
        InvokeRepeating("DisplayFrameRate", 1, 1); //Shows FPS refreshed every second
        InvokeRepeating("CountAllTriangles", 1, 5); // Shows Triangles and vertices, refreshed every 5 seconds
    }

    /// <summary>
    /// Displays FPS
    /// </summary>
    private void DisplayFrameRate()
    {
        float current = (int)(1f / Time.unscaledDeltaTime);
        fps_display.text = current.ToString() + " FPS";
    }

    /// <summary>
    /// Displays the number of triangles and vertices currently rendered in the scene
    /// </summary>
    private void CountAllTriangles()
    {
        int totalTriangles = 0;
        int totalVerticles = 0;
        MeshFilter[] allMeshes = UnityEngine.Object.FindObjectsOfType<MeshFilter>();
        foreach(MeshFilter filter in allMeshes)
        {
            if (filter.sharedMesh.isReadable)
            {
                totalTriangles += filter.sharedMesh.triangles.Length / 3;
                totalVerticles += filter.sharedMesh.vertexCount;
            }
        }
        tri_display.text = totalTriangles.ToString() + " Triangles";
        vert_display.text = totalVerticles.ToString() + " Vertices";
    }

    public void ToggleConsole()
    {
        devConsole.SetActive(!devConsole.activeSelf);
    }
}
