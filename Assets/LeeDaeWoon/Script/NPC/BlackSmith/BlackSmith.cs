using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class Weapon
{
    public string name;
    public int level;
    public GameObject weaponSelect;
    public Text levelText;
    public Text skill;
    public Text attackDamage;
    public Text attackDamageUpgrade;
    public Text ability;
    public Text abilityUpgrade;

    [Space(10)]
    public GameObject price;
    public GameObject requiredGold;
    public Text requiredPrice;
    public GameObject maxEnhance;

    public EWeapon eWeapon;
    [TextArea(5, 10)]
    public List<string> skillUpgrade = new List<string>();
}

public enum EWeapon
{
    Sword,
    Dagger,
    Axe,
}

public class BlackSmith : MonoBehaviour
{
    public static BlackSmith instnace { get; private set; }
    void Awake() => instnace = this;

    public List<Weapon> weapon = new List<Weapon>();

    [Header("상호작용 버튼")]
    [SerializeField] Image F_Button; // 상호작용 버튼
    [SerializeField] GameObject Upgrade; // 상호작용 오브젝트
    [SerializeField] Text Upgrade_Text; // 상호작용 텍스트

    [Header("무기 구매 및 강화 창")]
    [SerializeField] GameObject upBar;
    [SerializeField] GameObject downBar;
    [SerializeField] GameObject weaponWindow;
    [SerializeField] RectTransform weaponRect; // 창

    [SerializeField] Image FadeInout;

    const int openBar = 447;
    const int closeBar = 30;
    const float barSpeed = 0.23f;

    const int windowWidth = 1675;
    const int windowHeight = 885;

    [Header("버튼")]
    public Button closeBtn;
    public Button purchaseBtn;
    public Button enhanceBtn;

    public Button selectBtn;

    public Button leftArrowBtn;
    public Button rightArrowBtn;

    [Header("장착 확인")]
    [SerializeField] GameObject selcet;
    [SerializeField] GameObject noSoyu;

    bool isWindowOpenCheck = false;
    bool isCollisionCheck = false;
    bool isBlackSmithWindowClose = false;

    const float fadeSpeed = 0.5f;
    const int maxLevel = 5;

    void Start()
    {
        Upgrade_Text.DOFade(0f, 0f);
        F_Button.DOFade(0f, 0f);

        Btns();
    }

