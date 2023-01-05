using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Skill
{
    public string name;
    public string explanation;
    public Sprite icon;
    public Text coolTime;
    public float gold;
    public Text gold01;
    public Text gold02;
    public Text gold03;
}

public class Skill_List : MonoBehaviour
{
    public static Skill_List instance { get; private set; }
    void Awake() => instance = this;

    [Header("왼쪽 스킬")]
    public Image Left_Skill_Icon;
    public Text Left_Name;
    public Text Left_CoolTime;
    public Text Left_Explanation;
    public float Left_Gold;
    public Text Left_Gold_01;
    public Text Left_Gold_02;
    public Text Left_Gold_03;

    [Header("가운데 스킬")]
    public Image Among_Skill_Icon;
    public Text Among_Name;
    public Text Among_CoolTime;
    public Text Among_Explanation;
    public float Among_Gold;
    public Text Among_Gold_01;
    public Text Among_Gold_02;
    public Text Among_Gold_03;

    [Header("오른쪽 스킬")]
    public Image Right_Skill_Icon;
    public Text Right_Name;
    public Text Right_CoolTime;
    public Text Right_Explanation;
    public float Right_Gold;
    public Text Right_Gold_01;
    public Text Right_Gold_02;
    public Text Right_Gold_03;

    public SkillScript Left_Skill;
    public SkillScript Among_Skill;
    public SkillScript Right_Skill;
    void Start()
    {

    }

    public void Skill_Num(int i)
    {
        switch(i)
        {
            case 0:
                Left_Name.text = this.Left_Skill.SkillName;
                Left_CoolTime.text = this.Left_Skill.originalCooldown.ToString();
                Left_Explanation.text = this.Left_Skill.Description;
                break;
            case 1:
                Among_Name.text = this.Among_Skill.SkillName;
                Among_CoolTime.text = this.Among_Skill.originalCooldown.ToString();
                Among_Explanation.text = this.Among_Skill.Description;
                break;
            case 2:
                Right_Name.text = this.Right_Skill.SkillName;
                Right_CoolTime.text = this.Right_Skill.originalCooldown.ToString();
                Right_Explanation.text = this.Right_Skill.Description;
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
                this.Left_Skill = skill;
                Left_Name.text = this.Left_Skill.SkillName;
                Left_CoolTime.text = this.Left_Skill.originalCooldown.ToString();
                Left_Explanation.text = this.Left_Skill.Description;
                //if (Wave가 5일 경우)
                //{
                Left_Gold = this.Left_Skill.price[0];
                Left_Gold_01.text = Left_Gold.ToString();
                //}
                //if (Wave가 10일 경우)
                //{
                //  Left_Gold = this.Left_Skill.Gold_02;
                //  Left_Gold_02.text = "" + Left_Gold;
                //}
                //if (Wave가 15일 경우)
                //{
                //  Left_Gold = this.Left_Skill.Gold_03;
                //  Left_Gold_03.text = "" + Left_Gold;
                //}
                Left_Skill_Icon.sprite = this.Left_Skill.sprite;
                break;

            case 1:
                this.Among_Skill = skill;

                Among_Name.text = this.Among_Skill.SkillName;
                Among_CoolTime.text = this.Among_Skill.originalCooldown.ToString();
                Among_Explanation.text = this.Among_Skill.Description;
                //if (Wave가 5일 경우)
                //{
                Among_Gold = this.Among_Skill.price[0];
                Among_Gold_01.text = "" + Among_Gold;
                //}
                //if (Wave가 10일 경우)
                //{
                //  Among_Gold = this.Among_Skill.Gold_02;
                //  Among_Gold_02.text = "" + Among_Gold;
                //}
                //if (Wave가 15일 경우)
                //{
                //  Among_Gold = this.Among_Skill.Gold_03;
                //  Among_Gold_03.text = "" + Among_Gold;
                //}
                Among_Skill_Icon.sprite = this.Among_Skill.sprite;
                break;

            case 2:
                this.Right_Skill = skill;
                Right_Name.text = this.Right_Skill.SkillName;
                Right_CoolTime.text = this.Right_Skill.originalCooldown.ToString();
                Right_Explanation.text = this.Right_Skill.Description;
                //if (Wave가 5일 경우)
                //{
                Right_Gold = this.Right_Skill.price[0];
                Right_Gold_01.text = "" + Right_Gold;
                //}
                //if (Wave가 10일 경우)
                //{
                //  Right_Gold = this.Right_Skill.Gold_02;
                //  Right_Gold_02.text = "" + Right_Gold;
                //}
                //if (Wave가 15일 경우)
                //{
                //  Right_Gold = this.Right_Skill.Gold_03;
                //  Right_Gold_03.text = "" + Right_Gold;
                //}

                Right_Skill_Icon.sprite = this.Right_Skill.sprite;
                break;

        }
    }
}
