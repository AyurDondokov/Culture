using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button FindGameBtn;

    [SerializeField] private string region;

    private void Start()
    {
        PhotonNetwork.NickName = "Player-" + Random.Range(1000, 9999);  
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(region);
    }

    public void CreateGame()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("game", roomOptions, TypedLobby.Default);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room: " + PhotonNetwork.CurrentRoom);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Can't create room");
    }
    public override void OnConnectedToMaster()
    {
        FindGameBtn.interactable = true;
        Debug.Log("Connected to " + PhotonNetwork.CloudRegion);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("JoinedRoom");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            SceneManager.LoadScene("Game");
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            SceneManager.LoadScene("Game");
        }
    }

}
