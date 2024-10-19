using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCard : MonoBehaviour
{
    [SerializeField] CardVisual cardController;
    public List<CardData> cardList = new List<CardData>();

    public void RandomiseCard()
    {
        int cardNo = Random.Range(0, cardList.Count - 1);

        cardController.SwapCard(cardList[cardNo]);
    }
}
