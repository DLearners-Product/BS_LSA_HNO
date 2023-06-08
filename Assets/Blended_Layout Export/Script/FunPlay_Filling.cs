using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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
    [SerializeField] private TextMeshProUGUI countText;
    private static int Q_count;

    [Header("QA")]
#region QA
    public int qIndex;
    public GameObject questionGO;
    public GameObject answerGO;
    public GameObject[] _optionsGO;
    [SerializeField] Component question;
    [SerializeField] Component[] _questions;
    [SerializeField] Component[] _options;
    [SerializeField] Component[] _answers;
#endregion

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

        qIndex = 0;
        // testing need to remove
        // Main_Blended.OBJ_main_blended.levelno = 18;
        QAManager.instance.UpdateActivityQuestion();

        GetData(qIndex);
        // AssignData();
        UpdateQuestionCounter();
    }

    bool CheckOptionIsAns(Component option){
        for(int i=0; i<_answers.Length; i++){
            if(option.text == _answers[i].text){
                return true;
            }
        }
        return false;
    }

#region  QA
    void GetData(int questionIndex){
        question = QAManager.instance.GetQuestionAt(0, questionIndex);
        _questions = QAManager.instance.GetAllQuestions(0);
        _options = QAManager.instance.GetOption(0, questionIndex);
        _answers = QAManager.instance.GetAnswer(0, questionIndex);
    }

    void AssignData(){
        questionGO.GetComponent<Text>().text = question.text;

        for (int i = 0; i < _optionsGO.Length; i++)
        {
            _optionsGO[i].GetComponent<Text>().text = _options[i].text;
            _optionsGO[i].tag = "Untagged";
            if(CheckOptionIsAns(_options[i])){
            // if(_options[i].isAnswer){
                _optionsGO[i].tag = "answer";
            }
        }
    }

    int GetAnswerComp(GameObject selectedObject){
        for(int i=0; i<_optionsGO.Length; i++){
            if(_optionsGO[i].Equals(selectedObject)){
                // Debug.Log(optionsGO[i].name, optionsGO[i]);
                return _options[i].id;
            }
        }
        return -1;
    }
#endregion

    public void clicking()
    {
        selectedobject = EventSystem.current.currentSelectedGameObject;

        FunPlay_listValues.Add(selectedobject.name);
        if (selectedobject.tag == "answer")
        {
            if (click)
            {
                int answerID = _answers[0].id;

                ScoreManager.instance.RightAnswer(qIndex, questionID : question.id, answerID : answerID);

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
            ScoreManager.instance.WrongAnswer(qIndex, questionID : question.id);
            Q_count++;
            block.SetActive(true);
            coin_anim.Play("wrgans_coin");
            int random = Random.Range(1, clips.Length);
            wrong.clip = clips[random];
            wrong.Play();
            Invoke("coindefaultanim", AC_wrong.length);
        }

        // else
        // {
        //     Q_count = 1;
        // }
    }

    public void coinandanswer()
    {
        Debug.Log("ansshow");
        coin.SetActive(true);
        coin.gameObject.GetComponent<AudioSource>().Play();
        questions[count].SetActive(false);
        answers[count].SetActive(true);
        // questionGO.SetActive(false);
        // answerGO.SetActive(true);
        // answerGO.GetComponent<Text>().text = _answers[0].text + question.text;
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

        count++;

        if (count < questions.Length)
        {
            qIndex++;

            GetData(qIndex);
            // AssignData();

            questionGO.SetActive(true);
            answerGO.SetActive(false);
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
            BlendedOperations.instance.NotifyActivityCompleted();
            G_final_Screen.SetActive(true);
            G_dash.SetActive(false);
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i].SetActive(false);
                answers[i].SetActive(false);
                options[i].SetActive(false);
            }
        }
        UpdateQuestionCounter();
    }

    public void coindefaultanim()
    {
        coin_anim.Play("New State");
        block.SetActive(false);
    }

    public void UpdateQuestionCounter(){
        if(count < questions.Length)
        {
            countText.text = (count + 1) + "/" + questions.Length;
        }

    }
}


