using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public bool AIMODE = false;

    [Header("KeySetting")]
    [SerializeField] KeyCode Up;
    [SerializeField] KeyCode Down;

    public bool PlayerInfin = false;

    PlayerInterrabter alter;


    public float TOZAZA = 0.5f;
    public bool toza = false;
    public bool MoonGwa = false;
    public bool twin = false;
    public int twinValue = 1;


    public float Angler = 4f;


    public float Speed = 1;

    public float MapGimicspeed = 1;

    [SerializeField] GameObject Sprite;

    Rigidbody2D _rigid;
    bool Ice = false;

    Camera cam;

    private void OnEnable()
    {

        MapGimicspeed = 1;
        twinValue = 1;
        transform.position = new Vector3(transform.position.x, 0, 0);
        cam = Camera.main;
        StartCoroutine(GameManager.Instance.TeamSetting(gameObject));
        if (twin == false)
            StartCoroutine(twins());

        transform.localScale = new Vector3(0.3f, 1.4f, 1);
        GameManager.Instance.PlayerSetting(_playerEnum, this);
        if (_playerEnum == PlayerEnum.A)
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.A;
        else
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.B;
        _rigid = GetComponent<Rigidbody2D>();

        StartCoroutine(I());
    }

    IEnumerator I()
    {
        yield return new WaitForSeconds(0.2f);
        if (GameObject.Find("12_IceRoad_Map(Clone)"))
        {
            Ice = true;
        }
    }

    IEnumerator twins()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        if (twin == true)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
            GameObject obj = Instantiate(gameObject);

            obj.transform.position = transform.position;
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = true;
            obj.GetComponent<PlayerInterrabter>().twinValue = -1;
            obj.transform.parent = transform.parent;
            obj.transform.position += new Vector3(0.01f, 0, 0);

            alter = obj.GetComponent<PlayerInterrabter>();
        }
    }
    float t = 0;
    List<Ball> ballPos;


    // Update is called once per frame
    void Update()
    {
        if(AIMODE == false)
        {
            if (Ice == false)
            {

                if (Input.GetKey(Up))
                {
                    transform.position += new Vector3(0, twinValue) * MapGimicspeed * Speed * Time.deltaTime;
                    Debug.Log("U[");
                }
                if (Input.GetKey(Down))
                {
                    transform.position += new Vector3(0, -twinValue) * MapGimicspeed * Speed * Time.deltaTime;
                    Debug.Log("Down");
                }
                Debug.Log($"{new Vector3(0, -twinValue)} * {MapGimicspeed} * {Speed}");
            }
            else
            {

                transform.position =
                    Vector3.Lerp(transform.position, transform.position + new Vector3(0, t * twinValue, 0), 10 * Time.deltaTime);
                if (Input.GetKey(Up))
                {
                    t += Time.deltaTime;
                }
                else if (Input.GetKey(Down))
                {
                    t -= Time.deltaTime;
                }

                if (!Input.GetKey(Up) && t > 0)
                {
                    t -= Time.deltaTime;
                }
                if (!Input.GetKey(Down) && t < 0)
                {
                    t += Time.deltaTime;
                }
                if (t > 1)
                {
                    t = 1;
                }
                if (t < -1)
                {
                    t = -1;
                }
            }
        }
        else
        {
            ballPos = FindObjectsOfType<Ball>().ToList();
            for (int i = 1; i < ballPos.Count; i++)
            {
                if (Vector3.Distance(transform.position, ballPos[i - 1].transform.position)
                    < Vector3.Distance(transform.position, ballPos[i].transform.position))
                {
                    Ball b = ballPos[i - 1];
                    ballPos[i - 1] = ballPos[i];
                    ballPos[i] = b;
                }
            }


            if (Ice == false)
            {

                if (ballPos[0].transform.position.y > transform.position.y)
                {
                    transform.position += new Vector3(0, twinValue) * MapGimicspeed * Speed * Time.deltaTime;
                    Debug.Log("U[");
                }
                if (ballPos[0].transform.position.y < transform.position.y)
                {
                    transform.position += new Vector3(0, -twinValue) * MapGimicspeed * Speed * Time.deltaTime;
                    Debug.Log("Down");
                }
                Debug.Log($"{new Vector3(0, -twinValue)} * {MapGimicspeed} * {Speed}");
            }
            else
            {
                bool up = false;
                bool down = false;
                transform.position =
                    Vector3.Lerp(transform.position, transform.position + new Vector3(0, t * twinValue, 0), 10 * Time.deltaTime);
                if (ballPos[0].transform.position.y > transform.position.y)
                {
                    t += Time.deltaTime;
                    up = true;
                }
                else
                {
                    up = false;
                }
                if (ballPos[0].transform.position.y < transform.position.y)
                {
                    t -= Time.deltaTime;
                    down = true;
                }
                else
                {
                    down = false;
                }

                if (!up && t > 0)
                {
                    t -= Time.deltaTime;
                }
                if (!down && t < 0)
                {
                    t += Time.deltaTime;
                }
                if (t > 1)
                {
                    t = 1;
                }
                if (t < -1)
                {
                    t = -1;
                }
            }

        }
       

        if (PlayerInfin == true)
        {
            Sprite.SetActive(true);
            if (alter != null)
                alter.PlayerInfin = true;
        }
        else
        {
            Sprite.SetActive(false);
            if (alter != null)
                alter.PlayerInfin = false;
        }


        Vector3 pos = cam.WorldToViewportPoint(transform.position);



        if (pos.x < 0.025f) pos.x = 0.025f;

        if (pos.x > 0.975f) pos.x = 0.975f;

        if (pos.y < 0.025f) pos.y = 0.025f;

        if (pos.y > 0.975f) pos.y = 0.975f;



        transform.position = cam.ViewportToWorldPoint(pos);
    }

    public void SpeedSetting(float speed)
    {
        this.Speed = speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sans"))
            Destroy(this.gameObject);

        if (collision.gameObject.CompareTag("Ball")) // 공에 닿음녀
        {
              GetComponent<AudioSource>().Play();
            SetAbility(collision.gameObject.GetComponent<Ball>());
            Debug.Log("닿음");
            //StartCoroutine(BoxColliderONOFF());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("water"))
        {
            MapGimicspeed = 0.75f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("water"))
            MapGimicspeed = 1f;
    }


    void SetAbility(Ball b)
    {
        GameManager.Instance.SetAbility(_playerEnum, b, this);
    }
}
