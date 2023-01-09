using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBallAbility : MonoBehaviour
{
    [SerializeField] public BallEnum Ability = BallEnum.None;


    [SerializeField] float _speed = 0;

    [SerializeField] float _size = 0;
    [SerializeField] Vector2 _angle = new Vector2(0, 0);
    public virtual float Speed()
    {
        return _speed;
    }
    public virtual float Size()
    {
        return _size;
    }
    public virtual Vector2 angle()
    {
        return _angle;
    }
}
