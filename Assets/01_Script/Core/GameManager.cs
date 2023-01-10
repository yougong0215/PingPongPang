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

    [Header("TeamAll")]
    [SerializeField] public List<ALLAbility> All = new List<ALLAbility>();
    CardList cl;
    MapList map;

    public static bool AIMod = false;
    int A_WinScore = 0;
    int B_WinScore = 0;

    bool A_RoundWin = false;
    bool B_RoundWin = false;

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

    public void GameInit1()
    {
        GameManager.Instance.Reset();
        cl.gameObject.SetActive(true);

        
        StartCoroutine(cl.CardSelect(PlayerEnum.A, true));
    }

    public void GameSet2()
    {
        StartCoroutine(GameInit2());
    }

    public IEnumerator GameInit2()
    {
        yield return new WaitForSeconds(0.1f);
        cl.gameObject.SetActive(true);
        StartCoroutine(cl.CardSelect(PlayerEnum.B));
    }

    public void GameStart()
    {
        cl.gameObject.SetActive(false);

        map.Started();
    }

    public void GameSet(PlayerEnum pl)
    {
        map.RoundEnd();

        if (pl == PlayerEnum.A)
        {
            B_WinScore++;
        }
        else
        {
            A_WinScore++;
        }

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

    public IEnumerator TeamSetting(GameObject p)
    {
        for (int i = 0; i < All.Count; i++)
        {
            yield return null;
            switch (All[i].Ability)
            {
                case ALL.PlayerSpeed:
                    if (p.GetComponent<PlayerInterrabter>()) 
                        p.GetComponent<PlayerInterrabter>().Speed *= All[i].ValueReturn();
                    break;
                case ALL.PlayerSize:
                    if (p.GetComponent<PlayerInterrabter>())
                        p.transform.localScale *= All[i].ValueReturn();
                    break;
                case ALL.BallSpeed:
                    if (p.GetComponent<Ball>())
                        p.GetComponent<Ball>().SpeedSetting(All[i].ValueReturn());
                    break;
                case ALL.BallSize:
                    if (p.GetComponent<Ball>())
                        p.transform.localScale *= All[i].ValueReturn();
                    break;
            }
        }
    }
    IEnumerator PlayerListUp(List<BasePlayerAbility> t, PlayerInterrabter p)
    {
        float speed = 5;
        Vector3 size = new Vector3(0.3f, 1.4f, 1);
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
                case PlayerAbilityEnum.Tozaza:
                    p.toza = true;
                    break;
                case PlayerAbilityEnum.MoonGwa:
                    p.MoonGwa = true;
                    break;
                case PlayerAbilityEnum.Twin:
                    p.twin = true;
                    break;
            }
            Debug.Log(speed);
        }
        if (p.twin == true)
        {
            size *= 0.5f;
            speed *= 0.5f;
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
            if (t[i].Ability == BallEnum.Size)
            {
                Debug.Log($"ÀÌ°Å {t[i].Size()}");
            }
            b.Setting(t[i].Ability, t[i].Speed(), t[i].Size(), t[i].angle().x, t[i].angle().y);
            yield return null;
        }

        if (pl._playerEnum == PlayerEnum.A)
            b.LastSet(1, pl.Angler, pl);
        else
            b.LastSet(-1, pl.Angler, pl);


        if (pl.twin == true)
        {
            pl.TOZAZA += 0.25f * 0.5f;
        }
        else
        {
            pl.TOZAZA += 0.25f;
        }
    }
}
