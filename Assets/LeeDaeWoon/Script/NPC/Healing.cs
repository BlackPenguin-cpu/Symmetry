using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healing : MonoBehaviour
{
    [Header("신성 창 좌표")]
    [SerializeField] RectTransform Healing_Window_Position;

    [Header("신성 창 시간")]
    public float Timer = 0;

    [Header("신성 창")]
    public GameObject Healing_Window;
    public RectTransform Pole_02;
    public RectTransform Healing_RectTransform;
    public Text Healing_Gold_Text;

    [Header("힐 이펙트")]
    public GameObject Player_Position;
    public GameObject Healing_Effect;

    public int Healing_Gold = 0;
    public float Heal = 0;

    bool Healing_Colider_Check = true;
    bool Healing_Purchase_Check = true;


    void Start()
    {
        SoundManager.instance.PlaySoundClip("BGM_Store", SoundType.BGM);

        Healing_Window.SetActive(false);
        Healing_Price();

        Player_Position = GameObject.Find("Player");
    }

    void Update()
    {
        ScreentoWorld();
        StartCoroutine(Healing_Purchase());
    }

    void ScreentoWorld()
    {
        // 월드 좌표를 스크린 좌표로 변경을 해준다.
        Healing_Window_Position.localPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.localPosition + new Vector3(2.6f, -5f, 0));
    }

    public void Healing_Price()
    {
        // 정상 웨이브 : 5 / 10 / 15
        switch(WaveManager.Instance.m_WaveNum)
        {
            case 3:
                Healing_Gold = 560;
                Heal = 50f;
                break;

            case 5:
                Healing_Gold = 1230;
                Heal = 80f;
                break;

            case 15:
                Healing_Gold = 2116;
                Heal = 120f;
                break;
        }
        Healing_Gold_Text.text = Healing_Gold.ToString();
    }

    IEnumerator Healing_Purchase()
    {
        if (Input.GetKeyDown(KeyCode.F) && Healing_Colider_Check == false && GameManager.Instance._coin >= Healing_Gold && Healing_Purchase_Check == true)
        {
            SoundManager.instance.PlaySoundClip("SFX_God_healling", SoundType.SFX);

            GameManager.Instance._coin -= Healing_Gold;
            UI_Manager.Inst.PlayerMove_control = true;
            StartCoroutine(HealingWindow_Close_Coroutine());

            StartCoroutine(HealingEffect());
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);

            if (Player.Instance.stat._hp + Heal <= Player.Instance.stat._maxHp)
                Player.Instance.stat._hp += Heal;
            else
                Player.Instance.stat._hp = Player.Instance.stat._maxHp;

            Healing_Purchase_Check = false;

        }
    }

    IEnumerator HealingEffect()
    {
        Healing_Effect.SetActive(true);
        Healing_Effect.transform.localPosition = new Vector3(Player_Position.transform.localPosition.x, -0.18f, 0f);
        yield return new WaitForSeconds(2f);
        Healing_Effect.SetActive(false);
        UI_Manager.Inst.PlayerMove_control = false;
    }

    #region 신성 창
    IEnumerator HealingWindow_Coroutine()
    {
        Timer = 0;
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);
        while (Timer < 1)
        {
            Healing_RectTransform.localPosition = new Vector2(-1.995371f, Mathf.Lerp(-22.25f, -244f, Timer));
            Healing_RectTransform.sizeDelta = new Vector2(690f, Mathf.Lerp(0, 486f, Timer));
            Pole_02.localPosition = new Vector2(1.9954f, Mathf.Lerp(-70f, -382f, Timer));
            Timer += Time.deltaTime * 4f;
            yield return null;
        }
    }

    IEnumerator HealingWindow_Close_Coroutine()
    {
        Timer = 0;
        SoundManager.instance.PlaySoundClip("SFX_Window", SoundType.SFX);
        while (Timer < 1)
        {
            Healing_RectTransform.anchoredPosition = new Vector2(-1.995371f, Mathf.Lerp(-244f, -22.25f, Timer));
            Healing_RectTransform.sizeDelta = new Vector2(690f, Mathf.Lerp(486f, 0, Timer));
            Pole_02.anchoredPosition = new Vector2(1.9954f, Mathf.Lerp(-382f, -70f, Timer));
            Timer += Time.deltaTime * 4f;
            yield return null;
        }

        Healing_Window.SetActive(false);
    }
    #endregion

    #region 충돌체크
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null) && Healing_Colider_Check == true && Healing_Purchase_Check == true)
        {
            Healing_Colider_Check = false;
            Healing_Window.SetActive(true);
            StartCoroutine(HealingWindow_Coroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.GetComponent<ITypePlayer>() != null) && Healing_Colider_Check == false && Healing_Purchase_Check == true)
        {
            Healing_Colider_Check = true;
            StartCoroutine(HealingWindow_Close_Coroutine());
        }
    }
    #endregion
}
