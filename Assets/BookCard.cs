using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookCard : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;

    public void SetInfo(Sprite sprite, string title)
    {
        image.sprite = sprite;
        nameText.text = title;
    }
}
