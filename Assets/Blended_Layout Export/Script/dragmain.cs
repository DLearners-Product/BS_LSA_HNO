using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class dragmain : MonoBehaviour
{
    public static dragmain OBJ_dragmain;
    public GameObject[] GA_Questions;
    public int I_Qcount, I_Anscount;

    public GameObject G_oldnext, G_final;
    public AudioSource AS_crt, AS_wrg;

   
    public void Start()
    {
        OBJ_dragmain = this;
        I_Qcount = -1;
        G_oldnext.SetActive(false);
        G_final.SetActive(false);
        
        showquestion();
    }
    public void showquestion()
    {
        I_Qcount++;
        if (I_Qcount<GA_Questions.Length)
        {
            for (int i = 0; i < GA_Questions.Length; i++)
            {
                GA_Questions[i].SetActive(false);
            }
            GA_Questions[I_Qcount].SetActive(true);

            I_Anscount = 2;
        }
        else
        {
            G_final.SetActive(true);
        }
        
    }

    public void BUT_next()
    {
        showquestion();
    }
    public void THI_correct()
    {
        I_Anscount--;
        AS_crt.Play();
        if (I_Anscount == 0)
        {
            Invoke("showquestion", 2f);
        }
    }
    public void THI_wrg()
    {
        AS_wrg.Play();
    }
    
   
   

    
}
