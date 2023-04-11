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
    public Text timerText;
    public int min;
    public float sec;

    [Header("체력")]
    public float hp;
    public float hpBar;
    [SerializeField] GameObject bar;

    [Header("게임 오버")]
    [SerializeField] GameObject dieItemContent;
    [SerializeField] GameObject dieItemBar;
    bool isOnceCheck = false;

    [Header("마우스 포인터")]
    [SerializeField] Texture2D mousePointer;
    public bool isCursorFade = false;

    public bool isKingCheck = false;
    public bool isDarkPlayerGetCheck = false;

    void Start()
    {
        //Cursor.visible = false;
        Cursor.SetCursor(mousePointer, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Update()
    {
        switch (CurrentScene.instance.eScene)
        {
            case EScene.Main:
                if (isDarkPlayerGetCheck)
                {
                    isDarkPlayerGetCheck = false;
                    Destroy(GameObject.Find("DarkPlayer"));
                }
                break;
        }

        Wave();
        Timer();
        HPSystem();
        DieSystem();
        MoneySystem();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void OnLevelWasLoaded(int level)
    {
        if (isOnceCheck && SceneManager.GetActiveScene().name == "Main")
        {
            isOnceCheck = false;
            isDarkPlayerGetCheck = true;

            Player.Instance._hp = Player.Instance._maxHp; // 체력 
            Player.Instance.state = PlayerState.Idle; // 플레이어 행동
            WaveManager.instnace.m_WaveNum = 1; // Wave 초기화

            sec = 0;
            min = 0;

            // 각 무기 레벨 초기화
            //for(int i = 0; i < )

            Player.Instance.stat._level[PlayerWeaponType.Sword] = 0;
            Player.Instance.stat._level[PlayerWeaponType.Dagger] = 0;
            Player.Instance.stat._level[PlayerWeaponType.Axe] = 0;
        }
    }

    void Timer()
    {
        sec += Time.deltaTime;
        timerText.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);

        if ((int)sec > 59)
        {
            sec = 0;
            min++;
        }
    }

    void MoneySystem()
    {
        goldText.text = GameManager.Instance._coin.ToString();
        dimensionalText.text = GameManager.Instance.crystal.ToString();

        // 치트
        if (Input.GetKeyDown(KeyCode.G))
            GameManager.Instance._coin += 1000;
        if (Input.GetKeyDown(KeyCode.M))
            GameManager.Instance.crystal += 1000;
    }

    void Wave()
    {
        switch (CurrentScene.instance.eScene)
        {
            case EScene.Main:
                waveText.text = "폐허가된 성";
                break;

            case EScene.Dimension:
                waveText.text = "차원의 틈새";
                break;

            case EScene.Ingame:
                waveText.text = "Wave." + WaveManager.instnace.m_WaveNum;
                break;
        }
    }

    void HPSystem()
    {
        hpBar = bar.transform.localScale.y;
        hp = Player.Instance.stat._hp / Player.Instance.stat._maxHp;

        if (hpBar > hp)
            bar.transform.localScale = new Vector3(1, Mathf.Lerp(hpBar, hp - 0.00001f, Time.deltaTime * 20), 1);
        else
            bar.transform.localScale = new Vector3(1, Mathf.Lerp(hpBar, hp, Time.deltaTime * 20), 1);
    }

    public void DieSystem()
    {
        if (Player.Instance.stat._hp <= 0)
        {
            if (!isOnceCheck)
            {
                isOnceCheck = true;

                for (int i = 0; i < StopManager.instnace.itemDaHave.Count; i++)
                    Instantiate(dieItemBar, transform.position, Quaternion.identity, dieItemContent.transform);

                Fade.instance.fadeCanvas.sortingOrder = 10;
                Fade.instance.fadeInOut.DOFade(1, 5).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
                {
                    transform.GetChild(2).gameObject.SetActive(true);
                    DieWindow.instance.OpenWindow();
                });
            }
        }

    }
}
