using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityCard
{
    [Header("카드 오브잭트")]
    public Card _cardObj;

    public string NameExplain;
    public Sprite cardImg;
    public Sprite ItemImg;
    [TextArea]
    public string Explain;
    [Header("중복 유무")]
    public bool Overlap = false;

    [Header("카드 중복 겟수 ( 높을수록 나올확률 증가 )")]
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
    [SerializeField] public List<AbilityCard> _cardListed = new List<AbilityCard>();

    [SerializeField] GameObject A;
    [SerializeField] GameObject B;
    [SerializeField] GameObject C;

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
    Transform ts;

    private void Awake()
    {
        ts = GameObject.Find("Toilet").transform;
        ts.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        A.transform.GetChild(0).transform.parent = ts;
        B.transform.GetChild(0).transform.parent = ts;
        C.transform.GetChild(0).transform.parent = ts;
    }

    public void GameEnd()
    {
        Destroy(this);
    }


    public IEnumerator CardSelect(PlayerEnum pl, bool Choose = false)
    {

        for (int i = 0; i < _cardList.Count; i++)
        {
            for (int j = 0; j < _cardList[i].CardLuck; j++)
            {
                yield return null;
                _cardListed.Add(_cardList[i]);
            }
        }
        yield return null;
        _cardListed = GetShuffleList<AbilityCard>(_cardListed);
        yield return null;
        GameObject obj = null;
        obj = Instantiate(_cardListed[0]._cardObj.gameObject, A.transform);
        obj.GetComponent<Card>().Set(_cardListed[0].NameExplain, _cardListed[0].ItemImg, _cardListed[0].cardImg, _cardListed[0].Explain, pl, _cardListed[0].ItemImg,Choose);
        yield return null;
        obj = Instantiate(_cardListed[1]._cardObj.gameObject, B.transform);
        obj.GetComponent<Card>().Set(_cardListed[1].NameExplain, _cardListed[1].ItemImg, _cardListed[1].cardImg, _cardListed[1].Explain, pl, _cardListed[1].ItemImg, Choose);
        yield return null;
        obj = Instantiate(_cardListed[2]._cardObj.gameObject, C.transform);
        obj.GetComponent<Card>().Set(_cardListed[2].NameExplain, _cardListed[2].ItemImg, _cardListed[2].cardImg, _cardListed[2].Explain, pl, _cardListed[2].ItemImg,Choose);

        _cardListed.Clear();

        
    }


}
