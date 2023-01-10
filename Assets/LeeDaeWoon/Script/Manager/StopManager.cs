using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StopManager : MonoBehaviour
{
    public static StopManager instnace { get; private set; }

    float timer = 0f;
    [SerializeField] Image fadeBackGround;
    public bool inPause = false;

    public List<Item> itemDaHave = new List<Item>();

    [Header("버튼")]
    [SerializeField] Button backBtn;
    [SerializeField] Button settingBtn;
    [SerializeField] Button playerBtn;
    [SerializeField] Button mainWindowBtn;
    [SerializeField] Button exitBtn;

    [Space(10)]
    [SerializeField] Button playerItemBtn;
    [SerializeField] Button playerWeaponBtn;
    [SerializeField] Button settingCloseBtn;
    [SerializeField] Button playerCloseBtn;
    [SerializeField] Button mainWindowYesBtn;
    [SerializeField] Button mainWindowNoBtn;
    [SerializeField] Button exitWindowYesBtn;
    [SerializeField] Button exitWindowNoBtn;

    [Header("일시정지 창")]
    [SerializeField] GameObject pauseBarUp; // 일시정지 창의 윗 봉
    [SerializeField] GameObject pauseBarDown; // 일시정지 창의 아랫 봉 
    [SerializeField] RectTransform pauseRect; // 일시정지 창의 중간
    [SerializeField] GameObject pauseWindow; // 일시정지 창

    const int pauseBar = 370;
    const int pauseBarClose = 40;
    const float pauseBarSpeed = 0.38f;

    const int pauseWidth = 566;
    const int pauseHeight = 700;

    [Header("설정 창")]
    [SerializeField] GameObject settingBarUp; // 설정 창의 윗 봉
    [SerializeField] GameObject settingBarDown; // 설정 창의 아랫 봉 
    [SerializeField] RectTransform settingRect; // 설정 창의 중간 
    [SerializeField] GameObject settingWindow; // 설정 창
    [SerializeField] Slider Effect_Slider;
    [SerializeField] Slider BGM_Slider;
    [SerializeField] Text Resolution;
    public int Resolution_Num;

    const int settingBar = 440;
    const int settingBarClose = 30;
    const float settingBarSpeed = 0.35f;

    const int settingWidth = 1675;
    const int settingHeigh = 885;

    [Header("플레이어 창")]
    [SerializeField] GameObject playerBarUp; // 플레이어 창의 윗 봉
    [SerializeField] GameObject playerBarDown; // 플레이어 창의 아랫 봉 
    [SerializeField] RectTransform playerRect; // 플레이어 창의 중간 
    [SerializeField] GameObject playerWindow; // 플레이어 창

    [Space(10f)]
    [SerializeField] GameObject playerItemWindow; // 아이템 창
    [SerializeField] GameObject playerWeaponWindow; // 무기 창

    const int playerBar = 440;
    const int playerBarClose = 30;
    const float playerBarSpeed = 0.35f;

    const int playerWidth = 1675;
    const int playerHeigh = 885;

    [Header("플레이어_무기 창")]
    public GameObject axeWindow; // 도끼
    [SerializeField] Text axeLevel;
    [SerializeField] Text axeSkill;

    [SerializeField] Text axeAttack;
    [SerializeField] Text axeAttackUpgrade;
    [SerializeField] Text axeDefense;
    [SerializeField] Text axeDefenseUpgrade;

    [Space(10f)]
    [SerializeField] GameObject swordWindow; // 검
    [SerializeField] Text swordLevel;
    [SerializeField] Text swordSkill;

    [SerializeField] Text swordAttack;
    [SerializeField] Text swordAttackUpgrade;
    [SerializeField] Text swordMaxHp;
    [SerializeField] Text swordMaxHpUpgrade;

    [Space(10f)]
    [SerializeField] GameObject daggerWindow; // 단검
    [SerializeField] Text daggerLevel;
    [SerializeField] Text daggerSkill;

    [SerializeField] Text daggerAttack;
    [SerializeField] Text daggerAttackUpgrade;
    [SerializeField] Text daggerCritical;
    [SerializeField] Text daggerCriticalUpgrade;

    [Header("플레이어_아이템 창")]
    public Image itemLog; // 아이템 로그
    public Text itemName; // 아이템 이름
    public Image itemIcon; // 아이템 아이콘
    public Text itemExplanation; // 아이템 설명

    [Header("메인화면 창")]
    [SerializeField] GameObject mainBarUp; // 메인 창의 윗 봉
    [SerializeField] GameObject mainBarDown; // 메인 창의 아랫 봉 
    [SerializeField] RectTransform mainRect; // 메인 창의 중간 
    [SerializeField] GameObject mainWindow; // 메인 창
    bool Reset_Check; // 초기화 체크

    const int mainBar = 220;
    const int mainBarClose = 30;
    const float mainBarSpeed = 0.35f;

    const int mainWidth = 1585;
    const int mainHeigh = 395;

    [Header("게임종료 창")]
    public GameObject exitBarUp; // 게임종료 창의 윗 봉
    public GameObject exitBarDown; // 게임종료 창의 아랫 봉 
    public RectTransform exitRect; // 게임종료 창의 중간 
    public GameObject exitWindow; // 게임종료 창

    const int exitBar = 220;
    const int exitBarClose = 30;
    const float exitBarSpeed = 0.35f;

    const int exitWidth = 1585;
    const int exitHeigh = 395;

    const float waitTime = 0.5f;

    bool isESC = false;
    bool isEscCheck = false;

    void Start()
    {
        inPause = false;
        Btns();
    }

    void Update()
    {
        MainReset();

        // ESC 키를 누르면 일시정지 창이 열린다.
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (isESC == false && isEscCheck == false)
        //    {
        //        isEscCheck = true;
        //        StartCoroutine(PauseWindow());
        //    }

        //    else if (isESC == true && isEscCheck == false)
        //    {
        //        isEscCheck = true;

        //        pauseBarUp.transform.DOLocalMoveY(pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
        //        pauseBarDown.transform.DOLocalMoveY(-pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        //        {
        //            isEscCheck = false;
        //            isESC = false;

        //            pauseWindow.SetActive(false);
        //            Time.timeScale = 1f;
        //        });

        //        StartCoroutine(PauseWindowClose());
        //    }

        //}
    }

    void Awake()
    {
        if (instnace == null)
        {
            instnace = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void MainReset()
    {
        if (Reset_Check == true && SceneManager.GetActiveScene().name == "Main")
        {
            Reset_Check = false;

            itemDaHave.Clear(); // 소지한 아이템 초기화
            GameManager.Instance._coin = 0; // 골드 초기화
            WaveManager.instnace.m_WaveNum = 0; // Wave 초기화
            Player.Instance.stat._hp = Player.Instance.stat._maxHp; // 플레이어 HP 초기화

            // 타이머 초기화
            UIManager.instance.sec = 0;
            UIManager.instance.min = 0;

            // 무기 강화수치 초기화
            Player.Instance.stat._level[PlayerWeaponType.Sword] = 0;
            Player.Instance.stat._level[PlayerWeaponType.Dagger] = 0;
            Player.Instance.stat._level[PlayerWeaponType.Axe] = 0;

            // 기본 스킬로 초기화
            Skill_Manager.instance.Skill_Up.Add(SkillManager.Instance.SkillScriptList[6]);
            Skill_Manager.instance.Skill_Down.Add(SkillManager.Instance.SkillScriptList[8]);

            CardManager.instance.AddList();
        }
    }

    void Btns()
    {
        // 뒤로가기 버튼을 눌렀을 떄
        backBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            fadeBackGround.DOFade(0, waitTime).SetEase(Ease.Linear).SetUpdate(true);

            pauseBarUp.transform.DOLocalMoveY(pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            pauseBarDown.transform.DOLocalMoveY(-pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                pauseWindow.SetActive(false);
                Time.timeScale = 1f;
            });

            StartCoroutine(PauseWindowClose());
        });

        // 설정 버튼을 눌렀을 때
        settingBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

            pauseBarUp.transform.DOLocalMoveY(pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            pauseBarDown.transform.DOLocalMoveY(-pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                pauseWindow.SetActive(false);
                settingWindow.SetActive(true);

                settingBarUp.transform.DOLocalMoveY(settingBar, settingBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                settingBarDown.transform.DOLocalMoveY(-settingBar, settingBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                StartCoroutine(SettingWindow());
            });
            StartCoroutine(PauseWindowClose());
        });

        // 설정닫기 버튼을 눌렀을 때
        settingCloseBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

            settingBarUp.transform.DOLocalMoveY(settingBarClose, settingBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            settingBarDown.transform.DOLocalMoveY(-settingBarClose, settingBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                settingWindow.SetActive(false);
                Time.timeScale = 1;
            });
            StartCoroutine(SettingWindowClose());
        });

        // 플레이어 버튼을 눌렀을 때
        playerBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

            pauseBarUp.transform.DOLocalMoveY(pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            pauseBarDown.transform.DOLocalMoveY(-pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                pauseWindow.SetActive(false);
                playerWindow.SetActive(true);

                WeaponType();

                playerBarUp.transform.DOLocalMoveY(playerBar, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                playerBarDown.transform.DOLocalMoveY(-playerBar, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                StartCoroutine(PlayerWindow());
            });
            StartCoroutine(PauseWindowClose());
        });

        // 플레이어 닫기 버튼을 눌렀을 때
        playerCloseBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

            playerBarUp.transform.DOLocalMoveY(playerBarClose, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            playerBarDown.transform.DOLocalMoveY(-playerBarClose, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                playerWindow.SetActive(false);
                Time.timeScale = 1;
            });
            StartCoroutine(PlayerWindowClose());
        });

        // 플레이어 창의 아이템 버튼을 눌렀을 때
        playerItemBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

            playerBarUp.transform.DOLocalMoveY(playerBarClose, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            playerBarDown.transform.DOLocalMoveY(-playerBarClose, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                playerWeaponWindow.SetActive(false);
                playerItemWindow.SetActive(true);

                for (int i = 0; i < itemDaHave.Count; i++)
                {
                    itemLog.transform.GetChild(i).GetComponent<Image>().sprite = itemDaHave[i].icon;
                    itemLog.transform.GetChild(i).gameObject.SetActive(true);
                }

                playerBarUp.transform.DOLocalMoveY(playerBar, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                playerBarDown.transform.DOLocalMoveY(-playerBar, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                StartCoroutine(PlayerWindow());
            });
            StartCoroutine(PlayerWindowClose());
        });

        // 플레이어 창의 무기 버튼을 눌렀을 때
        playerWeaponBtn.onClick.AddListener(() =>
       {
           SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

           playerBarUp.transform.DOLocalMoveY(playerBarClose, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
           playerBarDown.transform.DOLocalMoveY(-playerBarClose, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
           {
               playerWeaponWindow.SetActive(true);
               playerItemWindow.SetActive(false);

               playerBarUp.transform.DOLocalMoveY(playerBar, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
               playerBarDown.transform.DOLocalMoveY(-playerBar, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
               StartCoroutine(PlayerWindow());
           });
           StartCoroutine(PlayerWindowClose());
       });

        // 메인화면 버튼을 눌렀을 때
        mainWindowBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

            pauseBarUp.transform.DOLocalMoveY(pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            pauseBarDown.transform.DOLocalMoveY(-pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                pauseWindow.SetActive(false);
                mainWindow.SetActive(true);

                mainBarUp.transform.DOLocalMoveY(mainBar, mainBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                mainBarDown.transform.DOLocalMoveY(-mainBar, mainBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                StartCoroutine(MainWindow());
            });
            StartCoroutine(PauseWindowClose());
        });

        // 메인화면에서 예 버튼을 눌렀을 때
        mainWindowYesBtn.onClick.AddListener(() =>
        {

        });

        // 메인화면에서 아니요 버튼을 누렀을 때
        mainWindowNoBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

            mainBarUp.transform.DOLocalMoveY(mainBarClose, mainBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            mainBarDown.transform.DOLocalMoveY(-mainBarClose, mainBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                mainWindow.SetActive(false);
                Time.timeScale = 1;
            });
            StartCoroutine(MainWindowClose());
        });

        // 나가기 버튼을 눌렀을 때
        exitBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

            pauseBarUp.transform.DOLocalMoveY(pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            pauseBarDown.transform.DOLocalMoveY(-pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                pauseWindow.SetActive(false);
                exitWindow.SetActive(true);

                exitBarUp.transform.DOLocalMoveY(exitBar, exitBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                exitBarDown.transform.DOLocalMoveY(-exitBar, exitBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                StartCoroutine(ExitWindow());
            });
            StartCoroutine(PauseWindowClose());
        });

        // 나가기 예 버튼을 눌렀을 때
        exitWindowYesBtn.onClick.AddListener(() =>
        {
            DOTween.KillAll();
            Application.Quit();
        });

        // 나가기 아니요 버튼을 누렀을 때
        exitWindowNoBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

            exitBarUp.transform.DOLocalMoveY(exitBarClose, exitBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            exitBarDown.transform.DOLocalMoveY(-exitBarClose, exitBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                exitWindow.SetActive(false);
                Time.timeScale = 1;
            });
            StartCoroutine(ExitWindowClose());
        });
    }

    #region 일시정지 창
    // 일시정지 창 열기
    IEnumerator PauseWindow()
    {
        Time.timeScale = 0f;

        timer = 0;
        pauseWindow.SetActive(true);
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

        pauseBarUp.transform.DOLocalMoveY(pauseBar, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
        pauseBarDown.transform.DOLocalMoveY(-pauseBar, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            isESC = true;
            isEscCheck = false;
        });

        while (timer < 1)
        {
            pauseRect.sizeDelta = new Vector2(pauseWidth, Mathf.Lerp(0, pauseHeight, timer));

            timer += Time.unscaledDeltaTime * 3.3f;
            yield return null;
        }

    }

    // 일시정지 창 닫기
    IEnumerator PauseWindowClose()
    {
        timer = 0;

        while (timer < 1)
        {
            pauseRect.sizeDelta = new Vector2(pauseWidth, Mathf.Lerp(pauseHeight, 0, timer));

            timer += Time.unscaledDeltaTime * 2.8f;
            yield return null;
        }
    }
    #endregion

    #region 설정 창
    // 설정 창 열기
    IEnumerator SettingWindow()
    {
        timer = 0;

        while (timer < 1)
        {
            settingRect.sizeDelta = new Vector2(settingWidth, Mathf.Lerp(0, settingHeigh, timer));
            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
    }

    // 설정 창 닫기
    IEnumerator SettingWindowClose()
    {
        timer = 0;

        while (timer < 1)
        {
            settingRect.sizeDelta = new Vector2(settingWidth, Mathf.Lerp(settingHeigh, 0, timer));
            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
    }
    #endregion

    #region 플레이어 창
    // 플레이어 창 열기
    IEnumerator PlayerWindow()
    {
        timer = 0;

        while (timer < 1)
        {
            playerRect.sizeDelta = new Vector2(playerWidth, Mathf.Lerp(0, playerHeigh, timer));
            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
    }

    // 플레이어 창 닫기
    IEnumerator PlayerWindowClose()
    {
        timer = 0;

        while (timer < 1)
        {
            playerRect.sizeDelta = new Vector2(playerWidth, Mathf.Lerp(playerHeigh, 0, timer));
            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
    }
    #endregion

    #region 메인 창
    // 메인 창 열기
    IEnumerator MainWindow()
    {
        timer = 0;

        while (timer < 1)
        {
            mainRect.sizeDelta = new Vector2(mainWidth, Mathf.Lerp(0, mainHeigh, timer));
            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
    }

    // 메인 창 닫기
    IEnumerator MainWindowClose()
    {
        timer = 0;

        while (timer < 1)
        {
            mainRect.sizeDelta = new Vector2(mainWidth, Mathf.Lerp(mainHeigh, 0, timer));
            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
    }
    #endregion

    #region 나가기 창
    // 나가기 창 열기
    IEnumerator ExitWindow()
    {
        timer = 0;

        while (timer < 1)
        {
            exitRect.sizeDelta = new Vector2(exitWidth, Mathf.Lerp(0, exitHeigh, timer));
            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
    }

    // 나가기 창 닫기
    IEnumerator ExitWindowClose()
    {
        timer = 0;

        while (timer < 1)
        {
            exitRect.sizeDelta = new Vector2(exitWidth, Mathf.Lerp(exitHeigh, 0, timer));
            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
    }
    #endregion

    public void WeaponType()
    {
        int swordStat = Player.Instance.stat._level[PlayerWeaponType.Sword];
        int daggerStat = Player.Instance.stat._level[PlayerWeaponType.Dagger];
        int axeStat = Player.Instance.stat._level[PlayerWeaponType.Axe];

        switch (Player.Instance.stat.weaponType)
        {
            case PlayerWeaponType.Sword:

                swordWindow.SetActive(true);
                daggerWindow.SetActive(false);
                axeWindow.SetActive(false);

                swordLevel.text = swordStat.ToString();

                swordAttack.text = (10 + (10 * swordStat)).ToString(); ;
                swordAttackUpgrade.text = (10 + (10 * (swordStat + 1))).ToString();

                swordMaxHp.text = (10 + (10 * swordStat)).ToString();
                swordMaxHpUpgrade.text = (10 + (10 * (swordStat + 1))).ToString();

                switch (swordStat)
                {
                    case 1:
                        swordSkill.transform.GetChild(0).gameObject.SetActive(false);
                        break;

                    case 3:
                        swordSkill.transform.GetChild(1).gameObject.SetActive(false);
                        break;

                    case 5:
                        swordSkill.transform.GetChild(2).gameObject.SetActive(false);
                        break;
                }
                break;

            case PlayerWeaponType.Dagger:
                swordWindow.SetActive(false);
                daggerWindow.SetActive(true);
                axeWindow.SetActive(false);

                daggerLevel.text = daggerStat.ToString();

                daggerAttack.text = (8 + (8 * daggerStat)).ToString();
                daggerAttackUpgrade.text = (8 + (8 * (daggerStat + 1))).ToString();

                daggerCritical.text = (0 + (2 * daggerStat)).ToString();
                daggerCriticalUpgrade.text = (0 + (2 * (daggerStat + 1))).ToString();

                switch (daggerStat)
                {
                    case 1:
                        daggerSkill.transform.GetChild(0).gameObject.SetActive(false);
                        break;

                    case 3:
                        daggerSkill.transform.GetChild(1).gameObject.SetActive(false);
                        break;

                    case 5:
                        daggerSkill.transform.GetChild(2).gameObject.SetActive(false);
                        break;
                }
                break;

            case PlayerWeaponType.Axe:
                swordWindow.SetActive(false);
                daggerWindow.SetActive(false);
                axeWindow.SetActive(true);

                axeLevel.text = axeStat.ToString();

                axeAttack.text = (20 + (15 * axeStat)).ToString();
                axeAttackUpgrade.text = (20 + (15 * (axeStat + 1))).ToString();

                axeDefense.text = (400 + (200 * axeStat)).ToString();
                axeDefenseUpgrade.text = (400 + (200 * (axeStat + 1))).ToString();

                switch (axeStat)
                {
                    case 1:
                        axeSkill.transform.GetChild(0).gameObject.SetActive(false);
                        break;

                    case 3:
                        axeSkill.transform.GetChild(1).gameObject.SetActive(false);
                        break;

                    case 5:
                        axeSkill.transform.GetChild(2).gameObject.SetActive(false);
                        break;
                }
                break;
        }
    }

    public void Main_Yes_Btn()
    {
        Reset_Check = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }
}