    void Update()
    {
        BlackSmith_Click();
        WeaponStat();

        #region 월드 좌표를 스크린 좌표로 변경을 해준다.
        Upgrade.transform.localPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.localPosition + new Vector3(-3f, 0.3f, 0));
        #endregion
    }


    public void WeaponStat()
    {
        if (weapon[0].level == maxLevel)
        {
            Debug.Log("asdfasdf");
            weapon[0].levelText.text = "Max";
            weapon[0].maxEnhance.SetActive(true);
            weapon[0].requiredGold.SetActive(false);
        }

        else if (weapon[0].level <= maxLevel)
        {
            weapon[0].levelText.text = weapon[0].level.ToString();
            switch (weapon[0].eWeapon)
            {
                #region 검
                case EWeapon.Sword:
                    Player.Instance.stat._level[PlayerWeaponType.Sword] = weapon[0].level;

                    weapon[0].attackDamage.text = (10 + (10 * weapon[0].level)).ToString();
                    weapon[0].attackDamageUpgrade.text = (10 + (10 * (weapon[0].level + 1))).ToString();
                    weapon[0].ability.text = (10 + (10 * weapon[0].level)).ToString();
                    weapon[0].abilityUpgrade.text = (10 + (10 * (weapon[0].level + 1))).ToString();
                    weapon[0].requiredPrice.text = (400 + (200 * weapon[0].level)).ToString();


                    noSoyu.SetActive(false);
                    purchaseBtn.gameObject.SetActive(false);
                    enhanceBtn.gameObject.SetActive(true);

                    if (Player.Instance.stat.weaponType == PlayerWeaponType.Sword)
                    {
                        selcet.SetActive(true);
                        selectBtn.gameObject.SetActive(false);
                    }
                    else
                    {
                        selcet.SetActive(false);
                        selectBtn.gameObject.SetActive(true);
                    }
                    break;
                #endregion

                #region 단검
                case EWeapon.Dagger:
                    Player.Instance.stat._level[PlayerWeaponType.Dagger] = weapon[0].level;

                    weapon[0].attackDamage.text = (8 + (8 * weapon[0].level)).ToString();
                    weapon[0].attackDamageUpgrade.text = (8 + (8 * (weapon[0].level + 1))).ToString();
                    weapon[0].ability.text = (0 + (2 * weapon[0].level)).ToString();
                    weapon[0].abilityUpgrade.text = (0 + (2 * (weapon[0].level + 1))).ToString();
                    weapon[0].requiredPrice.text = (400 + (200 * weapon[0].level)).ToString();


                    if (weapon[0].requiredGold.gameObject.activeSelf || weapon[0].maxEnhance.gameObject.activeSelf)
                    {
                        noSoyu.SetActive(false);
                        if (Player.Instance.stat.weaponType == PlayerWeaponType.Dagger)
                        {
                            selcet.SetActive(true);
                            selectBtn.gameObject.SetActive(false);
                            purchaseBtn.gameObject.SetActive(false);
                            enhanceBtn.gameObject.SetActive(true);
                        }
                        else
                        {
                            selcet.SetActive(false);
                            selectBtn.gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        noSoyu.SetActive(true);
                        selcet.SetActive(false);
                        selectBtn.gameObject.SetActive(false);
                        purchaseBtn.gameObject.SetActive(true);
                        enhanceBtn.gameObject.SetActive(false);
                    }
                    break;
                #endregion

                #region 도끼
                case EWeapon.Axe:
                    Player.Instance.stat._level[PlayerWeaponType.Axe] = weapon[0].level;

                    weapon[0].attackDamage.text = (20 + (15 * weapon[0].level)).ToString();
                    weapon[0].attackDamageUpgrade.text = (20 + (15 * (weapon[0].level + 1))).ToString();
                    weapon[0].ability.text = (400 + (200 * weapon[0].level)).ToString();
                    weapon[0].abilityUpgrade.text = (400 + (200 * (weapon[0].level + 1))).ToString();
                    weapon[0].requiredPrice.text = (400 + (200 * weapon[0].level)).ToString();

                    if (weapon[0].requiredGold.gameObject.activeSelf || weapon[0].maxEnhance.gameObject.activeSelf)
                    {
                        noSoyu.SetActive(false);
                        if (Player.Instance.stat.weaponType == PlayerWeaponType.Axe)
                        {
                            selcet.SetActive(true);
                            selectBtn.gameObject.SetActive(false);
                            purchaseBtn.gameObject.SetActive(false);
                            enhanceBtn.gameObject.SetActive(true);
                        }
                        else
                        {
                            selcet.SetActive(false);
                            selectBtn.gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        noSoyu.SetActive(true);
                        selcet.SetActive(false);
                        selectBtn.gameObject.SetActive(false);
                        purchaseBtn.gameObject.SetActive(true);
                        enhanceBtn.gameObject.SetActive(false);
                    }
                    break;
                    #endregion
            }
        }

        switch (weapon[0].level)
        {
            case 0:
                weapon[0].skill.text = weapon[0].skillUpgrade[0];
                break;

            case 1:
                weapon[0].skill.text = weapon[0].skillUpgrade[1];
                weapon[0].skill.transform.GetChild(0).gameObject.SetActive(false);
                break;

            case 3:
                weapon[0].skill.text = weapon[0].skillUpgrade[2];
                weapon[0].skill.transform.GetChild(1).gameObject.SetActive(false);
                break;

            case 5:
                weapon[0].skill.text = weapon[0].skillUpgrade[3];
                weapon[0].skill.transform.GetChild(2).gameObject.SetActive(false);
                break;
        }
    }

    public void BlackSmith_Click()
    {
        if (Input.GetKeyDown(KeyCode.F) && isCollisionCheck && !isWindowOpenCheck)
        {
            isWindowOpenCheck = true;
            UIManager.instance.isCursorFade = true;
            UIManager.instance.isPlayerControl = true;
            FadeInout.DOFade(0.5f, 1);
            OpenWindow();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isWindowOpenCheck)
        {
            Close();
        }
    }

    void Btns()
    {
        //닫기 버튼을 눌렀을 때
        closeBtn.onClick.AddListener(() =>
        {
            CloseWindow();
        });

        // 왼쪽 방향 버튼을 눌렀을 때
        leftArrowBtn.onClick.AddListener(() =>
        {
            int saveBlackSmiths = weapon.Count;
            weapon[0].weaponSelect.SetActive(false);

            weapon.Insert(0, weapon[saveBlackSmiths - 1]);
            weapon.RemoveAt(saveBlackSmiths);

            weapon[0].weaponSelect.SetActive(true);
        });

        // 오른쪽 방향 버튼을 눌렀을 때
        rightArrowBtn.onClick.AddListener(() =>
        {
            weapon[0].weaponSelect.SetActive(false);

            weapon.Add(weapon[0]);
            weapon.RemoveAt(0);

            weapon[0].weaponSelect.SetActive(true);
        });

        // 구매 버튼을 눌렀을 때
        purchaseBtn.onClick.AddListener(() =>
        {
            int purchaseGold = 300;

            if (GameManager.Instance._coin >= purchaseGold)
            {
                SoundManager.instance.PlaySoundClip("SFX_Buy", SoundType.SFX, 5f);
                GameManager.Instance._coin -= purchaseGold; // 골드 차감

                switch (weapon[0].eWeapon)
                {
                    case EWeapon.Dagger:
                        Player.Instance.stat.weaponType = PlayerWeaponType.Dagger;

                        purchaseBtn.gameObject.SetActive(false); // 구매 버튼 false
                        enhanceBtn.gameObject.SetActive(true); // 강화 버튼 true
                        break;

                    case EWeapon.Axe:
                        Player.Instance.stat.weaponType = PlayerWeaponType.Axe;

                        purchaseBtn.gameObject.SetActive(false); // 구매 버튼 false
                        enhanceBtn.gameObject.SetActive(true); // 강화 버튼 true
                        break;
                }

                weapon[0].price.SetActive(false);
                weapon[0].requiredGold.SetActive(true);

            }
            else
                SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
        });

        // 강화 버튼을 눌렀을 때
        enhanceBtn.onClick.AddListener(() =>
        {
            if (weapon[0].level < maxLevel)
            {
                if (GameManager.Instance._coin >= (400 + (200 * weapon[0].level)))
                {
                    SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);
                    GameManager.Instance._coin -= (400 + (200 * weapon[0].level));
                    ++weapon[0].level;
                }
                else
                    SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
            }
            else
                SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
        });

        // 장착 버튼을 눌렀을 때
        selectBtn.onClick.AddListener(() =>
        {
            switch (weapon[0].eWeapon)
            {
                case EWeapon.Sword:
                    Player.Instance.stat.weaponType = PlayerWeaponType.Sword;
                    break;
                case EWeapon.Dagger:
                    Player.Instance.stat.weaponType = PlayerWeaponType.Dagger;
                    break;
                case EWeapon.Axe:
                    Player.Instance.stat.weaponType = PlayerWeaponType.Axe;
                    break;
            }
        });
    }

    #region 창 연출
    public void Close()
    {
        CloseWindow();
    }

    public void OpenWindow()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);

        UIManager.instance.isCursorFade = true;
        weaponWindow.SetActive(true);
        upBar.transform.DOLocalMoveY(openBar, barSpeed).SetEase(Ease.Linear);
        downBar.transform.DOLocalMoveY(-openBar, barSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            isBlackSmithWindowClose = true;
        });

        weaponRect.DOSizeDelta(new Vector2(windowWidth, windowHeight), barSpeed).SetEase(Ease.Linear).SetUpdate(true);
    }

    public void CloseWindow()
    {
        if (isBlackSmithWindowClose)
        {
            SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);

            UIManager.instance.isCursorFade = false;
            isBlackSmithWindowClose = false;
            FadeInout.DOFade(0, 1);

            upBar.transform.DOLocalMoveY(closeBar, barSpeed).SetEase(Ease.Linear);
            downBar.transform.DOLocalMoveY(-closeBar, barSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                UIManager.instance.isPlayerControl = false;
                weaponWindow.SetActive(false);
                isWindowOpenCheck = false;
            });

            weaponRect.DOSizeDelta(new Vector2(windowWidth, 0), barSpeed).SetEase(Ease.Linear).SetUpdate(true);
        }
    }
    #endregion

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            isCollisionCheck = true;
            Upgrade_Text.DOFade(1, fadeSpeed);
            F_Button.DOFade(1, fadeSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            isCollisionCheck = false;
            Upgrade_Text.DOFade(0, fadeSpeed);
            F_Button.DOFade(0, fadeSpeed);
        }
    }
}
