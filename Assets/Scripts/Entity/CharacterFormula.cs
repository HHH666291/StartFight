using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterFormula
{
    private static int ClampLevel(int level)
    {
        if (level < 1)
        {
            return 1;
        }
        if (level > 999)
        {
            return 999;
        }
        return level;
    }


    public static int GetMaxHealth(int level)
    {
        level = ClampLevel(level);
        return 8 + (level - 1) * 2;
    }

    public static int GetAttackPower(int level)
    {
        level = ClampLevel(level);
        return 1 + (level - 1) * 2;
    }

    public static int GetExpToNextLevel(int level)
    {
        level = ClampLevel(level);
        return 5 + (level - 1) * 3;
    }

    public static int GetExpReward(int level)
    {
        level = ClampLevel(level);
        return 2 + (level - 1) * 2;
    }

}
