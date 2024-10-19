using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class CardVisual : MonoBehaviour
{
    public CardData cardData;

    [SerializeField] GameObject facedownContainer;
    [SerializeField] Sprite portraitCardBack;
    [SerializeField] Sprite horizontalCardBack;
    [SerializeField] Animator animator;

    [Header("State")]
    [SerializeField] GameObject currentVisualContainer;
    bool isFaceUp;

    [Header("Visual Containers")]
    [SerializeField] GameObject majorHeroContainer;
    [SerializeField] GameObject minorHeroContainer;
    [SerializeField] GameObject actionContainer;
    [SerializeField] GameObject missionContainer;
    [SerializeField] GameObject featureContainer;
    [SerializeField] GameObject obstacleContainer;

    [Header("Visual References")]
    [SerializeField] Image portrait;
    [SerializeField] Image frame;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI subtitle;
    [SerializeField] TextMeshProUGUI atk;
    [SerializeField] TextMeshProUGUI def;
    [SerializeField] TextMeshProUGUI cardType;
    [SerializeField] TextMeshProUGUI alignment;
    [SerializeField] TextMeshProUGUI subtype;
    [SerializeField] TextMeshProUGUI cardText;
    [SerializeField] TextMeshProUGUI flavourText;
    [SerializeField] TextMeshProUGUI cardID;

    [Header("Frame References")]
    [SerializeField] Sprite majorHeroFrameGood;
    [SerializeField] Sprite majorHeroFrameEvil;
    [SerializeField] Sprite minorHeroFrameGood;
    [SerializeField] Sprite minorHeroFrameEvil;
    [SerializeField] Sprite actionFrameGood;
    [SerializeField] Sprite actionFrameEvil;
    [SerializeField] Sprite missionFrame;
    [SerializeField] Sprite featureFrame;
    [SerializeField] Sprite obstacleFrame;

    public void SetInitialCardVisual(CardData startingCardData, bool faceUpState)
    {
        cardData = startingCardData;
        isFaceUp = faceUpState;

        if(currentVisualContainer == null)
            RegisterVisualRefs();

        LoadCard();
    }

    #region Setup
    void RegisterVisualRefs()
    {
        //set current container
        switch (cardData.cardType)
        {
            case CardData.cardTypes.majorHero:
                currentVisualContainer = majorHeroContainer;
                break;

            case CardData.cardTypes.minorHero:
                currentVisualContainer = minorHeroContainer;
                break;

            case CardData.cardTypes.action:
                currentVisualContainer = actionContainer;
                break;

            case CardData.cardTypes.feature:

                if (cardData.cardSubtype == CardData.cardSubtypes.obstacle)
                    currentVisualContainer = obstacleContainer;
                else
                    currentVisualContainer = featureContainer;

                break;

            case CardData.cardTypes.mission:
                currentVisualContainer = missionContainer;
                break;
        }

        currentVisualContainer.SetActive(true);

        VisualRefContainer refContainer = currentVisualContainer.GetComponent<VisualRefContainer>();

        portrait = refContainer.portraitRef;
        frame = refContainer.frameRef;
        icon = refContainer.iconRef;
        title = refContainer.titleRef;
        subtitle = refContainer.subtitleRef;
        atk = refContainer.atkRef;
        def = refContainer.defRef;
        cardType = refContainer.cardTypeRef;
        alignment = refContainer.alignmentRef;
        subtype = refContainer.subtypeRef;
        cardText = refContainer.cardTextRef;
        flavourText = refContainer.flavourTextRef;
        cardID = refContainer.cardIDRef;

        //card back
        Image facedownImage = facedownContainer.GetComponent<Image>();

        if (cardData.cardType == CardData.cardTypes.mission)
            facedownImage.sprite = horizontalCardBack;
        else
            facedownImage.sprite = portraitCardBack;

    }

    void LoadCard()
    {
        SetCardImagery();
        SetCardText();
        SetCardFlip();
    }
    
    void SetCardImagery()
    {
        portrait.sprite = cardData.cardPortrait;      
        
        //icon
        if(isHero())
            icon.sprite = cardData.cardIcon;

        //frame
        switch(cardData.cardType)
        {
            case CardData.cardTypes.majorHero:
                if (cardData.cardAlignment == CardData.cardAlignments.good)
                    frame.sprite = majorHeroFrameGood;
                else
                    frame.sprite = majorHeroFrameEvil;
                break;

            case CardData.cardTypes.minorHero:
                if (cardData.cardAlignment == CardData.cardAlignments.good)
                    frame.sprite = minorHeroFrameGood;
                else
                    frame.sprite = minorHeroFrameEvil;
                break;

            case CardData.cardTypes.action:
                if (cardData.cardAlignment == CardData.cardAlignments.good)
                    frame.sprite = actionFrameGood;
                else
                    frame.sprite = actionFrameEvil;
                break;

            case CardData.cardTypes.mission:
                frame.sprite = missionFrame;
                break;

            case CardData.cardTypes.feature:
                if (cardData.cardSubtype == CardData.cardSubtypes.obstacle)
                    frame.sprite = obstacleFrame;

                else
                    frame.sprite = featureFrame;
                break;
        }
    }

    void SetCardText()
    {
        //title
        title.text = cardData.cardTitle;

        //subtitle
        if (cardData.cardSubtitle != "")
            subtitle.text = cardData.cardSubtitle;   

        else 
            subtitle.text = "";

        //type
        switch(cardData.cardType)
        {
            case CardData.cardTypes.majorHero:
                cardType.text = "Major Hero";
                break;

            case CardData.cardTypes.minorHero:
                cardType.text = "Minor Hero";
                break;

            /*case CardData.cardTypes.action:
                if(cardData.cardSubtype == CardData.cardSubtypes.combatAction)
                    cardType.text = "Combat Action";

                else if (cardData.cardSubtype == CardData.cardSubtypes.exhaustAction)
                    cardType.text = "Exhaust Action";

                else cardType.text = "Free Action";
                break;*/

            case CardData.cardTypes.feature:
                cardType.text = "Feature: ";
                break;

            case CardData.cardTypes.mission:
                if (cardData.cardSubtype == CardData.cardSubtypes.expertMission)
                    cardType.text = "Expert Mission";

                else if (cardData.cardSubtype == CardData.cardSubtypes.mutualMission)
                    cardType.text = "Mutual Mission";

                else if (cardData.cardSubtype == CardData.cardSubtypes.soloMission)
                    cardType.text = "Solo Mission";

                else cardType.text = "Training Mission";
                break;
        }
        

        //alignment
        if (cardData.cardAlignment != CardData.cardAlignments.neutral)
        {
            if(cardData.cardType == CardData.cardTypes.majorHero)
                alignment.text = "• " + cardData.cardAlignment.ToString();

            else
                alignment.text = cardData.cardAlignment.ToString();
        }           
        
        else 
            alignment.text = "";

        //subtype
        subtype.text = SubtypeString();
        

        //flavour text
        if (cardData.flavourText != "") 
            flavourText.text = cardData.flavourText;

        else
            flavourText.text = "";

        //ATK and DEF
        if (isHero())
        {
            atk.text = cardData.attackValue.ToString();
            def.text = cardData.defenceValue.ToString();
        }

        //cardID
        cardID.text = cardData.cardSet.ToString() + "_" + cardData.cardNumber;

        //card text
        cardText.text = cardData.cardText;
    }
    #endregion

    #region Runtime Management
    public void SwapCard(CardData newCardData)
    {
        currentVisualContainer.SetActive(false);
        cardData = newCardData;
        RegisterVisualRefs();
        LoadCard();
    }
    #endregion

    #region Flipping
    public void FlipCard()
    {
        animator.SetBool("isFlipping", true);
    }

    public void FlipCardVisual()
    {
        if (isFaceUp)
            isFaceUp = false;

        else
            isFaceUp = true;

        SetCardFlip();
    }

    void SetCardFlip()
    {
        if (isFaceUp)
        {
            currentVisualContainer.SetActive(true);
            facedownContainer.SetActive(false);
        }


        else
        {
            currentVisualContainer.SetActive(false);
            facedownContainer.SetActive(true);
        }

        animator.SetBool("isFaceUp", isFaceUp);
        animator.SetBool("isFlipping", false);
    }
    #endregion

    #region Variables and Checks
    bool isHero()
    {
        if (cardData.cardType == CardData.cardTypes.majorHero || cardData.cardType == CardData.cardTypes.minorHero || cardData.cardSubtype == CardData.cardSubtypes.obstacle)
            return true;

        else
            return false;
    }

    string SubtypeString()
    {
        //subtype
        string subtypeString = "";
        switch (cardData.cardSubtype)
        {
            //heroes
            case CardData.cardSubtypes.dracoTitan:
                subtypeString = "• Draco-Titan";
                break;

            case CardData.cardSubtypes.gaiaTitan:
                subtypeString = "• Gaia-Titan";
                break;

            case CardData.cardSubtypes.hectoTitan:
                subtypeString = "• Hecto-Titan";
                break;

            case CardData.cardSubtypes.kronoTitan:
                subtypeString = "• Krono-Titan";
                break;

            case CardData.cardSubtypes.legendaryTitan:
                subtypeString = "• Legendary Titan";
                break;

            case CardData.cardSubtypes.lithoTitan:
                subtypeString = "• Litho-Titan";
                break;

            case CardData.cardSubtypes.mesoTitan:
                subtypeString = "• Meso-Titan";
                break;

            case CardData.cardSubtypes.swaraTitan:
                subtypeString = "• Swara-Titan";
                break;

            case CardData.cardSubtypes.yamaTitan:
                subtypeString = "• Yama-Titan";
                break;

            case CardData.cardSubtypes.seeker:
                subtypeString = "• Seeker";
                break;

            //actions
            case CardData.cardSubtypes.combatAction:
                subtypeString = "Combat Action";
                break;

            case CardData.cardSubtypes.exhaustAction:
                subtypeString = "Exhaust Action";
                break;

            case CardData.cardSubtypes.freeAction:
                subtypeString = "Free Action";
                break;

            //missions
            case CardData.cardSubtypes.expertMission:
                subtypeString = "Expert Mission";
                break;

            case CardData.cardSubtypes.mutualMission:
                subtypeString = "Expert Mission";
                break;

            case CardData.cardSubtypes.soloMission:
                subtypeString = "Expert Mission";
                break;

            case CardData.cardSubtypes.trainingMission:
                subtypeString = "Expert Mission";
                break;

            case CardData.cardSubtypes.objects:
                subtypeString = "Object";
                break;

            case CardData.cardSubtypes.obstacle:
                subtypeString = "Obstacle";
                break;

            case CardData.cardSubtypes.structure:
                subtypeString = "Structure";
                break;

            default:
                subtype.text = "";
                break;
        }

        return subtypeString;
    }
    #endregion
}
