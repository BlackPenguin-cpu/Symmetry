using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ItemCard_Mouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum Direction
    {
        Left,
        Among,
        Right
    }

    public Direction direction;
    float timer = 0;

    [Header("확인")]
    int LeftClick_Check = 1;
    bool LeftCloseWindow_Check = true;

    int AmongClick_Check = 1;
    bool AmongCloseWindow_Check = true;

    int RightClick_Check = 1;
    bool RightCloseWindow_Check = true;

    [Header("빛")]
    public Image Left_Light;
    public Image Among_Light;
    public Image Right_Light;

    [Header("아이템 창")]
    public GameObject Left_Window;
    public GameObject Among_Window;
    public GameObject Right_Window;

    [Header("아이템 봉 / 배경")]
    //왼쪽
    public GameObject Left_Pole_01;
    public GameObject Left_Pole_02;
    public RectTransform Left_Rect;

    // 가운데
    public GameObject Among_Pole_01;
    public GameObject Among_Pole_02;
    public RectTransform Among_Rect;

    // 오른쪽
    public GameObject Right_Pole_01;
    public GameObject Right_Pole_02;
    public RectTransform Right_Rect;

    void Start()
    {
        Card_Manager.instance.Left_Pick = true;
        Card_Manager.instance.Among_Pick = true;
        Card_Manager.instance.Right_Pick = true;
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (direction)
        {
            case Direction.Left:
                if (Card_Manager.instance.Among_Pick == true && Card_Manager.instance.Right_Pick == true && Card_Manager.instance.ItemCard_OpenCheck == false)
                {
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Left_Light.DOFade(1f, 0.5f);
                }
                break;

            case Direction.Among:
                if (Card_Manager.instance.Left_Pick == true && Card_Manager.instance.Right_Pick == true && Card_Manager.instance.ItemCard_OpenCheck == false)
                {
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Among_Light.DOFade(1f, 0.5f);
                }
                break;

            case Direction.Right:
                if (Card_Manager.instance.Left_Pick == true && Card_Manager.instance.Among_Pick == true && Card_Manager.instance.ItemCard_OpenCheck == false)
                {
                    SoundManager.instance.PlaySoundClip("SFX_Button_Over", SoundType.SFX);
                    Right_Light.DOFade(1f, 0.5f);
                }
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (direction == Direction.Left)
            Left_Light.DOFade(0f, 0.5f);

        if (direction == Direction.Among)
            Among_Light.DOFade(0f, 0.5f);

        if (direction == Direction.Right)
            Right_Light.DOFade(0f, 0.5f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 왼쪽 카드를 선택 했을 때
        if (direction == Direction.Left)
        {

            Card_Manager.instance.Fade.DOFade(0f, 0.5f);
            UI_Manager.instance.isCursorFade = false;

            if (Card_Manager.instance.Left_Pick == true && Card_Manager.instance.ItemCard_OpenCheck == false)
            {
                Card_Manager.instance.Left_Pick = false;
                Card_Manager.instance.Right_Pick = false;
                Card_Manager.instance.Among_Pick = false;

                // 방어구 및 장신구를 선택했을 때
                if (Card_Manager.instance.DA_Left == false)
                {
                    for (int i = 0; i < Card_Manager.instance.DABuffer.Count; i++)
                    {
                        if (Card_Manager.instance.DABuffer[i].Itme_Name == Card_Manager.instance.ItemDA_LeftCheck[0].Itme_Name)
                        {
                            Stop_Manager.Inst.ItemDA_Have.Add(Card_Manager.instance.DABuffer[i]);
                            Card_Manager.instance.DABuffer.RemoveAt(i);
                        }
                    }

                    switch (Card_Manager.instance.ItemDA_LeftCheck[0].Itme_Name)
                    {
                        case "바람의 귀걸이":
                            Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
                            break;

                        case "가시견갑":
                            Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
                            break;

                        case "칼날망토":
                            Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
                            break;

                        case "저주받은 단검":
                            Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
                            break;

                        case "피의 장갑":
                            Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
                            break;

                        case "수정구":
                            Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
                            break;

                        case "절대반지":
                            Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
                            break;

                    }

                }

                // 마정석을 선택했을 때
                if (Card_Manager.instance.DA_Left == true)
                {
                    switch (Card_Manager.instance.ItemDA_LeftCheck[0].Itme_Name)
                    {
                        case "힘의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.POWER]++;
                            break;

                        case "신속의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.SPEED]++;
                            break;

                        case "연속의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED]++;
                            break;

                        case "체력의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH]++;
                            break;

                        case "시간의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.TIME]++;
                            break;

                        case "방어의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE]++;
                            break;
                    }
                }


                if (Card_Manager.instance.Item_Left == false)
                    Stop_Manager.Inst.ItemDA_Have.Add(Card_Manager.instance.ItemDA_LeftCheck[0]);

                if (Card_Manager.instance.Item_bool == true)
                    Card_Manager.instance.Item_bool = false;

                else if (Card_Manager.instance.Item_bool == false)
                    Card_Manager.instance.Item_Check += LeftClick_Check;


                if (Card_Manager.instance.TimeItem_Count <= 2)
                {
                    Card_Manager.instance.Time_Item_Limit.Add(Card_Manager.instance.ItemDA_LeftCheck[0]);
                    for (int i = 0; i < Card_Manager.instance.Time_Item_Limit.Count; i++)
                    {
                        if (Card_Manager.instance.Time_Item_Limit[i].Itme_Name.Contains(Card_Manager.instance.ItemBuffer[4].Itme_Name))
                        {
                            Card_Manager.instance.TimeItem_Count++;
                            Card_Manager.instance.Time_Item_Limit.Clear();
                        }
                    }
                }

                Left_Light.DOFade(1f, 0.1f);
                Left_Window.transform.DOLocalMoveY(1100, 0.5f).SetEase(Ease.InQuad);
                StartCoroutine(Close_Dot());
            }
        }

        // 가운데 카드를 선택 했을 때
        if (direction == Direction.Among)
        {
            Card_Manager.instance.Fade.DOFade(0f, 0.5f);
            UI_Manager.instance.isCursorFade = false;

            if (Card_Manager.instance.Among_Pick == true && Card_Manager.instance.ItemCard_OpenCheck == false)
            {
                Card_Manager.instance.Among_Pick = false;
                Card_Manager.instance.Left_Pick = false;
                Card_Manager.instance.Right_Pick = false;

                // 방어구 및 장신구를 선택했을 때
                if (Card_Manager.instance.DA_Among == false)
                {
                    for (int i = 0; i < Card_Manager.instance.DABuffer.Count; i++)
                    {
                        if (Card_Manager.instance.DABuffer[i].Itme_Name == Card_Manager.instance.ItemDA_AmongCheck[0].Itme_Name)
                        {
                            Stop_Manager.Inst.ItemDA_Have.Add(Card_Manager.instance.DABuffer[i]);
                            Card_Manager.instance.DABuffer.RemoveAt(i);
                        }
                    }

                    switch (Card_Manager.instance.ItemDA_AmongCheck[0].Itme_Name)
                    {
                        case "바람의 귀걸이":
                            Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
                            break;

                        case "가시견갑":
                            Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
                            break;

                        case "칼날망토":
                            Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
                            break;

                        case "저주받은 단검":
                            Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
                            break;

                        case "피의 장갑":
                            Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
                            break;

                        case "수정구":
                            Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
                            break;

                        case "절대반지":
                            Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
                            break;

                    }
                }

                // 마정석을 선택했을 때
                if (Card_Manager.instance.DA_Among == true)
                {
                    switch (Card_Manager.instance.ItemDA_AmongCheck[0].Itme_Name)
                    {
                        case "힘의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.POWER]++;
                            break;

                        case "신속의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.SPEED]++;
                            break;

                        case "연속의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED]++;
                            break;

                        case "체력의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH]++;
                            break;

                        case "시간의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.TIME]++;
                            break;

                        case "방어의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE]++;
                            break;
                    }
                }


                if (Card_Manager.instance.Item_Among == false)
                    Stop_Manager.Inst.ItemDA_Have.Add(Card_Manager.instance.ItemDA_AmongCheck[0]);

                if (Card_Manager.instance.Item_bool == true)
                    Card_Manager.instance.Item_bool = false;

                else if (Card_Manager.instance.Item_bool == false)
                    Card_Manager.instance.Item_Check += AmongClick_Check;

                if (Card_Manager.instance.TimeItem_Count <= 2)
                {
                    Card_Manager.instance.Time_Item_Limit.Add(Card_Manager.instance.ItemDA_AmongCheck[0]);
                    for (int i = 0; i < Card_Manager.instance.Time_Item_Limit.Count; i++)
                    {
                        if (Card_Manager.instance.Time_Item_Limit[i].Itme_Name.Contains(Card_Manager.instance.ItemBuffer[4].Itme_Name))
                        {
                            Card_Manager.instance.TimeItem_Count++;
                            Card_Manager.instance.Time_Item_Limit.Clear();
                        }
                    }
                }

                Among_Light.DOFade(1f, 0.1f);
                Among_Window.transform.DOLocalMoveY(1150, 0.5f).SetEase(Ease.InQuad);
                StartCoroutine(Close_Dot());
            }

        }

        // 오른쪽 카드를 선택 했을 때
        if (direction == Direction.Right)
        {
            Card_Manager.instance.Fade.DOFade(0f, 0.5f);
            UI_Manager.instance.isCursorFade = false;

            if (Card_Manager.instance.Right_Pick == true && Card_Manager.instance.ItemCard_OpenCheck == false)
            {
                Card_Manager.instance.Right_Pick = false;
                Card_Manager.instance.Left_Pick = false;
                Card_Manager.instance.Among_Pick = false;

                // 방어구 및 장신구를 선택했을 때
                if (Card_Manager.instance.DA_Right == false)
                {
                    for (int i = 0; i < Card_Manager.instance.DABuffer.Count; i++)
                    {
                        if (Card_Manager.instance.DABuffer[i].Itme_Name == Card_Manager.instance.ItemDA_RightCheck[0].Itme_Name)
                        {
                            Stop_Manager.Inst.ItemDA_Have.Add(Card_Manager.instance.DABuffer[i]);
                            Card_Manager.instance.DABuffer.RemoveAt(i);
                        }
                    }

                    switch (Card_Manager.instance.ItemDA_RightCheck[0].Itme_Name)
                    {
                        case "바람의 귀걸이":
                            Player.Instance.stat.PlayerDATypeList.WindEarRing = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.WindEarRing);
                            break;

                        case "가시견갑":
                            Player.Instance.stat.PlayerDATypeList.NeedleArmour = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.NeedleArmour);
                            break;

                        case "칼날망토":
                            Player.Instance.stat.PlayerDATypeList.KnifeCape = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.KnifeCape);
                            break;

                        case "저주받은 단검":
                            Player.Instance.stat.PlayerDATypeList.CurseKnife = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.CurseKnife);
                            break;

                        case "피의 장갑":
                            Player.Instance.stat.PlayerDATypeList.BloodGauntlet = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.BloodGauntlet);
                            break;

                        case "수정구":
                            Player.Instance.stat.PlayerDATypeList.CrystalOrb = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.CrystalOrb);
                            break;

                        case "절대반지":
                            Player.Instance.stat.PlayerDATypeList.TheOneRing = true;
                            Debug.Log(Player.Instance.stat.PlayerDATypeList.TheOneRing);
                            break;

                    }
                }

                // 마정석을 선택했을 때
                if (Card_Manager.instance.DA_Right == true)
                {
                    switch (Card_Manager.instance.ItemDA_RightCheck[0].Itme_Name)
                    {
                        case "힘의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.POWER]++;
                            break;

                        case "신속의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.SPEED]++;
                            break;

                        case "연속의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.ATTACKSPEED]++;
                            break;

                        case "체력의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.HEALTH]++;
                            break;

                        case "시간의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.TIME]++;
                            break;

                        case "방어의 마정석":
                            Player.Instance.stat.Crystals[(int)CrystalsType.DEFFENCE]++;
                            break;
                    }
                }

                if (Card_Manager.instance.Item_Right == false)
                    Stop_Manager.Inst.ItemDA_Have.Add(Card_Manager.instance.ItemDA_RightCheck[0]);

                if (Card_Manager.instance.Item_bool == true)
                    Card_Manager.instance.Item_bool = false;

                else if (Card_Manager.instance.Item_bool == false)
                    Card_Manager.instance.Item_Check += RightClick_Check;

                if (Card_Manager.instance.TimeItem_Count <= 2)
                {
                    Card_Manager.instance.Time_Item_Limit.Add(Card_Manager.instance.ItemDA_RightCheck[0]);
                    for (int i = 0; i < Card_Manager.instance.Time_Item_Limit.Count; i++)
                    {
                        if (Card_Manager.instance.Time_Item_Limit[i].Itme_Name.Contains(Card_Manager.instance.ItemBuffer[4].Itme_Name))
                        {
                            Card_Manager.instance.TimeItem_Count++;
                            Card_Manager.instance.Time_Item_Limit.Clear();
                        }
                    }
                }

                Right_Light.DOFade(1f, 0.1f);
                Right_Window.transform.DOLocalMoveY(1150, 0.5f).SetEase(Ease.InQuad);
                StartCoroutine(Close_Dot());
            }
        }
    }

    public IEnumerator Close_Dot()
    {
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);

        timer = 0;
        if (direction == Direction.Left)
        {
            Among_Pole_01.transform.DOLocalMoveY(-53f, 0.5f);
            Among_Pole_02.transform.DOLocalMoveY(-125f, 0.5f);

            Right_Pole_01.transform.DOLocalMoveY(-32f, 0.5f);
            Right_Pole_02.transform.DOLocalMoveY(-108f, 0.5f);

            while (timer < 1)
            {
                Among_Rect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                Right_Rect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                timer += Time.deltaTime * 3f;
                yield return null;
            }

            LeftCloseWindow_Check = false;
            if (LeftCloseWindow_Check == false)
            {
                yield return new WaitForSeconds(0.2f);
                DOTween.PauseAll();
                Destroy(GameObject.Find("Item_Window(Clone)"));
            }
            UI_Manager.instance.timerCheck = true;

            WaveManager.Instance.StartCoroutine(WaveManager.Instance.WaveProcessing(WaveManager.Instance.m_WaveNum - 1));

            if (WaveManager.Instance.m_WaveNum == 3 || WaveManager.Instance.m_WaveNum == 5)
                Potal.Inst.Potal_M();

            Skill_Manager.instance.Instantiate_SkillCheck = false;
        }

        if (direction == Direction.Among)
        {
            Left_Pole_01.transform.DOLocalMoveY(50f, 0.5f);
            Left_Pole_02.transform.DOLocalMoveY(-26f, 0.5f);

            Right_Pole_01.transform.DOLocalMoveY(-32f, 0.5f);
            Right_Pole_02.transform.DOLocalMoveY(-108f, 0.5f);

            while (timer < 1)
            {
                Left_Rect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                Right_Rect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                timer += Time.deltaTime * 3f;
                yield return null;
            }

            AmongCloseWindow_Check = false;
            if (AmongCloseWindow_Check == false)
            {
                yield return new WaitForSeconds(0.2f);
                DOTween.PauseAll();
                Destroy(GameObject.Find("Item_Window(Clone)"));
            }
            UI_Manager.instance.timerCheck = true;

            WaveManager.Instance.StartCoroutine(WaveManager.Instance.WaveProcessing(WaveManager.Instance.m_WaveNum - 1));

            if (WaveManager.Instance.m_WaveNum == 3 || WaveManager.Instance.m_WaveNum == 5)
                Potal.Inst.Potal_M();

            Skill_Manager.instance.Instantiate_SkillCheck = false;
        }

        if (direction == Direction.Right)
        {
            Left_Pole_01.transform.DOLocalMoveY(50f, 0.5f);
            Left_Pole_02.transform.DOLocalMoveY(-26f, 0.5f);

            Among_Pole_01.transform.DOLocalMoveY(-53f, 0.5f);
            Among_Pole_02.transform.DOLocalMoveY(-125f, 0.5f);

            while (timer < 1)
            {
                Left_Rect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                Among_Rect.sizeDelta = new Vector2(522.6044f, Mathf.Lerp(824.77f, -20, timer));
                timer += Time.deltaTime * 3f;
                yield return null;
            }

            RightCloseWindow_Check = false;
            if (RightCloseWindow_Check == false)
            {
                yield return new WaitForSeconds(0.2f);
                DOTween.PauseAll();
                Destroy(GameObject.Find("Item_Window(Clone)"));
            }
            UI_Manager.instance.timerCheck = true;

            //TODO: 응애
            WaveManager.Instance.StartCoroutine(WaveManager.Instance.WaveProcessing(WaveManager.Instance.m_WaveNum - 1));

            if (WaveManager.Instance.m_WaveNum == 3 || WaveManager.Instance.m_WaveNum == 5)
                Potal.Inst.Potal_M();

            Skill_Manager.instance.Instantiate_SkillCheck = false;
        }
    }
}
