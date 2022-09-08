using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlackSmith : MonoBehaviour
{
    public static BlackSmith Inst { get; private set; }
    void Awake() => Inst = this;


    public bool BlackSmithWindow_Close = false;

    [Header("상호작용 버튼")]
    public Image F_Button; // 상호작용 버튼
    public GameObject Upgrade; // 상호작용 오브젝트
    public Text Upgrade_Text; // 상호작용 텍스트
    public bool Collision_Check = true; // 충돌 했는지 체크

    [Header("무기 구매 및 강화 창")]
    private float timer; // 창 열리는 속도
    public GameObject Pole_01; // 봉_01
    public GameObject Pole_02; // 봉_02
    public GameObject Weapon_Purchase_Window; // 창 오브젝트
    public RectTransform WeaponPurchase_RectWindow; // 창

    public Image FadeInout;

    public bool WindowOpen_Check = false;

    [Header("구매 및 강화 버튼")]
    public GameObject Purchase_Btn;
    public GameObject Enhance_Btn;

    [Header("장착 확인")]
    public GameObject Jang_cak_Btn;
    public GameObject Jang_cak;
    public GameObject No_Soyu;

    public GameObject Weapon;
    public List<GameObject> BlackSmiths = new List<GameObject>();

    [Header("도끼 수칫값")]
    public Text AxeLevel_Text;
    public Text Axe_Skill_Text;

    public Text Axe_AttackDamage;
    public Text Axe_AttackDamage_Upgrade;
    public Text Axe_Defense;
    public Text Axe_Defense_Upgrade;

    [Space(10)]
    public GameObject Axe_Price;
    public GameObject Axe_Required_Gold;
    public Text Axe_Required_Gold_Price;
    public GameObject Axe_MaxEnhance;

    [Header("검 수칫값")]
    public Text SwordLevel_Text;
    public Text Sword_Skill_Text;

    public Text Sword_AttackDamage;
    public Text Sword_AttackDamage_Upgrade;
    public Text Sword_MaxHp;
    public Text Sword_MaxHp_Upgrade;

    [Space(10)]
    public GameObject Sword_Required_Gold;
    public Text Sword_Required_Gold_Price;
    public GameObject Sword_MaxEnhance;


    [Header("단검 수칫값")]
    public Text DaggerLevel_Text;
    public Text Dagger_Skill_Text;

    public Text Dagger_AttackDamage;
    public Text Dagger_AttackDamage_Upgrade;
    public Text Dagger_Critical;
    public Text Dagger_Critical_Upgrade;

    [Space(10)]
    public GameObject Dagger_Price;
    public GameObject Dagger_Required_Gold;
    public Text Dagger_Required_Gold_Price;
    public GameObject Dagger_MaxEnhance;

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
                    Sword_Skill_Text.text = "<color=#747474>강철의 마음: 고정적으로 데미지가 5감소한다. \n\n</color>" +
                                            "<color=#747474>집념: 체력이 30%이하가 될 시 공격력, \n</color>" +
                                            "<color=#747474>      방어력이 30% 상승한다. \n\n</color>" +
                                            "<color=#747474>신의 가호: 체력이 0이 되는 공격을 받을 때 \n</color>" +
                                            "<color=#747474>           한번 버틴다. (1만 남고 버팀)</color>";
                    break;

                case 1:
                    Sword_Skill_Text.text = "강철의 마음: 고정적으로 데미지가 5감소한다. \n\n" +
                                            "<color=#747474>집념: 체력이 30%이하가 될 시 공격력, \n</color>" +
                                            "<color=#747474>      방어력이 30% 상승한다. \n\n</color>" +
                                            "<color=#747474>신의 가호: 체력이 0이 되는 공격을 받을 때 \n</color>" +
                                            "<color=#747474>           한번 버틴다. (1만 남고 버팀)</color>";
                    Sword_Skill_Text.transform.GetChild(0).gameObject.SetActive(false);
                    break;

                case 3:
                    Sword_Skill_Text.text = "강철의 마음: 고정적으로 데미지가 5감소한다. \n\n" +
                                            "집념: 체력이 30%이하가 될 시 공격력, \n" +
                                            "      방어력이 30% 상승한다. \n\n" +
                                            "<color=#747474>신의 가호: 체력이 0이 되는 공격을 받을 때 \n</color>" +
                                            "<color=#747474>           한번 버틴다. (1만 남고 버팀)</color>";
                    Sword_Skill_Text.transform.GetChild(1).gameObject.SetActive(false);
                    break;

                case 5:
                    Sword_Skill_Text.text = "강철의 마음: 고정적으로 데미지가 5감소한다. \n\n" +
                                            "집념: 체력이 30%이하가 될 시 공격력, \n" +
                                            "      방어력이 30% 상승한다. \n\n" +
                                            "신의 가호: 체력이 0이 되는 공격을 받을 때 \n" +
                                            "           한번 버틴다. (1만 남고 버팀)";
                    Sword_Skill_Text.transform.GetChild(2).gameObject.SetActive(false);
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
                    Dagger_Skill_Text.text ="<color=#747474>신속: 이동속도가 10% 증가하고 대쉬 \n</color>" +
                                            "<color=#747474>      횟수가 1회 추가된다. \n\n</color>" +
                                            "<color=#747474>연쇄 공격: 적 처치 후 5초간 이동속도, \n</color>" +
                                            "<color=#747474>           공격속도 5%증가(최대 5중첩) \n\n</color>"+
                                            "<color=#747474>비장의 패: 3타에 한 번씩 수리검을 던진다.</color>";
                    break;

                case 1:
                    Dagger_Skill_Text.text ="신속: 이동속도가 10% 증가하고 대쉬 \n" +
                                            "      횟수가 1회 추가된다. \n\n" +
                                            "<color=#747474>연쇄 공격: 적 처치 후 5초간 이동속도, \n</color>" +
                                            "<color=#747474>           공격속도 5%증가(최대 5중첩) \n\n</color>" +
                                            "<color=#747474>비장의 패: 3타에 한 번씩 수리검을 던진다.</color>";

                    Dagger_Skill_Text.transform.GetChild(0).gameObject.SetActive(false);
                    break;

                case 3:
                    Dagger_Skill_Text.text ="신속: 이동속도가 10% 증가하고 대쉬 \n" +
                                            "      횟수가 1회 추가된다. \n\n" +
                                            "연쇄 공격: 적 처치 후 5초간 이동속도, \n" +
                                            "           공격속도 5%증가(최대 5중첩) \n\n" +
                                            "<color=#747474>비장의 패: 3타에 한 번씩 수리검을 던진다.</color>";

                    Dagger_Skill_Text.transform.GetChild(1).gameObject.SetActive(false);
                    break;

                case 5:
                    Dagger_Skill_Text.text = "신속: 이동속도가 10% 증가하고 대쉬 \n" +
                                            "      횟수가 1회 추가된다. \n\n" +
                                            "연쇄 공격: 적 처치 후 5초간 이동속도, \n" +
                                            "           공격속도 5%증가(최대 5중첩) \n\n" +
                                            "비장의 패: 3타에 한 번씩 수리검을 던진다.";
                    Dagger_Skill_Text.transform.GetChild(2).gameObject.SetActive(false);
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
                    Axe_Skill_Text.text = "<color=#747474>부딪히기: 대쉬에 공격판정이 생긴다. \n\n</color>" +
                                            "<color=#747474>지진: 공격을 맞으면 2초동안 기절한다. \n\n</color>" +
                                            "<color=#747474>분화 : 2번째타를 공격 시 화염구들이 \n</color>" +
                                            "<color=#747474>       소환돼 모두를 공격</color>";
                    break;

                case 1:
                    Axe_Skill_Text.text = "부딪히기: 대쉬에 공격판정이 생긴다. \n\n" +
                                            "<color=#747474>지진: 공격을 맞으면 2초동안 기절한다. \n\n</color>" +
                                            "<color=#747474>분화 : 2번째타를 공격 시 화염구들이 \n</color>" +
                                            "<color=#747474>       소환돼 모두를 공격</color>";

                    Axe_Skill_Text.transform.GetChild(0).gameObject.SetActive(false);
                    break;

                case 3:
                    Axe_Skill_Text.text ="부딪히기: 대쉬에 공격판정이 생긴다. \n\n" +
                                            "지진: 공격을 맞으면 2초동안 기절한다. \n\n" +
                                            "<color=#747474>분화 : 2번째타를 공격 시 화염구들이 \n</color>" +
                                            "<color=#747474>       소환돼 모두를 공격</color>";

                    Axe_Skill_Text.transform.GetChild(1).gameObject.SetActive(false);
                    break;

                case 5:
                    Axe_Skill_Text.text ="부딪히기: 대쉬에 공격판정이 생긴다. \n\n" +
                                            "지진: 공격을 맞으면 2초동안 기절한다. \n\n" +
                                            "분화 : 2번째타를 공격 시 화염구들이 \n" +
                                            "       소환돼 모두를 공격";

                    Axe_Skill_Text.transform.GetChild(2).gameObject.SetActive(false);
                    break;
            }
        }

    }

    #region 버튼 클릭
    public void BlackSmith_Click()
    {
        if (Input.GetKeyDown(KeyCode.F) && Collision_Check == false && WindowOpen_Check == false)
        {
            UI_Manager.Inst.Cursor_Fade = true;
            UI_Manager.Inst.PlayerMove_control = true;
            FadeInout.DOFade(0.5f, 1f);
            StartCoroutine(Open_Window());
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
        StartCoroutine(Close_Window());
    }

    public IEnumerator Open_Window()
    {
        UI_Manager.Inst.Cursor_Fade = true;
        Weapon_Purchase_Window.SetActive(true);
        timer = 0f;
        Pole_01.transform.DOLocalMoveY(452, 0.5f);
        Pole_02.transform.DOLocalMoveY(-452, 0.5f);

        while (timer < 1)
        {
            WeaponPurchase_RectWindow.sizeDelta = new Vector2(1696.425f, Mathf.Lerp(0, 931.6482f, timer));
            timer += Time.deltaTime * 3f;
            yield return null;
        }
        BlackSmithWindow_Close = true;
    }

    public IEnumerator Close_Window()
    {
        if (BlackSmithWindow_Close == true)
        {
            UI_Manager.Inst.Cursor_Fade = false;
            BlackSmithWindow_Close = false;
            FadeInout.DOFade(0f, 1f);

            timer = 0f;
            Pole_01.transform.DOLocalMoveY(30, 0.5f);
            Pole_02.transform.DOLocalMoveY(-30, 0.5f);

            while (timer < 1)
            {
                WeaponPurchase_RectWindow.sizeDelta = new Vector2(1696.425f, Mathf.Lerp(931.6482f, 0, timer));
                timer += Time.deltaTime * 3f;
                yield return null;
            }
            UI_Manager.Inst.PlayerMove_control = false;
            Weapon_Purchase_Window.SetActive(false);
            WindowOpen_Check = false;
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
