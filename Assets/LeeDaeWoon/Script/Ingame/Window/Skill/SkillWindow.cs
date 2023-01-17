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
    [SerializeField] GameObject applySkillWindow; // 스킬적용 창
    [SerializeField] GameObject upBottomDirection; // 선택 키 오브젝트
    [SerializeField] GameObject applySkillBox; // 스킬 적용하기 전 스킬박스 이미지
    [SerializeField] GameObject leftDirection; // 왼쪽 화살표
    [SerializeField] Image applySkill; // 스킬 적용하기 전 스킬 이미지

    [Header("스킬 좌표")]
    [SerializeField] GameObject SalesmanNpc;

    [SerializeField] Image topDirectionLight;
    [SerializeField] Image bottomDirectionLight;

    public int skillNum; // 현재 몇 번째 구매 스킬과 충돌했는지 숫자 확인
    public bool isPurchase = false; // 현재 구매중인지 아닌지 확인
    public bool isUpDown = false; // 현재 위 인지 아래 인지 확인
    public bool isUpDownLimit = false; // 위아래 제한

    // 몇 번째 스킬을 구매했는지 확인
    public bool isSkill01Purchase = false;
    public bool isSkill02Purchase = false;
    public bool isSkill03Purchase = false;

    public bool isCollisionCheck = false; // 현재 구매 스킬들과 충돌 했는지 체크 확인

    SkillScript seletSkill;

    void Start()
    {
        applySkillWindow.gameObject.SetActive(false);

        LeftDirection();
    }

    void Update()
    {
        #region 월드 좌표를 스크린 좌표로 변경을 해준다.
        transform.localPosition = Camera.main.WorldToScreenPoint(SalesmanNpc.gameObject.transform.position + new Vector3(-17.5f, -4.4f, 0));
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
            if (!isPurchase && (GameManager.Instance._coin >= Skill_List.instance.leftSkill.gold || GameManager.Instance._coin >= Skill_List.instance.amongSkill.gold || GameManager.Instance._coin >= Skill_List.instance.rightSkill.gold))
            {
                SoundManager.instance.PlaySoundClip("SFX_Buy", SoundType.SFX, 5f);

                UIManager.instance.isPlayerControl = true;

                for (int i = 0; i <= 1; i++)
                    applySkillWindow.transform.GetChild(i).gameObject.SetActive(true);

                seletSkill = Skill_Manager.instance.Skill[skillNum];
                applySkill.sprite = seletSkill.sprite;

                GameManager.Instance._coin -= seletSkill.price[0];

                // 정상작동 웨이브 : 5 / 10 / 15
                //switch(WaveManager.Instance.m_WaveNum)
                //{
                //    case 3:
                //        GameManager.Instance._coin -= seletSkill.price[0];
                //        break;

                //    case 5:
                //        GameManager.Instance._coin -= seletSkill.price[1];
                //        break;

                //    case 15:
                //        GameManager.Instance._coin -= seletSkill.price[2];
                //        break;
                //}

                Skill_Manager.instance.Skill_Have.Add(seletSkill);
                Skill_Manager.instance.Skill_Shop.Add(seletSkill);

                isPurchase = true; // 이것을 통하여 스킬구매 -> 스킬적용으로 넘겨준다.

                transform.GetChild(skillNum).GetChild(2).gameObject.SetActive(true); // soldOutText
                transform.GetChild(skillNum).GetChild(3).gameObject.SetActive(false); // shopSkillBox

                // 스킬창을 닫아준다.
                CloseWindow(skillNum);

                switch (skillNum)
                {
                    case 0:
                        isSkill01Purchase = true;
                        break;
                    case 1:
                        isSkill02Purchase = true;
                        break;
                    case 2:
                        isSkill03Purchase = true;
                        break;
                }
            }

            // 스킬적용
            else if (isPurchase)
            {
                for (int i = 0; i <= 1; i++)
                    applySkillWindow.transform.GetChild(i).gameObject.SetActive(false);

                SkillHave(); // SkillHave 함수를 실행시킨다.
                isPurchase = false; // 이것을 통하여 스킬적용 -> 스킬구매로 넘겨준다.
            }
        }
    }


    #region 구매 후 스킬 적용
    void SkillHave()
    {
        // 윗 부분에 스킬을 적용할려 할 떄
        if (!isUpDown && !isUpDownLimit)
        {
            isUpDownLimit = true;

            // 윗 부분에 있던 스킬은 상점에으로 보낸다.
            Skill_Manager.instance.SkillBuffer.Add(Skill_Manager.instance.Skill_Up[0]);
            Skill_Manager.instance.Skill_Up.RemoveAt(0);

            // 구매한 스킬을 윗 부분에 넣어준다.
            Skill_Manager.instance.Skill_Up.Add(Skill_Manager.instance.Skill_Have[0]);
            Skill_Manager.instance.Skill_Have.RemoveAt(0);

            Vector3[] SaveSkillPos = new Vector3[2];
            SaveSkillPos[0] = applySkill.transform.position;
            SaveSkillPos[1] = applySkillBox.transform.position;

            //스킬 적용 애니메이션
            applySkill.transform.DOLocalMove(basicsSkillA.transform.localPosition, 0.5f).SetEase(Ease.InOutQuad);
            applySkillBox.transform.DOLocalMove(basicsSkillA.transform.localPosition, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                UIManager.instance.isPlayerControl = false;

                // 스킬 전환한데로 구매한 스킬을 적용시킨다.
                if (!Skill_Manager.instance.isASLimit) 
                    basicsSkillA.sprite = seletSkill.sprite;
                else 
                    basicsSkillS.sprite = seletSkill.sprite; 

                applySkill.transform.position = SaveSkillPos[0];
                applySkillBox.transform.position = SaveSkillPos[1];

                upBottomDirection.gameObject.SetActive(true);
                applySkillWindow.SetActive(false);

                isUpDownLimit = false;
            });


        }

        // 아랫 부분에 스킬을 적용할려 할 떄
        else if (isUpDown && !isUpDownLimit)
        {
            isUpDownLimit = true;

            // 아랫 부분에 있던 스킬은 상점으로 보낸다.
            Skill_Manager.instance.SkillBuffer.Add(Skill_Manager.instance.Skill_Down[0]);
            Skill_Manager.instance.Skill_Down.RemoveAt(0);

            // 구매한 스킬을 아랫 부분에 넣어준다.
            Skill_Manager.instance.Skill_Down.Add(Skill_Manager.instance.Skill_Have[0]);
            Skill_Manager.instance.Skill_Have.RemoveAt(0);

            Vector3[] SaveSkillPos = new Vector3[2];
            SaveSkillPos[0] = applySkill.transform.position;
            SaveSkillPos[1] = applySkillBox.transform.position;

            //스킬 적용 애니메이션
            applySkill.transform.DOLocalMove(basicsSkillS.transform.localPosition, 0.5f).SetEase(Ease.InOutQuad);
            applySkillBox.transform.DOLocalMove(basicsSkillS.transform.localPosition, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                UIManager.instance.isPlayerControl = false;

                // 스킬 전환한데로 구매한 스킬을 적용시킨다.
                if (Skill_Manager.instance.AS_Limit_02 == true)
                    basicsSkillS.sprite = seletSkill.sprite;
                else
                    basicsSkillA.sprite = seletSkill.sprite;

                applySkill.transform.position = SaveSkillPos[0];
                applySkillBox.transform.position = SaveSkillPos[1];

                upBottomDirection.gameObject.SetActive(true);
                applySkillWindow.SetActive(false);

                isUpDownLimit = false;
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
        if (isPurchase && !isUpDownLimit)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && isUpDown)
            {
                isUpDown = false;

                for (int i = 0; i < applySkillWindow.transform.childCount; i++)
                    applySkillWindow.transform.GetChild(i).transform.DOLocalMoveY(basicsSkillA.transform.localPosition.y, 1).SetEase(Ease.OutBack);
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow) && !isUpDown)
            {
                isUpDown = true;

                for (int i = 0; i < applySkillWindow.transform.childCount; i++)
                    applySkillWindow.transform.GetChild(i).transform.DOLocalMoveY(basicsSkillS.transform.localPosition.y, 1).SetEase(Ease.OutBack);
            }
        }
    }

    #region 스킬 창
    public void OpenWindow(int skillNum)
    {
        downBar.transform.DOKill();
        skillWindowRect.transform.DOKill();

        switch (skillNum)
        {
            case 0:
                if (!isSkill01Purchase)
                {
                    int leftDistance = 220;
                    skillWindow.transform.DOLocalMoveX(-leftDistance, 0);
                }
                break;

            case 1:
                if (!isSkill02Purchase)
                {
                    int amongDistance = 180;
                    skillWindow.transform.DOLocalMoveX(amongDistance, 0);
                }
                break;

            case 2:
                if (!isSkill03Purchase)
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
                if (!isSkill01Purchase)
                {
                    int leftDistance = 220;
                    skillWindow.transform.DOLocalMoveX(-leftDistance, 0);
                }
                break;

            case 1:
                if (!isSkill02Purchase)
                {
                    int amongDistance = 180;
                    skillWindow.transform.DOLocalMoveX(amongDistance, 0);
                }
                break;

            case 2:
                if (!isSkill03Purchase)
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
            applySkillWindow.gameObject.SetActive(true);
    }
    #endregion
}
