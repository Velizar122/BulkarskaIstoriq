using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    //Private SerializeField variables//

    [SerializeField] GameObject options;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameLobby;
    [SerializeField] GameObject continueButtonInOptions;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject lostMenu;
    [SerializeField] GameObject epohiMenu;
    [SerializeField] TextMeshProUGUI question;
    [SerializeField] TextAsset[] jsonFile;



    //Private variables//
    HighScorePlayerPrefsScript highScoreManager;


    int currentJSONFile=0;



    private void Start()
    {
        highScoreManager = GameObject.FindGameObjectWithTag("HighScoreManagerAndPlayerPrefs").GetComponent<HighScorePlayerPrefsScript>();
        LoadNewQuestion();
    }

    public void OpenMainMenu()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
        gameLobby.SetActive(false);
        lostMenu.SetActive(false);
        epohiMenu.SetActive(false);
        Time.timeScale = 1.0f;
        LoadNewQuestion();
    }

    public void OpenOptionsMenu()
    {
        options.SetActive(true);
        if (gameLobby.activeSelf == true)
        {
            continueButtonInOptions.SetActive(true);
        }
        else
        {
            continueButtonInOptions.SetActive(false);
        }
        Time.timeScale = 0f;
    }


    public void CloseOptionsMenu()
    {
        options.SetActive(false);
        Time.timeScale = 1.0f;
        LoadNewQuestion();
    }



    public void OpenGameLobbyMenu()
    {
        options.SetActive(false);
        gameLobby.SetActive(true);

        GameManager gameManagerScript = gameManager.GetComponent<GameManager>();
        gameManagerScript.LoadNewQuestion();
        Time.timeScale = 1.0f;
    }



    public void OpenEpohiMenu()
    {
        highScoreManager.IstoriqIKulturaResultati();
        mainMenu.SetActive(false);
        epohiMenu.SetActive(true);
    } 


    public void CloseEpohiMenu()
    {
        mainMenu.SetActive(true);
        epohiMenu.SetActive(false);
    }



    public void CloseGame()
    {
        Application.Quit();
    }


    public void LoadNewQuestion()
    {
        currentJSONFile=SetRandomJSONFile();
        Question currentQuestion = GetQuestionById(SetRandomQuestion());

        question.text = currentQuestion.questionText;

    }


    public int SetRandomQuestion()
    {

        int randomQuestion = UnityEngine.Random.Range(1, 40);

        return randomQuestion;
    }

    public int SetRandomJSONFile()
    {

        int randomJSONFile = UnityEngine.Random.Range(0, 4);

        return randomJSONFile;
    }




    public Question GetQuestionById(int targetId)
    {
        Debug.Log(currentJSONFile);
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
}
