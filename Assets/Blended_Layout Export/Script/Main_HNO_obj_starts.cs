﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Main_HNO_obj_starts : MonoBehaviour
{
    public string STR_gamename;
    public static Main_HNO_obj_starts OBJ_main_selectedcorrectobj;
    public GameObject selectedobj,Lettertohide,fireworks;
    //public GameObject blur;
    //public Text objname;
    //public int I_attempt;

    public GameObject[] fires;
    public int firecount;
    public Material greyscale;

    public Animator bat;
    public AnimationClip AC_happy, AC_sad,AC_firework;

    //public GameObject G_block;
    public AudioClip[] wrongs;
    public AudioSource wrong;

   // public GameObject[] Answers;
    public GameObject G_final_screen;

    public List<string> ObjStarts_listValues;

#region  QA
    public GameObject questionGO;
    public GameObject[] optionsGO;
    Component question;
    Component[] options;
#endregion

    // Start is called before the first frame update
    void Start()
    {
        G_final_screen.SetActive(false);
        STR_gamename = this.gameObject.name;
        // OBJ_main_selectedcorrectobj = this;
        firecount = -1;
        for (int i = 0; i < fires.Length; i++)
        {
            fires[i].SetActive(false);
        }
        Lettertohide.SetActive(true);
        fireworks.SetActive(false);
        Main_Blended.OBJ_main_blended.levelno = 4;
        QAManager.instance.UpdateActivityQuestion();
        AssignData();
    }

    void AssignData(){
        question = QAManager.instance.GetQuestionAt(0, 0);
        questionGO.GetComponent<Image>().sprite = question._sprite;

        options = QAManager.instance.GetOption(0, 0);
        for (int i=0; i< optionsGO.Length; i++)
        {
            optionsGO[i].GetComponent<Image>().sprite = options[i]._sprite;
        }
    }

    public void Clicking()
    {
        selectedobj = EventSystem.current.currentSelectedGameObject;

        if (selectedobj.tag == "answer")
        {
            firecount++;
            fires[firecount].SetActive(true);
            bat.Play("Bat_happy");

            ObjStarts_listValues.Add(selectedobj.name);

            Invoke("batdefaultanim", AC_happy.length);

            selectedobj.gameObject.GetComponent<mouse>().enabled = false;   //removing hover effect

            selectedobj.gameObject.GetComponent<Image>().material = greyscale;

            selectedobj.GetComponent<Button>().enabled = false;
            //wrong.clip = wrongs[0];
            //wrong.Play();

            selectedobj.GetComponent<AudioSource>().Play();

            Invoke("hideletter", selectedobj.GetComponent<AudioSource>().clip.length);

        }
        else
        {
            ObjStarts_listValues.Add(selectedobj.name);

            int random=Random.Range(1, wrongs.Length);
            wrong.clip = wrongs[random];
            wrong.Play();
            bat.Play("Bat_sad");
            Invoke("batdefaultanim", AC_sad.length);
        }
        

    }
    public void hideletter()
    {
        if (firecount == 5)
        {
            Lettertohide.SetActive(false);
            Invoke("Function_firework", 1f);
        }
    }

    
    public void Function_firework()
    {
        fireworks.SetActive(true);
        fireworks.GetComponent<Animator>().Play("firework_anim");
        fireworks.gameObject.GetComponent<AudioSource>().Play();
        Invoke("batdefaultanim", AC_firework.length);
        
    }
    public void batdefaultanim()
    {
        if (firecount < 5)
        {
            bat.Play("Bat_Idle");
        }
        else
        {
            bat.Play("Bat_completed");
            Invoke("completescreen", 2.5f);
         //   Main_Blended.OBJ_main_blended.PostDataToDB(STR_gamename, ObjStarts_listValues);
        }
    }
    public void completescreen()
    {
        G_final_screen.SetActive(true);
    }


   /* public void Reset_obj_start()
    {
        
        bat.Play("Bat_Idle");
        G_final_screen.SetActive(false);
        for (int i=0;i<Answers.Length;i++)
        {
            Answers[i].gameObject.GetComponent<Button>().enabled = true;
            Answers[i].gameObject.GetComponent<Image>().material = null;
            Answers[i].gameObject.GetComponent<mouse>().enabled = true;
        }
        firecount = -1;
        for (int i = 0; i < fires.Length; i++)
        {
            fires[i].SetActive(false);
        }
        Lettertohide.SetActive(true);
        fireworks.SetActive(false);

    }*/
}