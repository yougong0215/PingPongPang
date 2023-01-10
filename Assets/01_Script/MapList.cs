using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapList : MonoBehaviour
{
    [Header("게임 배이스")]
    [SerializeField] GameObject BaseGame;

    [SerializeField] List<GameObject> _mapList = new List<GameObject>();

    public static int MapSpeed = 1;

    float t;

    private void Start()
    {
        GameManager.Instance.GameInit1();
    }


    GameObject map;
    GameObject Base;

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
        this.t = 30;
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

        //_mapList.Remove(_mapList[0]);
    }

    private void Update()
    {
        t -= Time.deltaTime;
        if(t < 0)
        {
            MapSpeed = 3;
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
