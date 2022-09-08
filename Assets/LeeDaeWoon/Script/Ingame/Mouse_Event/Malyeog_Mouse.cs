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
    public int Malyeog_Num; // 마력 강화 순서
    public int Malyeog_Upgrade; // 마력 강화 업그레이드
    public Text MalyeogUp_Text; // 마력 강화 텍스트

    [Header("스킬 잠금해제")]
    public bool BodyOpen_Check = false;
    public bool MagicOpen_Check = false;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (malyeog == Malyeog.Magic)
        {
            Foundation.Inst.Price_obj.SetActive(true);
            Foundation.Inst.Close_Btn.SetActive(false);
            switch (Malyeog_Num)
            {
                case 0:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Foundation.Inst.Title.text = "보이지 않는 손";
                    MalyeogUp_Text.text = Malyeog_Upgrade + "/4";
                    switch (Malyeog_Upgrade)
                    {
                        case 0:
                            Foundation.Inst.Dimensional_Price.text = (700 + Malyeog_Upgrade * 150).ToString();
                            Foundation.Inst.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                            break;

                        case 1:
                            Foundation.Inst.Dimensional_Price.text = (700 + Malyeog_Upgrade * 150).ToString();
                            Foundation.Inst.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "10% / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                            break;

                        case 2:
                            Foundation.Inst.Dimensional_Price.text = (700 + Malyeog_Upgrade * 150).ToString();
                            Foundation.Inst.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "20% / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                            break;

                        case 3:
                            Foundation.Inst.Dimensional_Price.text = (700 + Malyeog_Upgrade * 150).ToString();
                            Foundation.Inst.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "30% / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                            break;

                        case 4:
                            Foundation.Inst.Dimensional_Price.text = "Max";
                            Foundation.Inst.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "40%" + " 증가한다.";
                            break;
                    }
                    break;

                case 1:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Foundation.Inst.Title.text = "꿰뚫어보는 눈";
                    MalyeogUp_Text.text = Malyeog_Upgrade + "/4";
                    switch (Malyeog_Upgrade)
                    {
                        case 0:
                            Foundation.Inst.Dimensional_Price.text = (1100 + Malyeog_Upgrade * 200).ToString();
                            Foundation.Inst.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                            break;

                        case 1:
                            Foundation.Inst.Dimensional_Price.text = (1100 + Malyeog_Upgrade * 200).ToString();
                            Foundation.Inst.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "3% / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                            break;

                        case 2:
                            Foundation.Inst.Dimensional_Price.text = (1100 + Malyeog_Upgrade * 200).ToString();
                            Foundation.Inst.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "5% / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                            break;

                        case 3:
                            Foundation.Inst.Dimensional_Price.text = (1100 + Malyeog_Upgrade * 200).ToString();
                            Foundation.Inst.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "7% / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                            break;

                        case 4:
                            Foundation.Inst.Dimensional_Price.text = "Max";
                            Foundation.Inst.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "10%" + " 상승한다.";
                            break;
                    }
                    break;

                case 2:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Foundation.Inst.Title.text = "시간가속";
                    MalyeogUp_Text.text = Malyeog_Upgrade + "/4";
                    switch (Malyeog_Upgrade)
                    {
                        case 0:
                            Foundation.Inst.Dimensional_Price.text = (1700 + Malyeog_Upgrade * 250).ToString();
                            Foundation.Inst.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                            break;

                        case 1:
                            Foundation.Inst.Dimensional_Price.text = (1700 + Malyeog_Upgrade * 250).ToString();
                            Foundation.Inst.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "5% / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                            break;

                        case 2:
                            Foundation.Inst.Dimensional_Price.text = (1700 + Malyeog_Upgrade * 250).ToString();
                            Foundation.Inst.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "10% / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                            break;

                        case 3:
                            Foundation.Inst.Dimensional_Price.text = (1700 + Malyeog_Upgrade * 250).ToString();
                            Foundation.Inst.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "15% / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                            break;

                        case 4:
                            Foundation.Inst.Dimensional_Price.text = "Max";
                            Foundation.Inst.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "20%" + " 감소한다.";
                            break;
                    }
                    break;

                case 3:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Foundation.Inst.Title.text = "연금술";
                    MalyeogUp_Text.text = Malyeog_Upgrade + "/2";
                    switch (Malyeog_Upgrade)
                    {
                        case 0:
                            Foundation.Inst.Dimensional_Price.text = (3300 + Malyeog_Upgrade * 3000).ToString();
                            Foundation.Inst.Explanation.text = "금단의 연금술을 사용해 획득하는 골드의 양이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " 감소한다.";
                            break;

                        case 1:
                            Foundation.Inst.Dimensional_Price.text = (3300 + Malyeog_Upgrade * 3000).ToString();
                            Foundation.Inst.Explanation.text = "금단의 연금술을 사용해 획득하는 골드의 양이 " + "5% / " + "<color=#877D78>" + "10%" + "</color>" + " 감소한다.";
                            break;

                        case 2:
                            Foundation.Inst.Dimensional_Price.text = "Max";
                            Foundation.Inst.Explanation.text = "금단의 연금술을 사용해 획득하는 골드의 양이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "10%" + " 감소한다.";
                            break;
                    }
                    break;
            }
        }

        if (malyeog == Malyeog.Body)
        {
            Foundation.Inst.Price_obj.SetActive(true);
            Foundation.Inst.Close_Btn.SetActive(false);
            switch (Malyeog_Num)
            {
                case 0:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Foundation.Inst.Title.text = "실피드의 도약";
                    MalyeogUp_Text.text = Malyeog_Upgrade + "/4";
                    switch (Malyeog_Upgrade)
                    {
                        case 0:
                            Foundation.Inst.Dimensional_Price.text = (600 + Malyeog_Upgrade * 150).ToString();
                            Foundation.Inst.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                            break;

                        case 1:
                            Foundation.Inst.Dimensional_Price.text = (600 + Malyeog_Upgrade * 150).ToString();
                            Foundation.Inst.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 10% / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                            break;

                        case 2:
                            Foundation.Inst.Dimensional_Price.text = (600 + Malyeog_Upgrade * 150).ToString();
                            Foundation.Inst.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "20% / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                            break;

                        case 3:
                            Foundation.Inst.Dimensional_Price.text = (600 + Malyeog_Upgrade * 150).ToString();
                            Foundation.Inst.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "30% / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                            break;

                        case 4:
                            Foundation.Inst.Dimensional_Price.text = "Max";
                            Foundation.Inst.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "40%" + " 상승한다.";
                            break;
                    }
                    break;

                case 1:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Foundation.Inst.Title.text = "거인의 힘";
                    MalyeogUp_Text.text = Malyeog_Upgrade + "/4";
                    switch (Malyeog_Upgrade)
                    {
                        case 0:
                            Foundation.Inst.Dimensional_Price.text = (1000 + Malyeog_Upgrade * 200).ToString();
                            Foundation.Inst.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                            break;

                        case 1:
                            Foundation.Inst.Dimensional_Price.text = (1000 + Malyeog_Upgrade * 200).ToString();
                            Foundation.Inst.Explanation.text = "거인의 힘을 받아 공격력이 15% / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                            break;

                        case 2:
                            Foundation.Inst.Dimensional_Price.text = (1000 + Malyeog_Upgrade * 200).ToString();
                            Foundation.Inst.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "30% / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                            break;

                        case 3:
                            Foundation.Inst.Dimensional_Price.text = (1000 + Malyeog_Upgrade * 200).ToString();
                            Foundation.Inst.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "45% / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                            break;

                        case 4:
                            Foundation.Inst.Dimensional_Price.text = "Max";
                            Foundation.Inst.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "60%" + " 상승한다.";
                            break;
                    }
                    break;

                case 2:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Foundation.Inst.Title.text = "강철 피부";
                    MalyeogUp_Text.text = Malyeog_Upgrade + "/4";
                    switch (Malyeog_Upgrade)
                    {
                        case 0:
                            Foundation.Inst.Dimensional_Price.text = (1500 + Malyeog_Upgrade * 250).ToString();
                            Foundation.Inst.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "<color=#877D78>" + "10" + "</color>" + " / " + "<color=#877D78>" + "15" + "</color>" + " / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                            break;

                        case 1:
                            Foundation.Inst.Dimensional_Price.text = (1500 + Malyeog_Upgrade * 250).ToString();
                            Foundation.Inst.Explanation.text = "강철같은 피부를 얻어 방어력이 5 / " + "<color=#877D78>" + "10" + "</color>" + " / " + "<color=#877D78>" + "15" + "</color>" + " / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                            break;

                        case 2:
                            Foundation.Inst.Dimensional_Price.text = (1500 + Malyeog_Upgrade * 250).ToString();
                            Foundation.Inst.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "10 / " + "<color=#877D78>" + "15" + "</color>" + " / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                            break;

                        case 3:
                            Foundation.Inst.Dimensional_Price.text = (1500 + Malyeog_Upgrade * 250).ToString();
                            Foundation.Inst.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "<color=#877D78>" + "10" + "</color>" + " / " + "15 / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                            break;

                        case 4:
                            Foundation.Inst.Dimensional_Price.text = "Max";
                            Foundation.Inst.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "<color=#877D78>" + "10" + "</color>" + " / " + "<color=#877D78>" + "15" + "</color>" + " / " + "20" + " 상승한다.";
                            break;
                    }
                    break;

                case 3:
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Foundation.Inst.Title.text = "마공학 심장";
                    MalyeogUp_Text.text = Malyeog_Upgrade + "/2";
                    switch (Malyeog_Upgrade)
                    {
                        case 0:
                            Foundation.Inst.Dimensional_Price.text = (3000 + Malyeog_Upgrade * 3000).ToString();
                            Foundation.Inst.Explanation.text = "마정석 심장이 두번째 기회를 줘 최대체력의 " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "50%" + "</color>" + " 상승한다.";
                            break;

                        case 1:
                            Foundation.Inst.Dimensional_Price.text = (3000 + Malyeog_Upgrade * 3000).ToString();
                            Foundation.Inst.Explanation.text = "마정석 심장이 두번째 기회를 줘 최대체력의 20% / " + "<color=#877D78>" + "50%" + "</color>" + " 상승한다.";
                            break;

                        case 2:
                            Foundation.Inst.Dimensional_Price.text = "Max";
                            Foundation.Inst.Explanation.text = "마정석 심장이 두번째 기회를 줘 최대체력의 " + "<color=#877D78>" + "20%" + "</color>" + " / " + "50%" + " 상승한다.";
                            break;
                    }
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Foundation.Inst.Price_obj.SetActive(false);
        Foundation.Inst.Close_Btn.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (malyeog == Malyeog.Magic)
        {
            switch (Malyeog_Num)
            {
                case 0:
                    if (GameManager.Instance.crystal >= (700 + Malyeog_Upgrade * 150) && Foundation.Inst.Dimensional_Price.text != "Max")
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        GameManager.Instance.crystal -= (700 + Malyeog_Upgrade * 150);

                        Malyeog_Upgrade++;
                        MalyeogUp_Text.text = Malyeog_Upgrade + "/4";
                        Player.Instance.stat.magicPower.invisibleHand++;
                        switch (Malyeog_Upgrade)
                        {
                            case 1:
                                Foundation.Inst.Dimensional_Price.text = (700 + Malyeog_Upgrade * 150).ToString();
                                Foundation.Inst.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "10% / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                                break;

                            case 2:
                                Foundation.Inst.Dimensional_Price.text = (700 + Malyeog_Upgrade * 150).ToString();
                                Foundation.Inst.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "20% / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                                break;

                            case 3:
                                Foundation.Inst.Dimensional_Price.text = (700 + Malyeog_Upgrade * 150).ToString();
                                Foundation.Inst.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "30% / " + "<color=#877D78>" + "40%" + "</color>" + " 증가한다.";
                                break;

                            case 4:
                                Foundation.Inst.Dimensional_Price.text = "Max";
                                Foundation.Inst.Explanation.text = "보이지 않는 손들이 함께 공격해 공격 속도가 " + "\n" + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "40%" + " 증가한다.";
                                break;
                        }

                        MagicOpen_Check = true;
                        if (MagicOpen_Check == true && Foundation.Inst.Magic_Open == Malyeog_Num)
                            Foundation.Inst.Magic_Open++;
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 1:
                    if (GameManager.Instance.crystal >= (1100 + Malyeog_Upgrade * 200) && Foundation.Inst.Dimensional_Price.text != "Max" && Foundation.Inst.Magic_Open >= Malyeog_Num)
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);


                        GameManager.Instance.crystal -= (1100 + Malyeog_Upgrade * 200);

                        Malyeog_Upgrade++;
                        MalyeogUp_Text.text = Malyeog_Upgrade + "/4";
                        switch (Malyeog_Upgrade)
                        {
                            case 1:
                                Player.Instance.stat.magicPower.sharpEye = 1;
                                Foundation.Inst.Dimensional_Price.text = (1100 + Malyeog_Upgrade * 200).ToString();
                                Foundation.Inst.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "3% / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                                break;

                            case 2:
                                Player.Instance.stat.magicPower.sharpEye = 2;
                                Foundation.Inst.Dimensional_Price.text = (1100 + Malyeog_Upgrade * 200).ToString();
                                Foundation.Inst.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "5% / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                                break;

                            case 3:
                                Player.Instance.stat.magicPower.sharpEye = 3;
                                Foundation.Inst.Dimensional_Price.text = (1100 + Malyeog_Upgrade * 200).ToString();
                                Foundation.Inst.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "7% / " + "<color=#877D78>" + "10%" + "</color>" + " 상승한다.";
                                break;

                            case 4:
                                Player.Instance.stat.magicPower.sharpEye = 4;
                                Foundation.Inst.Dimensional_Price.text = "Max";
                                Foundation.Inst.Explanation.text = "눈으로 상대의 약점을 꿰뚫어봐 치명타 확률이 " + "\n" + "<color=#877D78>" + "3%" + "</color>" + " / " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "7%" + "</color>" + " / " + "10%" + " 상승한다.";
                                break;
                        }

                        MagicOpen_Check = true;
                        if (MagicOpen_Check == true && Foundation.Inst.Magic_Open == Malyeog_Num)
                            Foundation.Inst.Magic_Open++;
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 2:
                    if (GameManager.Instance.crystal >= (1700 + Malyeog_Upgrade * 250) && Foundation.Inst.Dimensional_Price.text != "Max" && Foundation.Inst.Magic_Open >= Malyeog_Num)
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);


                        GameManager.Instance.crystal -= (1700 + Malyeog_Upgrade * 250);

                        Malyeog_Upgrade++;
                        MalyeogUp_Text.text = Malyeog_Upgrade + "/4";
                        Player.Instance.stat.magicPower.timeQuick++;
                        switch (Malyeog_Upgrade)
                        {
                            case 1:
                                Foundation.Inst.Dimensional_Price.text = (1700 + Malyeog_Upgrade * 250).ToString();
                                Foundation.Inst.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "5% / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                                break;

                            case 2:
                                Foundation.Inst.Dimensional_Price.text = (1700 + Malyeog_Upgrade * 250).ToString();
                                Foundation.Inst.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "10% / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                                break;

                            case 3:
                                Foundation.Inst.Dimensional_Price.text = (1700 + Malyeog_Upgrade * 250).ToString();
                                Foundation.Inst.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "15% / " + "<color=#877D78>" + "20%" + "</color>" + " 감소한다.";
                                break;

                            case 4:
                                Foundation.Inst.Dimensional_Price.text = "Max";
                                Foundation.Inst.Explanation.text = "시간을 가속 시켜서 쿨타임이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "15%" + "</color>" + " / " + "20%" + " 감소한다.";
                                break;
                        }

                        MagicOpen_Check = true;
                        if (MagicOpen_Check == true && Foundation.Inst.Magic_Open == Malyeog_Num)
                            Foundation.Inst.Magic_Open++;
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 3:
                    if (GameManager.Instance.crystal >= (3300 + Malyeog_Upgrade * 3000) && Foundation.Inst.Dimensional_Price.text != "Max" && Foundation.Inst.Magic_Open >= Malyeog_Num)
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);


                        GameManager.Instance.crystal -= (3300 + Malyeog_Upgrade * 3000);

                        Malyeog_Upgrade++;
                        MalyeogUp_Text.text = Malyeog_Upgrade + "/2";
                        Player.Instance.stat.magicPower.thaumcraft++;
                        switch (Malyeog_Upgrade)
                        {
                            case 1:
                                Foundation.Inst.Dimensional_Price.text = (3300 + Malyeog_Upgrade * 3000).ToString();
                                Foundation.Inst.Explanation.text = "금단의 연금술을 사용해 획득하는 골드의 양이 " + "5% / " + "<color=#877D78>" + "10%" + "</color>" + " 감소한다.";
                                break;

                            case 2:
                                Foundation.Inst.Dimensional_Price.text = "Max";
                                Foundation.Inst.Explanation.text = "금단의 연금술을 사용해 획득하는 골드의 양이 " + "<color=#877D78>" + "5%" + "</color>" + " / " + "10%" + " 감소한다.";
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
            switch (Malyeog_Num)
            {
                case 0:
                    if (GameManager.Instance.crystal >= (600 + Malyeog_Upgrade * 150) && Foundation.Inst.Dimensional_Price.text != "Max")
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);


                        GameManager.Instance.crystal -= (600 + Malyeog_Upgrade * 150);

                        Malyeog_Upgrade++;
                        MalyeogUp_Text.text = Malyeog_Upgrade + "/4";
                        Player.Instance.stat.magicPower.silpidLeap++;
                        switch (Malyeog_Upgrade)
                        {
                            case 1:
                                Foundation.Inst.Dimensional_Price.text = (600 + Malyeog_Upgrade * 150).ToString();
                                Foundation.Inst.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 10% / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                                break;

                            case 2:
                                Foundation.Inst.Dimensional_Price.text = (600 + Malyeog_Upgrade * 150).ToString();
                                Foundation.Inst.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "20% / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                                break;

                            case 3:
                                Foundation.Inst.Dimensional_Price.text = (600 + Malyeog_Upgrade * 150).ToString();
                                Foundation.Inst.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "30% / " + "<color=#877D78>" + "40%" + "</color>" + " 상승한다.";
                                break;

                            case 4:
                                Foundation.Inst.Dimensional_Price.text = "Max";
                                Foundation.Inst.Explanation.text = "실피드의 도약력으로 대쉬횟수가 1 증가하고 이동 속도가 " + "<color=#877D78>" + "10%" + "</color>" + " / " + "<color=#877D78>" + "20%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "40%" + " 상승한다.";
                                break;
                        }

                        BodyOpen_Check = true;
                        if (BodyOpen_Check == true && Foundation.Inst.Body_Open == Malyeog_Num)
                            Foundation.Inst.Body_Open++;
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 1:
                    if (GameManager.Instance.crystal >= (1000 + Malyeog_Upgrade * 200) && Foundation.Inst.Dimensional_Price.text != "Max" && Foundation.Inst.Body_Open >= Malyeog_Num)
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);


                        GameManager.Instance.crystal -= (1000 + Malyeog_Upgrade * 200);

                        Malyeog_Upgrade++;
                        MalyeogUp_Text.text = Malyeog_Upgrade + "/4";
                        Player.Instance.stat.magicPower.giantPower++;
                        switch (Malyeog_Upgrade)
                        {
                            case 1:
                                Foundation.Inst.Dimensional_Price.text = (1000 + Malyeog_Upgrade * 200).ToString();
                                Foundation.Inst.Explanation.text = "거인의 힘을 받아 공격력이 15% / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                                break;

                            case 2:
                                Foundation.Inst.Dimensional_Price.text = (1000 + Malyeog_Upgrade * 200).ToString();
                                Foundation.Inst.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "30% / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                                break;

                            case 3:
                                Foundation.Inst.Dimensional_Price.text = (1000 + Malyeog_Upgrade * 200).ToString();
                                Foundation.Inst.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "45% / " + "<color=#877D78>" + "60%" + "</color>" + " 상승한다.";
                                break;

                            case 4:
                                Foundation.Inst.Dimensional_Price.text = "Max";
                                Foundation.Inst.Explanation.text = "거인의 힘을 받아 공격력이 " + "<color=#877D78>" + "15%" + "</color>" + " / " + "<color=#877D78>" + "30%" + "</color>" + " / " + "<color=#877D78>" + "45%" + "</color>" + " / " + "60%" + " 상승한다.";
                                break;
                        }

                        BodyOpen_Check = true;
                        if (BodyOpen_Check == true && Foundation.Inst.Body_Open == Malyeog_Num)
                            Foundation.Inst.Body_Open++;
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 2:
                    if (GameManager.Instance.crystal >= (1500 + Malyeog_Upgrade * 250) && Foundation.Inst.Dimensional_Price.text != "Max" && Foundation.Inst.Body_Open >= Malyeog_Num)
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        GameManager.Instance.crystal -= (1500 + Malyeog_Upgrade * 250);

                        Malyeog_Upgrade++;
                        MalyeogUp_Text.text = Malyeog_Upgrade + "/4";
                        Player.Instance.stat.magicPower.ironSkin++;
                        switch (Malyeog_Upgrade)
                        {
                            case 1:
                                Foundation.Inst.Dimensional_Price.text = (1500 + Malyeog_Upgrade * 250).ToString();
                                Foundation.Inst.Explanation.text = "강철같은 피부를 얻어 방어력이 5 / " + "<color=#877D78>" + "10" + "</color>" + " / " + "<color=#877D78>" + "15" + "</color>" + " / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                                break;

                            case 2:
                                Foundation.Inst.Dimensional_Price.text = (1500 + Malyeog_Upgrade * 250).ToString();
                                Foundation.Inst.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "10 / " + "<color=#877D78>" + "15" + "</color>" + " / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                                break;

                            case 3:
                                Foundation.Inst.Dimensional_Price.text = (1500 + Malyeog_Upgrade * 250).ToString();
                                Foundation.Inst.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "<color=#877D78>" + "10" + "</color>" + " / " + "15 / " + "<color=#877D78>" + "20" + "</color>" + " 상승한다.";
                                break;

                            case 4:
                                Foundation.Inst.Dimensional_Price.text = "Max";
                                Foundation.Inst.Explanation.text = "강철같은 피부를 얻어 방어력이 " + "<color=#877D78>" + "5" + "</color>" + " / " + "<color=#877D78>" + "10" + "</color>" + " / " + "<color=#877D78>" + "15" + "</color>" + " / " + "20" + " 상승한다.";
                                break;
                        }

                        BodyOpen_Check = true;
                        if (BodyOpen_Check == true && Foundation.Inst.Body_Open == Malyeog_Num)
                            Foundation.Inst.Body_Open++;
                    }
                    else
                        SoundManager.instance.PlaySoundClip("SFX_Error", SoundType.SFX);
                    break;

                case 3:
                    if (GameManager.Instance.crystal >= (3000 + Malyeog_Upgrade * 3000) && Foundation.Inst.Dimensional_Price.text != "Max" && Foundation.Inst.Body_Open >= Malyeog_Num)
                    {
                        SoundManager.instance.PlaySoundClip("SFX_Enforce", SoundType.SFX);

                        GameManager.Instance.crystal -= (3000 + Malyeog_Upgrade * 3000);

                        Malyeog_Upgrade++;
                        MalyeogUp_Text.text = Malyeog_Upgrade + "/2";
                        switch (Malyeog_Upgrade)
                        {
                            case 1:
                                Player.Instance.stat.magicPower.magicHeart++;
                                Foundation.Inst.Dimensional_Price.text = (3000 + Malyeog_Upgrade * 3000).ToString();
                                Foundation.Inst.Explanation.text = "마정석 심장이 두번째 기회를 줘 최대체력의 20% / " + "<color=#877D78>" + "50%" + "</color>" + " 상승한다.";
                                break;

                            case 2:
                                Player.Instance.stat.magicPower.magicHeart++;
                                Foundation.Inst.Dimensional_Price.text = "Max";
                                Foundation.Inst.Explanation.text = "마정석 심장이 두번째 기회를 줘 최대체력의 " + "<color=#877D78>" + "20%" + "</color>" + " / " + "50%" + " 상승한다.";
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
