using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMenu : MonoBehaviour
{
    public GameObject SaveSlots;
    public GameObject Confirmation;
    private delegate void ConfirmAction();
    private ConfirmAction confirm;
    private bool IsConfirming;
    public void OnQuitBtn()
    {
        if (IsConfirming)
        {
            Confirmation.SetActive(false);
            SaveSlots.SetActive(true);
        }
        gameObject.SetActive(false);
    }
    public void OpenConfirmation(int i)
    {
        var desc = Confirmation.transform.Find("Description").GetComponent<TextMeshProUGUI>();
        if (MainMenu.saves[i-1] == "")
        {
            desc.text = "Create new save file?";
            confirm = CreateNewSave;
        }
        else
        {
            desc.text = "Overwrite save file?";
            confirm = OverwriteSave;
        }
        SaveSlots.SetActive(false);
        Confirmation.SetActive(true);
        IsConfirming = true;
    }
    public void ConfirmationYes()
    {
        confirm();
        SceneManager.LoadScene("MainScene");
    }
    public void ConfirmationNo()
    {
        SaveSlots.SetActive(true);
        Confirmation.SetActive(false);
        IsConfirming = false;
    }
    private void CreateNewSave()
    {
        print("new save");
    }
    private void OverwriteSave()
    {
        print("overwrite save");
    }
}
