using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FunPlay_Filling : MonoBehaviour
{
    public string STR_gamename;
    public GameObject selectedobject, G_dash,block;
    public GameObject handle, rolling, coin;
    

    public Animator coin_anim;
    public AnimationClip AC_right, AC_wrong;

    public GameObject[] questions, answers,options;
    public int count;

   
    public AudioSource wrong;
    public AudioClip[] clips,answer_clips;
    public List<string> FunPlay_listValues;
    public string answer_name;
    public bool click;
    public GameObject G_final_Screen;

    //public GameObject optionPanel; //T_Phonemic-1
    //public GameObject[] otherOptions;  //T_Phonemic-1

    private void Start()
    {
        G_final_Screen.SetActive(false);
        count = 0;
        STR_gamename = this.gameObject.name;
        click = true;
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(false);
            answers[i].SetActive(false);
            options[i].SetActive(false);
        }
        questions[count].SetActive(true);
        options[count].SetActive(true);


        answer_name = answers[count].name; //Edited by emerson

        handle.SetActive(true);
        coin.SetActive(false);
        rolling.SetActive(false);
        G_dash.SetActive(true);
        block.SetActive(false);

    }

    public void clicking()
    {
        selectedobject = EventSystem.current.currentSelectedGameObject;

        FunPlay_listValues.Add(selectedobject.name);

        if (selectedobject.tag == "answer")
        {
            if (click)
            {
                Debug.Log("ans");
                coin_anim.Play("crtans_coin");
                G_dash.SetActive(false);
                handle.SetActive(false);
                rolling.SetActive(true);
                rolling.gameObject.GetComponent<AudioSource>().Play();
                Invoke("coindefaultanim", AC_right.length);
                Invoke("coinandanswer", clips[0].length);
               // wrong.clip = clips[0];
               // wrong.Play();
                block.SetActive(true);
                click = false;
                //selectedobject.gameObject.GetComponent<Button>().enabled = false;
                //this.GetComponent<DragandDrop>().enabled = false;
            }

        }
        else
        {
            block.SetActive(true);
            coin_anim.Play("wrgans_coin");
            int random = Random.Range(1, clips.Length);
            wrong.clip = clips[random];
            wrong.Play();
            Invoke("coindefaultanim", AC_wrong.length);
        }

    }

    public void coinandanswer()
    {
        Debug.Log("ansshow");
        coin.SetActive(true);
        coin.gameObject.GetComponent<AudioSource>().Play();
        questions[count].SetActive(false);
        answers[count].SetActive(true);
        wrong.clip = answer_clips[count];
        wrong.Play();

        Invoke("changequestion", 2f);
    }

    public void changequestion()
    {
        handle.SetActive(true);
        coin.SetActive(false);
        rolling.SetActive(false);
        G_dash.SetActive(true);
        click = true;
       // Main_Blended.OBJ_main_blended.PostDataToDB(STR_gamename, answer_name, FunPlay_listValues);

        if (count < 4)
        {
            count++;
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i].SetActive(false);
                answers[i].SetActive(false);
                
                options[i].SetActive(false);
            }
            block.SetActive(false);
            answer_name =  answers[count].name; //Edited by emerson
            questions[count].SetActive(true);
            options[count].SetActive(true);


            FunPlay_listValues.Clear();
        }
        else
        {
            G_final_Screen.SetActive(true);
            G_dash.SetActive(false);
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i].SetActive(false);
                answers[i].SetActive(false);
                options[i].SetActive(false);
            }

        }
    }
    public void coindefaultanim()
    {
        coin_anim.Play("New State");
        block.SetActive(false);
    }
}


