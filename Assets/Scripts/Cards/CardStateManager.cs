using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStateManager : MonoBehaviour
{
    [SerializeField] CardVisual cardVisual;
    [SerializeField] CardData cardData;

    [Header("State References")]

    [SerializeField] bool isFaceUp;
    [SerializeField] int currentATK;
    [SerializeField] int currentDEF;

    private void OnEnable()
    {
        SetInitialCardState();
        cardVisual.SetInitialCardVisual(cardData, isFaceUp);
    }

    void SetInitialCardState()
    {
        SetCurrentATKValue(cardData.attackValue);
        SetCurrentDEFValue(cardData.defenceValue);
    }

    public void SetCurrentATKValue(int newValue)
    {
        currentATK = cardData.attackValue;
    }

    public void SetCurrentDEFValue(int newValue)
    {
        currentDEF = cardData.defenceValue;
    }

    public void FlipCard() => cardVisual.FlipCard();

}
