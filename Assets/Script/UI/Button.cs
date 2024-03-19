using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    private TextMeshProUGUI text;
    public Color color;
    private Color init;
    public void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    private void OnMouseEnter()
    {
        print("hover");
        init = text.color;
        text.color = color;
    }
    private void OnMouseExit()
    {
        text.color = init;
    }
}
