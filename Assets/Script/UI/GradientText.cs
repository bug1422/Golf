using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GradientText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float speed = 1f;
    // Update is called once per frame
    void Update()
    {
        float hue = Time.time * speed % 1f;

        Color color = Color.HSVToRGB(hue, 1f, 1f);

        text.color = color;
    }
}
