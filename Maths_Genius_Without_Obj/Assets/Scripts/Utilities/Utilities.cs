using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    private static System.Random random = new System.Random();

    /// <summary>
    /// Shuffles the elements of a list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="list">The list to shuffle.</param>
    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static T GetRandomElement<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new ArgumentException("The list is null or empty.");
        }

        int randomIndex = random.Next(list.Count);
        return list[randomIndex];
    }

    public static int GetRandomNumber(int min, int max)
    {
        return UnityEngine.Random.Range(min, max + 1);
    }
}
