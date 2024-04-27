using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckData : MonoBehaviour
{
    [SerializeField] private List<int> startCardIds = new List<int>(); 
    public static List<int> cardIds = new List<int>();

    private void Start()
    {
        cardIds = startCardIds;
    }
    public void AddCard(int id)
    {
        cardIds.Add(id);
    }
    public void RemoveCard(int id)
    {
        if (cardIds.Contains(id))
        {
            cardIds.Remove(id);
        }
        else
        {
            Debug.Log("You don't have this card");
        }
    }
}
