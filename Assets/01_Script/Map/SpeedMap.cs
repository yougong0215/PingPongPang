using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedMap : MonoBehaviour
{
    [SerializeField] public static float MapSpeed = 1;
    [SerializeField] public static bool Trickstar;
    [SerializeField] public TextMeshPro tmp;
    [SerializeField] public TextMeshPro Persent;
    float time = 2;
    float o;

    
    private void OnEnable()
    {
        MapSpeed = 1;
        Trickstar = true;
    }
    public void Update()
    {
        if(transform.parent.name == "bitcoin_icon")
        {
            time -= Time.deltaTime;

            if (time < 0)
            {
                o = Random.Range(0.7f, 1.3f);

                time = Mathf.Abs(o) * 5;
                if (time < 2)
                {
                    time = 2;
                }

                MapSpeed = o;

                Persent.text = $"{(int)((o - 1) * 100)}%";
            }
            tmp.text = $"0{time.ToString("F1")}{Random.Range(0, 10)}";
        }
        else
        {
            Trickstar = true;
        }
    }

}
