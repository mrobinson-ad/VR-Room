using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleMenu : MonoBehaviour
{
    public XRRayInteractor leftController;
    public GameObject menu;
    public void Toggle()
    {
        menu.SetActive(!menu.activeSelf);
        leftController.enabled = !leftController.isActiveAndEnabled;
    }
}
