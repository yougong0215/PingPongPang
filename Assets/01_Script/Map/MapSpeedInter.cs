using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpeedInter : MonoBehaviour
{
    public static float Speed = 1;


    [SerializeField] float localspeed = 1;
    [SerializeField] float HitNum = 3;
    private void OnEnable()
    {
        Speed = 1;
        localspeed = 1;
    }

    public void HitGimik(PlayerInterrabter pl)
    {
        HitNum--;

        if(HitNum < 0)
        {
            pl.Speed = localspeed;
        }
    }

    

}
