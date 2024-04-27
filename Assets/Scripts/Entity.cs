using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class Entity : MonoBehaviour, IPunObservable
{
    public int health = 3;
    public int maxHealth = 3;
    public int damage = 1;
    public int speed = 1;

    private Rigidbody2D rb;
    private PhotonView photonView;
    private Zone zone;
    private Vector2 direction;
    public bool isAttack;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        if (!photonView.IsMine)
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (!isAttack)
        {
            if (zone != null)
            {
                direction = (photonView.IsMine ? Vector3.up : Vector3.down) + Vector3.right * (zone.transform.position - transform.position).normalized.x * 4;
            }
            rb.velocity = direction * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Zone>() != null)
        {
            zone = collision.GetComponent<Zone>();
        }
        else if (collision.GetComponent<Player>() != null)
        {
            collision.GetComponent<Player>().TakeDamage();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
            stream.SendNext(damage);
            stream.SendNext(speed);
            stream.SendNext(isAttack);
        }
        else
        {
            health = (int)stream.ReceiveNext();
            damage = (int)stream.ReceiveNext();
            speed = (int)stream.ReceiveNext();
            isAttack = (bool)stream.ReceiveNext();
        }
    }
}
