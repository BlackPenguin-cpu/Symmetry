using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkillList
{
    public Image icon;
    public Text name;
    public Text explanation;
    public Text coolTime;
    public float gold;
    public Text goldText;
}

public class Skill_List : MonoBehaviour
{
    public static Skill_List instance { get; private set; }
    void Awake() => instance = this;

    public SkillList leftSkill;
    public SkillList amongSkill;
    public SkillList rightSkill;

    public SkillScript Left_Skill;
    public SkillScript Among_Skill;
    public SkillScript Right_Skill;
    void Start()
    {

    }

    public void Skill_Num(int i)
    {
        switch (i)
        {
            case 0:
                leftSkill.name.text = Left_Skill.SkillName;
                leftSkill.coolTime.text = Left_Skill.originalCooldown.ToString();
                leftSkill.explanation.text = Left_Skill.Description;
                break;
            case 1:
                amongSkill.name.text = Among_Skill.SkillName;
                amongSkill.coolTime.text = Among_Skill.originalCooldown.ToString();
                amongSkill.explanation.text = Among_Skill.Description;
                break;
            case 2:
                rightSkill.name.text = Right_Skill.SkillName;
                rightSkill.coolTime.text = Right_Skill.originalCooldown.ToString();
                rightSkill.explanation.text = Right_Skill.Description;
                break;
        }
        return;
    }
    void Update()
    {

    }

    public void SkillCard(SkillScript skill, int skillIndex)
    {
        switch (skillIndex)
        {
            case 0:
                Left_Skill = skill;
                leftSkill.name.text = Left_Skill.SkillName;
                leftSkill.coolTime.text = Left_Skill.originalCooldown.ToString();
                leftSkill.explanation.text = Left_Skill.Description;
                //if (Wave啊 5老 版快)
                //{
                leftSkill.gold = Left_Skill.price[0];
                leftSkill.goldText.text = leftSkill.gold.ToString();
                //}
                //if (Wave啊 10老 版快)
                //{
                //  Left_Gold = this.Left_Skill.Gold_02;
                //  Left_Gold_02.text = "" + Left_Gold;
                //}
                //if (Wave啊 15老 版快)
                //{
                //  Left_Gold = this.Left_Skill.Gold_03;
                //  Left_Gold_03.text = "" + Left_Gold;
                //}
                leftSkill.icon.sprite = Left_Skill.sprite;
                break;

            case 1:
                Among_Skill = skill;

                amongSkill.name.text = Among_Skill.SkillName;
                amongSkill.coolTime.text = Among_Skill.originalCooldown.ToString();
                amongSkill.explanation.text = Among_Skill.Description;
                //if (Wave啊 5老 版快)
                //{
                amongSkill.gold = Among_Skill.price[0];
                amongSkill.goldText.text = amongSkill.gold.ToString();
                //}
                //if (Wave啊 10老 版快)
                //{
                //  Among_Gold = this.Among_Skill.Gold_02;
                //  Among_Gold_02.text = "" + Among_Gold;
                //}
                //if (Wave啊 15老 版快)
                //{
                //  Among_Gold = this.Among_Skill.Gold_03;
                //  Among_Gold_03.text = "" + Among_Gold;
                //}
                amongSkill.icon.sprite = Among_Skill.sprite;
                break;

            case 2:
                Right_Skill = skill;
                rightSkill.name.text = Right_Skill.SkillName;
                rightSkill.coolTime.text = Right_Skill.originalCooldown.ToString();
                rightSkill.explanation.text = Right_Skill.Description;
                //if (Wave啊 5老 版快)
                //{
                rightSkill.gold = Right_Skill.price[0];
                rightSkill.goldText.text = rightSkill.gold.ToString();
                //}
                //if (Wave啊 10老 版快)
                //{
                //  Right_Gold = this.Right_Skill.Gold_02;
                //  Right_Gold_02.text = "" + Right_Gold;
                //}
                //if (Wave啊 15老 版快)
                //{
                //  Right_Gold = this.Right_Skill.Gold_03;
                //  Right_Gold_03.text = "" + Right_Gold;
                //}

                rightSkill.icon.sprite = Right_Skill.sprite;
                break;

        }
    }
}
