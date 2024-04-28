using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[Serializable]
public class CardInfo
{
    public Sprite sprite;
    public string name;
    public string description;
}

public class CardBook : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private GameObject CardInfoPanel;
    [SerializeField] private CardInfo[] cardInfos;
    public void OpenCardInfo(int i)
    {
        image.sprite = cardInfos[i].sprite;
        title.text = cardInfos[i].name;
        description.text = cardInfos[i].description;
        CardInfoPanel.SetActive(true);
    }
    public void CloseCardInfo()
    {
        CardInfoPanel.SetActive(false);
    }
}
