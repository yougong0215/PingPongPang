using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapList : MonoBehaviour
{
    [Header("게임 배이스")]
    [SerializeField] GameObject BaseGame;

    [SerializeField] List<GameObject> _mapList = new List<GameObject>();

    [Header("능력")]
    [SerializeField] public GameObject playerA;
    [SerializeField] public GameObject playerB;
    [SerializeField] public Image baseimg;

    [SerializeField] public Image basewinimg;
    [SerializeField] public GameObject playerAWin;
    [SerializeField] public GameObject playerBWin;

    [SerializeField] public Image panel;

    public static int MapSpeed = 1;

    float t;

    private void Start()
    {
        GameManager.Instance.GameInit1();
    }


    GameObject map;
    GameObject Base;

    public void PlayerAAdd(Sprite spi)
    {
        GameObject obj = Instantiate(baseimg.gameObject, playerA.transform);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = spi;
    }

    public void PlayerBAdd(Sprite spi)
    {
        GameObject obj = Instantiate(baseimg.gameObject, playerB.transform);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = spi;
        obj.transform.localEulerAngles = new Vector3(0, 0, 180);
    }

    public void PlayerAWin()
    {
        GameObject obj = Instantiate(basewinimg.gameObject, playerAWin.transform);
    }

    public void PlayerBWin()
    {
        GameObject obj = Instantiate(basewinimg.gameObject, playerBWin.transform);
        obj.transform.localEulerAngles = new Vector3(0, 0, 180);
    }


    public List<T> GetShuffleList<T>(List<T> _list)
    {
        for (int i = _list.Count - 1; i > 0; i--)
        {
            int rnd = UnityEngine.Random.Range(0, i);
            T temp = _list[i];
            _list[i] = _list[rnd];
            _list[rnd] = temp;
        }
        return _list;
    }

    public void Started()
    {
        StartCoroutine(Startedd());
    }
    public IEnumerator Startedd()
    {

        GameObject t = Instantiate(BaseGame);
        t.transform.position = transform.position;
        Base = t;

        _mapList = GetShuffleList(_mapList);

        yield return null;

        GameObject o = Instantiate(_mapList[0]);

        map = o;
        o.transform.position = transform.position;

        this.t = 60;

        //_mapList.Remove(_mapList[0]);
    }

    private void Update()
    {
        t -= Time.deltaTime;
        Debug.Log(t);
        if (t < 0)
        {
            MapSpeed = 3;
        }
        else if (t < 30)
        {
            MapSpeed = 2;
        }
        else
        {
            MapSpeed = 1;
        }
    }

    public void RoundEnd()
    {
        Destroy(map);
        Destroy(Base);
    }


}
