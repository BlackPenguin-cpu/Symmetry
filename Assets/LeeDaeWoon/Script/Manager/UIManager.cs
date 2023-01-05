using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public bool isPlayerControl = false;

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
    public float hpBar;
    public float hp;
    [SerializeField] GameObject bar;

    [SerializeField] Image fadeInOutDie;
    [SerializeField] Text dieText;
    [SerializeField] Text anyText;

    public bool isOnceCheck = false;

    [Header("마우스 포인터")]
    [SerializeField] Texture2D mousePointer;
    public bool isCursorFade = false;

    [Header("페이드인아웃")]
    public Image fadeInOut;

    public bool isKingCheck = false;
    public bool isDarkPlayerGetCheck = false;

    void Start()
    {
        //Cursor.visible = false;
        timerCheck = true;
        Cursor.SetCursor(mousePointer, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Update()
    {
        StartCoroutine(Die_System());

        if (isDarkPlayerGetCheck == true && SceneManager.GetActiveScene().name == "Main")
        {
            isDarkPlayerGetCheck = false;
            Destroy(GameObject.Find("DarkPlayer"));
        }

        Timer();
        Money_System();
        HP_System();
        Wave();
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
        if (isOnceCheck == true && SceneManager.GetActiveScene().name == "Main")
        {
            isOnceCheck = false;
            isDarkPlayerGetCheck = true;

            Player.Instance._hp = Player.Instance._maxHp; // 체력 
            Player.Instance.state = PlayerState.Idle; // 플레이어 행동
            WaveManager.instnace.m_WaveNum = 1; // Wave 초기화

            // 타이머 초기화 
            sec = 0;
            min = 0;

            // 각 무기 레벨 초기화
            //for(int i = 0; i < )

            Player.Instance.stat._level[PlayerWeaponType.Sword] = 0;
            Player.Instance.stat._level[PlayerWeaponType.Dagger] = 0;
            Player.Instance.stat._level[PlayerWeaponType.Axe] = 0;

            fadeInOutDie.color = new Color(0, 0, 0, 0);
            dieText.color = new Color(255, 255, 255, 0);
            anyText.color = new Color(255, 255, 255, 0);
        }
    }

    void Timer()
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
    }

    void Money_System()
    {
        goldText.text = GameManager.Instance._coin.ToString();
        dimensionalText.text = GameManager.Instance.crystal.ToString();

        if (Input.GetKeyDown(KeyCode.G))
            GameManager.Instance._coin += 1000;
        if (Input.GetKeyDown(KeyCode.M))
            GameManager.Instance.crystal += 1000;
    }

    void Wave()
    {
        // 인게임
        if (SceneManager.GetActiveScene().name == "test")
            waveText.text = "Wave." + WaveManager.instnace.m_WaveNum;

        // 차원의 틈새
        else if (SceneManager.GetActiveScene().name == "Dimension")
            waveText.text = "차원의 틈새";

        // 폐허가된 성
        else if (SceneManager.GetActiveScene().name == "Main")
            waveText.text = "폐허가된 성";
    }

    #region 체력
    
    void HP_System()
    {
        hpBar = bar.transform.localScale.y;
        hp = Player.Instance.stat._hp / Player.Instance.stat._maxHp;

        if (hpBar > hp)
            bar.transform.localScale = new Vector3(1, Mathf.Lerp(hpBar, hp - 0.00001f, Time.deltaTime * 20), 1);
        else
            bar.transform.localScale = new Vector3(1, Mathf.Lerp(hpBar, hp, Time.deltaTime * 20), 1);
    }

    public IEnumerator Die_System()
    {
        if (Player.Instance.stat._hp == 0)
        {
            if (Input.anyKeyDown && isOnceCheck == true)
                SceneManager.LoadScene("Main");

            if (isOnceCheck == false)
            {
                fadeInOutDie.DOFade(0.5f, 1f);
                dieText.DOFade(1f, 1f);
                anyText.DOFade(1f, 1f);
                yield return new WaitForSeconds(1f);
                isOnceCheck = true;
            }
        }

    }
    #endregion
}
