using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Stop_Manager : MonoBehaviour
{
    public static Stop_Manager Inst { get; private set; }

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

    [Header("일시정지 창")]
    public GameObject Pause_Pole01; // 일시정지 창의 윗 봉
    public GameObject Pause_Pole02; // 일시정지 창의 아랫 봉 
    public RectTransform Pause_Window; // 일시정지 창의 중간
    public GameObject Pause_Window_Canvas; // 일시정지 창

    [Header("설정 창")]
    public GameObject Setting_Pole01; // 설정 창의 윗 봉
    public GameObject Setting_Pole02; // 설정 창의 아랫 봉 
    public RectTransform Setting_Window; // 설정 창의 중간 
    public GameObject Setting_Window_Canvas; // 설정 창
    public Slider Effect_Slider;
    public Slider BGM_Slider;

    public Text Resolution;
    public int Resolution_Num;

    [Header("플레이어 창")]
    public GameObject Player_Pole01; // 플레이어 창의 윗 봉
    public GameObject Player_Pole02; // 플레이어 창의 아랫 봉 
    public RectTransform Player_Window; // 플레이어 창의 중간 

    [Space(10f)]
    public GameObject Player_Item_Window; // 아이템 창
    public GameObject Player_Weapon_Window; // 무기 창
    public GameObject Player_Window_Canvas; // 플레이어 창
    public bool WI_Check = true; // 현재 무기창이 열려져 있는지 아이템 창이 열려져 있는지 확인한다.

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

    void Start()
    {
        WI_Check = true;
        InPause = false;
        Start_Sound();
    }

    void Update()
    {
        Item_Log();
        WeaponType();
        Resolution_Size();
        Sound_Control();

        Main_Reset();

        // ESC 키를 누르면 일시정지 창이 열린다.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseWindow_Open == false && SettingWindow_Open == false && MainWindow_Open == false && GameExitWindow_Open == false && PlayerWindow_Open == false)
            {
                PauseWindow_Open = true;
                Pause_Window_Canvas.SetActive(true);
                Fade_Background.DOFade(0.5f, 0.5f);
                UI_Manager.instance.Cursor_Fade = true;
                StartCoroutine(Pause_Window_Open());
            }

            if (PauseWindow_Close == true && SettingWindow_Open == false && MainWindow_Open == false && GameExitWindow_Open == false && PlayerWindow_Open == false)
                StartCoroutine(Back_Window_Coroutine());

            if (SettingWindow_Open == true)
                Setting_Close_Btn();

            if (MainWindow_Open == true)
                Main_No_Btn();

            if (GameExitWindow_Open == true)
                Exit_No_Btn();

            if (PlayerWindow_Open == true && PlayerWindow_Check == true)
                Player_Close_Btn();
        }
    }

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
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

    #region 일시정지 창
    public IEnumerator Pause_Window_Open()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        timer = 0;
        // 일시정지 창 봉
        Pause_Pole01.transform.DOLocalMoveY(374f, 0.5f);
        Pause_Pole02.transform.DOLocalMoveY(-385.76f, 0.5f);

        while (timer < 1)
        {
            Pause_Window.sizeDelta = new Vector2(610f, Mathf.Lerp(0, 772.4f, timer));

            timer += Time.deltaTime * 3.3f;
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0f;

        PauseWindow_Close = true;
        BackBtn_Check = true;
    }
    #endregion

    #region 돌아가기 버튼
    public void Back_Btn()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        StartCoroutine(Back_Window_Coroutine());
    }

    public IEnumerator Back_Window_Coroutine() // 돌아가기 버튼
    {
        if (BackBtn_Check == true)
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
            BackBtn_Check = false;
            PauseWindow_Close = false;
            timer = 0;
            Fade_Background.DOFade(0, 0.5f).SetUpdate(true);
            Pause_Pole01.transform.DOLocalMoveY(48f, 0.5f).SetUpdate(true);
            Pause_Pole02.transform.DOLocalMoveY(-36, 0.5f).SetUpdate(true);

            while (timer < 1)
            {
                Pause_Window.sizeDelta = new Vector2(610f, Mathf.Lerp(772.4f, 5f, timer));

                timer += Time.unscaledDeltaTime * 2.5f;
                yield return null;
            }
            UI_Manager.instance.Cursor_Fade = false;
            yield return new WaitForSecondsRealtime(0.1f);
            Pause_Window_Canvas.SetActive(false);
            PauseWindow_Open = false;
            Time.timeScale = 1f;
        }
    }
    #endregion

    #region 설정 버튼
    public void Setting_Btn() => StartCoroutine(Setting_Window_Coroutine01());

    public void Setting_Close_Btn()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        StartCoroutine(Setting_Window_Close());
    }

    public IEnumerator Setting_Window_Coroutine01() // 설정버튼을 클릭했을 떄
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

        if (SettingWindow_Open == false)
        {
            SettingWindow_Open = true;
            timer = 0;
            Pause_Pole01.transform.DOLocalMoveY(48f, 0.5f).SetUpdate(true);
            Pause_Pole02.transform.DOLocalMoveY(-36, 0.5f).SetUpdate(true);

            while (timer < 1)
            {
                Pause_Window.sizeDelta = new Vector2(557.1f, Mathf.Lerp(772.4f, 5f, timer));

                timer += Time.unscaledDeltaTime * 2.5f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            Pause_Window_Canvas.SetActive(false);
            yield return new WaitForSecondsRealtime(0.1f);
            Setting_Window_Canvas.SetActive(true);
            StartCoroutine(Setting_Window_Coroutine02());
        }
    }

    public IEnumerator Setting_Window_Coroutine02() // 설정 창 열림
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);

        timer = 0;
        Setting_Pole01.transform.DOLocalMoveY(447.5388f, 0.48f).SetUpdate(true);
        Setting_Pole02.transform.DOLocalMoveY(-428f, 0.48f).SetUpdate(true);

        while (timer < 1)
        {
            Setting_Window.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(0f, 916.9f, timer));
            timer += Time.unscaledDeltaTime * 3f;
            yield return null;
        }
        SettingWindow_Close = true;
    }

    public IEnumerator Setting_Window_Close() // 설정 창 닫힘
    {
        if (SettingWindow_Close == true)
        {
            SettingWindow_Close = false;
            timer = 0;
            Fade_Background.DOFade(0, 0.5f).SetUpdate(true);
            Setting_Pole01.transform.DOLocalMoveY(30, 0.48f).SetUpdate(true);
            Setting_Pole02.transform.DOLocalMoveY(-30, 0.48f).SetUpdate(true);

            while (timer < 1)
            {
                Setting_Window.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(916.9f, 0f, timer));

                timer += Time.unscaledDeltaTime * 3f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            Setting_Window_Canvas.SetActive(false);
            Time.timeScale = 1f;
            SettingWindow_Open = false;
            UI_Manager.instance.Cursor_Fade = false;
        }
    }

    public void Resolution_Size()
    {
        switch (Resolution_Num)
        {
            case 0:
                Resolution.text = "1920 * 1080";
                Screen.SetResolution(1920, 1080, true);
                break;
            case 1:
                Resolution.text = "1680 * 1050";
                Screen.SetResolution(1680, 1050, true);
                break;
            case 2:
                Resolution.text = "1400 * 1050";
                Screen.SetResolution(1400, 1050, true);
                break;
            case 3:
                Resolution.text = "1280 * 600";
                Screen.SetResolution(1280, 600, true);
                break;

        }
    }

    public void ResolutionSize_Left()
    {
        if (Resolution_Num > 0)
            Resolution_Num--;
    }

    public void ResolutionSize_Right()
    {
        if (Resolution_Num < 3)
            Resolution_Num++;
    }

    public void Start_Sound() =>
        BGM_Slider.value = SoundManager.instance.audioSourceClasses[SoundType.BGM].audioVolume;

    public void Sound_Control()
    {
        SoundManager.instance.audioSourceClasses[SoundType.BGM].audioVolume = BGM_Slider.value;
    }
    #endregion

    #region 플레이어 버튼
    public void Player_Btn() => StartCoroutine(Player_Window_Coroutine01());

    public void Player_Close_Btn()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        StartCoroutine(Player_Window_Close());
    }

    public void Item_Log()
    {
        if (Card_Manager.instance.Item_bool == false)
        {
            Player_Item_Log.transform.GetChild(Card_Manager.instance.Item_Check).GetComponent<Image>().sprite = ItemDA_Have[Card_Manager.instance.Item_Check].Item_Icon;
            Player_Item_Log.transform.GetChild(Card_Manager.instance.Item_Check).gameObject.SetActive(true);

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
            Pause_Pole01.transform.DOLocalMoveY(48f, 0.5f).SetUpdate(true);
            Pause_Pole02.transform.DOLocalMoveY(-36, 0.5f).SetUpdate(true);

            while (timer < 1)
            {
                Pause_Window.sizeDelta = new Vector2(557.1f, Mathf.Lerp(772.4f, 5f, timer));

                timer += Time.unscaledDeltaTime * 2.5f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            Pause_Window_Canvas.SetActive(false);
            yield return new WaitForSecondsRealtime(0.1f);
            Player_Window_Canvas.SetActive(true);
            StartCoroutine(Player_Window_Coroutine02());
        }
    }

    public IEnumerator Player_Window_Coroutine02() // 플레이어 창 열림
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        timer = 0;
        Player_Pole01.transform.DOLocalMoveY(447.5388f, 0.48f).SetUpdate(true);
        Player_Pole02.transform.DOLocalMoveY(-445.16f, 0.48f).SetUpdate(true);

        while (timer < 1)
        {
            Player_Window.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(0f, 916.9f, timer));
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
            Player_Pole01.transform.DOLocalMoveY(30, 0.48f).SetUpdate(true);
            Player_Pole02.transform.DOLocalMoveY(-30, 0.48f).SetUpdate(true);

            while (timer < 1)
            {
                Player_Window.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(916.9f, 0f, timer));

                timer += Time.unscaledDeltaTime * 3f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            Player_Window_Canvas.SetActive(false);
            PauseWindow_Open = true;
            Time.timeScale = 1f;
            PlayerWindow_Open = false;
            UI_Manager.instance.Cursor_Fade = false;
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
            Player_Pole01.transform.DOLocalMoveY(30, 0.48f).SetUpdate(true);
            Player_Pole02.transform.DOLocalMoveY(-30, 0.48f).SetUpdate(true);

            while (timer < 1)
            {
                Player_Window.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(916.9f, 0f, timer));

                timer += Time.unscaledDeltaTime * 3f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            Player_Weapon_Window.SetActive(true);
            Player_Item_Window.SetActive(false);
            yield return new WaitForSecondsRealtime(0.1f);
            StartCoroutine(Player_Weapon_Open02());
        }
    }

    public IEnumerator Player_Weapon_Open02()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        timer = 0;
        Fade_Background.DOFade(0.5f, 0.5f).SetUpdate(true);
        Player_Pole01.transform.DOLocalMoveY(447.5388f, 0.48f).SetUpdate(true);
        Player_Pole02.transform.DOLocalMoveY(-445.16f, 0.48f).SetUpdate(true);

        while (timer < 1)
        {
            Player_Window.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(0f, 916.9f, timer));
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
            Player_Pole01.transform.DOLocalMoveY(30, 0.48f).SetUpdate(true);
            Player_Pole02.transform.DOLocalMoveY(-30, 0.48f).SetUpdate(true);

            while (timer < 1)
            {
                Player_Window.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(916.9f, 0f, timer));

                timer += Time.unscaledDeltaTime * 3f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            Player_Weapon_Window.SetActive(false);
            Player_Item_Window.SetActive(true);
            yield return new WaitForSecondsRealtime(0.1f);
            StartCoroutine(Player_Item_Open02());
        }
    }

    public IEnumerator Player_Item_Open02()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX, 1f);
        timer = 0;
        Fade_Background.DOFade(0.5f, 0.5f).SetUpdate(true);
        Player_Pole01.transform.DOLocalMoveY(447.5388f, 0.48f).SetUpdate(true);
        Player_Pole02.transform.DOLocalMoveY(-445.16f, 0.48f).SetUpdate(true);

        while (timer < 1)
        {
            Player_Window.sizeDelta = new Vector2(1732.5f, Mathf.Lerp(0f, 916.9f, timer));
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
            Pause_Pole01.transform.DOLocalMoveY(48f, 0.5f).SetUpdate(true);
            Pause_Pole02.transform.DOLocalMoveY(-36, 0.5f).SetUpdate(true);

            while (timer < 1)
            {
                Pause_Window.sizeDelta = new Vector2(557.1f, Mathf.Lerp(772.4f, 5f, timer));

                timer += Time.unscaledDeltaTime * 2.5f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            Pause_Window_Canvas.SetActive(false);
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
            UI_Manager.instance.Cursor_Fade = false;
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
            Pause_Pole01.transform.DOLocalMoveY(48f, 0.5f).SetUpdate(true);
            Pause_Pole02.transform.DOLocalMoveY(-36, 0.5f).SetUpdate(true);

            while (timer < 1)
            {
                Pause_Window.sizeDelta = new Vector2(557.1f, Mathf.Lerp(772.4f, 5f, timer));

                timer += Time.unscaledDeltaTime * 2.5f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            Pause_Window_Canvas.SetActive(false);
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
            UI_Manager.instance.Cursor_Fade = false;
        }
    }
    #endregion
}
