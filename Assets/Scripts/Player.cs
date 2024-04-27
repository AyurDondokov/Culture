using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour, IPunObservable
{
    public GameManager gameManager;
    public int maxHealth = 3;
    public int health = 3;

    private PhotonView photonView;

    private void Start()
    {
        health = maxHealth;
        photonView = GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager.enemy = this;
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        }
        else
        {
            gameManager.player = this;
        }
    }
    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            gameManager.Lose();
        }
        gameManager.UpdateHealth();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else
        {

        }
    }
}
