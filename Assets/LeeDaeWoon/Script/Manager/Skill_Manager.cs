using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Skill_Manager : MonoBehaviour
{
    public static Skill_Manager instance { get; private set; }
    void Awake() => instance = this;

    public bool isSummonSKill = false;

    [Header("A스킬, S스킬")]
    [SerializeField] RectTransform upSkill;
    [SerializeField] RectTransform downSkill;

    [Header("A스킬 쿨타임")]
    [SerializeField] GameObject coolTimeAObj;
    [SerializeField] Text coolTimeAText;
    [SerializeField] Image fillAmountA;

    float coolTimeA = 0;
    float currentCoolTimeA = 0;

    bool isCoolDown01 = false;

    [Header("S스킬 쿨타임")]
    [SerializeField] GameObject coolTimeSObj;
    [SerializeField] Text coolTimeSText;
    [SerializeField] Image fillAmountS;

    float coolTimeS = 0;
    float currentCoolTimeS = 0;

    bool isCoolDown02 = false;

    [Header("A스킬, S스킬 제한")]
    public bool isPotalMove = false;
    public bool isASLimit = false;
    public bool AS_Limit_02 = true;
    bool isLimit = false;

    [Header("스킬 획득 체크")]
    public Dictionary<SkillScript, bool> haveSkillInfo = new Dictionary<SkillScript, bool>();
    int randomSkill = 0;
    int SumPer = 0;

    [SerializeField] SkillSo SkillSo;

    public List<SkillScript> Skill_Up = new List<SkillScript>();
    public List<SkillScript> Skill_Down = new List<SkillScript>();

    public List<SkillScript> Skill_Have = new List<SkillScript>();
    public List<SkillScript> Skill = new List<SkillScript>();
    public List<SkillScript> Skill_Shop = new List<SkillScript>();
    public List<SkillScript> SkillBuffer = new List<SkillScript>();

    SkillManager skillManager;

    void Start()
    {
        skillManager = SkillManager.Instance;
        AddList();
    }

    private void Update()
    {
        AddSkill();

        SkillCoolTimeA();
        SkillCoolTimeS();

        SkillHaveCheck();
        SkillChangeClick();
    }

    void AddList()
    {
        SkillBuffer = skillManager.SkillScriptList;
    }

    public int Skill_Percent(List<SkillScript> Percent_Skill)
    {
        SumPer = 0;

        //if(Wave가 5일 경우)
        //{
        foreach (SkillScript addper in Percent_Skill)
        {
            SumPer += (int)addper.appearChance[0];
        }
        int percent_01 = Random.Range(1, SumPer);
        for (int i = 0; i < Percent_Skill.Count; i++)
        {
            if (percent_01 < Percent_Skill[i].appearChance[0])
            {
                return i;
            }
            percent_01 -= (int)Percent_Skill[i].appearChance[0];
        }
        return 0;
        //}

        //if(Wave가 10일 경우)
        //{
        //foreach (Skill addper in Percent_Skill)
        //{
        //    SumPer += addper.Percent_02;


        //}
        //int percent_02 = Random.Range(1, SumPer);
        //for (int i = 0; i < percent_skill.Count; i++)
        //{
        //    if (percent_02 < Percent_Skill[i].Percent_02)
        //    {
        //        return i;
        //    }
        //    percent_02 -= Percent_Skill[i].Percent_02;
        //}
        //return 0;
        //}        

        //if(Wave가 15일 경우)
        //{
        //foreach (Skill addper in Percent_Skill)
        //{
        //    SumPer += addper.Percent_03;
        //}
        //int percent_03 = Random.Range(1, SumPer);
        //for (int i = 0; i < Percent_Skill.Count; i++)
        //{
        //    if (percent_03 < Percent_Skill[i].Percent_03)
        //    {
        //        return i;
        //    }
        //    percent_03 -= Percent_Skill[i].Percent_03;
        //}
        //return 0;
        //}
    }

    void SkillHaveCheck()
    {
        SkillScript[] skills = haveSkillInfo.Keys.ToArray();
        for (int i = 0; i < haveSkillInfo.Count; i++)
        {
            haveSkillInfo[skills[i]] = false;
        }

        haveSkillInfo[Skill_Up[0]] = true;
        haveSkillInfo[Skill_Down[0]] = true;
    }

    // 스킬 소환하는 함수
    public void AddSkill()
    {
        switch (CurrentScene.instance.eScene)
        {
            case EScene.Dimension:
                if (!isSummonSKill)
                {
                    int SkillIndex = 0;
                    isSummonSKill = true;

                    Skill.Clear();

                    var card = GameObject.Find("Skill_Shop").GetComponent<Skill_List>();

                    for (int i = 0; i < 3; i++)
                    {
                        randomSkill = Skill_Percent(SkillBuffer);
                        Skill.Add(SkillBuffer[randomSkill]);
                        card.SkillCard(Skill[i], SkillIndex++);
                        SkillBuffer.RemoveAt(randomSkill);
                    }
                }
                break;
        }
    }

    #region A_스킬 쿨타임
    void SkillCoolTimeA() // 스킬 A의 쿨타임
    {
        // 현재 씬이 Main이 아닐 경우 스킬을 사용할 수 있다.
        if (SceneManager.GetActiveScene().name != "Main")
        {
            if (Input.GetKeyDown(KeyCode.A) && isCoolDown01 == false && isASLimit == false)
            {
                coolTimeA = Skill_Up[0]._cooldown;
                coolTimeAObj.SetActive(true);
                fillAmountA.fillAmount = 1f;
                currentCoolTimeA = coolTimeA;
                StartCoroutine(CoolTimeA());

                coolTimeAText.text = currentCoolTimeA.ToString();

                StartCoroutine(CoolTimeCounterA());

                skillManager.UseSkill(Skill_Up[0].name, DimensionType.OVER);

                isCoolDown01 = true;
            }

            else if (Input.GetKeyDown(KeyCode.S) && isCoolDown01 == false && isASLimit == true)
            {
                coolTimeA = Skill_Up[0]._cooldown;
                coolTimeAObj.SetActive(true);
                fillAmountA.fillAmount = 1f;
                currentCoolTimeA = coolTimeS;
                StartCoroutine(CoolTimeA());

                coolTimeAText.text = currentCoolTimeA.ToString();

                StartCoroutine(CoolTimeCounterA());

                skillManager.UseSkill(Skill_Down[0].name, DimensionType.UNDER);
                isCoolDown01 = true;
            }
        }
    }

    IEnumerator CoolTimeA() // 쿨타임
    {
        while (fillAmountA.fillAmount > 0)
        {
            fillAmountA.fillAmount = currentCoolTimeA / coolTimeA;
            yield return null;
        }

        isCoolDown01 = false;
        coolTimeAObj.SetActive(false);
        yield break;
    }

    IEnumerator CoolTimeCounterA() // 남은 쿨타임을 계산할 코르틴을 만든다.
    {
        WaitForSeconds waitSec = new WaitForSeconds(1);
        while (currentCoolTimeA > 0)
        {
            yield return waitSec;
            currentCoolTimeA -= 1f;
            coolTimeAText.text = currentCoolTimeA.ToString();
        }
        yield break;
    }
    #endregion

    #region S_스킬 쿨타임
    void SkillCoolTimeS() // 스킬 S의 쿨타임
    {
        // 현재 씬이 Main이 아닐 경우 스킬을 사용할 수 있다.
        if (SceneManager.GetActiveScene().name != "Main")
        {
            if (Input.GetKeyDown(KeyCode.S) && isCoolDown02 == false && AS_Limit_02 == true)
            {
                coolTimeSObj.SetActive(true);

                coolTimeS = Skill_Down[0]._cooldown;
                fillAmountS.fillAmount = 1;
                currentCoolTimeS = coolTimeS;
                StartCoroutine(CoolTimeS());

                coolTimeSText.text = currentCoolTimeS.ToString();

                skillManager.UseSkill(Skill_Down[0].name, DimensionType.UNDER);
                StartCoroutine(CoolTimeCounterS());

                isCoolDown02 = true;
            }

            else if (Input.GetKeyDown(KeyCode.A) && isCoolDown02 == false && AS_Limit_02 == false)
            {
                coolTimeS = Skill_Down[0]._cooldown;
                coolTimeSObj.SetActive(true);
                fillAmountS.fillAmount = 1f;
                currentCoolTimeS = coolTimeA;
                StartCoroutine(CoolTimeS());

                coolTimeSText.text = currentCoolTimeS.ToString();

                skillManager.UseSkill(Skill_Up[0].name, DimensionType.OVER);
                StartCoroutine(CoolTimeCounterS());

                isCoolDown02 = true;
            }
        }
    }

    IEnumerator CoolTimeS() // 쿨타임
    {
        while (fillAmountS.fillAmount > 0)
        {
            fillAmountS.fillAmount = currentCoolTimeS / coolTimeS;
            yield return null;
        }

        isCoolDown02 = false;
        coolTimeSObj.SetActive(false);
        yield break;
    }

    IEnumerator CoolTimeCounterS() // 남은 쿨타임을 계산할 코르틴을 만든다.
    {
        while (currentCoolTimeS > 0)
        {
            yield return new WaitForSeconds(1f);
            currentCoolTimeS -= 1f;
            coolTimeSText.text = currentCoolTimeS.ToString();
        }
        yield break;
    }
    #endregion

    #region 스킬 A, S키 위치 설정
    void SkillChangeClick()
    {
        // 왼쪽 쉬프트를 누르고, Potal을 통하여 이동을 하지 않을 경우 이 함수를 실행할 수 있다.
        if (Input.GetKeyDown(KeyCode.LeftShift) && isPotalMove == false)
            SkillChange();
    }

    void SkillChange()
    {
        int skillPosY = 134;
        float ChangeSkill = 0.4f;

        Skill_Up.Add(Skill_Down[0]);
        Skill_Down.Add(Skill_Up[0]);
        Skill_Up.RemoveAt(0);
        Skill_Down.RemoveAt(0);

        if (!isASLimit && !isLimit)
        {
            SoundManager.instance.PlaySoundClip("SFX_Skill_Swap", SoundType.SFX);

            isLimit = true;
            upSkill.transform.DOLocalMoveY(-skillPosY, ChangeSkill).SetEase(Ease.OutBack);
            downSkill.transform.DOLocalMoveY(skillPosY, ChangeSkill).SetEase(Ease.OutBack).OnComplete(() =>
            {
                AS_Limit_02 = false;
                isASLimit = true;
            });
        }

        else if (isASLimit && isLimit)
        {
            SoundManager.instance.PlaySoundClip("SFX_Skill_Swap", SoundType.SFX);

            isLimit = false;
            upSkill.transform.DOLocalMoveY(0, ChangeSkill).SetEase(Ease.OutBack);
            downSkill.transform.DOLocalMoveY(0, ChangeSkill).SetEase(Ease.OutBack).OnComplete(() =>
            {
                AS_Limit_02 = true;
                isASLimit = false;
            });
        }
    }
    #endregion
}
