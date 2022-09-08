using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Salesman : MonoBehaviour
{
    public static Salesman Inst { get; private set; }
    void Awake() => Inst = this;

    [Header("다른 상품 보기")]
    public GameObject Different_Product;

    public Text Different_Product_Text;
    public Text Gold_Text;

    public Image Gold_Image;
    public Image F_Button;

    private int Gold_Num;
    public bool Apply_Check = true;
    private bool Re_Roll_Check = true;
    private bool Collision_Check = true;

    private GameObject AfterObject;
    void Start()
    {
        #region 오브젝트 찾기
        Different_Product = GameObject.Find("Salesman");
        F_Button = GameObject.Find("F_Image").GetComponent<Image>();
        Gold_Image = GameObject.Find("Salesman_Gold_Image").GetComponent<Image>();

        Gold_Text = GameObject.Find("Salesman_Gold_Text").GetComponent<Text>();
        Different_Product_Text = GameObject.Find("Different_Product_Text").GetComponent<Text>();
        AfterObject = GameObject.Find("After_Purchase");
        #endregion

        // 정상 웨이브 : 5 / 10 / 15
        switch(WaveManager.Instance.m_WaveNum)
        {
            case 3:
                Gold_Num = 600;
                break;

            case 5:
                Gold_Num = 1052;
                break;
            case 15:
                Gold_Num = 2019;
                break;
        }
        Gold_Text.text = Gold_Num.ToString();
    }
    void Update()
    {
        Re_Roll();
        Different_Product.transform.localPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.localPosition + new Vector3(-8, -4.9f, 0));
    }

    private void Re_Roll()
    {
        if (GameManager.Instance._coin >= Gold_Num)
        {
            if (Input.GetKeyDown(KeyCode.F) && Collision_Check == false)
            {
                AfterObject.SetActive(true);
                Destroy(GameObject.Find("Skill_Shop(Clone)"));
                Apply_Check = false;
                GameManager.Instance._coin -= Gold_Num;
                Gold_Text.text = (Gold_Num += 200).ToString();


                for (int i = 0; i < Skill_Manager.Inst.Skill.Count; i++)
                {
                    for (int j = 0; j < Skill_Manager.Inst.Skill_Shop.Count; j++)
                    {
                        if (Skill_Manager.Inst.Skill[i].name == Skill_Manager.Inst.Skill_Shop[j].name)
                        {
                            Re_Roll_Check = false;
                            Skill_Manager.Inst.Skill.RemoveAt(i);
                            Skill_Manager.Inst.Skill_Shop.RemoveAt(j--);
                        }
                    }
                }

                if (Re_Roll_Check == false)
                {
                    for (int i = 0; i < Skill_Manager.Inst.Skill.Count; i++)
                    {
                        Skill_Manager.Inst.SkillBuffer.Add(Skill_Manager.Inst.Skill[i]);
                        Skill_Manager.Inst.Skill.RemoveAt(i--);
                    }
                }
                Skill_Manager.Inst.AddSkill();
            }
        }
    }

    #region 충돌체크

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            Collision_Check = false;
            Different_Product_Text.DOFade(1f, 0.5f);
            F_Button.DOFade(1f, 0.5f);
            Gold_Text.DOFade(1f, 0.5f);
            Gold_Image.DOFade(1f, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            Collision_Check = true;
            Different_Product_Text.DOFade(0f, 0.5f);
            F_Button.DOFade(0f, 0.5f);
            Gold_Text.DOFade(0f, 0.5f);
            Gold_Image.DOFade(0f, 0.5f);
        }
    }
    #endregion
}
