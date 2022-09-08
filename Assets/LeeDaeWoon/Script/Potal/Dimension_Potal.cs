using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Dimension_Potal : MonoBehaviour
{
    public Image F_Button;
    public Text Move_Text;
    public GameObject Move;

    private bool collision_Check = false;

    void Start()
    {

    }

    void Update()
    {
        F_Click();
        Move.transform.localPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.localPosition + new Vector3(-3f, -4.5f, 0));
    }

    void F_Click()
    {
        if (collision_Check == true && Input.GetKeyDown(KeyCode.F))
            SceneManager.LoadScene("test");
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<ITypePlayer>() != null)
        {
            collision_Check = true;
            F_Button.DOFade(1f, 0.5f);
            Move_Text.DOFade(1f, 0.5f);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ITypePlayer>() != null)
        {
            F_Button.DOFade(0f, 0.5f);
            Move_Text.DOFade(0f, 0.5f);
        }
    }
}
