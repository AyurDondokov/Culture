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
    private bool isCanAttackPlayer = true;
    private bool isPlayer = true;

    [Header("Автор, Историческая личность, Герой произведения")]
    public string role = "Автор"; // Историческая личность, Герой произведения

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        if (!photonView.IsMine)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
            isPlayer = true;
        }
    }

    private void FixedUpdate()
    {
        if (!isAttack)
        {
            if (zone != null)
            {
                direction = Mathf.Abs(transform.position.x - zone.transform.position.x) < 0.2f ?
                    (photonView.IsMine ? Vector3.up : Vector3.down) : 
                    Vector3.right * (zone.transform.position.x > transform.position.x ? 1 : -1);
            }
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Zone>() != null)
        {
            zone = collision.GetComponent<Zone>();
        }
        else if (collision.GetComponent<Player>() != null && isCanAttackPlayer)
        {
            collision.GetComponent<Player>().TakeDamage();
            isCanAttackPlayer = false;
        }
        else if (!isAttack && collision.GetComponent<Entity>() != null)
        {
            StartCoroutine(AttackEnemy(collision.GetComponent<Entity>()));
        } 
    }

    IEnumerator AttackEnemy(Entity enemy)
    {
        isAttack = true;
        while (enemy != null)
        {
            enemy.TakeDamage(damage + ((enemy.role == "Автор" && role == "Историческая личность" ||  enemy.role == "Герой произведения" && role == "Автор" || enemy.role == "Автор" && role == "Историческая личность") ? 1 : 0));
            yield return new WaitForSeconds(1f);
        }
        isAttack = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
            stream.SendNext(damage);
            stream.SendNext(speed);
            stream.SendNext(isAttack);
            stream.SendNext(role);
        }
        else
        {
            health = (int)stream.ReceiveNext();
            damage = (int)stream.ReceiveNext();
            speed = (int)stream.ReceiveNext();
            isAttack = (bool)stream.ReceiveNext();
            role = (string)stream.ReceiveNext();
        }
    }
}
