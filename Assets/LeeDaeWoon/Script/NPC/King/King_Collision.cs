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
        King.instance.F_Button.gameObject.SetActive(false);
    }

    void Update()
    {
        DialogueBtn_FadeInOut();
        NextDialogue_F();

        if (King.instance.Magic_Creation == true)
            Foundation.Inst.MagicCircle_Rotation();
    }

    public void NextDialogue_F()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            King.instance.F_Button.gameObject.SetActive(false);
            StartCoroutine(NextDialogue());
        }
    }

    public void DialogueBtn_FadeInOut()
    {
        if ((King.instance.Dialogue_Text.text == King.instance.Dialogue[King.instance.Sequence_Text] && Dialogue_End == false))
            King.instance.F_Button.gameObject.SetActive(true);
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
                        if (King.instance.Dialogue_Text.text == King.instance.Dialogue[King.instance.Sequence_Text] && King.instance.Sequence_Text <= 7 && Dialogue_End == false)
                        {
                            if (King.instance.Sequence_Text < 7)
                            {
                                King.instance.Sequence_Text++;
                                King.instance.Dialogue_Text.text = "";
                                King.instance.Dialogue_Text.DOPause();
                                King.instance.Dialogue_Text.DOText(King.instance.Dialogue[King.instance.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                            }

                            else if (King.instance.Sequence_Text == 7)
                            {
                                Range_Reach = false;
                                Dialogue_End = true;
                                King.instance.Dialogue_Exit = true;
                                King.instance.King_NPC.DOFade(0f, 1f);
                                King.instance.Area01_Box.enabled = false;
                                yield return new WaitForSeconds(1.2f);

                                King.instance.King_NPC.transform.DOLocalMoveX(15f, 1f);

                                yield return new WaitForSeconds(1f);
                                King.instance.Sequence_Text++;
                                King.instance.Zoom_Shrinking();
                                King.instance.Dialogue_Text.text = "";
                                King.instance.King_NPC.DOFade(1f, 0.1f);
                            }


                        }

                        else // 전문이 다 출력되기전 F키를 눌렀을 시
                        {
                            // 치고 있던 타이핑은 멈추고, 바로 전문이 완성되도록 한다.
                            King.instance.Dialogue_Text.DOPause();
                            King.instance.Dialogue_Text.DOText(King.instance.Dialogue[King.instance.Sequence_Text], 0, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                        }
                    }
                }
                break;

            case Area.Area_02:
                if (Range_Reach == true)
                {

                    // 전문이 타이핑이 됬을 경우 && 대사가 7번 이하 나왔을 경우 && 대사가 아직 안 끝났을 경우
                    if (King.instance.Dialogue_Text.text == King.instance.Dialogue[King.instance.Sequence_Text] && King.instance.Sequence_Text <= 12 && Dialogue_End == false)
                    {
                        
                        if (King.instance.Sequence_Text < 12)
                        {
                            King.instance.Sequence_Text++;
                            King.instance.Dialogue_Text.text = "";
                            King.instance.Dialogue_Text.DOPause();
                            King.instance.Dialogue_Text.DOText(King.instance.Dialogue[King.instance.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                        }

                        else if (King.instance.Sequence_Text == 12)
                        {
                            Range_Reach = false;
                            Dialogue_End = true;
                            King.instance.Dialogue_Exit = true;
                            King.instance.King_NPC.DOFade(0f, 1f);
                            King.instance.Area02_Box.enabled = false;
                            yield return new WaitForSeconds(1.2f);

                            King.instance.King_NPC.transform.DOLocalMoveX(23f, 1f);

                            yield return new WaitForSeconds(1f);
                            King.instance.Sequence_Text++;
                            King.instance.Zoom_Shrinking();
                            King.instance.Dialogue_Text.text = "";
                            King.instance.King_NPC.DOFade(1f, 0.1f);
                        }
                    }

                    else // 전문이 다 출력되기전 F키를 눌렀을 시
                    {
                        // 치고 있던 타이핑은 멈추고, 바로 전문이 완성되도록 한다.
                        King.instance.Dialogue_Text.DOPause();
                        King.instance.Dialogue_Text.DOText(King.instance.Dialogue[King.instance.Sequence_Text], 0, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    }
                }
                break;

            case Area.Area_03:
                if (Range_Reach == true)
                {

                    // 전문이 타이핑이 됬을 경우 && 대사가 7번 이하 나왔을 경우 && 대사가 아직 안 끝났을 경우
                    if (King.instance.Dialogue_Text.text == King.instance.Dialogue[King.instance.Sequence_Text] && King.instance.Sequence_Text <= 18 && Dialogue_End == false)
                    {
                        if (King.instance.Sequence_Text < 18)
                        {
                            King.instance.Sequence_Text++;
                            King.instance.Dialogue_Text.text = "";
                            King.instance.Dialogue_Text.DOPause();
                            King.instance.Dialogue_Text.DOText(King.instance.Dialogue[King.instance.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                        }

                        else if (King.instance.Sequence_Text == 18)
                        {
                            Range_Reach = false;
                            Dialogue_End = true;
                            King.instance.Dialogue_Exit = true;
                            King.instance.King_NPC.DOFade(0f, 1f);
                            King.instance.Area03_Box.enabled = false;
                            yield return new WaitForSeconds(1.2f);

                            King.instance.King_NPC.transform.DOLocalMoveX(32f, 1f);

                            yield return new WaitForSeconds(1f);
                            King.instance.Sequence_Text++;
                            King.instance.Zoom_Shrinking();
                            King.instance.Dialogue_Text.text = "";
                            King.instance.King_NPC.DOFade(1f, 0.1f);
                        }
                    }

                    else // 전문이 다 출력되기전 F키를 눌렀을 시
                    {
                        // 치고 있던 타이핑은 멈추고, 바로 전문이 완성되도록 한다.
                        King.instance.Dialogue_Text.DOPause();
                        King.instance.Dialogue_Text.DOText(King.instance.Dialogue[King.instance.Sequence_Text], 0, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    }
                }
                break;

            case Area.Area_04:
                if (Range_Reach == true)
                {

                    // 전문이 타이핑이 됬을 경우 && 대사가 7번 이하 나왔을 경우 && 대사가 아직 안 끝났을 경우
                    if (King.instance.Dialogue_Text.text == King.instance.Dialogue[King.instance.Sequence_Text] && King.instance.Sequence_Text <= 23 && Dialogue_End == false)
                    {
                        if (King.instance.Sequence_Text < 23)
                        {
                            King.instance.Sequence_Text++;
                            King.instance.Dialogue_Text.text = "";
                            King.instance.Dialogue_Text.DOPause();
                            King.instance.Dialogue_Text.DOText(King.instance.Dialogue[King.instance.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                        }

                        else if (King.instance.Sequence_Text == 23)
                        {
                            Range_Reach = false;
                            Dialogue_End = true;
                            King.instance.Dialogue_Exit = true;
                            King.instance.King_NPC.DOFade(0f, 1f);
                            King.instance.Area04_Box.enabled = false;

                            yield return new WaitForSeconds(1f);
                            King.instance.Magic_Creation = true;
                            King.instance.Zoom_Shrinking();
                            UI_Manager.instance.King_Check = true;
                        }
                    }

                    else // 전문이 다 출력되기전 F키를 눌렀을 시
                    {
                        // 치고 있던 타이핑은 멈추고, 바로 전문이 완성되도록 한다.
                        King.instance.Dialogue_Text.DOPause();
                        King.instance.Dialogue_Text.DOText(King.instance.Dialogue[King.instance.Sequence_Text], 0, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    }


                }
                break;
        }
    }


    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ITypePlayer>() != null && UI_Manager.instance.King_Check == false)
        {
            King.instance.Zoom_Expansion(); // 카메라 확대 시킨다.
            switch (area)
            {
                case Area.Area_01:
                    Range_Reach = true;
                    King.instance.Camera_obj.GetComponent<CameraManager>().enabled = false;
                    King.instance.Camera_obj.transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(0.5f);

                    King.instance.Dialogue_Text.DOText(King.instance.Dialogue[King.instance.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    break;

                case Area.Area_02:
                    Range_Reach = true;
                    King.instance.Camera_obj.GetComponent<CameraManager>().enabled = false;
                    King.instance.Camera_obj.transform.DOLocalMoveX(9, 0.5f).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(0.5f);

                    King.instance.Dialogue_Text.DOText(King.instance.Dialogue[King.instance.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    break;

                case Area.Area_03:
                    Range_Reach = true;
                    King.instance.Camera_obj.GetComponent<CameraManager>().enabled = false;
                    King.instance.Camera_obj.transform.DOLocalMoveX(17.82f, 0.5f).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(0.5f);

                    King.instance.Dialogue_Text.DOText(King.instance.Dialogue[King.instance.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    break;

                case Area.Area_04:
                    Range_Reach = true;
                    King.instance.Camera_obj.GetComponent<CameraManager>().enabled = false;
                    King.instance.Camera_obj.transform.DOLocalMoveX(26.76f, 0.5f).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(0.5f);

                    King.instance.Dialogue_Text.DOText(King.instance.Dialogue[King.instance.Sequence_Text], 3f, richTextEnabled = true, scrambleMode = ScrambleMode.None, scrambleChars_Tool = null);
                    break;
            }
        }
    }
}
