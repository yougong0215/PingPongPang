using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerAbility : MonoBehaviour
{
    [SerializeField] public PlayerAbilityEnum Ability = PlayerAbilityEnum.None;

    [SerializeField] float _speed = 0;
    [SerializeField] Vector2 _size = new Vector2(0,0);
    [SerializeField] float Angler = 0.15f;
    public virtual float Speed()
    {
        return _speed;
    }
    public virtual Vector2 Size()
    {
        return _size;
    }
    public virtual float Anglers()
    {
        return Angler;
    }
}
