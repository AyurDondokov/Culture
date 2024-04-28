using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField] DeckData deckData;
    [SerializeField] Animator animator;
    int score = 0;
    public void ClickOnChest()
    {
        if (score <= 80)
        {
            transform.localScale += Vector3.one * 0.02f;
            score += 10;
            if (score > 80)
            {
                ChestOpen();
            }
        }

    }
    public void ChestOpen()
    {
        animator.SetTrigger("Open");
        deckData.AddCard(0);
    }
}
