using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.UI;

public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] CanvasGroup canvasGroup;
    Vector2 originalPosition;
    Canvas canvas;
    [SerializeField] CardStateManager cardStateManager;

    void Awake()
    {
        //SetParentCanvas();
    }

    void SetParentCanvas()
    {
        canvas = transform.GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (cardStateManager.IsDraggable())
        {
            originalPosition = rectTransform.position;
            canvasGroup.blocksRaycasts = false;
            cardStateManager.cardSubState = CardStateManager.CardSubStates.beingDragged;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (cardStateManager.cardSubState == CardStateManager.CardSubStates.beingDragged) 
        { 
            rectTransform.position = Input.mousePosition;         
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (cardStateManager.cardSubState == CardStateManager.CardSubStates.beingDragged)
        {
            canvasGroup.blocksRaycasts = true;
            rectTransform.position = originalPosition;
            cardStateManager.cardSubState = CardStateManager.CardSubStates.ready;

            //SetParentCanvas();
        }
    }
}
