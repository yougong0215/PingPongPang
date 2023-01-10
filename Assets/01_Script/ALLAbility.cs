using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ALL
{
    PlayerSpeed,
    PlayerSize,
    BallSpeed,
    BallSize
}


public class ALLAbility : MonoBehaviour
{
    [SerializeField] public ALL Ability;

    [SerializeField] float Value;

    public float ValueReturn()
    {
        return Value;
    }
}
