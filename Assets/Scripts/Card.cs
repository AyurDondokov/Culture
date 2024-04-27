using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class Card : MonoBehaviour
{
    public int id = 0;
    private PhotonView photonView;
    public Deck deck;

    [SerializeField] private string entityPrefabName;
    private Animator an;
    public enum CardState
    {
        InDeck, Moving
    }
    private CardState state = CardState.InDeck;
    private void Start()
    {
        an = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();

        if (!photonView.IsMine)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }


    private void OnMouseOver()
    {
        if (photonView.IsMine)
            an.SetBool("isHover", true);
    }
    private void OnMouseExit()
    {
        if (photonView.IsMine)
            an.SetBool("isHover", false);
    }

    private void OnMouseDown()
    {
        if (photonView.IsMine)
        {
            an.SetTrigger("isMoving");
            state = CardState.Moving;
            StartCoroutine(MoveToMouse());
        }
    }
    private void OnMouseUp()
    {
        if (photonView.IsMine)
        {
            StopAllCoroutines();
            PhotonNetwork.Instantiate(entityPrefabName + id, transform.position, Quaternion.identity);
            deck.cards.Remove(this);
            deck.UpdateCardPositions();
            PhotonNetwork.Destroy(photonView);
        }
    }

    IEnumerator MoveToMouse()
    {
        while (true) 
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward*5;
            yield return null;
        }
    }
}
