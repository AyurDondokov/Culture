using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI GameoverTitle;
    [SerializeField] private Transform playerSpot;
    [SerializeField] private string PlayerPrefabName = "Player";
    [SerializeField] private Image PlayerHealthImage;
    [SerializeField] private Image EnemyHealthImage;
    [SerializeField] private GameObject GameoverPanel;

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
        GameoverTitle.text = "Поражение";
        GameoverPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void Win()
    {
        GameoverTitle.text = "Победа";
        GameoverPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void MenuBtn()
    {
        SceneManager.LoadScene("Lobby");
    }
}
