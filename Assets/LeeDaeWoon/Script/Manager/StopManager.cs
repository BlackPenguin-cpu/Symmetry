using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StopManager : MonoBehaviour
{
    public static StopManager instnace { get; private set; }

    public float timer = 0f;
    public Image Fade_Background;
    public bool InPause = false;

    bool PauseWindow_Open = false;
    bool PauseWindow_Close = false;

    bool BackBtn_Check = false;

    bool SettingWindow_Open = false;
    bool SettingWindow_Close = false;

    bool PlayerWindow_Open = false;
    bool PlayerWindow_Close = false;
    bool PlayerWindow_Check = false;

    bool MainWindow_Open = false;
    bool MainWindow_Close = false;

    bool GameExitWindow_Open = false;
    bool GameExitWindow_Close = false;


    public List<Item> ItemDA_Have = new List<Item>();

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
    public bool WI_Check = true; // 현재 무기창이 열려져 있는지 아이템 창이 열려져 있는지 확인한다.

    const int playerBar = 440;
    const int playerBarClose = 30;
    const float playerBarSpeed = 0.35f;

    const int playerWidth = 1675;
    const int playerHeigh = 885;

    [Header("플레이어_무기 창")]
    public GameObject Axe_Window; // 도끼
    [SerializeField] Text AxeLevel_Text;
    [SerializeField] Text Axe_Skill_Text;

    [SerializeField] Text Axe_AttackDamage;
    [SerializeField] Text Axe_AttackDamage_Upgrade;
    [SerializeField] Text Axe_Defense;
    [SerializeField] Text Axe_Defense_Upgrade;

    [Space(10f)]
    [SerializeField] GameObject Sword_Window; // 검
    [SerializeField] Text SwordLevel_Text;
    [SerializeField] Text Sword_Skill_Text;

    [SerializeField] Text Sword_AttackDamage;
    [SerializeField] Text Sword_AttackDamage_Upgrade;
    [SerializeField] Text Sword_MaxHp;
    [SerializeField] Text Sword_MaxHp_Upgrade;

    [Space(10f)]
    [SerializeField] GameObject Dagger_Window; // 단검
    [SerializeField] Text DaggerLevel_Text;
    [SerializeField] Text Dagger_Skill_Text;

    [SerializeField] Text Dagger_AttackDamage;
    [SerializeField] Text Dagger_AttackDamage_Upgrade;
    [SerializeField] Text Dagger_Critical;
    [SerializeField] Text Dagger_Critical_Upgrade;

    [Header("플레이어_아이템 창")]
    public Image Player_Item_Log; // 아이템 로그
    public Text Player_Item_Name; // 아이템 이름
    public Image Player_Item_Icon; // 아이템 아이콘
    public Text Player_Item_Explanation; // 아이템 설명

    bool Icon_Check = true;

    [Header("메인화면 창")]
    [SerializeField] GameObject Main_Pole01; // 메인 창의 윗 봉
    [SerializeField] GameObject Main_Pole02; // 메인 창의 아랫 봉 
    [SerializeField] RectTransform Main_Window; // 메인 창의 중간 
    [SerializeField] GameObject Main_Window_Canvas; // 메인 창

    bool Reset_Check; // 초기화 체크

    [Header("게임종료 창")]
    public GameObject Exit_Pole01; // 게임종료 창의 윗 봉
    public GameObject Exit_Pole02; // 게임종료 창의 아랫 봉 
    public RectTransform Exit_Window; // 게임종료 창의 중간 
    public GameObject Exit_Window_Canvas; // 게임종료 창

    const float waitTime = 0.5f;

    void Start()
    {
        WI_Check = true;
        InPause = false;
        Btns();
    }

    void Update()
    {
        Item_Log();
        WeaponType();


        Main_Reset();

        // ESC 키를 누르면 일시정지 창이 열린다.
        if (Input.GetKeyDown(KeyCode.Escape))
            StartCoroutine(PauseWindow());
    }

    private void Awake()
    {
        if (instnace == null)
        {
            instnace = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void Main_Reset()
    {
        if (Reset_Check == true && SceneManager.GetActiveScene().name == "Main")
        {
            Reset_Check = false;

            Card_Manager.instance.Item_Reset(); // 방어구 및 장신구 , 마정석 정보 초기화

            ItemDA_Have.Clear(); // 소지한 아이템 초기화
            GameManager.Instance._coin = 0; // 골드 초기화
            WaveManager.Instance.m_WaveNum = 0; // Wave 초기화
            Player.Instance.stat._hp = Player.Instance.stat._maxHp; // 플레이어 HP 초기화

            // 타이머 초기화
            UI_Manager.instance.sec = 0;
            UI_Manager.instance.min = 0;

            // 무기 강화수치 초기화
            Player.Instance.stat._level[PlayerWeaponType.Sword] = 0;
            Player.Instance.stat._level[PlayerWeaponType.Dagger] = 0;
            Player.Instance.stat._level[PlayerWeaponType.Axe] = 0;

            // 기본 스킬로 초기화
            Skill_Manager.instance.Skill_Up.Add(SkillManager.Instance.SkillScriptList[6]);
            Skill_Manager.instance.Skill_Down.Add(SkillManager.Instance.SkillScriptList[8]);

            Card_Manager.instance.AddList();
        }
    }

    void Btns()
    {
        // 뒤로가기 버튼을 눌렀을 떄
        backBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            Fade_Background.DOFade(0, waitTime).SetEase(Ease.Linear).SetUpdate(true);

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

                playerBarUp.transform.DOLocalMoveY(playerBar, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                playerBarDown.transform.DOLocalMoveY(-playerBar, playerBarSpeed).SetEase(Ease.Linear).SetUpdate(true);
                StartCoroutine(PlayerWindow());
            });
            StartCoroutine(PlayerWindowClose());
        });

        // 플레이어 창의 무기 버튼을 눌렀을 때
        playerWeaponBtn.onClick .AddListener(() => 
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

        });

        // 나가기 버튼을 눌렀을 때
        exitBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

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
        pauseBarDown.transform.DOLocalMoveY(-pauseBar, pauseBarSpeed).SetEase(Ease.Linear).SetUpdate(true);

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


    #region 플레이어 버튼
    public void Player_Btn() => StartCoroutine(Player_Window_Coroutine01());

    public void Player_Close_Btn()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        StartCoroutine(Player_Window_Close());
    }

    public void Item_Log()
    {
        if (Card_Manager.instance.isItemBool == false)
        {
            Player_Item_Log.transform.GetChild(Card_Manager.instance.itemCheck).GetComponent<Image>().sprite = ItemDA_Have[Card_Manager.instance.itemCheck].Item_Icon;
            Player_Item_Log.transform.GetChild(Card_Manager.instance.itemCheck).gameObject.SetActive(true);

            if (Icon_Check == true)
            {
                Icon_Check = false;
                Player_Item_Icon.sprite = ItemDA_Have[0].Item_Icon;
                Player_Item_Name.text = ItemDA_Have[0].Itme_Name;
                Player_Item_Explanation.text = ItemDA_Have[0].Item_Explanation;
            }
        }
    }

    public IEnumerator Player_Window_Coroutine01() // 플레이어버튼을 클릭했을 떄
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        if (PlayerWindow_Open == false)
        {
            PlayerWindow_Open = true;
            timer = 0;
            pauseBarUp.transform.DOLocalMoveY(48f, 0.5f).SetUpdate(true);
            pauseBarDown.transform.DOLocalMoveY(-36, 0.5f).SetUpdate(true);

            while (timer < 1)
            {
                pauseRect.sizeDelta = new Vector2(557.1f, Mathf.Lerp(772.4f, 5f, timer));

                timer += Time.unscaledDeltaTime * 2.5f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            pauseWindow.SetActive(false);
            yield return new WaitForSecondsRealtime(0.1f);
            playerWindow.SetActive(true);
            StartCoroutine(Player_Window_Coroutine02());
        }
    }

    public IEnumerator Player_Window_Coroutine02() // 플레이어 창 열림
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        timer = 0;
        playerBarUp.transform.DOLocalMoveY(447.5388f, 0.48f).SetUpdate(true);
        playerBarDown.transform.DOLocalMoveY(-445.16f, 0.48f).SetUpdate(true);

        while (timer < 1)
        {
            playerRect.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(0f, 916.9f, timer));
            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
        PlayerWindow_Close = true;
    }

    public IEnumerator Player_Window_Close() // 플레이어 창 닫힘
    {
        if (PlayerWindow_Close == true)
        {
            PlayerWindow_Close = false;
            timer = 0;
            Fade_Background.DOFade(0, 0.5f).SetUpdate(true);
            playerBarUp.transform.DOLocalMoveY(30, 0.48f).SetUpdate(true);
            playerBarDown.transform.DOLocalMoveY(-30, 0.48f).SetUpdate(true);

            while (timer < 1)
            {
                playerRect.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(916.9f, 0f, timer));

                timer += Time.unscaledDeltaTime * 3f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            playerWindow.SetActive(false);
            PauseWindow_Open = true;
            Time.timeScale = 1f;
            PlayerWindow_Open = false;
            UI_Manager.instance.isCursorFade = false;
        }
    }

    #region 플레이어 -> 무기 창 & 아이템 창

    public void Player_WeaPon_Btn() // 무기 버튼을 눌렀을 떄
    {
        if (WI_Check == false)
        {
            PlayerWindow_Check = false;
            StartCoroutine(Player_Weapon_Open01());
        }
    }

    public void Player_Item_Btn() // 아이템 버튼을 눌렀을 때
    {
        if (WI_Check == true)
        {
            PlayerWindow_Check = false;
            StartCoroutine(Player_Item_Open01());
        }
    }

    public IEnumerator Player_Weapon_Open01()
    {
        if (PlayerWindow_Check == false)
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            WI_Check = true;
            timer = 0;
            Fade_Background.DOFade(0, 0.5f).SetUpdate(true);
            playerBarUp.transform.DOLocalMoveY(30, 0.48f).SetUpdate(true);
            playerBarDown.transform.DOLocalMoveY(-30, 0.48f).SetUpdate(true);

            while (timer < 1)
            {
                playerRect.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(916.9f, 0f, timer));

                timer += Time.unscaledDeltaTime * 3f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            playerWeaponWindow.SetActive(true);
            playerItemWindow.SetActive(false);
            yield return new WaitForSecondsRealtime(0.1f);
            StartCoroutine(Player_Weapon_Open02());
        }
    }

    public IEnumerator Player_Weapon_Open02()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        timer = 0;
        Fade_Background.DOFade(0.5f, 0.5f).SetUpdate(true);
        playerBarUp.transform.DOLocalMoveY(447.5388f, 0.48f).SetUpdate(true);
        playerBarDown.transform.DOLocalMoveY(-445.16f, 0.48f).SetUpdate(true);

        while (timer < 1)
        {
            playerRect.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(0f, 916.9f, timer));
            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
        PlayerWindow_Check = true;
    }

    public IEnumerator Player_Item_Open01()
    {
        if (PlayerWindow_Check == false)
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            WI_Check = false;
            timer = 0;
            Fade_Background.DOFade(0, 0.5f).SetUpdate(true);
            playerBarUp.transform.DOLocalMoveY(30, 0.48f).SetUpdate(true);
            playerBarDown.transform.DOLocalMoveY(-30, 0.48f).SetUpdate(true);

            while (timer < 1)
            {
                playerRect.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(916.9f, 0f, timer));

                timer += Time.unscaledDeltaTime * 3f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            playerWeaponWindow.SetActive(false);
            playerItemWindow.SetActive(true);
            yield return new WaitForSecondsRealtime(0.1f);
            StartCoroutine(Player_Item_Open02());
        }
    }

    public IEnumerator Player_Item_Open02()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        timer = 0;
        Fade_Background.DOFade(0.5f, 0.5f).SetUpdate(true);
        playerBarUp.transform.DOLocalMoveY(447.5388f, 0.48f).SetUpdate(true);
        playerBarDown.transform.DOLocalMoveY(-445.16f, 0.48f).SetUpdate(true);

        while (timer < 1)
        {
            playerRect.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(0f, 916.9f, timer));
            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
        PlayerWindow_Check = true;
    }
    #endregion

    #region 무기 창
    public void WeaponType()
    {
        int Sword_Level = Player.Instance.stat._level[PlayerWeaponType.Sword];
        int Dagger_Level = Player.Instance.stat._level[PlayerWeaponType.Dagger];
        int Axe_Level = Player.Instance.stat._level[PlayerWeaponType.Axe];

        switch (Player.Instance.stat.weaponType)
        {
            case PlayerWeaponType.Sword:
                SwordLevel_Text.text = "" + Sword_Level;

                Sword_AttackDamage.text = "" + (10 + (10 * Sword_Level));
                Sword_AttackDamage_Upgrade.text = "" + (10 + (10 * (Sword_Level + 1)));

                Sword_MaxHp.text = "" + (10 + (10 * Sword_Level));
                Sword_MaxHp_Upgrade.text = "" + (10 + (10 * (Sword_Level + 1)));

                switch (Sword_Level)
                {
                    case 1:
                        Sword_Skill_Text.transform.GetChild(0).gameObject.SetActive(false);
                        break;

                    case 3:
                        Sword_Skill_Text.transform.GetChild(1).gameObject.SetActive(false);
                        break;

                    case 5:
                        Sword_Skill_Text.transform.GetChild(2).gameObject.SetActive(false);
                        break;
                }

                Sword_Window.SetActive(true);
                Dagger_Window.SetActive(false);
                Axe_Window.SetActive(false);
                break;

            case PlayerWeaponType.Dagger:
                DaggerLevel_Text.text = "" + Dagger_Level;

                Dagger_AttackDamage.text = "" + (8 + (8 * Dagger_Level));
                Dagger_AttackDamage_Upgrade.text = "" + (8 + (8 * (Dagger_Level + 1)));

                Dagger_Critical.text = "" + (0 + (2 * Dagger_Level));
                Dagger_Critical_Upgrade.text = "" + (0 + (2 * (Dagger_Level + 1)));

                switch (Dagger_Level)
                {
                    case 1:
                        Dagger_Skill_Text.transform.GetChild(0).gameObject.SetActive(false);
                        break;

                    case 3:
                        Dagger_Skill_Text.transform.GetChild(1).gameObject.SetActive(false);
                        break;

                    case 5:
                        Dagger_Skill_Text.transform.GetChild(2).gameObject.SetActive(false);
                        break;
                }

                Sword_Window.SetActive(false);
                Dagger_Window.SetActive(true);
                Axe_Window.SetActive(false);
                break;

            case PlayerWeaponType.Axe:
                AxeLevel_Text.text = "" + Axe_Level;

                Axe_AttackDamage.text = "" + (20 + (15 * Axe_Level));
                Axe_AttackDamage_Upgrade.text = "" + (20 + (15 * (Axe_Level + 1)));

                Axe_Defense.text = "" + (400 + (200 * Axe_Level));
                Axe_Defense_Upgrade.text = "" + (400 + (200 * (Axe_Level + 1)));

                switch (Axe_Level)
                {
                    case 1:
                        Axe_Skill_Text.transform.GetChild(0).gameObject.SetActive(false);
                        break;

                    case 3:
                        Axe_Skill_Text.transform.GetChild(1).gameObject.SetActive(false);
                        break;

                    case 5:
                        Axe_Skill_Text.transform.GetChild(2).gameObject.SetActive(false);
                        break;
                }

                Sword_Window.SetActive(false);
                Dagger_Window.SetActive(false);
                Axe_Window.SetActive(true);
                break;
        }
    }
    #endregion

    #endregion

    #region 메인화면 버튼
    public void Main_Btn() => StartCoroutine(Main_Window_Coroutine01());

    public void Main_Yes_Btn()
    {
        Reset_Check = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    public void Main_No_Btn()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        StartCoroutine(Main_Window_Close());
    }

    public IEnumerator Main_Window_Coroutine01() // 메인버튼을 클릭했을 떄
    {
        if (MainWindow_Open == false)
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            MainWindow_Open = true;
            timer = 0;
            pauseBarUp.transform.DOLocalMoveY(48f, 0.5f).SetUpdate(true);
            pauseBarDown.transform.DOLocalMoveY(-36, 0.5f).SetUpdate(true);

            while (timer < 1)
            {
                pauseRect.sizeDelta = new Vector2(557.1f, Mathf.Lerp(772.4f, 5f, timer));

                timer += Time.unscaledDeltaTime * 2.5f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            pauseWindow.SetActive(false);
            yield return new WaitForSecondsRealtime(0.1f);
            Main_Window_Canvas.SetActive(true);
            StartCoroutine(Main_Window_Coroutine02());
        }
    }

    public IEnumerator Main_Window_Coroutine02() // 메인 창 열림
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        timer = 0;
        Main_Pole01.transform.DOLocalMoveY(222f, 0.42f).SetUpdate(true);
        Main_Pole02.transform.DOLocalMoveY(-194f, 0.3f).SetUpdate(true);

        while (timer < 1)
        {
            Main_Window.sizeDelta = new Vector2(1730f, Mathf.Lerp(0f, 520f, timer));
            timer += Time.unscaledDeltaTime * 4f;
            yield return null;
        }
        MainWindow_Close = true;
    }

    public IEnumerator Main_Window_Close()
    {
        if (MainWindow_Close == true)
        {
            MainWindow_Close = false;
            timer = 0;
            Fade_Background.DOFade(0, 0.5f).SetUpdate(true);
            Main_Pole01.transform.DOLocalMoveY(30f, 0.42f).SetUpdate(true);
            Main_Pole02.transform.DOLocalMoveY(-30f, 0.3f).SetUpdate(true);

            while (timer < 1)
            {
                Main_Window.sizeDelta = new Vector2(1730f, Mathf.Lerp(520f, 0f, timer));

                timer += Time.unscaledDeltaTime * 4.5f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            Main_Window_Canvas.SetActive(false);
            PauseWindow_Open = true;
            Time.timeScale = 1f;
            MainWindow_Open = false;
            UI_Manager.instance.isCursorFade = false;
        }
    }
    #endregion

    #region 게임종료 버튼
    public void Exit_Btn() => StartCoroutine(Exit_Window_Coroutine01());

    public void Exit_Yes_Btn()
    {
        Debug.Log("앱이 졸료 됩니다.");
        Application.Quit();
    }

    public void Exit_No_Btn()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        StartCoroutine(Exit_Window_Close());
    }

    public IEnumerator Exit_Window_Coroutine01() //게임종료 버튼을 클릭했을 떄
    {
        if (GameExitWindow_Open == false)
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            GameExitWindow_Open = true;
            timer = 0;
            pauseBarUp.transform.DOLocalMoveY(48f, 0.5f).SetUpdate(true);
            pauseBarDown.transform.DOLocalMoveY(-36, 0.5f).SetUpdate(true);

            while (timer < 1)
            {
                pauseRect.sizeDelta = new Vector2(557.1f, Mathf.Lerp(772.4f, 5f, timer));

                timer += Time.unscaledDeltaTime * 2.5f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            pauseWindow.SetActive(false);
            yield return new WaitForSecondsRealtime(0.1f);
            Exit_Window_Canvas.SetActive(true);
            StartCoroutine(Exit_Window_Coroutine02());
        }
    }

    public IEnumerator Exit_Window_Coroutine02() //게임종료 창 열림
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        timer = 0;
        Exit_Pole01.transform.DOLocalMoveY(222f, 0.3f).SetUpdate(true);
        Exit_Pole02.transform.DOLocalMoveY(-211, 0.3f).SetUpdate(true);

        while (timer < 1)
        {
            Exit_Window.sizeDelta = new Vector2(1730f, Mathf.Lerp(0f, 520f, timer));
            timer += Time.unscaledDeltaTime * 4f;
            yield return null;
        }
        GameExitWindow_Close = true;
    }

    public IEnumerator Exit_Window_Close()
    {
        if (GameExitWindow_Close == true)
        {
            GameExitWindow_Close = false;
            timer = 0;
            Fade_Background.DOFade(0, 0.5f).SetUpdate(true);
            Exit_Pole01.transform.DOLocalMoveY(30f, 0.42f).SetUpdate(true);
            Exit_Pole02.transform.DOLocalMoveY(-30f, 0.3f).SetUpdate(true);

            while (timer < 1)
            {
                Exit_Window.sizeDelta = new Vector2(1730f, Mathf.Lerp(520f, 0f, timer));

                timer += Time.unscaledDeltaTime * 4.5f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            Exit_Window_Canvas.SetActive(false);
            PauseWindow_Open = true;
            Time.timeScale = 1f;
            GameExitWindow_Open = false;
            UI_Manager.instance.isCursorFade = false;
        }
    }
    #endregion
}
