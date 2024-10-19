
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardData : ScriptableObject
{
    public enum cardTypes { majorHero, minorHero, action, mission, feature }
    public enum cardSubtypes { none, seeker, dracoTitan, gaiaTitan, hectoTitan, kronoTitan, lithoTitan, mesoTitan, swaraTitan, yamaTitan, legendaryTitan, combatAction, freeAction, exhaustAction, obstacle, objects, structure, trainingMission, mutualMission, expertMission, soloMission}
    public enum cardAlignments { good, evil, neutral }
    public enum cardSets { SAS, LGS, OAL, PRO }


    [Header("Key Data")]
    public string cardTitle;
    public string cardSubtitle;
    public cardTypes cardType;
    public cardSubtypes cardSubtype;
    public cardAlignments cardAlignment;
    public int attackValue;
    public int defenceValue;

    [Header("Visuals")]
    public Sprite cardVisual;
    public Sprite cardPortrait;
    public Sprite cardIcon;

    [Header("Text")]
    [TextArea] public string cardText;
    [TextArea] public string flavourText;

    [Header("Extra Info")]
    public cardSets cardSet;
    public string cardNumber;

    [Header("Keywords")]
    public List<KeywordData> keywords = new List<KeywordData>();
}
