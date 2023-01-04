using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Skill_Window : MonoBehaviour
{
    public static Skill_Window instance { get; private set; }
    void Awake() => instance = this;

    [Header("스킬창 시간")]
    public float Timer = 0;

    [Header("스킬 창")]
    public RectTransform Pole_01 = new RectTransform();
    public RectTransform Pole_02 = new RectTransform();
    public RectTransform Skill_RectTransform = new RectTransform();

    [Header("기본 스킬 이미지")]
    public Image Basics_Skill_A;
    public Image Basics_Skill_S;

    [Header("구매 후 창")]
    public GameObject AfterPurchase_Window_Prefab; // 스킬 적용 창 프리팹
    public GameObject AfterPurchase_Key; // 선택 키 오브젝트


    public GameObject AfterPurchase_Skill; // 스킬 적용하기 전 스킬 이미지
    public GameObject AfterPurchase_Window; // 스킬적용 창
    public GameObject AfterPurchase_Skill_Box; // 스킬 적용하기 전 스킬박스 이미지
    public GameObject AfterPurchase_LeftDirection; // 왼쪽 화살표

    [Header("스킬 좌표")]
    [SerializeField] GameObject Skill_Shop;

    public Image AfterPurchase_Top_Light;
    public Image AfterPurchase_Bottom_Light;

    public int SkillNum; // 현재 몇 번째 구매 스킬과 충돌했는지 숫자 확인
    public bool UpDown = true; // 현재 위 인지 아래 인지 확인
    public bool Purchase = true; // 현재 구매중인지 아닌지 확인
    public bool SkillWindow = true; // 스킬 창이 나오는지 확인
    public bool UpDown_Limit = true; // 위아래 제한
    public bool SkillColider_Check; // 현재 구매 스킬들과 충돌 했는지 체크 확인

    // 몇 번째 스킬을 구매했는지 확인
    public bool Skill01_Purchase = true;
    public bool Skill02_Purchase = true;
    public bool Skill03_Purchase = true;

    public int RandomTest;

    public bool MoreThanOnce_Purchase = true; // 1번 이상 스킬을 구매할 시

    SkillScript SeletSkill;

    void Start()
    {
        #region GameObject.Find
        Basics_Skill_A = GameObject.Find("Basic_Skill01").GetComponent<Image>();
        Basics_Skill_S = GameObject.Find("Basic_Skill02").GetComponent<Image>();

        AfterPurchase_Window = GameObject.Find("After_Purchase");
        AfterPurchase_Key = GameObject.Find("Direction_Key");
        AfterPurchase_Skill = GameObject.Find("After_Skill_Image");
        AfterPurchase_Skill_Box = GameObject.Find("After_Skill_Box");
        AfterPurchase_LeftDirection = GameObject.Find("Left_Direction");
        #endregion

        AfterPurchase_Left();
        SkillColider_Check = false;
        AfterPurchase_Window.gameObject.SetActive(false);

        // 게임이 시작할 때 오브젝트의 3번째 자식(스킬 창)을 꺼준다.
        this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
    }

    void Update()
    {
        ScreentoWorld();
        Skill_Purchase();
        AfterPurchase_UpDown();
    }

    void ScreentoWorld()
    {
        #region 월드 좌표를 스크린 좌표로 변경을 해준다.
        transform.localPosition = Camera.main.WorldToScreenPoint(Skill_Shop.gameObject.transform.position + new Vector3(-17.5f, -4.4f, 0));
        #endregion
    }

    public void Skill_Window_Active()
    {
        // 이 스크립트가 들어가 있는 오브젝트의 3번째 자식(스킬 창)을 켜준다.
        this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void Skill_Purchase()
    {
        // F키를 통하여 구매 혹은 스킬적용을 할 수 있다.
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 스킬구매
            if (Purchase == true && SkillColider_Check == true && (GameManager.Instance._coin >= Skill_List.instance.Left_Gold || GameManager.Instance._coin >= Skill_List.instance.Among_Gold || GameManager.Instance._coin >= Skill_List.instance.Right_Gold))
            {
                SoundManager.instance.PlaySoundClip("SFX_Buy", SoundType.SFX, 5f);
                UIManager.instance.PlayerMove_control = true;

                SeletSkill = Skill_Manager.instance.Skill[SkillNum];
                AfterPurchase_Skill.GetComponent<Image>().sprite = SeletSkill.sprite;
                GameManager.Instance._coin -= SeletSkill.price[0];

                // 정상작동 웨이브 : 5 / 10 / 15
                //switch(WaveManager.Instance.m_WaveNum)
                //{
                //    case 3:
                //        GameManager.Instance._coin -= SeletSkill.price[0];
                //        break;

                //    case 5:
                //        GameManager.Instance._coin -= SeletSkill.price[1];
                //        break;

                //    case 15:
                //        GameManager.Instance._coin -= SeletSkill.price[2];
                //        break;
                //}

                Skill_Manager.instance.Skill_Have.Add(SeletSkill);
                Skill_Manager.instance.Skill_Shop.Add(SeletSkill);

                Purchase = false; // 이것을 통하여 스킬구매 -> 스킬적용으로 넘겨준다.

                this.gameObject.transform.GetChild(SkillNum).GetChild(2).gameObject.SetActive(true);
                this.gameObject.transform.GetChild(SkillNum).GetChild(3).gameObject.SetActive(false);

                // 스킬창을 닫아준다.
                StartCoroutine(SkillWindowClose_Coroutine());

                if (SkillNum == 0)
                    Skill01_Purchase = false;
                else if (SkillNum == 1)
                    Skill02_Purchase = false;
                else if (SkillNum == 2)
                    Skill03_Purchase = false;
            }

            // 스킬적용
            else if (Purchase == false)
            {
                AfterPurchase_Key.gameObject.SetActive(false);
                StartCoroutine(SkillHave()); // SkillHave 코루틴을 실행시킨다.
                Purchase = true; // 이것을 통하여 스킬적용 -> 스킬구매로 넘겨준다.
                UIManager.instance.PlayerMove_control = false;
            }
        }
    }


    #region 구매 후 스킬 적용
    public IEnumerator SkillHave()
    {
        // 윗 부분에 스킬을 적용할려 할 떄
        if (UpDown == true && UpDown_Limit == true)
        {
            UpDown_Limit = false;

            // 윗 부분에 있던 스킬은 상점에으로 보낸다.
            Skill_Manager.instance.SkillBuffer.Add(Skill_Manager.instance.Skill_Up[0]);
            Skill_Manager.instance.Skill_Up.RemoveAt(0);

            // 구매한 스킬을 윗 부분에 넣어준다.
            Skill_Manager.instance.Skill_Up.Add(Skill_Manager.instance.Skill_Have[0]);
            Skill_Manager.instance.Skill_Have.RemoveAt(0);

            Vector3[] SaveSkillPos = new Vector3[2];
            SaveSkillPos[0] = AfterPurchase_Skill.transform.position;
            SaveSkillPos[1] = AfterPurchase_Skill_Box.transform.position;

            //스킬 적용 애니메이션
            AfterPurchase_Skill.transform.DOLocalMove(new Vector3(-774f, 66f, 0f), 0.5f).SetEase(Ease.InOutQuad);
            AfterPurchase_Skill_Box.transform.DOLocalMove(new Vector3(-774f, 66f, 0f), 0.5f).SetEase(Ease.InOutQuad);

            yield return new WaitForSeconds(0.5f);

            // AS_Limit = Shift를 통한 스킬 전환 체크
            if (Skill_Manager.instance.AS_Limit == true) // true일 경우 A스킬에 구매한 스킬을 적용시킨다.
            {
                Basics_Skill_A.sprite = SeletSkill.sprite;
            }
            else Basics_Skill_S.sprite = SeletSkill.sprite; // false일 경우 S스킬에 구매한 스킬을 적용시킨다.

            // 한 번 미만 스킬을 적용시킬 시 실행시킨다.
            AfterPurchase_Skill.transform.position = SaveSkillPos[0];
            AfterPurchase_Skill_Box.transform.position = SaveSkillPos[1];
            AfterPurchase_Key.gameObject.SetActive(true);
            if (MoreThanOnce_Purchase == true)
            {
                AfterPurchase_Window.SetActive(false);
                MoreThanOnce_Purchase = false;
            }

            // 한 번 이상 스킬을 적용시킬 시 실행시킨다.
            else
                AfterPurchase_Window.SetActive(false);

            UpDown_Limit = true;
        }

        // 아랫 부분에 스킬을 적용할려 할 떄
        else if (UpDown == false && UpDown_Limit == true)
        {
            UpDown_Limit = false;

            // 아랫 부분에 있던 스킬은 상점으로 보낸다.
            Skill_Manager.instance.SkillBuffer.Add(Skill_Manager.instance.Skill_Down[0]);
            Skill_Manager.instance.Skill_Down.RemoveAt(0);

            // 구매한 스킬을 아랫 부분에 넣어준다.
            Skill_Manager.instance.Skill_Down.Add(Skill_Manager.instance.Skill_Have[0]);
            Skill_Manager.instance.Skill_Have.RemoveAt(0);

            Vector3[] SaveSkillPos = new Vector3[2];
            SaveSkillPos[0] = AfterPurchase_Skill.transform.position;
            SaveSkillPos[1] = AfterPurchase_Skill_Box.transform.position;

            //스킬 적용 애니메이션
            AfterPurchase_Skill.transform.DOLocalMove(new Vector3(-784, 80f, 0f), 0.5f).SetEase(Ease.InOutQuad);
            AfterPurchase_Skill_Box.transform.DOLocalMove(new Vector3(-784, 80f, 0f), 0.5f).SetEase(Ease.InOutQuad);

            yield return new WaitForSeconds(0.5f);

            // AS_Limit = Shift를 통한 스킬 전환 체크
            if (Skill_Manager.instance.AS_Limit_02 == true) // true일 경우 S스킬에 구매한 스킬을 적용시킨다.
                Basics_Skill_S.sprite = SeletSkill.sprite;
            else
                Basics_Skill_A.sprite = SeletSkill.sprite;

            // 한 번 미만 스킬을 적용시킬 시 실행시킨다.
            AfterPurchase_Skill.transform.position = SaveSkillPos[0];
            AfterPurchase_Skill_Box.transform.position = SaveSkillPos[1];
            AfterPurchase_Key.gameObject.SetActive(true);
            if (MoreThanOnce_Purchase == true)
            {
                AfterPurchase_Window.SetActive(false);
                MoreThanOnce_Purchase = false;
            }

            // 한 번 이상 스킬을 적용시킬 시 실행시킨다.
            else
                AfterPurchase_Window.SetActive(false);
            UpDown_Limit = true;
        }
    }
    #endregion

    #region 스킬 구매 후 왼쪽방향 화살표
    public void AfterPurchase_Left()
    {
        StartCoroutine(AfterPurchase_Left_forward());
    }

    public IEnumerator AfterPurchase_Left_forward()
    {
        AfterPurchase_LeftDirection.transform.DOLocalMoveX(-570, 1f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(AfterPurchase_Left_Back());
    }

    public IEnumerator AfterPurchase_Left_Back()
    {
        AfterPurchase_LeftDirection.transform.DOLocalMoveX(-550, 1f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(AfterPurchase_Left_forward());
    }
    #endregion

    #region 스킬구매 후 위,아랫 방향 화살표
    public void AfterPurchase_UpDown()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && Purchase == false && UpDown == false && UpDown_Limit == true)
        {
            UpDown = true;
            AfterPurchase_Window.transform.DOLocalMoveY(5f, 1f).SetEase(Ease.OutBack);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) && Purchase == false && UpDown == true && UpDown_Limit == true)
        {
            UpDown = false;
            AfterPurchase_Window.transform.DOLocalMoveY(-145f, 1f).SetEase(Ease.OutBack);
        }
    }
    #endregion

    #region 스킬 창

    public IEnumerator SkillWindow_Coroutine()
    {
        // 스킬 창을 열어주는 코루틴
        Timer = 0;
        if (SkillNum == 0 && Skill01_Purchase == true && SkillWindow == true)
        {
            Skill_Window_Active();
            SkillWindow = false;
            while (Timer < 1)
            {
                Skill_RectTransform.localPosition = new Vector2(1.995371f, Mathf.Lerp(245.1f, 34f, Timer));
                Skill_RectTransform.sizeDelta = new Vector2(690f, Mathf.Lerp(0f, 485f, Timer));
                Pole_01.localPosition = new Vector3(0.2999878f, 237f, 0);
                Pole_02.localPosition = new Vector2(-2f, Mathf.Lerp(182f, -149f, Timer));
                Timer += Time.deltaTime * 4f;
                yield return null;
            }
        }

        else if (SkillNum == 1 && Skill02_Purchase == true && SkillWindow == true)
        {
            Skill_Window_Active();
            SkillWindow = false;
            while (Timer < 1)
            {
                Skill_RectTransform.localPosition = new Vector2(404.9954f, Mathf.Lerp(245.1f, 34f, Timer));
                Skill_RectTransform.sizeDelta = new Vector2(690f, Mathf.Lerp(0f, 485f, Timer));
                Pole_01.localPosition = new Vector3(403.3f, 237f, 0);
                Pole_02.localPosition = new Vector2(401f, Mathf.Lerp(202.92f, -149f, Timer));
                Timer += Time.deltaTime * 4f;
                yield return null;
            }
        }

        else if (SkillNum == 2 && Skill03_Purchase == true && SkillWindow == true)
        {
            Skill_Window_Active();
            SkillWindow = false;
            while (Timer < 1)
            {
                Skill_RectTransform.localPosition = new Vector2(819.9954f, Mathf.Lerp(245.1f, 34f, Timer));
                Skill_RectTransform.sizeDelta = new Vector2(690f, Mathf.Lerp(0f, 485f, Timer));
                Pole_01.localPosition = new Vector3(818.3f, 237f, 0);
                Pole_02.localPosition = new Vector2(816f, Mathf.Lerp(202.92f, -149f, Timer));
                Timer += Time.deltaTime * 4f;
                yield return null;
            }
        }
    }

    public IEnumerator SkillWindowClose_Coroutine()
    {
        // 스킬 창을 닫아주는 코루틴

        Timer = 0;
        if (SkillNum == 0 && Skill01_Purchase == true && SkillWindow == false)
        {
            SkillWindow = true;
            while (Timer < 1)
            {
                Pole_01.localPosition = new Vector3(0.2999878f, 237f, 0);
                Pole_02.localPosition = new Vector2(-2f, Mathf.Lerp(-149f, 200f, Timer));
                Skill_RectTransform.sizeDelta = new Vector2(690f, Mathf.Lerp(485f, 0f, Timer));
                Skill_RectTransform.localPosition = new Vector2(1.995371f, Mathf.Lerp(34f, 245.1f, Timer));

                Timer += Time.deltaTime * 4f;
                yield return null;
            }
        }

        else if (SkillNum == 1 && Skill02_Purchase == true && SkillWindow == false)
        {
            SkillWindow = true;
            while (Timer < 1)
            {
                Pole_01.localPosition = new Vector3(403.3f, 237f, 0);
                Pole_02.localPosition = new Vector2(401f, Mathf.Lerp(-149f, 200f, Timer));
                Skill_RectTransform.sizeDelta = new Vector2(690f, Mathf.Lerp(485f, 0f, Timer));
                Skill_RectTransform.localPosition = new Vector2(404.9954f, Mathf.Lerp(34f, 245.1f, Timer));

                Timer += Time.deltaTime * 4f;
                yield return null;
            }
        }
        else if (SkillNum == 2 && Skill03_Purchase == true && SkillWindow == false)
        {
            SkillWindow = true;
            while (Timer < 1)
            {
                Pole_01.localPosition = new Vector3(818.3f, 237f, 0);
                Pole_02.localPosition = new Vector2(816f, Mathf.Lerp(-149f, 202.92f, Timer));
                Skill_RectTransform.sizeDelta = new Vector2(690f, Mathf.Lerp(485f, 0f, Timer));
                Skill_RectTransform.localPosition = new Vector2(819.9954f, Mathf.Lerp(34f, 245.1f, Timer));

                Timer += Time.deltaTime * 4f;
                yield return null;
            }
        }

        this.gameObject.transform.GetChild(3).gameObject.SetActive(false);

        if (Purchase == false)
        {
            AfterPurchase_Window.gameObject.SetActive(true);
        }
    }
    #endregion
}
