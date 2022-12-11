using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLockScript : MonoBehaviour
{
    public static bool[] lockedLevels = { false, true, true, true };

    public void UpdateLockedLevels(int index)
    {
        lockedLevels[index] = true;
    }
}
