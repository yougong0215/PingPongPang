using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerEnum
{
    A,
    B
}

public enum PlayerAbilityEnum
{
    None,
    Size,
    SizeOne,
    SizeTwe,
    Speed,
    Angler,
    Tozaza,
    MoonGwa,
    Twin
}

public class PlayerInterrabter : MonoBehaviour
{
    [Header("PlayerSetting")]
    [SerializeField] public PlayerEnum _playerEnum;

    [Header("KeySetting")]
    [SerializeField] KeyCode Up;
    [SerializeField] KeyCode Down;

    public float TOZAZA = 0.5f;
    public bool toza = false;
    public bool MoonGwa = false;
    public bool twin = false;
    public int twinValue = 1;


    public float Angler = 4f;


    public float Speed = 1;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        StartCoroutine(GameManager.Instance.TeamSetting(gameObject));
        if (twin == false)
            StartCoroutine(twins());

        transform.localScale = new Vector3(0.3f, 1.4f, 1);
        GameManager.Instance.PlayerSetting(_playerEnum, this);
        if(_playerEnum == PlayerEnum.A)
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.A;
        else
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.B;
    }

    IEnumerator twins()
    {
        yield return null;
        yield return null;
        yield return null;
        if (twin == true)
        {
            GameObject obj = Instantiate(gameObject);

            obj.transform.position = transform.position;
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = true;
            obj.GetComponent<PlayerInterrabter>().twinValue = -1;
            obj.transform.parent = transform.parent;
            obj.transform.position += new Vector3(0.01f, 0, 0);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(Up))
            transform.position += new Vector3(0, twinValue) * Speed * Time.deltaTime;
        if (Input.GetKey(Down))
            transform.position += new Vector3(0, -twinValue) * Speed * Time.deltaTime;


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.3f, 4.3f), 0);
    }

    public void SpeedSetting(float speed)
    {
        this.Speed = speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")) // 공에 닿음녀
        {

            SetAbility(collision.gameObject.GetComponent<Ball>());
            Debug.Log("닿음");
            //StartCoroutine(BoxColliderONOFF());
        }
    }

    void SetAbility(Ball b)
    {
        GameManager.Instance.SetAbility(_playerEnum, b, this);
    }
}
