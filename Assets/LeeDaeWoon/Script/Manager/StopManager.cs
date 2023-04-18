using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[System.Serializable]
public class CurrentWeapon
{
    public string name;
    public int level;
}

public class StopManager : MonoBehaviour
{
    public static StopManager instnace { get; private set; }

    public bool inPause = false;

    public List<Item> itemDaHave = new List<Item>();
    public List<CurrentWeapon> weapon = new List<CurrentWeapon>();

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

    #region 일시정지 창
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
    #endregion

    #region 설정 창
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
    #endregion

    #region 플레이어 창
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
    #endregion

    #region 메인화면 창
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
    #endregion

    #region 게임종료 창
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
    #endregion

    const float waitTime = 0.5f;

    bool isESC = false;
    public bool isEscCheck = false;

    void Start()
    {
        inPause = false;
        Btns();
    }

    void Update()
    {
        MainReset();
        WeaponLevel();

        //ESC 키를 누르면 일시정지 창이 열린다.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isEscCheck)
            {
                if (!isESC)
                {
                    isEscCheck = true;
                    Fade.instance.fadeInOut.DOKill();
                    Fade.instance.fadeInOut.DOFade(0.5f, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                    PauseWindow();
                }

                else
                {
                    isEscCheck = true;

                    pauseBarUp.transform.DOLocalMoveY(pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                    pauseBarDown.transform.DOLocalMoveY(-pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
                    {
                        isEscCheck = false;
                        isESC = false;

                        Fade.instance.fadeInOut.DOFade(0, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                        pauseWindow.SetActive(false);
                        Time.timeScale = 1f;
                    });

                    pauseRect.DOSizeDelta(new Vector2(pauseWidth, 0), pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                }
            }

        }
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
        switch (CurrentScene.instance.eScene)
        {
            case EScene.Main:
                if (Reset_Check)
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
                break;
        }
    }

    void WeaponLevel()
    {
        switch (CurrentScene.instance.eScene)
        {
            case EScene.Dimension:
                for (int i = 0; i < weapon.Count; i++)
                {
                    weapon[i].level = BlackSmith.instnace.weapon[i].level;
                }
                break;
        }
    }

    void Btns()
    {
        // 뒤로가기 버튼을 눌렀을 떄
        backBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            Fade.instance.fadeInOut.DOFade(0, waitTime).SetEase(Ease.Linear).SetUpdate(true);

            pauseBarUp.transform.DOLocalMoveY(pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            pauseBarDown.transform.DOLocalMoveY(-pauseBarClose, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                pauseWindow.SetActive(false);
                Time.timeScale = 1f;
            });

            pauseRect.DOSizeDelta(new Vector2(pauseWidth, 0), pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
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
                settingRect.DOSizeDelta(new Vector2(settingWidth, settingHeigh), settingBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            });
            pauseRect.DOSizeDelta(new Vector2(pauseWidth, 0), pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
        });

        // 설정닫기 버튼을 눌렀을 때
        settingCloseBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            Fade.instance.fadeInOut.DOFade(0, waitTime).SetEase(Ease.Linear).SetUpdate(true);

            settingBarUp.transform.DOLocalMoveY(settingBarClose, settingBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            settingBarDown.transform.DOLocalMoveY(-settingBarClose, settingBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                settingWindow.SetActive(false);
                Time.timeScale = 1;
            });
            settingRect.DOSizeDelta(new Vector2(settingWidth, 0), settingBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
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

                playerWeaponWindow.SetActive(true);
                playerItemWindow.SetActive(false);

                WeaponType();

                playerBarUp.transform.DOLocalMoveY(playerBar, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                playerBarDown.transform.DOLocalMoveY(-playerBar, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                playerRect.DOSizeDelta(new Vector2(playerWidth, playerHeigh), playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            });
            pauseRect.DOSizeDelta(new Vector2(pauseWidth, 0), pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
        });

        // 플레이어 닫기 버튼을 눌렀을 때
        playerCloseBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            Fade.instance.fadeInOut.DOFade(0, waitTime).SetEase(Ease.Linear).SetUpdate(true);

            playerBarUp.transform.DOLocalMoveY(playerBarClose, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            playerBarDown.transform.DOLocalMoveY(-playerBarClose, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                playerWindow.SetActive(false);
                Time.timeScale = 1;
            });
            playerRect.DOSizeDelta(new Vector2(playerWidth, 0), playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
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
                playerRect.DOSizeDelta(new Vector2(playerWidth, playerHeigh), playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            });
            playerRect.DOSizeDelta(new Vector2(playerWidth, 0), playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
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
               playerRect.DOSizeDelta(new Vector2(playerWidth, playerHeigh), playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
           });
           playerRect.DOSizeDelta(new Vector2(playerWidth, 0), playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
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
                mainRect.DOSizeDelta(new Vector2(mainWidth, mainHeigh), mainBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            });
            pauseRect.DOSizeDelta(new Vector2(pauseWidth, 0), pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
        });

        // 메인화면에서 예 버튼을 눌렀을 때
        mainWindowYesBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            Fade.instance.fadeInOut.DOFade(0, waitTime).SetEase(Ease.Linear).SetUpdate(true);
            SceneManager.LoadScene(1);
        });

        // 메인화면에서 아니요 버튼을 누렀을 때
        mainWindowNoBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            Fade.instance.fadeInOut.DOFade(0, waitTime).SetEase(Ease.Linear).SetUpdate(true);

            mainBarUp.transform.DOLocalMoveY(mainBarClose, mainBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            mainBarDown.transform.DOLocalMoveY(-mainBarClose, mainBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                mainWindow.SetActive(false);
                Time.timeScale = 1;
            });
            mainRect.DOSizeDelta(new Vector2(mainWidth, 0), mainBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
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
                exitRect.DOSizeDelta(new Vector2(exitWidth, exitHeigh), exitBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            });
            pauseRect.DOSizeDelta(new Vector2(pauseWidth, 0), pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
        });

        // 나가기 예 버튼을 눌렀을 때
        exitWindowYesBtn.onClick.AddListener(() =>
        {
            Fade.instance.fadeInOut.DOFade(0, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
            DOTween.KillAll();
            Application.Quit();
        });

        // 나가기 아니요 버튼을 누렀을 때
        exitWindowNoBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            Fade.instance.fadeInOut.DOFade(0, waitTime).SetEase(Ease.Linear).SetUpdate(true);

            exitBarUp.transform.DOLocalMoveY(exitBarClose, exitBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
            exitBarDown.transform.DOLocalMoveY(-exitBarClose, exitBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                exitWindow.SetActive(false);
                Time.timeScale = 1;
            });
            exitRect.DOSizeDelta(new Vector2(exitWidth, 0), exitBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
        });
    }

    // 일시정지 창 열기
    void PauseWindow()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

        Time.timeScale = 0f;

        pauseWindow.SetActive(true);

        pauseBarUp.transform.DOLocalMoveY(pauseBar, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
        pauseBarDown.transform.DOLocalMoveY(-pauseBar, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            isESC = true;
            isEscCheck = false;
        });
        pauseRect.DOSizeDelta(new Vector2(pauseWidth, pauseHeight), pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
    }

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
