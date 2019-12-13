using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameDifficult {
    Easy, Medium, Hard,
    Count
}

public static class Settings
{
    public static GameDifficult CurrentGameDifficult { get; set; }

}
