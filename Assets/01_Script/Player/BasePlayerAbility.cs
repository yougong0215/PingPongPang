using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerAbility : MonoBehaviour
{
    [SerializeField] public PlayerAbilityEnum Ability = PlayerAbilityEnum.None;

    [SerializeField] float _speed = 0;
    [SerializeField] Vector2 _size = new Vector2(0,0);
    [SerializeField] float Angler = 0.15f;
    [SerializeField] private float _sizeOne;

    public float Speed()
    {
        return _speed;
    }

    public float Size()
    {
        return _sizeOne;
    }
    public Vector2 SizeXY()
    {
        return _size;
    }
    public float Anglers()
    {
        return Angler;
    }
}
