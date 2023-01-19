using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Salesman : MonoBehaviour
{
    [Header("다른 상품 보기")]
    [SerializeField] GameObject differentProduct;
    [SerializeField] GameObject AfterObject;

    [SerializeField] Text differentProductText;
    [SerializeField] Text goldText;

    [SerializeField] Image goldImage;
    [SerializeField] Image fBtn;

    public bool isApplyCheck = true;

    int goldNum;
    bool isCollisionCheck = false;

    const float waitTime = 0.5f;

    void Start()
    {
        // 정상 웨이브 : 5 / 10 / 15
        switch (WaveManager.instnace.m_WaveNum)
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
        ScreenVector(new Vector3(-8, -4.9f, 0));
        ReRoll();

    }

    void ScreenVector(Vector3 vec) => differentProduct.transform.localPosition = Camera.main.WorldToScreenPoint(transform.localPosition + vec);

    private void ReRoll()
    {
        if (GameManager.Instance._coin >= goldNum)
        {
            if (Input.GetKeyDown(KeyCode.F) && isCollisionCheck)
            {
                isApplyCheck = false;
                GameManager.Instance._coin -= goldNum;
                goldText.text = (goldNum += 200).ToString();


                for (int i = 0; i < Skill_Manager.instance.Skill.Count; i++)
                {
                    for (int j = 0; j < Skill_Manager.instance.Skill_Shop.Count; j++)
                    {
                        if (Skill_Manager.instance.Skill[i].name == Skill_Manager.instance.Skill_Shop[j].name)
                        {
                            Skill_Manager.instance.Skill.RemoveAt(i);
                            Skill_Manager.instance.Skill_Shop.RemoveAt(j--);
                        }
                    }
                }

                for (int i = 0; i < Skill_Manager.instance.Skill.Count; i++)
                {
                    Skill_Manager.instance.SkillBuffer.Add(Skill_Manager.instance.Skill[i]);
                    Skill_Manager.instance.Skill.RemoveAt(i--);
                }

                for (int i = 0; i < 3; i++)
                {
                    var soldOutText = SkillWindow.instance.transform.GetChild(i).GetChild(2).gameObject;
                    var shopSkillBox = SkillWindow.instance.transform.GetChild(i).GetChild(3).gameObject;

                    if (soldOutText.activeSelf)
                        soldOutText.SetActive(false);
                    if (!shopSkillBox.activeSelf)
                        shopSkillBox.SetActive(true);
                }

                Skill_Manager.instance.isSummonSKill = false;
                Skill_Manager.instance.AddSkill();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            isCollisionCheck = true;

            differentProductText.DOFade(1, waitTime).SetEase(Ease.Linear);
            fBtn.DOFade(1, waitTime).SetEase(Ease.Linear);
            goldText.DOFade(1, waitTime).SetEase(Ease.Linear);
            goldImage.DOFade(1, waitTime).SetEase(Ease.Linear);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null)
        {
            isCollisionCheck = false;

            differentProductText.DOFade(0, waitTime).SetEase(Ease.Linear);
            fBtn.DOFade(0, waitTime).SetEase(Ease.Linear);
            goldText.DOFade(0, waitTime).SetEase(Ease.Linear);
            goldImage.DOFade(0, waitTime).SetEase(Ease.Linear);
        }
    }
}
