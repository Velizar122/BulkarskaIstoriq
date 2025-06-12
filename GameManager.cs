using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //public//

    public int currentJSONFile=0;


    //private SerializeField variables//
    [SerializeField] TextMeshProUGUI question;
    [SerializeField] TextMeshProUGUI questionCount;
    [SerializeField] TextMeshProUGUI timeCount;
    [SerializeField] TextMeshProUGUI correctAnswerInEndMenu;
    [SerializeField] TextMeshProUGUI gameMode;
    [SerializeField] GameObject[] answers;
    [SerializeField] GameObject lostMenu;
    [SerializeField] GameObject gameLobby;
    [SerializeField] TextAsset[] jsonFile;


    //private//
    HighScorePlayerPrefsScript highScoreManager;

    string currentCorrectAnswer;

    int numberOfQuestionsInJSON=0;
    int count = 0;

    float timer = 15;

    SoundManager soundManager;

   [SerializeField] List<int> alreadyAnswered = new List<int>();


    private void Start()
    {
        highScoreManager = GameObject.FindGameObjectWithTag("HighScoreManagerAndPlayerPrefs").GetComponent<HighScorePlayerPrefsScript>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }


    private void OnEnable()
    {
        count = 1;
        timer = 15;
        timeCount.text = Convert.ToInt32(timer).ToString();
    }


    private void Update()
    {   
        if (gameLobby.activeInHierarchy==true)
        {
            timer -= Time.deltaTime;
            timeCount.text = Convert.ToInt32(timer).ToString();
            if (timer <= 0)
            {
                WrongAnswer();
            }
        }
    }

    public void LoadNewQuestion()
    {
        Question currentQuestion = GetQuestionById(SetRandomQuestion());

        Debug.Log(currentQuestion);
        List<string> answersList = new List<string>(currentQuestion.options);

        ShuffleList(answersList);


        question.text = currentQuestion.questionText;

        for (int i = 0; i < answers.Length; i++)
        {
            Button button = answers[i].GetComponent<Button>();
            TextMeshProUGUI buttonText = answers[i].GetComponentInChildren<TextMeshProUGUI>();

            answers[i].GetComponentInChildren<TextMeshProUGUI>().text = answersList[i];

            button.onClick.RemoveAllListeners();

            if (answersList[i] == currentQuestion.options[currentQuestion.correctOptionIndex])
            {
               currentCorrectAnswer = answersList[i];
               button.onClick.AddListener(CorrectAnswer);
               button.onClick.AddListener(soundManager.PlayCorrectSound);
                //button.image.color = Color.green;
            }
            else 
            {
                button.GetComponent<Button>().onClick.AddListener(WrongAnswer);
                button.GetComponent<Button>().onClick.AddListener(soundManager.PlayWrongSound);
                //button.image.color = Color.red;
            }

        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }


    public int SetRandomQuestion()
    {
        if (alreadyAnswered.Count >= numberOfQuestionsInJSON-1)
        {
            AllQuestionsAnswerd();
            return 0;
        }

        int randomQuestion;
        int maxAttempts = 100;
        int attempts = 0;

        do
        {
            randomQuestion = UnityEngine.Random.Range(1, numberOfQuestionsInJSON);
            attempts++;
            if (attempts > maxAttempts)
            {
                Debug.LogError("Failed to generate a random question after multiple attempts.");
                return -1;
            }
        }
        while (alreadyAnswered.Contains(randomQuestion));

        alreadyAnswered.Add(randomQuestion);
        Debug.Log(randomQuestion);
        return randomQuestion;
    }




    public Question GetQuestionById(int targetId)
    {
        Questions questionsInJSON = JsonUtility.FromJson<Questions>(jsonFile[currentJSONFile].text);

        foreach (Question question in questionsInJSON.questionsToSet)
        {
            if (question.id == targetId)
            {
                return question;
            }
        }

        Debug.LogWarning("Question with ID " + targetId + " not found.");
        return null;
    }



    public void CorrectAnswer()
    {
        count += 1;
        questionCount.text = count.ToString();
        timer += 10;
        highScoreManager.SaveScoreAndTIme(currentJSONFile, count, timer);
        LoadNewQuestion();
    }

    public void WrongAnswer()
    {
        Time.timeScale = 0;
        correctAnswerInEndMenu.text ="Правилният отговор е - " + currentCorrectAnswer;
        highScoreManager.SaveScoreAndTIme(currentJSONFile, count, timer);
        count = 1;
        timer = 15;
        questionCount.text = count.ToString();
        lostMenu.SetActive(true);
        alreadyAnswered.Clear();
        LoadNewQuestion();
    }

    public void AllQuestionsAnswerd()
    {
        Time.timeScale = 0;
        count = 1;
        timer = 15;
        questionCount.text = count.ToString();
        correctAnswerInEndMenu.text = "Браво!!! Отговорихте на всички въпроси!";
        lostMenu.SetActive(true);
        alreadyAnswered.Clear();
        LoadNewQuestion();
    }


    public void NewGame()
    {
        alreadyAnswered.Clear();
        Time.timeScale = 1;
        count = 1;
        questionCount.text = count.ToString();
        timer = 15;
        LoadNewQuestion();
        lostMenu.SetActive(false);
    }

    public void LoadPurvoCarstv()
    {
        currentJSONFile = 0;
        numberOfQuestionsInJSON = 40;
        gameMode.text = "Първо българско царство";
    }
    public void LoadVtoroCarstv()
    {
        currentJSONFile = 1;
        numberOfQuestionsInJSON = 40;
        gameMode.text = "Второ българско царство";
    } 
    public void LoadTursko()
    {
        currentJSONFile = 2;
        numberOfQuestionsInJSON = 40;
        gameMode.text = "Под турско време";
    }
    public void LoadKultura()
    {
        currentJSONFile = 3;
        numberOfQuestionsInJSON = 80;
        gameMode.text = "Култура";
    }
    public void LoadProizvodni()
    {
        currentJSONFile = 4;
        numberOfQuestionsInJSON = 200;
        gameMode.text = "Произволни";
    }
}
