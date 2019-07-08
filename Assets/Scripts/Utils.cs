using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utils : MonoBehaviour
{
    private static Utils _this;

    void Awake()
    {
        _this = this;
    }

    public static IEnumerator ScaledInvoke(Action callback, float delay)
    {
        return _this.InternalScaledInvoke(callback, delay);
    }

    private IEnumerator InternalScaledInvoke(Action callback, float delay)
    {
        IEnumerator coroutine = InternalInvoke(callback, delay);
        StartCoroutine(coroutine);
        return coroutine;
    }

    private IEnumerator InternalInvoke(Action callback, float delay)
    {
        while (delay >= 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
        callback();
    }

}


public static class EnumerableExtension
{
    public static T PickRandom<T>(this IEnumerable<T> source)
    {
        return source.PickRandom(1).Single();
    }

    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
    {
        return source.Shuffle().Take(count);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }
}