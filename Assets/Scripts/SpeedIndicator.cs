using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIndicator : MonoBehaviour
{
    [SerializeField] private GameObject[] lightnings;
    [SerializeField] private int indicator;

    private void UpdateIndecator()
    {
        for (int i = 0;  i < lightnings.Length; i++)
        {
            lightnings[i].SetActive(indicator - 1 >= i);
        }
    }
    private void Start()
    {
        UpdateIndecator();
    }
}
