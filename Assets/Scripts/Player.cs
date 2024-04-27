using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour, IPunObservable
{
    public GameManager gameManager;
    public int maxHealth = 3;
    public int health = 3;

    private void Start()
    {
        health = maxHealth;
    }
    private void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            gameManager.Lose();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.GetComponent<Entity>();
        if (entity != null)
        {
            PhotonView view = entity.GetComponent<PhotonView>();
            if (!view.IsMine)
            {
                TakeDamage();
                PhotonNetwork.Destroy(view);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int)stream.ReceiveNext();
        }
    }
}
