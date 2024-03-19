using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject saveMenu;
    public GameObject configMenu;
    public static string[] saves = new string[3];
    public void OnStartBtn() 
    {
        //Call get saves function
        //delete this after apply get save function
        var slots = saveMenu.transform.Find("Save Slots");
        for (int i = 0; i < slots.childCount; i++)
        {
            saves[i] = "";
            slots.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = "N/A";
        }
        //
        saveMenu.SetActive(true);
    }

    public void OnConfigBtn()
    {
        configMenu.SetActive(true);
    }

    public void OnQuitBtn()
    {
        Application.Quit();
    }

}
