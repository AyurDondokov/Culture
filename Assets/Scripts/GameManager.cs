using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform playerSpot;
    [SerializeField] private string PlayerPrefabName = "Player";

    private void Start()
    {
        PhotonNetwork.Instantiate(PlayerPrefabName, playerSpot.position, Quaternion.identity);
    }
    public void Lose()
    {

    }
}
