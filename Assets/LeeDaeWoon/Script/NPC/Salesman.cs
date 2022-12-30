using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Salesman : MonoBehaviour
{
    [Header("다른 상품 보기")]
    [SerializeField] GameObject differentProduct;

    [SerializeField] Text differentProductText;
    [SerializeField] Text goldText;

    [SerializeField] Image goldImage;
    [SerializeField] Image fBtn;

    public bool isApplyCheck = true;

    int goldNum;
    bool isReRollCheck = true;
    bool isCollisionCheck = true;

    private GameObject AfterObject;
    void Start()
    {
        #region 오브젝트 찾기
        differentProduct = GameObject.Find("Salesman");
        fBtn = GameObject.Find("F_Image").GetComponent<Image>();
        goldImage = GameObject.Find("Salesman_Gold_Image").GetComponent<Image>();

        goldText = GameObject.Find("Salesman_Gold_Text").GetComponent<Text>();
        differentProductText = GameObject.Find("Different_Product_Text").GetComponent<Text>();
        AfterObject = GameObject.Find("After_Purchase");
        #endregion

        // 정상 웨이브 : 5 / 10 / 15
        switch(WaveManager.instnace.m_WaveNum)
        {
            case 3:
                goldNum = 600;
                break;

            case 5:
                goldNum = 1052;
                break;
            case 15:
                goldNum = 2019;
                break;
        }
        goldText.text = goldNum.ToString();
    }
    void Update()
    {
        Re_Roll();
        differentProduct.transform.localPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.localPosition + new Vector3(-8, -4.9f, 0));
    }

    private void Re_Roll()
    {
        if (GameManager.Instance._coin >= goldNum)
        {
            if (Input.GetKeyDown(KeyCode.F) && isCollisionCheck == false)
            {
                AfterObject.SetActive(true);
                Destroy(GameObject.Find("Skill_Shop(Clone)"));
                isApplyCheck = false;
                GameManager.Instance._coin -= goldNum;
                goldText.text = (goldNum += 200).ToString();


                for (int i = 0; i < Skill_Manager.instance.Skill.Count; i++)
                {
                    for (int j = 0; j < Skill_Manager.instance.Skill_Shop.Count; j++)
                    {
                        if (Skill_Manager.instance.Skill[i].name == Skill_Manager.instance.Skill_Shop[j].name)
                        {
                            isReRollCheck = false;
                            Skill_Manager.instance.Skill.RemoveAt(i);
                            Skill_Manager.instance.Skill_Shop.RemoveAt(j--);
                        }
                    }
                }

                if (isReRollCheck == false)
                {
                    for (int i = 0; i < Skill_Manager.instance.Skill.Count; i++)
                    {
                        Skill_Manager.instance.SkillBuffer.Add(Skill_Manager.instance.Skill[i]);
                        Skill_Manager.instance.Skill.RemoveAt(i--);
                    }
                }
                Skill_Manager.instance.AddSkill();
            }
        }
    }

    #region 충돌체크

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            isCollisionCheck = false;
            differentProductText.DOFade(1f, 0.5f);
            fBtn.DOFade(1f, 0.5f);
            goldText.DOFade(1f, 0.5f);
            goldImage.DOFade(1f, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            isCollisionCheck = true;
            differentProductText.DOFade(0f, 0.5f);
            fBtn.DOFade(0f, 0.5f);
            goldText.DOFade(0f, 0.5f);
            goldImage.DOFade(0f, 0.5f);
        }
    }
    #endregion
}
