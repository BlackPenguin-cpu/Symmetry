using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Malyeog_Mouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum Malyeog
    {
        Magic,
        Body
    }

    [Header("마력 강화")]
    [SerializeField] Malyeog malyeog;
    public int malyeogNum = 0; // 마력 강화 순서
    public int malyeogUpgrade = 0; // 마력 강화 업그레이드
    public Text malyeogUpgradeText; // 마력 강화 텍스트

    [Header("스킬 잠금해제")]
    public bool BodyOpen_Check = false;
    public bool MagicOpen_Check = false;

    Foundation foundation;

    void Start()
    {
        foundation = Foundation.instance;
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (malyeog == Malyeog.Magic)
        {
            foundation.transform.GetChild(0).gameObject.SetActive(true);
            foundation.transform.GetChild(1).gameObject.SetActive(false);

            switch (malyeogNum)
            {
                case 0:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    foundation.Title.text = "보이지 않는 손";
                    malyeogUpgradeText.text = malyeogUpgrade + "/4";
                    switch (malyeogUpgrade)
                    {
                        case 0:
                            foundation.Dimensional_Price.text = (700 + malyeogUpgrade * 150).ToString();
                            foundation.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                            break;

                        case 1:
                            foundation.Dimensional_Price.text = (700 + malyeogUpgrade * 150).ToString();
                            foundation.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "10% / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                            break;

                        case 2:
                            foundation.Dimensional_Price.text = (700 + malyeogUpgrade * 150).ToString();
                            foundation.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "20% / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                            break;

                        case 3:
                            foundation.Dimensional_Price.text = (700 + malyeogUpgrade * 150).ToString();
                            foundation.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "30% / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                            break;

                        case 4:
                            foundation.Dimensional_Price.text = "Max";
                            foundation.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "40%" + " 증가한다.";
                            break;
                    }
                    break;

                case 1:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    foundation.Title.text = "꿰뚫어보는 눈";
                    malyeogUpgradeText.text = malyeogUpgrade + "/4";
                    switch (malyeogUpgrade)
                    {
                        case 0:
                            foundation.Dimensional_Price.text = (1100 + malyeogUpgrade * 200).ToString();
                            foundation.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                            break;

                        case 1:
                            foundation.Dimensional_Price.text = (1100 + malyeogUpgrade * 200).ToString();
                            foundation.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "3% / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                            break;

                        case 2:
                            foundation.Dimensional_Price.text = (1100 + malyeogUpgrade * 200).ToString();
                            foundation.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "5% / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                            break;

                        case 3:
                            foundation.Dimensional_Price.text = (1100 + malyeogUpgrade * 200).ToString();
                            foundation.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "7% / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                            break;

                        case 4:
                            foundation.Dimensional_Price.text = "Max";
                            foundation.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "10%" + " 상승한다.";
                            break;
                    }
                    break;

                case 2:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    foundation.Title.text = "시간가속";
                    malyeogUpgradeText.text = malyeogUpgrade + "/4";
                    switch (malyeogUpgrade)
                    {
                        case 0:
                            foundation.Dimensional_Price.text = (1700 + malyeogUpgrade * 250).ToString();
                            foundation.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                            break;

                        case 1:
                            foundation.Dimensional_Price.text = (1700 + malyeogUpgrade * 250).ToString();
                            foundation.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "5% / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                            break;

                        case 2:
                            foundation.Dimensional_Price.text = (1700 + malyeogUpgrade * 250).ToString();
                            foundation.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "10% / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                            break;

                        case 3:
                            foundation.Dimensional_Price.text = (1700 + malyeogUpgrade * 250).ToString();
                            foundation.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "15% / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                            break;

                        case 4:
                            foundation.Dimensional_Price.text = "Max";
                            foundation.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "20%" + " 감소한다.";
                            break;
                    }
                    break;

                case 3:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    foundation.Title.text = "연금술";
                    malyeogUpgradeText.text = malyeogUpgrade + "/2";
                    switch (malyeogUpgrade)
                    {
                        case 0:
                            foundation.Dimensional_Price.text = (3300 + malyeogUpgrade * 3000).ToString();
                            foundation.Explanation.text = "금단의 연금술을 사용해 획득하는 골드의 양이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " 감소한다.";
                            break;

                        case 1:
                            foundation.Dimensional_Price.text = (3300 + malyeogUpgrade * 3000).ToString();
                            foundation.Explanation.text = "금단의 연금술을 사용해 획득하는 골드의 양이 " + "5% / " + "<color=#877D78>" + "10%" + "</color>" + " 감소한다.";
                            break;

                        case 2:
                            foundation.Dimensional_Price.text = "Max";
                            foundation.Explanation.text = "금단의 연금술을 사용해 획득하는 골드의 양이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "10%" + " 감소한다.";
                            break;
                    }
                    break;
            }
        }

        if (malyeog == Malyeog.Body)
        {
            foundation.transform.GetChild(0).gameObject.SetActive(true);
            foundation.transform.GetChild(1).gameObject.SetActive(false);
            switch (malyeogNum)
            {
                case 0:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    foundation.Title.text = "실피드의 도약";
                    malyeogUpgradeText.text = malyeogUpgrade + "/4";
                    switch (malyeogUpgrade)
                    {
                        case 0:
                            foundation.Dimensional_Price.text = (600 + malyeogUpgrade * 150).ToString();
                            foundation.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                            break;

                        case 1:
                            foundation.Dimensional_Price.text = (600 + malyeogUpgrade * 150).ToString();
                            foundation.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 10% / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                            break;

                        case 2:
                            foundation.Dimensional_Price.text = (600 + malyeogUpgrade * 150).ToString();
                            foundation.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "20% / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                            break;

                        case 3:
                            foundation.Dimensional_Price.text = (600 + malyeogUpgrade * 150).ToString();
                            foundation.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "30% / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                            break;

                        case 4:
                            foundation.Dimensional_Price.text = "Max";
                            foundation.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "40%" + " 상승한다.";
                            break;
                    }
                    break;

                case 1:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    foundation.Title.text = "거인의 힘";
                    malyeogUpgradeText.text = malyeogUpgrade + "/4";
                    switch (malyeogUpgrade)
                    {
                        case 0:
                            foundation.Dimensional_Price.text = (1000 + malyeogUpgrade * 200).ToString();
                            foundation.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                            break;

                        case 1:
                            foundation.Dimensional_Price.text = (1000 + malyeogUpgrade * 200).ToString();
                            foundation.Explanation.text = "거인의 힘을 받아 공격력이 15% / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                            break;

                        case 2:
                            foundation.Dimensional_Price.text = (1000 + malyeogUpgrade * 200).ToString();
                            foundation.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "30% / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                            break;

                        case 3:
                            foundation.Dimensional_Price.text = (1000 + malyeogUpgrade * 200).ToString();
                            foundation.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "45% / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                            break;

                        case 4:
                            foundation.Dimensional_Price.text = "Max";
                            foundation.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "60%" + " 상승한다.";
                            break;
                    }
                    break;

                case 2:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    foundation.Title.text = "강철 피부";
                    malyeogUpgradeText.text = malyeogUpgrade + "/4";
                    switch (malyeogUpgrade)
                    {
                        case 0:
                            foundation.Dimensional_Price.text = (1500 + malyeogUpgrade * 250).ToString();
                            foundation.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "<color=#877D78>" + "10" + "</color>" + " / " + "<color=#877D78>" + "15" + "</color>" + " / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                            break;

                        case 1:
                            foundation.Dimensional_Price.text = (1500 + malyeogUpgrade * 250).ToString();
                            foundation.Explanation.text = "강철같은 피부를 얻어 방어력이 5 / " + "<color=#877D78>" + "10" + "</color>" + " / " + "<color=#877D78>" + "15" + "</color>" + " / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                            break;

                        case 2:
                            foundation.Dimensional_Price.text = (1500 + malyeogUpgrade * 250).ToString();
                            foundation.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "10 / " + "<color=#877D78>" + "15" + "</color>" + " / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                            break;

                        case 3:
                            foundation.Dimensional_Price.text = (1500 + malyeogUpgrade * 250).ToString();
                            foundation.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "<color=#877D78>" + "10" + "</color>" + " / " + "15 / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                            break;

                        case 4:
                            foundation.Dimensional_Price.text = "Max";
                            foundation.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "<color=#877D78>" + "10" + "</color>" + " / " + "<color=#877D78>" + "15" + "</color>" + " / " + "20" + " 상승한다.";
                            break;
                    }
                    break;

                case 3:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    foundation.Title.text = "마공학 심장";
                    malyeogUpgradeText.text = malyeogUpgrade + "/2";
                    switch (malyeogUpgrade)
                    {
                        case 0:
                            foundation.Dimensional_Price.text = (3000 + malyeogUpgrade * 3000).ToString();
                            foundation.Explanation.text = "마정석 심장이 두번째 기회를 줘 최대체력의 " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "50%" + "</color>" + " 상승한다.";
                            break;

                        case 1:
                            foundation.Dimensional_Price.text = (3000 + malyeogUpgrade * 3000).ToString();
                            foundation.Explanation.text = "마정석 심장이 두번째 기회를 줘 최대체력의 20% / " + "<color=#877D78>" + "50%" + "</color>" + " 상승한다.";
                            break;

                        case 2:
                            foundation.Dimensional_Price.text = "Max";
                            foundation.Explanation.text = "마정석 심장이 두번째 기회를 줘 최대체력의 " + "<color=#877D78>" + "20%" + "</color>" + " / " + "50%" + " 상승한다.";
                            break;
                    }
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foundation.transform.GetChild(0).gameObject.SetActive(false);
        foundation.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (malyeog == Malyeog.Magic)
        {
            switch (malyeogNum)
            {
                case 0:
                    if (GameManager.Instance.crystal >= (700 + malyeogUpgrade * 150) && foundation.Dimensional_Price.text != "Max")
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        GameManager.Instance.crystal -= (700 + malyeogUpgrade * 150);

                        malyeogUpgrade++;
                        malyeogUpgradeText.text = malyeogUpgrade + "/4";
                        Player.Instance.stat.magicPower.invisibleHand++;
                        switch (malyeogUpgrade)
                        {
                            case 1:
                                foundation.Dimensional_Price.text = (700 + malyeogUpgrade * 150).ToString();
                                foundation.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "10% / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                                break;

                            case 2:
                                foundation.Dimensional_Price.text = (700 + malyeogUpgrade * 150).ToString();
                                foundation.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "20% / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                                break;

                            case 3:
                                foundation.Dimensional_Price.text = (700 + malyeogUpgrade * 150).ToString();
                                foundation.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "30% / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                                break;

                            case 4:
                                foundation.Dimensional_Price.text = "Max";
                                foundation.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "40%" + " 증가한다.";
                                break;
                        }

                        MagicOpen_Check = true;
                        if (MagicOpen_Check == true && foundation.Magic_Open == malyeogNum)
                            foundation.Magic_Open++;
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 1:
                    if (GameManager.Instance.crystal >= (1100 + malyeogUpgrade * 200) && foundation.Dimensional_Price.text != "Max" && foundation.Magic_Open >= malyeogNum)
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);


                        GameManager.Instance.crystal -= (1100 + malyeogUpgrade * 200);

                        malyeogUpgrade++;
                        malyeogUpgradeText.text = malyeogUpgrade + "/4";
                        switch (malyeogUpgrade)
                        {
                            case 1:
                                Player.Instance.stat.magicPower.sharpEye = 1;
                                foundation.Dimensional_Price.text = (1100 + malyeogUpgrade * 200).ToString();
                                foundation.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "3% / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                                break;

                            case 2:
                                Player.Instance.stat.magicPower.sharpEye = 2;
                                foundation.Dimensional_Price.text = (1100 + malyeogUpgrade * 200).ToString();
                                foundation.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "5% / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                                break;

                            case 3:
                                Player.Instance.stat.magicPower.sharpEye = 3;
                                foundation.Dimensional_Price.text = (1100 + malyeogUpgrade * 200).ToString();
                                foundation.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "7% / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                                break;

                            case 4:
                                Player.Instance.stat.magicPower.sharpEye = 4;
                                foundation.Dimensional_Price.text = "Max";
                                foundation.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "10%" + " 상승한다.";
                                break;
                        }

                        MagicOpen_Check = true;
                        if (MagicOpen_Check == true && foundation.Magic_Open == malyeogNum)
                            foundation.Magic_Open++;
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 2:
                    if (GameManager.Instance.crystal >= (1700 + malyeogUpgrade * 250) && foundation.Dimensional_Price.text != "Max" && foundation.Magic_Open >= malyeogNum)
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);


                        GameManager.Instance.crystal -= (1700 + malyeogUpgrade * 250);

                        malyeogUpgrade++;
                        malyeogUpgradeText.text = malyeogUpgrade + "/4";
                        Player.Instance.stat.magicPower.timeQuick++;
                        switch (malyeogUpgrade)
                        {
                            case 1:
                                foundation.Dimensional_Price.text = (1700 + malyeogUpgrade * 250).ToString();
                                foundation.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "5% / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                                break;

                            case 2:
                                foundation.Dimensional_Price.text = (1700 + malyeogUpgrade * 250).ToString();
                                foundation.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "10% / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                                break;

                            case 3:
                                foundation.Dimensional_Price.text = (1700 + malyeogUpgrade * 250).ToString();
                                foundation.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "15% / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                                break;

                            case 4:
                                foundation.Dimensional_Price.text = "Max";
                                foundation.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "20%" + " 감소한다.";
                                break;
                        }

                        MagicOpen_Check = true;
                        if (MagicOpen_Check == true && foundation.Magic_Open == malyeogNum)
                            foundation.Magic_Open++;
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 3:
                    if (GameManager.Instance.crystal >= (3300 + malyeogUpgrade * 3000) && foundation.Dimensional_Price.text != "Max" && foundation.Magic_Open >= malyeogNum)
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);


                        GameManager.Instance.crystal -= (3300 + malyeogUpgrade * 3000);

                        malyeogUpgrade++;
                        malyeogUpgradeText.text = malyeogUpgrade + "/2";
                        Player.Instance.stat.magicPower.thaumcraft++;
                        switch (malyeogUpgrade)
                        {
                            case 1:
                                foundation.Dimensional_Price.text = (3300 + malyeogUpgrade * 3000).ToString();
                                foundation.Explanation.text = "금단의 연금술을 사용해 획득하는 골드의 양이 " + "5% / " + "<color=#877D78>" + "10%" + "</color>" + " 감소한다.";
                                break;

                            case 2:
                                foundation.Dimensional_Price.text = "Max";
                                foundation.Explanation.text = "금단의 연금술을 사용해 획득하는 골드의 양이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "10%" + " 감소한다.";
                                break;
                        }
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;
            }
        }

        if (malyeog == Malyeog.Body)
        {
            switch (malyeogNum)
            {
                case 0:
                    if (GameManager.Instance.crystal >= (600 + malyeogUpgrade * 150) && foundation.Dimensional_Price.text != "Max")
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);


                        GameManager.Instance.crystal -= (600 + malyeogUpgrade * 150);

                        malyeogUpgrade++;
                        malyeogUpgradeText.text = malyeogUpgrade + "/4";
                        Player.Instance.stat.magicPower.silpidLeap++;
                        switch (malyeogUpgrade)
                        {
                            case 1:
                                foundation.Dimensional_Price.text = (600 + malyeogUpgrade * 150).ToString();
                                foundation.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 10% / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                                break;

                            case 2:
                                foundation.Dimensional_Price.text = (600 + malyeogUpgrade * 150).ToString();
                                foundation.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "20% / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                                break;

                            case 3:
                                foundation.Dimensional_Price.text = (600 + malyeogUpgrade * 150).ToString();
                                foundation.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "30% / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                                break;

                            case 4:
                                foundation.Dimensional_Price.text = "Max";
                                foundation.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "40%" + " 상승한다.";
                                break;
                        }

                        BodyOpen_Check = true;
                        if (BodyOpen_Check == true && foundation.Body_Open == malyeogNum)
                            foundation.Body_Open++;
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 1:
                    if (GameManager.Instance.crystal >= (1000 + malyeogUpgrade * 200) && foundation.Dimensional_Price.text != "Max" && foundation.Body_Open >= malyeogNum)
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);


                        GameManager.Instance.crystal -= (1000 + malyeogUpgrade * 200);

                        malyeogUpgrade++;
                        malyeogUpgradeText.text = malyeogUpgrade + "/4";
                        Player.Instance.stat.magicPower.giantPower++;
                        switch (malyeogUpgrade)
                        {
                            case 1:
                                foundation.Dimensional_Price.text = (1000 + malyeogUpgrade * 200).ToString();
                                foundation.Explanation.text = "거인의 힘을 받아 공격력이 15% / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                                break;

                            case 2:
                                foundation.Dimensional_Price.text = (1000 + malyeogUpgrade * 200).ToString();
                                foundation.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "30% / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                                break;

                            case 3:
                                foundation.Dimensional_Price.text = (1000 + malyeogUpgrade * 200).ToString();
                                foundation.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "45% / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                                break;

                            case 4:
                                foundation.Dimensional_Price.text = "Max";
                                foundation.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "60%" + " 상승한다.";
                                break;
                        }

                        BodyOpen_Check = true;
                        if (BodyOpen_Check == true && foundation.Body_Open == malyeogNum)
                            foundation.Body_Open++;
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 2:
                    if (GameManager.Instance.crystal >= (1500 + malyeogUpgrade * 250) && foundation.Dimensional_Price.text != "Max" && foundation.Body_Open >= malyeogNum)
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        GameManager.Instance.crystal -= (1500 + malyeogUpgrade * 250);

                        malyeogUpgrade++;
                        malyeogUpgradeText.text = malyeogUpgrade + "/4";
                        Player.Instance.stat.magicPower.ironSkin++;
                        switch (malyeogUpgrade)
                        {
                            case 1:
                                foundation.Dimensional_Price.text = (1500 + malyeogUpgrade * 250).ToString();
                                foundation.Explanation.text = "강철같은 피부를 얻어 방어력이 5 / " + "<color=#877D78>" + "10" + "</color>" + " / " + "<color=#877D78>" + "15" + "</color>" + " / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                                break;

                            case 2:
                                foundation.Dimensional_Price.text = (1500 + malyeogUpgrade * 250).ToString();
                                foundation.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "10 / " + "<color=#877D78>" + "15" + "</color>" + " / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                                break;

                            case 3:
                                foundation.Dimensional_Price.text = (1500 + malyeogUpgrade * 250).ToString();
                                foundation.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "<color=#877D78>" + "10" + "</color>" + " / " + "15 / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                                break;

                            case 4:
                                foundation.Dimensional_Price.text = "Max";
                                foundation.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "<color=#877D78>" + "10" + "</color>" + " / " + "<color=#877D78>" + "15" + "</color>" + " / " + "20" + " 상승한다.";
                                break;
                        }

                        BodyOpen_Check = true;
                        if (BodyOpen_Check == true && foundation.Body_Open == malyeogNum)
                            foundation.Body_Open++;
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 3:
                    if (GameManager.Instance.crystal >= (3000 + malyeogUpgrade * 3000) && foundation.Dimensional_Price.text != "Max" && foundation.Body_Open >= malyeogNum)
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        GameManager.Instance.crystal -= (3000 + malyeogUpgrade * 3000);

                        malyeogUpgrade++;
                        malyeogUpgradeText.text = malyeogUpgrade + "/2";
                        switch (malyeogUpgrade)
                        {
                            case 1:
                                Player.Instance.stat.magicPower.magicHeart++;
                                foundation.Dimensional_Price.text = (3000 + malyeogUpgrade * 3000).ToString();
                                foundation.Explanation.text = "마정석 심장이 두번째 기회를 줘 최대체력의 20% / " + "<color=#877D78>" + "50%" + "</color>" + " 상승한다.";
                                break;

                            case 2:
                                Player.Instance.stat.magicPower.magicHeart++;
                                foundation.Dimensional_Price.text = "Max";
                                foundation.Explanation.text = "마정석 심장이 두번째 기회를 줘 최대체력의 " + "<color=#877D78>" + "20%" + "</color>" + " / " + "50%" + " 상승한다.";
                                break;
                        }
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;
            }
        }
    }

}
