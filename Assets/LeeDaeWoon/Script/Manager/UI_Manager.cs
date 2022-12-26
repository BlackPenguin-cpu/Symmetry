using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    public bool PlayerMove_control = false;

    [Header("재화")]
    public int gold;
    public int dimensional;
    [SerializeField] Text goldText;
    [SerializeField] Text dimensionalText;

    [Header("웨이브")]
    [SerializeField] Text waveText;

    [Header("타이머")]
    [SerializeField] Text timerText;
    public bool timerCheck;
    public int min;
    public float sec;

    [Header("체력")]
    public float HP_Bar;

    public float HP;
    [SerializeField] GameObject Bar;

    [SerializeField] Image FadeInOut_Die;
    [SerializeField] Text Die_Text;
    [SerializeField] Text Any_Text;

    public bool Once_Check = false;

    [Header("마우스 포인터")]
    [SerializeField] Texture2D MousePointer;
    public bool Cursor_Fade;

    [Header("페이드인아웃")]
    public Image FadeInOut;

    public bool King_Check = false;

    public bool DarkPlayerGet_Check;

    void Start()
    {
        //Cursor.visible = false;
        timerCheck = true;
        Cursor.SetCursor(MousePointer, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Update()
    {
        StartCoroutine(Die_System());

        if (DarkPlayerGet_Check == true && SceneManager.GetActiveScene().name == "Main")
        {
            DarkPlayerGet_Check = false;
            Destroy(GameObject.Find("DarkPlayer"));
        }

        Timer_System();
        Money_System();
        HP_System();
        Wave();
        Cheats();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (Once_Check == true && SceneManager.GetActiveScene().name == "Main")
        {
            Once_Check = false;
            DarkPlayerGet_Check = true;

            Player.Instance._hp = Player.Instance._maxHp; // 체력 
            Player.Instance.state = PlayerState.Idle; // 플레이어 행동
            WaveManager.Instance.m_WaveNum = 1; // Wave 초기화

            // 타이머 초기화 
            sec = 0;
            min = 0;

            // 각 무기 레벨 초기화
            //for(int i = 0; i < )


            Player.Instance.stat._level[PlayerWeaponType.Sword] = 0;
            Player.Instance.stat._level[PlayerWeaponType.Dagger] = 0;
            Player.Instance.stat._level[PlayerWeaponType.Axe] = 0;

            FadeInOut_Die.color = new Color(0, 0, 0, 0);
            Die_Text.color = new Color(255, 255, 255, 0);
            Any_Text.color = new Color(255, 255, 255, 0);
        }
    }

    #region 타이머
    public void Timer_System()
    {
        if (timerCheck == true)
        {
            sec += Time.deltaTime;
            timerText.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);

            if ((int)sec > 59)
            {
                sec = 0;
                min++;
            }
        }
        else
        {

        }
    }
    #endregion

    #region 재화
    public void Money_System()
    {
        goldText.text = GameManager.Instance._coin.ToString();
        dimensionalText.text = GameManager.Instance.crystal.ToString();

        if (Input.GetKeyDown(KeyCode.G))
            GameManager.Instance._coin += 1000;
        if (Input.GetKeyDown(KeyCode.M))
            GameManager.Instance.crystal += 1000;
    }

    #endregion

    #region 웨이브
    public void Wave()
    {
        // 인게임
        if (SceneManager.GetActiveScene().name == "test")
            waveText.text = "Wave." + WaveManager.Instance.m_WaveNum;

        // 차원의 틈새
        else if (SceneManager.GetActiveScene().name == "Dimension")
            waveText.text = "차원의 틈새";

        // 폐허가된 성
        else if (SceneManager.GetActiveScene().name == "Main")
            waveText.text = "폐허가된 성";
    }
    #endregion

    #region 체력
    public void HP_System()
    {
        HP_Bar = Bar.transform.localScale.y;
        HP = Player.Instance.stat._hp / Player.Instance.stat._maxHp;


        if (HP_Bar > HP)
            Bar.transform.localScale = new Vector3(1, Mathf.Lerp(HP_Bar, HP - 0.00001f, Time.deltaTime * 20), 1);
        else
            Bar.transform.localScale = new Vector3(1, Mathf.Lerp(HP_Bar, HP, Time.deltaTime * 20), 1);
    }

    public IEnumerator Die_System()
    {
        if (Player.Instance.stat._hp == 0)
        {
            if (Input.anyKeyDown && Once_Check == true)
                SceneManager.LoadScene("Main");

            if (Once_Check == false)
            {
                FadeInOut_Die.DOFade(0.5f, 1f);
                Die_Text.DOFade(1f, 1f);
                Any_Text.DOFade(1f, 1f);
                yield return new WaitForSeconds(1f);
                Once_Check = true;
            }
        }

    }
    #endregion

    #region Cheat
    public void Cheats()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            DOTween.PauseAll();
            SceneManager.LoadScene("test");
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            DOTween.PauseAll();
            SceneManager.LoadScene("Dimension");

        }
    }
    #endregion
}
