using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Player_Vector : MonoBehaviour
{
    public static Player_Vector Inst;
    public GameObject PlayerObj;

    public bool M_VectorCheck = false;
    public bool I_VectorCheck = false;
    public bool D_VectorCheck = false;

    void Start()
    {

        PlayerObj = Player.Instance.gameObject;
    }

    void Update()
    {
        Scene_Vector();
    }

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnLevelWasLoaded(int level)
    {
        DOTween.PauseAll();

        if (SceneManager.GetActiveScene().name.Equals("title"))
        {
            UI_Manager.instance.fadeInOut.DOFade(0f, 0f);
        }

        // TODO : ÀÏ°ü¼º.
        if (SceneManager.GetActiveScene().name == "Dimension")
        {
            Potal.Inst.Player.DOFade(1f, 0f);
            Potal.Inst.Dark_Player.DOFade(1f, 0f);

            Skill_Manager.instance.Skill_PotalMove = false;
            UI_Manager.instance.PlayerMove_control = false;

            UI_Manager.instance.fadeInOut.DOFade(0f, 0f);
        }


    }


    public void Scene_Vector()
    {
        if (SceneManager.GetActiveScene().name == "Main" && M_VectorCheck == false)
        {
            M_VectorCheck = true;
            PlayerObj.transform.localPosition = new Vector3(-8.9f, -1.39f, 0f);
        }
        else if (SceneManager.GetActiveScene().name != "Main")
            M_VectorCheck = false;


        if (SceneManager.GetActiveScene().name == "test" && I_VectorCheck == false)
        {
            I_VectorCheck = true;
            PlayerObj.transform.localPosition = new Vector3(0f, 1f, 0f);
        }
        else if (SceneManager.GetActiveScene().name != "test")
            I_VectorCheck = false;


        if (SceneManager.GetActiveScene().name == "Dimension" && D_VectorCheck == false)
        {
            D_VectorCheck = true;
            PlayerObj.transform.localPosition = new Vector3(0f, 1f, 0f);
        }
        else if (SceneManager.GetActiveScene().name != "Dimension")
            D_VectorCheck = false;
    }
}
