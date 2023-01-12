using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public CardList cl;
    public MapList map;

    public GameObject UserA;
    public GameObject UserB;

    public static bool AIMod = false;
    int A_WinScore = 0;
    int B_WinScore = 0;

    bool A_RoundWin = false;
    bool B_RoundWin = false;

    public Sprite A;
    public Sprite B;

    UISquence ui;


    public void Reset()
    {

        cl = GameObject.FindObjectOfType<CardList>(true);
        map = GameObject.FindObjectOfType<MapList>();
        ui = GameObject.FindObjectOfType<UISquence>(true);

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
        cl.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        cl.gameObject.SetActive(true);
        StartCoroutine(cl.CardSelect(PlayerEnum.B));
    }

    public void GameStart()
    {
        cl.gameObject.SetActive(false);

        map.Started();


    }

    public void GameSet(PlayerEnum pl)// 진에
    {
        map.RoundEnd();

        A_RoundWin = false;
        B_RoundWin = false;
        if (A_WinScore == 5)
        {
            SceneManager.LoadScene("GameVSMODEWinA");
            return;
        }
        else if (B_WinScore == 5)
        {

            SceneManager.LoadScene("GameVSMODEWinB");
            return;
        }

        cl.gameObject.SetActive(true);
        StartCoroutine(cl.CardSelect(pl)); // 진에 카드고름

    }

    public void A_SpriteAdd(Sprite spi)
    {
        map.PlayerAAdd(spi);
    }

    public void B_SpriteAdd(Sprite spi)
    {
        map.PlayerBAdd(spi);
    }

    public void PlayerRound(PlayerEnum pl) // 진에 들어옴
    {
        ui.gameObject.SetActive(true);

        int a = A_WinScore;
        int b = B_WinScore;

        if (A_RoundWin == true && pl == PlayerEnum.B) // A 가 한번이겻고 진에가 b면
        {
            StartCoroutine(MapEndGame(PlayerEnum.B));
            ui.GameWinA();
            map.PlayerAWin(a);
            A_WinScore++;
            return;
        }
        if(B_RoundWin == true && pl == PlayerEnum.A)
        {
            StartCoroutine(MapEndGame(PlayerEnum.A));
            ui.GameWinB();
            B_WinScore++;
            map.PlayerBWin(b);
            return;
        }

        StartCoroutine(MapAnimation());

        if (A_RoundWin == true && pl == PlayerEnum.A && B_RoundWin == false)
        {
            B_RoundWin = true;
            ui.RoundWinPlayerOther();
            map.PlayerBWin(b);
            return;
        }
        if(B_RoundWin == true && pl == PlayerEnum.B && A_RoundWin == false)
        {
            A_RoundWin = true;
            ui.RoundWinPlayerOther();
            map.PlayerAWin(a);
            return;
        }

        if(pl == PlayerEnum.A)
        {
            ui.RoundWinPlayer2();
            map.PlayerBWin(b);
            B_RoundWin = true;
        }
        else
        {
            ui.RoundWinPlayer1();
            map.PlayerAWin(a);
            A_RoundWin = true;
        }



    }

    public IEnumerator MapEndGame(PlayerEnum T)
    {
        ui.SequenceEnd = false;
        

        yield return new WaitUntil(() => ui.SequenceEnd);
        ui.SequenceEnd = false;
        ui.gameObject.SetActive(false);
        GameSet(T);
    }

    public IEnumerator MapAnimation()
    {
        ui.SequenceEnd = false;


        yield return new WaitUntil(()=> ui.SequenceEnd);

        ui.SequenceEnd = false;
        ui.gameObject.SetActive(false);
        map.RoundEnd();
        map.Started();
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
        }
        Debug.Log($"스피드 : {speed}");
        if(p._playerEnum == PlayerEnum.A && map.PlayAAIMODE == true)
        {
            p.AIMODE = true;
        }
        else
        {
            p.AIMODE = false;
        }

        if (p._playerEnum == PlayerEnum.B && map.PlayBAIMODE == true)
        {
            p.AIMODE = true;
        }
        else
        {
            p.AIMODE = false;
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
                Debug.Log($"이거 {t[i].Size()}");
            }
            b.Setting(t[i].Ability, t[i].Speed(), t[i].Size(), t[i].angle().x, t[i].angle().y);
            yield return null;
        }

        if (pl._playerEnum == PlayerEnum.A)
            b.LastSet(1, pl.Angler, pl);
        else
            b.LastSet(-1, pl.Angler, pl);
        yield return null;

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
