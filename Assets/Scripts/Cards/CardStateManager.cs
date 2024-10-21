using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStateManager : MonoBehaviour
{
    public enum CardMainStates { inHand, inPlay }
    public enum CardSubStates { inactive, hovered, beingDragged, dropped }

    [SerializeField] CardVisual cardVisual;
    [SerializeField] CardData cardData;

    [Header("Core State")]
    public CardMainStates cardMainState;
    public CardSubStates cardSubState;
    [SerializeField] bool isFaceUp;

    [Header("In Play State")]
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
