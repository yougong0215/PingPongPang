using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;


public class GameManager : Singleton<GameManager>
{
    [Header("Player A")]
    [SerializeField] public List<BaseBallAbility> a_ability1 = new List<BaseBallAbility>();
    [SerializeField] public List<BasePlayerAbility> a_ability2 = new List<BasePlayerAbility>();


    [Header("Player B")]
    [SerializeField] public List<BaseBallAbility> b_ability1 = new List<BaseBallAbility>();
    [SerializeField] public List<BasePlayerAbility> b_ability2 = new List<BasePlayerAbility>();

    CardList cl;
    MapList map;
   

    int A_WinScore = 0;
    int B_WinScore = 0;

    public Sprite A;
    public Sprite B;


    public void Reset()
    {

        cl = GameObject.FindObjectOfType<CardList>(true);
        map = GameObject.FindObjectOfType<MapList>();

        A = FindObjectOfType<CharacterSelect>().GetA();
        B = FindObjectOfType<CharacterSelect>().GetB();

        A_WinScore = 0;
        B_WinScore = 0;
        a_ability1.Clear();
        a_ability2.Clear();
        b_ability1.Clear();
        b_ability2.Clear();
    }

    public void GameStart()
    {
        cl.gameObject.SetActive(false);

        map.Started();
    }

    public void GameSet(PlayerEnum pl)
    {
        map.RoundEnd();

        cl.gameObject.SetActive(true);
        StartCoroutine(cl.CardSelect(pl));
        
    }



    public void PlayerSetting(PlayerEnum pl, PlayerInterrabter p)
    {
        switch (pl)
        {
            case PlayerEnum.A:
                StartCoroutine(PlayerListUp(a_ability2, p));
                break;
            case PlayerEnum.B:
                StartCoroutine(PlayerListUp(b_ability2, p));
                break;
        }
    }
    public void SetAbility(PlayerEnum pl, Ball b, PlayerInterrabter p)
    {
        switch (pl)
        {
            case PlayerEnum.A:
                StartCoroutine(BallListUp(a_ability1, b, p));
                break;
            case PlayerEnum.B:
                StartCoroutine(BallListUp(b_ability1, b, p));
                break;
        }

    }
    IEnumerator PlayerListUp(List<BasePlayerAbility> t, PlayerInterrabter p)
    {
        float speed = 1;
        Vector3 size = new Vector3(0.3f, 0.8f);
        float Anger = 0.15f;
        for (int i = 0; i < t.Count; i++)
        {
            yield return null;
            switch (t[i].Ability)
            {
                case PlayerAbilityEnum.None:
                    Debug.Log($"[ {i} ] : None Inter");
                    break;
                case PlayerAbilityEnum.Size:
                    size *= t[i].Size();
                    break;
                case PlayerAbilityEnum.Speed:
                    speed *= t[i].Speed();
                    break;
                case PlayerAbilityEnum.SizeOne:
                    size.x *= t[i].SizeXY().x;
                    break;
                case PlayerAbilityEnum.SizeTwe:
                    size.y *= t[i].SizeXY().y;
                    break;
                case PlayerAbilityEnum.Angler:
                    Anger += t[i].Anglers();
                    break;
            }
        }
        if (t.Count > 0)
        {
            p.transform.localScale = size;
            p.Angler = Anger;
            p.SpeedSetting(speed);
            
        }
    }
    IEnumerator BallListUp(List<BaseBallAbility> t, Ball b, PlayerInterrabter pl)
    {
        b.Reset();
        for (int i = 0; i < t.Count; i++)
        {
            if(t[i].Ability == BallEnum.Size)
            {
                Debug.Log($"ÀÌ°Å {t[i].Size()}");
            }
            b.Setting(t[i].Ability, t[i].Speed(), t[i].Size(), t[i].angle().x, t[i].angle().y);
            yield return null;
        }

        if (pl._playerEnum == PlayerEnum.A)
            b.LastSet(1, pl.Angler);
        else
            b.LastSet(-1, pl.Angler);
    }
}