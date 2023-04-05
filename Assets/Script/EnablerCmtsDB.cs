using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class EnablerCmtsDB
{
    public string welcome;
    public string S_Sound_Intro;
    public string A_Sound_Intro;
    public string T_Sound_Intro;
    public string P_Sound_Intro;
    public string Beginning_STAP;
    public string Ending_TP;
    public string H_Intro;
    public string H_Sound_Intro;
    public string H_Tracing;
    public string H_Obj_Start;
    public string H_Filling_begin;
    public string H_Story;
    public string N_Intro;
    public string N_Sound_Intro;
    public string N_Tracing;
    public string N_Obj_Start;
    public string N_Filling_begin;
    public string N_Story;
    public string O_Intro;
    public string O_Sound_Intro;
    public string O_Tracing;
    public string O_Obj_Start;
    public string O_Story;
    public string goodbye;

    public EnablerCmtsDB()
    {
        welcome = Main_Blended.OBJ_main_blended.enablerComments[0];
        S_Sound_Intro = Main_Blended.OBJ_main_blended.enablerComments[1];
        A_Sound_Intro = Main_Blended.OBJ_main_blended.enablerComments[2];
        T_Sound_Intro = Main_Blended.OBJ_main_blended.enablerComments[3];
        P_Sound_Intro = Main_Blended.OBJ_main_blended.enablerComments[4];
        Beginning_STAP = Main_Blended.OBJ_main_blended.enablerComments[5];
        Ending_TP = Main_Blended.OBJ_main_blended.enablerComments[6];
        H_Intro = Main_Blended.OBJ_main_blended.enablerComments[7];
        H_Sound_Intro = Main_Blended.OBJ_main_blended.enablerComments[8];
        H_Tracing = Main_Blended.OBJ_main_blended.enablerComments[9];
        H_Obj_Start = Main_Blended.OBJ_main_blended.enablerComments[10];
        H_Filling_begin = Main_Blended.OBJ_main_blended.enablerComments[11];
        H_Story = Main_Blended.OBJ_main_blended.enablerComments[12];
        N_Intro = Main_Blended.OBJ_main_blended.enablerComments[13];
        N_Sound_Intro = Main_Blended.OBJ_main_blended.enablerComments[14];
        N_Tracing = Main_Blended.OBJ_main_blended.enablerComments[15];
        N_Obj_Start = Main_Blended.OBJ_main_blended.enablerComments[16];
        N_Filling_begin = Main_Blended.OBJ_main_blended.enablerComments[17];
        N_Story = Main_Blended.OBJ_main_blended.enablerComments[18];
        O_Intro = Main_Blended.OBJ_main_blended.enablerComments[19];
        O_Sound_Intro = Main_Blended.OBJ_main_blended.enablerComments[20];
        O_Tracing = Main_Blended.OBJ_main_blended.enablerComments[21];
        O_Obj_Start = Main_Blended.OBJ_main_blended.enablerComments[22];
        O_Story = Main_Blended.OBJ_main_blended.enablerComments[23];
        goodbye = Main_Blended.OBJ_main_blended.enablerComments[24];
    }
}