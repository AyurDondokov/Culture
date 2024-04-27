using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Deck : MonoBehaviour
{
    [SerializeField] private string cardPrefabName = "Card_";
    [SerializeField] private Transform firstCardSpot;
    public List<Card> cards = new List<Card>();

    private void Start()
    {
        int cardsCount = DeckData.cardIds.Count;
        for (int i = 0; i < cardsCount; i++)
        {
            Vector3 step = new Vector3(Mathf.Abs(firstCardSpot.position.x*2) / cardsCount, 0, -0.1f);
            Vector3 pos = firstCardSpot.position + step * i;
            Card card = PhotonNetwork.Instantiate(cardPrefabName + DeckData.cardIds[i], pos, Quaternion.identity).GetComponent<Card>();
            card.transform.parent = transform;
            card.deck = this;
            cards.Add(card);
        }
    }
    public void UpdateCardPositions()
    {
        for (int i = 0; i<cards.Count; i++)
        {
            Vector3 step = new Vector3(i*(Mathf.Abs(firstCardSpot.position.x * 2) / cards.Count), 0, -0.1f*i);
            Vector3 pos = firstCardSpot.position + step;
            cards[i].transform.position = pos;
        }
    }

}
