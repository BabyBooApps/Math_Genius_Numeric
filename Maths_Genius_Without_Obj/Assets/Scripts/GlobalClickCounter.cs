using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalClickCounter
{
    public static int clickCount = 0;

    public static int incrementClickCounter()
    {
        return ++clickCount;
    }

    public static void ResetClickCounter()
    {
        clickCount = 0;
    }
}
