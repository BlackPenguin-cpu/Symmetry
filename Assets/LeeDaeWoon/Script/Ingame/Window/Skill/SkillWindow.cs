using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SkillWindow : MonoBehaviour
{
    public static SkillWindow instance { get; private set; }
    void Awake() => instance = this;

    #region 스킬 창
    [Header("스킬 창")]
    [SerializeField] GameObject downBar;
    [SerializeField] GameObject skillWindow;
    [SerializeField] RectTransform skillWindowRect;

    const int openBar = 149;
    const int closeBar = 190;

    const float closeSpeed = 0.22f;
    const float barSpeed = 0.24f;

    const int windowOpen = 34;
    const int windowClose = 240;
    const int windowWidth = 620;
    const int windowHeight = 360;
    #endregion

    [Header("기본 스킬 이미지")]
    [SerializeField] Image basicsSkillA;
    [SerializeField] Image basicsSkillS;

    [Header("구매 후 창")]
    [SerializeField] GameObject AfterPurchase_Window_Prefab;
    [SerializeField] GameObject AfterPurchase_Key; // 선택 키 오브젝트

    [SerializeField] GameObject AfterPurchase_Skill; // 스킬 적용하기 전 스킬 이미지
    [SerializeField] GameObject AfterPurchase_Window; // 스킬적용 창
    [SerializeField] GameObject AfterPurchase_Skill_Box; // 스킬 적용하기 전 스킬박스 이미지
    [SerializeField] GameObject leftDirection; // 왼쪽 화살표

    [Header("스킬 좌표")]
    [SerializeField] GameObject Skill_Shop;

    public Image AfterPurchase_Top_Light;
    public Image AfterPurchase_Bottom_Light;

    public int SkillNum; // 현재 몇 번째 구매 스킬과 충돌했는지 숫자 확인
    public bool UpDown = true; // 현재 위 인지 아래 인지 확인
    public bool isPurchase = false; // 현재 구매중인지 아닌지 확인
    public bool UpDown_Limit = true; // 위아래 제한
    public bool isCollisionCheck = false; // 현재 구매 스킬들과 충돌 했는지 체크 확인

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
        AfterPurchase_Window = GameObject.Find("After_Purchase");
        AfterPurchase_Key = GameObject.Find("Direction_Key");
        AfterPurchase_Skill = GameObject.Find("After_Skill_Image");
        AfterPurchase_Skill_Box = GameObject.Find("After_Skill_Box");
        leftDirection = GameObject.Find("Left_Direction");
        #endregion

        LeftDirection();
        AfterPurchase_Window.gameObject.SetActive(false);
    }

    void Update()
    {
        #region 월드 좌표를 스크린 좌표로 변경을 해준다.
        transform.localPosition = Camera.main.WorldToScreenPoint(Skill_Shop.gameObject.transform.position + new Vector3(-17.5f, -4.4f, 0));
        #endregion

        UpDownkey();
        SkillPurchase();
    }

    void SkillPurchase()
    {
        // F키를 통하여 구매 혹은 스킬적용을 할 수 있다.
        if (Input.GetKeyDown(KeyCode.F) && isCollisionCheck)
        {
            // 스킬구매
            if (isPurchase == false && (GameManager.Instance._coin >= Skill_List.instance.leftSkill.gold || GameManager.Instance._coin >= Skill_List.instance.amongSkill.gold || GameManager.Instance._coin >= Skill_List.instance.rightSkill.gold))
            {
                SoundManager.instance.PlaySoundClip("SFX_Buy", SoundType.SFX, 5f);
                UIManager.instance.isPlayerControl = true;

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

                isPurchase = true; // 이것을 통하여 스킬구매 -> 스킬적용으로 넘겨준다.

                transform.GetChild(SkillNum).GetChild(2).gameObject.SetActive(true);
                transform.GetChild(SkillNum).GetChild(3).gameObject.SetActive(false);

                // 스킬창을 닫아준다.
                CloseWindow(SkillNum);

                switch (SkillNum)
                {
                    case 0:
                        Skill01_Purchase = false;
                        break;
                    case 1:
                        Skill02_Purchase = false;
                        break;
                    case 2:
                        Skill03_Purchase = false;
                        break;
                }
            }

            // 스킬적용
            else if (isPurchase)
            {
                UIManager.instance.isPlayerControl = false;

                AfterPurchase_Key.gameObject.SetActive(false);
                SkillHave(); // SkillHave 함수를 실행시킨다.
                isPurchase = false; // 이것을 통하여 스킬적용 -> 스킬구매로 넘겨준다.
            }
        }
    }


    #region 구매 후 스킬 적용
    void SkillHave()
    {
        // 윗 부분에 스킬을 적용할려 할 떄
        if (UpDown && UpDown_Limit)
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
            AfterPurchase_Skill.transform.DOLocalMove(basicsSkillA.transform.localPosition, 0.5f).SetEase(Ease.InOutQuad);
            AfterPurchase_Skill_Box.transform.DOLocalMove(basicsSkillA.transform.localPosition, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                // AS_Limit = Shift를 통한 스킬 전환 체크
                if (Skill_Manager.instance.AS_Limit == true) // true일 경우 A스킬에 구매한 스킬을 적용시킨다.
                {
                    basicsSkillA.sprite = SeletSkill.sprite;
                }
                else basicsSkillS.sprite = SeletSkill.sprite; // false일 경우 S스킬에 구매한 스킬을 적용시킨다.

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
            });


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
            AfterPurchase_Skill.transform.DOLocalMove(basicsSkillS.transform.localPosition, 0.5f).SetEase(Ease.InOutQuad);
            AfterPurchase_Skill_Box.transform.DOLocalMove(basicsSkillS.transform.localPosition, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                //// AS_Limit = Shift를 통한 스킬 전환 체크
                //if (Skill_Manager.instance.AS_Limit_02 == true) // true일 경우 S스킬에 구매한 스킬을 적용시킨다.
                //    basicsSkillS.sprite = SeletSkill.sprite;
                //else
                //    basicsSkillA.sprite = SeletSkill.sprite;

                //// 한 번 미만 스킬을 적용시킬 시 실행시킨다.
                //AfterPurchase_Skill.transform.position = SaveSkillPos[0];
                //AfterPurchase_Skill_Box.transform.position = SaveSkillPos[1];
                //AfterPurchase_Key.gameObject.SetActive(true);
                //if (MoreThanOnce_Purchase == true)
                //{
                //    AfterPurchase_Window.SetActive(false);
                //    MoreThanOnce_Purchase = false;
                //}

                //// 한 번 이상 스킬을 적용시킬 시 실행시킨다.
                //else
                //    AfterPurchase_Window.SetActive(false);
                //UpDown_Limit = true;

            });
        }
    }
    #endregion

    void LeftDirection()
    {
        int leftDirectionPosX = 630;
        leftDirection.transform.DOLocalMoveX(-leftDirectionPosX, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    void UpDownkey()
    {
        if (isPurchase && UpDown_Limit)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !UpDown)
            {
                UpDown = true;

                for(int i = 0; i < AfterPurchase_Window.transform.childCount; i++)
                    AfterPurchase_Window.transform.GetChild(i).transform.DOLocalMoveY(basicsSkillA.transform.localPosition.y, 1).SetEase(Ease.OutBack);
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow) && UpDown)
            {
                UpDown = false;

                for (int i = 0; i < AfterPurchase_Window.transform.childCount; i++)
                    AfterPurchase_Window.transform.GetChild(i).transform.DOLocalMoveY(basicsSkillS.transform.localPosition.y, 1).SetEase(Ease.OutBack);
            }
        }
    }

    #region 스킬 창
    public void OpenWindow(int skillNum)
    {
        // 스킬 창을 열어주는 코루틴
        downBar.transform.DOKill();
        skillWindowRect.transform.DOKill();

        switch (skillNum)
        {
            case 0:
                if (Skill01_Purchase)
                {
                    int leftDistance = 220;
                    skillWindow.transform.DOLocalMoveX(-leftDistance, 0);
                }
                break;

            case 1:
                if (Skill02_Purchase)
                {
                    int amongDistance = 180;
                    skillWindow.transform.DOLocalMoveX(amongDistance, 0);
                }
                break;

            case 2:
                if (Skill03_Purchase)
                {
                    int rightDistance = 600;
                    skillWindow.transform.DOLocalMoveX(rightDistance, 0);
                }
                break;
        }

        skillWindow.SetActive(true);
        downBar.transform.DOLocalMoveY(-openBar, barSpeed).SetEase(Ease.Linear);
        skillWindowRect.DOLocalMoveY(windowOpen, barSpeed).SetEase(Ease.Linear);
        skillWindowRect.DOSizeDelta(new Vector2(windowWidth, windowHeight), barSpeed).SetEase(Ease.Linear);
    }

    public void CloseWindow(int skillNum)
    {
        switch (skillNum)
        {
            case 0:
                if (Skill01_Purchase)
                {
                    int leftDistance = 220;
                    skillWindow.transform.DOLocalMoveX(-leftDistance, 0);
                }
                break;

            case 1:
                if (Skill02_Purchase)
                {
                    int amongDistance = 180;
                    skillWindow.transform.DOLocalMoveX(amongDistance, 0);
                }
                break;

            case 2:
                if (Skill03_Purchase)
                {
                    int rightDistance = 600;
                    skillWindow.transform.DOLocalMoveX(rightDistance, 0);
                }
                break;
        }
        downBar.transform.DOLocalMoveY(closeBar, closeSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            skillWindow.SetActive(false);
        });
        skillWindowRect.DOLocalMoveY(windowClose, closeSpeed).SetEase(Ease.Linear);
        skillWindowRect.DOSizeDelta(new Vector2(windowWidth, 0), barSpeed).SetEase(Ease.Linear);

        if (isPurchase)
            AfterPurchase_Window.gameObject.SetActive(true);
    }
    #endregion
}
