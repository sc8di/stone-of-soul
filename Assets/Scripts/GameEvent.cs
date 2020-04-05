using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public const string HEALTH_UPDATED = "HEALTH_UPDATED";
    public const string INVENTORY_REFRESH = "INVENTORY_REFRESH";
    public const string LEVEL_COMPLETE = "LEVEL_COMPLETE";
    public const string LEVEL_FAILED = "LEVEL_FAILED";
    public const string GAME_COMPLETE = "GAME_COMPLETE";
    public const string RETURN_TO_CHECKPOINT = "RETURN_TO_CHECKPOINT";
    public const string SCORE_UPDATED = "SCORE_UPDATED";
}
