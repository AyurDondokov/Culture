using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject BookPanel;

    public void ToggleBookPanel()
    {
        if (BookPanel != null)
        {
            BookPanel.SetActive(!BookPanel.activeSelf);
        }
    }
}
