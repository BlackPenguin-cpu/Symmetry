using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class King_Collision : MonoBehaviour
{
    public enum Area
    {
        Area_01,
        Area_02,
        Area_03,
        Area_04,
    }
    public Area area;

    [Header("텍스트 연출")]
    public string scrambleChars_Tool;
    public bool richTextEnabled;
    public ScrambleMode scrambleMode;

    bool Range_Reach;
    public bool Dialogue_End;

    void Start()
    {
        King.Inst.F_Button.gameObject.SetActive(false);
    }

    void Update()
    {
        DialogueBtn_FadeInOut();
        NextDialogue_F();

        if (King.Inst.Magic_Creation == true)
            Foundation.Inst.MagicCircle_Rotation();
    }

    public void NextDialogue_F()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            King.Inst.F_Button.gameObject.SetActive(false);
            StartCoroutine(NextDialogue());
        }
    }

    public void DialogueBtn_FadeInOut()
    {
        if ((King.Inst.Dialogue_Text.text == King.Inst.Dialogue[King.Inst.Sequence_Text] && Dialogue_End == false))
            King.Inst.F_Button.gameObject.SetActive(true);
    }

    public IEnumerator NextDialogue()
    {
        switch (area)
        {
            case Area.Area_01:
                {
                    if (Range_Reach == true)
                    {
                        // 전문이 타이핑이 됬을 경우 && 대사가 7번 이하 나왔을 경우 && 대사가 아직 안 끝났을 경우
                        if (King.Inst.Dialogue_Text.text == King.Inst.Dialogue[King.Inst.Sequence_Text] && King.Inst.Sequence_Text <= 7 && Dialogue_End == false)
                        {
                            if (King.Inst.Sequence_Text < 7)
                            {
                                King.Inst.Sequence_Text++;
                                King.Inst.Dialogue_Text.text = "";
                                King.Inst.Dialogue_Text.DOPause();
                                King.Inst.Dialogue_Text.DOText(King.Inst.Dialogue[King.Inst.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                            }

                            else if (King.Inst.Sequence_Text == 7)
                            {
                                Range_Reach = false;
                                Dialogue_End = true;
                                King.Inst.Dialogue_Exit = true;
                                King.Inst.King_NPC.DOFade(0f, 1f);
                                King.Inst.Area01_Box.enabled = false;
                                yield return new WaitForSeconds(1.2f);

                                King.Inst.King_NPC.transform.DOLocalMoveX(15f, 1f);

                                yield return new WaitForSeconds(1f);
                                King.Inst.Sequence_Text++;
                                King.Inst.Zoom_Shrinking();
                                King.Inst.Dialogue_Text.text = "";
                                King.Inst.King_NPC.DOFade(1f, 0.1f);
                            }


                        }

                        else // 전문이 다 출력되기전 F키를 눌렀을 시
                        {
                            // 치고 있던 타이핑은 멈추고, 바로 전문이 완성되도록 한다.
                            King.Inst.Dialogue_Text.DOPause();
                            King.Inst.Dialogue_Text.DOText(King.Inst.Dialogue[King.Inst.Sequence_Text], 0, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                        }
                    }
                }
                break;

            case Area.Area_02:
                if (Range_Reach == true)
                {

                    // 전문이 타이핑이 됬을 경우 && 대사가 7번 이하 나왔을 경우 && 대사가 아직 안 끝났을 경우
                    if (King.Inst.Dialogue_Text.text == King.Inst.Dialogue[King.Inst.Sequence_Text] && King.Inst.Sequence_Text <= 12 && Dialogue_End == false)
                    {
                        
                        if (King.Inst.Sequence_Text < 12)
                        {
                            King.Inst.Sequence_Text++;
                            King.Inst.Dialogue_Text.text = "";
                            King.Inst.Dialogue_Text.DOPause();
                            King.Inst.Dialogue_Text.DOText(King.Inst.Dialogue[King.Inst.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                        }

                        else if (King.Inst.Sequence_Text == 12)
                        {
                            Range_Reach = false;
                            Dialogue_End = true;
                            King.Inst.Dialogue_Exit = true;
                            King.Inst.King_NPC.DOFade(0f, 1f);
                            King.Inst.Area02_Box.enabled = false;
                            yield return new WaitForSeconds(1.2f);

                            King.Inst.King_NPC.transform.DOLocalMoveX(23f, 1f);

                            yield return new WaitForSeconds(1f);
                            King.Inst.Sequence_Text++;
                            King.Inst.Zoom_Shrinking();
                            King.Inst.Dialogue_Text.text = "";
                            King.Inst.King_NPC.DOFade(1f, 0.1f);
                        }
                    }

                    else // 전문이 다 출력되기전 F키를 눌렀을 시
                    {
                        // 치고 있던 타이핑은 멈추고, 바로 전문이 완성되도록 한다.
                        King.Inst.Dialogue_Text.DOPause();
                        King.Inst.Dialogue_Text.DOText(King.Inst.Dialogue[King.Inst.Sequence_Text], 0, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    }
                }
                break;

            case Area.Area_03:
                if (Range_Reach == true)
                {

                    // 전문이 타이핑이 됬을 경우 && 대사가 7번 이하 나왔을 경우 && 대사가 아직 안 끝났을 경우
                    if (King.Inst.Dialogue_Text.text == King.Inst.Dialogue[King.Inst.Sequence_Text] && King.Inst.Sequence_Text <= 18 && Dialogue_End == false)
                    {
                        if (King.Inst.Sequence_Text < 18)
                        {
                            King.Inst.Sequence_Text++;
                            King.Inst.Dialogue_Text.text = "";
                            King.Inst.Dialogue_Text.DOPause();
                            King.Inst.Dialogue_Text.DOText(King.Inst.Dialogue[King.Inst.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                        }

                        else if (King.Inst.Sequence_Text == 18)
                        {
                            Range_Reach = false;
                            Dialogue_End = true;
                            King.Inst.Dialogue_Exit = true;
                            King.Inst.King_NPC.DOFade(0f, 1f);
                            King.Inst.Area03_Box.enabled = false;
                            yield return new WaitForSeconds(1.2f);

                            King.Inst.King_NPC.transform.DOLocalMoveX(32f, 1f);

                            yield return new WaitForSeconds(1f);
                            King.Inst.Sequence_Text++;
                            King.Inst.Zoom_Shrinking();
                            King.Inst.Dialogue_Text.text = "";
                            King.Inst.King_NPC.DOFade(1f, 0.1f);
                        }
                    }

                    else // 전문이 다 출력되기전 F키를 눌렀을 시
                    {
                        // 치고 있던 타이핑은 멈추고, 바로 전문이 완성되도록 한다.
                        King.Inst.Dialogue_Text.DOPause();
                        King.Inst.Dialogue_Text.DOText(King.Inst.Dialogue[King.Inst.Sequence_Text], 0, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    }
                }
                break;

            case Area.Area_04:
                if (Range_Reach == true)
                {

                    // 전문이 타이핑이 됬을 경우 && 대사가 7번 이하 나왔을 경우 && 대사가 아직 안 끝났을 경우
                    if (King.Inst.Dialogue_Text.text == King.Inst.Dialogue[King.Inst.Sequence_Text] && King.Inst.Sequence_Text <= 23 && Dialogue_End == false)
                    {
                        if (King.Inst.Sequence_Text < 23)
                        {
                            King.Inst.Sequence_Text++;
                            King.Inst.Dialogue_Text.text = "";
                            King.Inst.Dialogue_Text.DOPause();
                            King.Inst.Dialogue_Text.DOText(King.Inst.Dialogue[King.Inst.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                        }

                        else if (King.Inst.Sequence_Text == 23)
                        {
                            Range_Reach = false;
                            Dialogue_End = true;
                            King.Inst.Dialogue_Exit = true;
                            King.Inst.King_NPC.DOFade(0f, 1f);
                            King.Inst.Area04_Box.enabled = false;

                            yield return new WaitForSeconds(1f);
                            King.Inst.Magic_Creation = true;
                            King.Inst.Zoom_Shrinking();
                            UI_Manager.instance.King_Check = true;
                        }
                    }

                    else // 전문이 다 출력되기전 F키를 눌렀을 시
                    {
                        // 치고 있던 타이핑은 멈추고, 바로 전문이 완성되도록 한다.
                        King.Inst.Dialogue_Text.DOPause();
                        King.Inst.Dialogue_Text.DOText(King.Inst.Dialogue[King.Inst.Sequence_Text], 0, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    }


                }
                break;
        }
    }


    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ITypePlayer>() != null && UI_Manager.instance.King_Check == false)
        {
            King.Inst.Zoom_Expansion(); // 카메라 확대 시킨다.
            switch (area)
            {
                case Area.Area_01:
                    Range_Reach = true;
                    King.Inst.Camera_obj.GetComponent<CameraManager>().enabled = false;
                    King.Inst.Camera_obj.transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(0.5f);

                    King.Inst.Dialogue_Text.DOText(King.Inst.Dialogue[King.Inst.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    break;

                case Area.Area_02:
                    Range_Reach = true;
                    King.Inst.Camera_obj.GetComponent<CameraManager>().enabled = false;
                    King.Inst.Camera_obj.transform.DOLocalMoveX(9, 0.5f).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(0.5f);

                    King.Inst.Dialogue_Text.DOText(King.Inst.Dialogue[King.Inst.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    break;

                case Area.Area_03:
                    Range_Reach = true;
                    King.Inst.Camera_obj.GetComponent<CameraManager>().enabled = false;
                    King.Inst.Camera_obj.transform.DOLocalMoveX(17.82f, 0.5f).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(0.5f);

                    King.Inst.Dialogue_Text.DOText(King.Inst.Dialogue[King.Inst.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    break;

                case Area.Area_04:
                    Range_Reach = true;
                    King.Inst.Camera_obj.GetComponent<CameraManager>().enabled = false;
                    King.Inst.Camera_obj.transform.DOLocalMoveX(26.76f, 0.5f).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(0.5f);

                    King.Inst.Dialogue_Text.DOText(King.Inst.Dialogue[King.Inst.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    break;
            }
        }
    }
}
