using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapList : MonoBehaviour
{
    [Header("게임 배이스")]
    [SerializeField] GameObject BaseGame;

    [SerializeField] List<GameObject> _mapList = new List<GameObject>();


    GameObject map;

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
        GameObject t = Instantiate(BaseGame);
        t.transform.position = transform.position;


        _mapList = GetShuffleList(_mapList);

        map = _mapList[0];

        _mapList.Remove(_mapList[0]);
    }

    public void SelectEnd()
    {
        Destroy(map);
    }


}
