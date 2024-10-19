using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class KeywordData : ScriptableObject
{
    public enum AllKeywords { unblockable, stun, forceful, obstacles, objects }

    public AllKeywords keywordName;
    [TextArea] public string keywordTooltip;
}
