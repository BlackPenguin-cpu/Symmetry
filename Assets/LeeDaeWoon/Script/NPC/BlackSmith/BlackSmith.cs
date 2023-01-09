using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class Weapon
{
    public string name;
    public Text level;
    public Text skill;
    public Text attackDamage;
    public Text attackDamageUpgrade;
    public Text ability;
    public Text abilityUpgrade;

    [TextArea(5, 10)]
    public List<string> skillUpgrade = new List<string>();
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

    [Header("구매 및 강화 버튼")]
    public GameObject Purchase_Btn;
    public GameObject Enhance_Btn;

    [Header("장착 확인")]
    [SerializeField] GameObject Jang_cak_Btn;
    [SerializeField] GameObject Jang_cak;
    [SerializeField] GameObject No_Soyu;

    public GameObject Weapon;
    public List<GameObject> BlackSmiths = new List<GameObject>();

    [Header("도끼 수칫값")]
    [SerializeField] Text AxeLevel_Text;
    [SerializeField] Text axeSkill;

    [SerializeField] Text Axe_AttackDamage;
    [SerializeField] Text Axe_AttackDamage_Upgrade;
    [SerializeField] Text Axe_Defense;
    [SerializeField] Text Axe_Defense_Upgrade;

    [Space(10)]
    public GameObject Axe_Price;
    public GameObject Axe_Required_Gold;
    [SerializeField] Text Axe_Required_Gold_Price;
    [SerializeField] GameObject Axe_MaxEnhance;

    [Header("검 수칫값")]
    [SerializeField] Text SwordLevel_Text;
    [SerializeField] Text swordSkill;

    [SerializeField] Text Sword_AttackDamage;
    [SerializeField] Text Sword_AttackDamage_Upgrade;
    [SerializeField] Text Sword_MaxHp;
    [SerializeField] Text Sword_MaxHp_Upgrade;

    [Space(10)]
    [SerializeField] GameObject Sword_Required_Gold;
    [SerializeField] Text Sword_Required_Gold_Price;
    [SerializeField] GameObject Sword_MaxEnhance;


    [Header("단검 수칫값")]
    [SerializeField] Text DaggerLevel_Text;
    [SerializeField] Text daggerSkill;

    [SerializeField] Text Dagger_AttackDamage;
    [SerializeField] Text Dagger_AttackDamage_Upgrade;
    [SerializeField] Text Dagger_Critical;
    [SerializeField] Text Dagger_Critical_Upgrade;

    [Space(10)]
    public GameObject Dagger_Price;
    public GameObject Dagger_Required_Gold;
    [SerializeField] Text Dagger_Required_Gold_Price;
    [SerializeField] GameObject Dagger_MaxEnhance;

    void Start()
    {
        Upgrade_Text.DOFade(0f, 0f);
        F_Button.DOFade(0f, 0f);
    }

    void Update()
    {
        BlackSmith_Click();
        Weapon_Stat();

        #region 월드 좌표를 스크린 좌표로 변경을 해준다.
        Upgrade.transform.localPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.localPosition + new Vector3(-3f, 0.3f, 0));
        #endregion
    }


    public void Weapon_Stat()
    {
        int Sword_Level = Player.Instance.stat._level[PlayerWeaponType.Sword];
        int Dagger_Level = Player.Instance.stat._level[PlayerWeaponType.Dagger];
        int Axe_Level = Player.Instance.stat._level[PlayerWeaponType.Axe];

        if (Weapon.transform.GetChild(0).gameObject.activeSelf == true && Sword_Level <= 5)
        {
            if (Sword_Level == 5)
            {
                SwordLevel_Text.text = "Max";
                Sword_Required_Gold.SetActive(false);
                Sword_MaxEnhance.SetActive(true);
            }
            else
                SwordLevel_Text.text = Sword_Level.ToString();

            Sword_AttackDamage.text = (10 + (10 * Sword_Level)).ToString();
            Sword_AttackDamage_Upgrade.text = (10 + (10 * (Sword_Level + 1))).ToString();

            Sword_MaxHp.text = (10 + (10 * Sword_Level)).ToString();
            Sword_MaxHp_Upgrade.text = (10 + (10 * (Sword_Level + 1))).ToString();

            Sword_Required_Gold_Price.text = (400 + (200 * Sword_Level)).ToString();

            Purchase_Btn.SetActive(false);
            Enhance_Btn.SetActive(true);

            No_Soyu.SetActive(false);
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

            switch (Sword_Level)
            {
                case 0:
                    swordSkill.text = weapon[0].skillUpgrade[0];
                    break;

                case 1:
                    swordSkill.text = weapon[0].skillUpgrade[1];
                    swordSkill.transform.GetChild(0).gameObject.SetActive(false);
                    break;

                case 3:
                    swordSkill.text = weapon[0].skillUpgrade[2];
                    swordSkill.transform.GetChild(1).gameObject.SetActive(false);
                    break;

                case 5:
                    swordSkill.text = weapon[0].skillUpgrade[3];
                    swordSkill.transform.GetChild(2).gameObject.SetActive(false);
                    break;
            }
        }

        if (Weapon.transform.GetChild(1).gameObject.activeSelf == true && Dagger_Level <= 5)
        {
            if (Dagger_Level == 5)
            {
                DaggerLevel_Text.text = "Max";
                Dagger_Required_Gold.SetActive(false);
                Dagger_MaxEnhance.SetActive(true);
            }
            else
                DaggerLevel_Text.text = Dagger_Level.ToString();

            Dagger_AttackDamage.text = (8 + (8 * Dagger_Level)).ToString();
            Dagger_AttackDamage_Upgrade.text = (8 + (8 * (Dagger_Level + 1))).ToString();

            Dagger_Critical.text = (0 + (2 * Dagger_Level)).ToString();
            Dagger_Critical_Upgrade.text = (0 + (2 * (Dagger_Level + 1))).ToString();

            Dagger_Required_Gold_Price.text = (400 + (200 * Dagger_Level)).ToString();

            if (Dagger_Required_Gold.activeSelf == true || Dagger_MaxEnhance.activeSelf == true)
            {
                No_Soyu.SetActive(false);
                if (Player.Instance.stat.weaponType == PlayerWeaponType.Dagger)
                {
                    Jang_cak.SetActive(true);
                    Jang_cak_Btn.SetActive(false);
                    Purchase_Btn.SetActive(false);
                    Enhance_Btn.SetActive(true);
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
                Purchase_Btn.SetActive(true);
                Enhance_Btn.SetActive(false);
            }

            switch (Dagger_Level)
            {
                case 0:
                    daggerSkill.text = weapon[1].skillUpgrade[0];
                    break;

                case 1:
                    daggerSkill.text = weapon[1].skillUpgrade[1];

                    daggerSkill.transform.GetChild(0).gameObject.SetActive(false);
                    break;

                case 3:
                    daggerSkill.text = weapon[1].skillUpgrade[2];

                    daggerSkill.transform.GetChild(1).gameObject.SetActive(false);
                    break;

                case 5:
                    daggerSkill.text = weapon[1].skillUpgrade[3];
                    daggerSkill.transform.GetChild(2).gameObject.SetActive(false);
                    break;
            }
        }

        if (Weapon.transform.GetChild(2).gameObject.activeSelf == true && Axe_Level <= 5)
        {
            if (Axe_Level == 5)
            {
                AxeLevel_Text.text = "Max";
                Axe_Required_Gold.SetActive(false);
                Axe_MaxEnhance.SetActive(true);
            }
            else
                AxeLevel_Text.text = Axe_Level.ToString();

            Axe_AttackDamage.text = (20 + (15 * Axe_Level)).ToString();
            Axe_AttackDamage_Upgrade.text = (20 + (15 * (Axe_Level + 1))).ToString();

            Axe_Defense.text = (400 + (200 * Axe_Level)).ToString();
            Axe_Defense_Upgrade.text = (400 + (200 * (Axe_Level + 1))).ToString();

            Axe_Required_Gold_Price.text = (400 + (200 * Axe_Level)).ToString();

            if (Axe_Required_Gold.activeSelf == true || Axe_MaxEnhance.activeSelf == true)
            {
                No_Soyu.SetActive(false);
                if (Player.Instance.stat.weaponType == PlayerWeaponType.Axe)
                {
                    Jang_cak.SetActive(true);
                    Jang_cak_Btn.SetActive(false);
                    Purchase_Btn.SetActive(false);
                    Enhance_Btn.SetActive(true);
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
                Purchase_Btn.SetActive(true);
                Enhance_Btn.SetActive(false);
            }

            switch (Axe_Level)
            {
                case 0:
                    axeSkill.text = weapon[2].skillUpgrade[0];
                    break;

                case 1:
                    axeSkill.text = weapon[2].skillUpgrade[1];

                    axeSkill.transform.GetChild(0).gameObject.SetActive(false);
                    break;

                case 3:
                    axeSkill.text = weapon[2].skillUpgrade[2];

                    axeSkill.transform.GetChild(1).gameObject.SetActive(false);
                    break;

                case 5:
                    axeSkill.text = weapon[2].skillUpgrade[3];

                    axeSkill.transform.GetChild(2).gameObject.SetActive(false);
                    break;
            }
        }

    }

    #region 버튼 클릭
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
    #endregion

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
