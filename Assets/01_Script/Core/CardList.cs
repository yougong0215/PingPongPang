using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class AbilityCard
{
    public string NameExplain;
    public Sprite cardImg;
    [TextArea]
    public string Explain;
    [Header("�ߺ� ����")]
    public bool Overlap = false;

    [Header("ī�� �ߺ� �ټ� ( �������� ����Ȯ�� ���� )")]
    public int CardLuck = 1;

    [System.NonSerialized]
    public bool A = false;
    [System.NonSerialized]
    public bool B = false;
    
}
public class CardList : MonoBehaviour
{
    
    [Header("Items")]
    [SerializeField] public List<AbilityCard> _cardList = new List<AbilityCard>();

    [SerializeField] Card A;
    [SerializeField] Card B;
    [SerializeField] Card C;

        public List<T> GetShuffleList<T>(List<T>_list)
    {
        for(int i = _list.Count -1; i>0; i--)
        {
            int rnd = UnityEngine.Random.Range(0, i);
            T temp = _list[i];
            _list[i] = _list[rnd];
            _list[rnd] = temp;
        }
        return _list;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void GameEnd()
    {
        Destroy(this);
    }


    public void CardSelect(PlayerEnum pl)
    {
        _cardList = GetShuffleList(_cardList);

        A.Set(_cardList[0].NameExplain, _cardList[0].cardImg, _cardList[0].Explain, pl);
        B.Set(_cardList[1].NameExplain, _cardList[1].cardImg, _cardList[1].Explain, pl);
        C.Set(_cardList[2].NameExplain, _cardList[2].cardImg, _cardList[2].Explain, pl);
    }


}
