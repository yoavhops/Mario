using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBlock : MonoBehaviour , IEquatable<LandBlock>
{
    [SerializeField]
    private int _x;
    [SerializeField]
    private int _y;

    public static LandBlock LandBlockFactory(LandBlock landBlockPrefab, int x, int y, Transform parent)
    {
        var landBlock = Instantiate(landBlockPrefab);
        landBlock.Init(x, y, parent);
        return landBlock;
    }

    void Init(int x, int y, Transform parent)
    {
        _x = x;
        _y = y;
        transform.parent = parent;
        transform.localPosition = new Vector3(x, y);
    }











    public override bool Equals(object obj)
    {
        return this.Equals(obj as LandBlock);
    }

    public bool Equals(LandBlock p)
    {
        // If parameter is null, return false.
        if (System.Object.ReferenceEquals(p, null))
        {
            return false;
        }

        // Optimization for a common success case.
        if (System.Object.ReferenceEquals(this, p))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (this.GetType() != p.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (_x == p._x) && (_y == p._y);
    }

    public override int GetHashCode()
    {
        return _x * 10000 + _y * 10;
    }

    public static bool operator ==(LandBlock lhs, LandBlock rhs)
    {
        // Check for null on left side.
        if (System.Object.ReferenceEquals(lhs, null))
        {
            if (System.Object.ReferenceEquals(rhs, null))
            {
                // null == null = true.
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }

    public static bool operator !=(LandBlock lhs, LandBlock rhs)
    {
        return !(lhs == rhs);
    }


}
