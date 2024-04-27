using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform playerSpot;
    [SerializeField] private string PlayerPrefabName = "Player";
    [SerializeField] private Image PlayerHealthImage;
    [SerializeField] private Image EnemyHealthImage;

    public Player player;
    public Player enemy;

    private void Start()
    {
        Player p = PhotonNetwork.Instantiate(PlayerPrefabName, playerSpot.position, Quaternion.identity).GetComponent<Player>();
        p.gameManager = this;
    }
    public void UpdateHealth()
    {
        PlayerHealthImage.fillAmount = player.health / (float)player.maxHealth;
        EnemyHealthImage.fillAmount = enemy.health / (float)enemy.maxHealth;
    }

    public void Lose()
    {
        Debug.Log(PhotonNetwork.NickName + " lose battle");
        Time.timeScale = 0.0f;
    }
}
