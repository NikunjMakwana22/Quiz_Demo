using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [SerializeField]
    private List<Question> questions;
    private Question SelectedQuestions;

    public GameObject EndScreen;
    public GameObject MainScreen;
   
    public TextMeshProUGUI Question;
    public GameObject[] ans;
    public TextMeshProUGUI ansIndicator;
    public Image progressBar;
    public int currentQuestion = 0;
    public Color[] Color;

    public TextMeshProUGUI ResultDes;
    public TextMeshProUGUI ScoreText;
    public int Score=0;
    private int j;



    private void Awake()
    {
        currentQuestion = 0;
        PrintQues(currentQuestion);
    }
    void Start()
    {
        PrintQues(currentQuestion);
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void PrintQues(int index)
    {
        Question.text = questions[index].question;
        ansIndicator.text = (currentQuestion+1 + "/" + questions.Count);
        progressBar.fillAmount = ((float)(currentQuestion + 1 / questions.Count) / 5) + 0.2f;
        for (j = 0; j <= 3; j++)
        {
            ans[j].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = questions[index].options[j];
        }
        ButtonColorChanger(index);

    }

    public void Next()
    {

        if (currentQuestion == questions.Count - 1)
        {
            EndScreen.SetActive(true);
            MainScreen.SetActive(false);
            ResultDes.text = GenerateResult();
            ScoreText.text = Score + "/" + questions.Count;
            
        }
        else
        {
            currentQuestion++;
            PrintQues(currentQuestion);
        }
        
    }

    public void prev()
    {
        if (currentQuestion!=0)
        {
            currentQuestion--;
            PrintQues(currentQuestion);
        }
    }

    public void RegAns(Button b)
    {
        Debug.Log(b.name.Substring(2, 1));
        int i = int.Parse(b.name.Substring(2, 1));
        questions[currentQuestion].ansSelected = i;
        ButtonColorChanger(currentQuestion);
    }

    public void ButtonColorChanger(int index)
    {
        if (questions[index].ansSelected == 0)
        {
            for (j = 0; j <= 3; j++)
                ans[j].GetComponent<Image>().color = Color[0];
        }
        else
        {
            int i = questions[index].ansSelected - 1;
            for (j = 0; j <= 3; j++)
            {
                if (j == i)
                    ans[j].GetComponent<Image>().color = Color[1];
                else
                    ans[j].GetComponent<Image>().color = Color[0];
            }
        }

    }

    public void ResetQuiz()
    {
        currentQuestion = 0;
        Score = 0;
        ResultDes.text = "";
        ScoreText.text = "";
        int num = questions.Count;
        for(j=0;j<num;j++)
        {
            questions[j].ansSelected = 0;
         }
        PrintQues(currentQuestion);
    }


    public string GenerateResult()
    {
        string s = "";
        int num = questions.Count;
        for(j=0;j<num;j++)
        {
            s += "<color=black> Question" + (j + 1) + ":";
            if (questions[j].correctAns == questions[j].ansSelected)
            {
                s += "<color=green> Correct<br>";
                Score++;
            }
            else
                s += "<color=red> Incorrect<br>";
        }
        return s;
    }
}

[System.Serializable]

public class Question
{
    public string question;
    public List<string> options;
    public int correctAns;
    public int ansSelected;
}
