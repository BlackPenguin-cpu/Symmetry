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

    public bool BlackSmithWindow_Close = false;

    [Header("상호작용 버튼")]
    [SerializeField] Image F_Button; // 상호작용 버튼
    [SerializeField] GameObject Upgrade; // 상호작용 오브젝트
    [SerializeField] Text Upgrade_Text; // 상호작용 텍스트
    public bool Collision_Check = true; // 충돌 했는지 체크

    [Header("무기 구매 및 강화 창")]
    float timer;
    [SerializeField] GameObject upBar;
    [SerializeField] GameObject downBar;
    [SerializeField] GameObject weaponWindow;
    [SerializeField] RectTransform weaponRect; // 창

    [SerializeField] Image FadeInout;

    public bool WindowOpen_Check = false;

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
    [SerializeField] GameObject Jang_cak_Btn;
    [SerializeField] GameObject Jang_cak;
    [SerializeField] GameObject No_Soyu;

    public GameObject weaponObj;

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


                    No_Soyu.SetActive(false);
                    purchaseBtn.gameObject.SetActive(false);
                    enhanceBtn.gameObject.SetActive(true);

                    if (Player.Instance.stat.weaponType == PlayerWeaponType.Sword)
                    {
                        Jang_cak.SetActive(true);
                        Jang_cak_Btn.SetActive(false);
                    }
                    else
                    {
                        Jang_cak.SetActive(false);
                        Jang_cak_Btn.SetActive(true);
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
                        No_Soyu.SetActive(false);
                        if (Player.Instance.stat.weaponType == PlayerWeaponType.Dagger)
                        {
                            Jang_cak.SetActive(true);
                            Jang_cak_Btn.SetActive(false);
                            purchaseBtn.gameObject.SetActive(false);
                            enhanceBtn.gameObject.SetActive(true);
                        }
                        else
                        {
                            Jang_cak.SetActive(false);
                            Jang_cak_Btn.SetActive(true);
                        }
                    }
                    else
                    {
                        No_Soyu.SetActive(true);
                        Jang_cak.SetActive(false);
                        Jang_cak_Btn.SetActive(false);
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
                        No_Soyu.SetActive(false);
                        if (Player.Instance.stat.weaponType == PlayerWeaponType.Axe)
                        {
                            Jang_cak.SetActive(true);
                            Jang_cak_Btn.SetActive(false);
                            purchaseBtn.gameObject.SetActive(false);
                            enhanceBtn.gameObject.SetActive(true);
                        }
                        else
                        {
                            Jang_cak.SetActive(false);
                            Jang_cak_Btn.SetActive(true);
                        }
                    }
                    else
                    {
                        No_Soyu.SetActive(true);
                        Jang_cak.SetActive(false);
                        Jang_cak_Btn.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.F) && Collision_Check == false && WindowOpen_Check == false)
        {
            UIManager.instance.isCursorFade = true;
            UIManager.instance.isPlayerControl = true;
            FadeInout.DOFade(0.5f, 1f);
            StartCoroutine(OpenWindow());
            WindowOpen_Check = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && BlackSmithWindow_Close == true)
        {
            Close();
        }
    }

    void Btns()
    {
        //닫기 버튼을 눌렀을 때
        closeBtn.onClick.AddListener(() =>
        {
            StartCoroutine(CloseWindow());
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
        StartCoroutine(CloseWindow());
    }

    public IEnumerator OpenWindow()
    {
        timer = 0f;
        UIManager.instance.isCursorFade = true;
        weaponWindow.SetActive(true);
        upBar.transform.DOLocalMoveY(openBar, barSpeed).SetEase(Ease.Linear);
        downBar.transform.DOLocalMoveY(-openBar, barSpeed).SetEase(Ease.Linear);

        while (timer < 1)
        {
            weaponRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            timer += Time.deltaTime * 4;
            yield return null;
        }
        BlackSmithWindow_Close = true;
    }

    public IEnumerator CloseWindow()
    {
        if (BlackSmithWindow_Close == true)
        {
            timer = 0f;
            UIManager.instance.isCursorFade = false;
            BlackSmithWindow_Close = false;
            FadeInout.DOFade(0f, 1f);

            upBar.transform.DOLocalMoveY(closeBar, barSpeed).SetEase(Ease.Linear);
            downBar.transform.DOLocalMoveY(-closeBar, barSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                UIManager.instance.isPlayerControl = false;
                weaponWindow.SetActive(false);
                WindowOpen_Check = false;
            });

            while (timer < 1)
            {
                weaponRect.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
                timer += Time.deltaTime * 4;
                yield return null;
            }
        }
    }
    #endregion

    #region 충돌 체크
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<ITypePlayer>() != null)
        {
            Collision_Check = false;
            Upgrade_Text.DOFade(1f, 0.5f);
            F_Button.DOFade(1f, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ITypePlayer>() != null)
        {
            Collision_Check = true;
            Upgrade_Text.DOFade(0f, 0.5f);
            F_Button.DOFade(0f, 0.5f);
        }
    }
    #endregion
}
