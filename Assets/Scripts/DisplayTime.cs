using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class DisplayTime : MonoBehaviour
{
    DateTime now;
    float timer = 0f;

    void Start()
    {
        now = DateTime.Now;
        GetComponent<TextMeshProUGUI>().text = "" + now.Hour + ":" + now.Minute + ":" + now.Second;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            now = DateTime.Now;
            GetComponent<TextMeshProUGUI>().text = now.Hour.ToString("00") + ":" + now.Minute.ToString("00") + ":" + now.Second.ToString("00");
            timer = 0f;
        }
    }
}
