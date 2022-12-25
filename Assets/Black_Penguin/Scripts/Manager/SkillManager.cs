using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    public List<SkillScript> SkillScriptList = new List<SkillScript>();
    public Dictionary<string, BaseSkill> SkillList = new Dictionary<string, BaseSkill>();
    public Dictionary<string, float> SkillsCooldown = new Dictionary<string, float>();

    private Skill_Manager skillManager;
    private Player player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        skillManager = Skill_Manager.instance;
        player = Player.Instance;
        BaseSkill[] Skills = Resources.LoadAll<BaseSkill>("Skills/SkillObj/");

        foreach (BaseSkill skill in Skills)
        {
            SkillList.Add(skill.name, skill);
            SkillsCooldown.Add(skill.name, 0);
        }
    }
    private void Update()
    {
        if (SkillsCooldown.ContainsKey(skillManager.Skill_Up[0].name))
            SkillsCooldown[skillManager.Skill_Up[0].name] -= Time.deltaTime;

        if (SkillsCooldown.ContainsKey(skillManager.Skill_Down[0].name))
            SkillsCooldown[skillManager.Skill_Down[0].name] -= Time.deltaTime;
    }

    //TODO: 스킬 스트링을 담고있는 클래스 or 구조체를 만들어 스트링으로 넣는 인수를 최소화 시키자
    public void UseSkill(string name, DimensionType dimensionType)
    {
        BaseSkill skill = SkillList[name];
        if (SkillsCooldown.TryGetValue(name, out float cooldown) && cooldown < skill.SkillInfo._cooldown)
        {
            SkillsCooldown[name] = SkillList[name].SkillInfo._cooldown;
            BaseSkill skillObj = ObjectPool.Instance.CreateObj
                (skill.gameObject, new Vector3(player.transform.position.x, skill.StartPosY * (dimensionType == DimensionType.OVER ? 1 : -1)), Quaternion.identity).GetComponent<BaseSkill>();
            skillObj.dimensionType = dimensionType;
            skillObj.Init();
        }
    }

}
