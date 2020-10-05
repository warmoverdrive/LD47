using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text textBox;

    private void Start()
    {
        textBox = GetComponent<Text>();
        UpdateText((FindObjectOfType<ScoreController>().GetScore()).ToString());
    }

    public void UpdateText(string text)
    {
        textBox.text = "Score: " + text;
    }
}
