using System;
using UnityEngine;

[CreateAssetMenu(menuName = "LaserObject")]
public class LaserObject : ScriptableObject
{
    [SerializeField] private int bounces;

    [SerializeField] private Material laserColor;

    public bool GetBounce()
    {
        if (bounces > 0)
        {
            bounces -= 1;
            return true;
        }
        else if (bounces == -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}