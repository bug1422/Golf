using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigMenu : MonoBehaviour
{
    List<Resolution> resolutions = new List<Resolution>();
    public TMP_Dropdown dropdown;
    public Toggle toggle;
    private void Start()
    {
        toggle.isOn = Screen.fullScreen;
        foreach(var resolution in Screen.resolutions)
        {
            if (resolution.width / resolution.height == 16 / 9) resolutions.Add(resolution);
        }
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        int currenResolutionIndex = 0;
        for(int i = 0; i < resolutions.Count(); i++)
        {
            string option = string.Format("{0} x {1}", resolutions[i].width, resolutions[i].height);
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
            {
                currenResolutionIndex = i;
            }
        }
        dropdown.AddOptions(options);
        dropdown.value = currenResolutionIndex;
        dropdown.RefreshShownValue();
    }
    public void OnQuitBtn()
    {
        gameObject.SetActive(false);
    }
    public void SwitchFullScreen()
    {
        Screen.fullScreen = toggle.isOn;
    }
    public void GetResolution()
    {
        var resolution = resolutions[dropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
