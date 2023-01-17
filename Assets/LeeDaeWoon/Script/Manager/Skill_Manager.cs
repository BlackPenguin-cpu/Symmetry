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

    bool isSummonSKill = false;

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
    public int RandomTest;
    private int SumPer = 0;

    [SerializeField] SkillSo SkillSo;
    [SerializeField] GameObject SkillPrefab;

    public List<SkillScript> Skill_Up = new List<SkillScript>();
    public List<SkillScript> Skill_Down = new List<SkillScript>();

    public List<SkillScript> Skill_Have = new List<SkillScript>();
    public List<SkillScript> Skill = new List<SkillScript>();
    public List<SkillScript> Skill_Shop = new List<SkillScript>();
    public List<SkillScript> SkillBuffer = new List<SkillScript>();

    private SkillManager skillManager;

    void Start()
    {
        skillManager = SkillManager.Instance;
        AddList();
    }

    private void Update()
    {
        AddSkill();

        Skill_CoolTime_A();
        Skill_CoolTime_S();
        SkillHave_Check();
        AS_Location();
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

    public void SkillHave_Check()
    {
        SkillScript[] skills = haveSkillInfo.Keys.ToArray();
        for (int i = 0; i < haveSkillInfo.Count; i++)
        {
            haveSkillInfo[skills[i]] = false;
        }

        haveSkillInfo[Skill_Up[0]] = true;
        haveSkillInfo[Skill_Down[0]] = true;
    }

    #region 스킬 소환
    public void AddSkill()
    {
        if (SceneManager.GetActiveScene().name == "Dimension" && !isSummonSKill)
        {
            // 스킬 소환
            isSummonSKill = true;
            int SkillIndex = 0;

            Skill.Clear();
            var card = SkillPrefab.GetComponent<Skill_List>();

            for (int i = 0; i < 3; i++)
            {
                RandomTest = Skill_Percent(SkillBuffer);
                Skill.Add(SkillBuffer[RandomTest]);
                card.SkillCard(Skill[i], SkillIndex++);
                SkillBuffer.RemoveAt(RandomTest);
            }
        }
    }
    #endregion

    #region A_스킬 쿨타임
    public void Skill_CoolTime_A() // 스킬 A의 쿨타임
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
                StartCoroutine(A_CoolTime());

                coolTimeAText.text = currentCoolTimeA.ToString();

                StartCoroutine(A_CoolTimeCounter());

                skillManager.UseSkill(Skill_Up[0].name, DimensionType.OVER);

                isCoolDown01 = true;
            }

            else if (Input.GetKeyDown(KeyCode.S) && isCoolDown01 == false && isASLimit == true)
            {
                coolTimeA = Skill_Up[0]._cooldown;
                coolTimeAObj.SetActive(true);
                fillAmountA.fillAmount = 1f;
                currentCoolTimeA = coolTimeS;
                StartCoroutine(A_CoolTime());

                coolTimeAText.text = currentCoolTimeA.ToString();

                StartCoroutine(A_CoolTimeCounter());

                skillManager.UseSkill(Skill_Down[0].name, DimensionType.UNDER);
                isCoolDown01 = true;
            }
        }
    }

    public IEnumerator A_CoolTime() // 쿨타임
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

    public IEnumerator A_CoolTimeCounter() // 남은 쿨타임을 계산할 코르틴을 만든다.
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
    public void Skill_CoolTime_S() // 스킬 S의 쿨타임
    {
        // 현재 씬이 Main이 아닐 경우 스킬을 사용할 수 있다.
        if (SceneManager.GetActiveScene().name != "Main")
        {
            if (Input.GetKeyDown(KeyCode.S) && isCoolDown02 == false && AS_Limit_02 == true)
            {
                coolTimeS = Skill_Down[0]._cooldown;
                coolTimeSObj.SetActive(true);
                fillAmountS.fillAmount = 1f;
                currentCoolTimeS = coolTimeS;
                StartCoroutine(S_CoolTime());

                coolTimeSText.text = currentCoolTimeS.ToString();

                skillManager.UseSkill(Skill_Down[0].name, DimensionType.UNDER);
                StartCoroutine(S_CoolTimeCounter());

                isCoolDown02 = true;
            }

            else if (Input.GetKeyDown(KeyCode.A) && isCoolDown02 == false && AS_Limit_02 == false)
            {
                coolTimeS = Skill_Down[0]._cooldown;
                coolTimeSObj.SetActive(true);
                fillAmountS.fillAmount = 1f;
                currentCoolTimeS = coolTimeA;
                StartCoroutine(S_CoolTime());

                coolTimeSText.text = currentCoolTimeS.ToString();

                skillManager.UseSkill(Skill_Up[0].name, DimensionType.OVER);
                StartCoroutine(S_CoolTimeCounter());

                isCoolDown02 = true;
            }
        }


    }
    public IEnumerator S_CoolTime() // 쿨타임
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

    public IEnumerator S_CoolTimeCounter() // 남은 쿨타임을 계산할 코르틴을 만든다.
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
    public void AS_Location()
    {
        // 왼쪽 쉬프트를 누르고, Potal을 통하여 이동을 하지 않을 경우 이 함수를 실행할 수 있다.
        if (Input.GetKeyDown(KeyCode.LeftShift) && isPotalMove == false)
            SkillChange();
    }

    void SkillChange()
    {
        int skillPosY = 134;
        float waitTime = 0.5f;

        Skill_Up.Add(Skill_Down[0]);
        Skill_Down.Add(Skill_Up[0]);
        Skill_Up.RemoveAt(0);
        Skill_Down.RemoveAt(0);

        if (!isASLimit && !isLimit)
        {
            SoundManager.instance.PlaySoundClip("SFX_Skill_Swap", SoundType.SFX);

            isLimit = true;
            upSkill.transform.DOLocalMoveY(-skillPosY, waitTime).SetEase(Ease.Linear);
            downSkill.transform.DOLocalMoveY(skillPosY, waitTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                AS_Limit_02 = false;
                isASLimit = true;
            });
        }

        else if (isASLimit && isLimit)
        {
            SoundManager.instance.PlaySoundClip("SFX_Skill_Swap", SoundType.SFX);

            isLimit = false;
            upSkill.transform.DOLocalMoveY(0, waitTime).SetEase(Ease.Linear);
            downSkill.transform.DOLocalMoveY(0, waitTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                AS_Limit_02 = true;
                isASLimit = false;
            });
        }
    }
    #endregion
}
