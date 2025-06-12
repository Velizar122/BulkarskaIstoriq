using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HighScorePlayerPrefsScript : MonoBehaviour
{

    //public/
    public TextMeshProUGUI textScore1, textScore2, textScoreTursko, textScoreKultura;


    public void SaveScoreAndTIme(int jsonFileNumber, int counter, float timer)
    {
        Debug.Log("AAAAAAAAAAAAAAA " + jsonFileNumber);
        switch (jsonFileNumber)
        {
            case 0:
                if (PlayerPrefs.GetInt("textScore1Key")<counter)
                {
                    PlayerPrefs.SetInt("textScore1Key", counter);
                    PlayerPrefs.SetInt("textScore1TimeKey", Convert.ToInt32(timer));
                }
                break;
            case 1:
                if (PlayerPrefs.GetInt("textScore2Key") < counter)
                {
                    PlayerPrefs.SetInt("textScore2Key", counter);
                    PlayerPrefs.SetInt("textScore2TimeKey", Convert.ToInt32(timer));
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("textScoreTurskoKey") < counter)
                {
                    PlayerPrefs.SetInt("textScoreTurskoKey", counter);
                    PlayerPrefs.SetInt("textScoreTurskoTimeKey", Convert.ToInt32(timer));
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("textScoreKulturaKey") < counter)
                {
                    PlayerPrefs.SetInt("textScoreKulturaKey", counter);
                    PlayerPrefs.SetInt("textScoreKulturaTimeKey", Convert.ToInt32(timer));
                }
                break;

            default:
                break;
        }
    }


    public void IstoriqIKulturaResultati() 
    {
        if (PlayerPrefs.HasKey("textScore1Key") && PlayerPrefs.HasKey("textScore1TimeKey"))
        {
            textScore1.text = $"{PlayerPrefs.GetInt("textScore1Key")} / {PlayerPrefs.GetInt("textScore1TimeKey")}";
        }
        else
        {
            PlayerPrefs.SetInt("textScore1Key", 0);
            PlayerPrefs.SetInt("textScore1TimeKey", 0);
            textScore1.text = $"{PlayerPrefs.GetInt("textScore1Key")} / {PlayerPrefs.GetInt("textScore1TimeKey")}";
        }



        if (PlayerPrefs.HasKey("textScore2Key") && PlayerPrefs.HasKey("textScore2TimeKey"))
        {
            textScore2.text = $"{PlayerPrefs.GetInt("textScore2Key")} / {PlayerPrefs.GetInt("textScore2TimeKey")}";
        }
        else
        {
            PlayerPrefs.SetInt("textScore2Key", 0);
            PlayerPrefs.SetInt("textScore2TimeKey", 0);
            textScore2.text = $"{PlayerPrefs.GetInt("textScore2Key")} / {PlayerPrefs.GetInt("textScore2TimeKey")}";
        }




        if (PlayerPrefs.HasKey("textScoreTurskoKey") && PlayerPrefs.HasKey("textScoreTurskoTimeKey"))
        {
            textScoreTursko.text = $"{PlayerPrefs.GetInt("textScoreTurskoKey")} / {PlayerPrefs.GetInt("textScoreTurskoTimeKey")}";

        }
        else
        {
            PlayerPrefs.SetInt("textScoreTurskoKey", 0);
            PlayerPrefs.SetInt("textScoreTurskoTimeKey", 0);
            textScoreTursko.text = $"{PlayerPrefs.GetInt("textScoreTurskoKey")} / {PlayerPrefs.GetInt("textScoreTurskoTimeKey")}";
        }



        if (PlayerPrefs.HasKey("textScoreKulturaKey") && PlayerPrefs.HasKey("textScoreKulturaTimeKey"))
        {
            textScoreKultura.text = $"{PlayerPrefs.GetInt("textScoreKulturaKey")} / {PlayerPrefs.GetInt("textScoreKulturaTimeKey")}";
        }
        else
        {
            PlayerPrefs.SetInt("textScoreKulturaKey", 0);
            PlayerPrefs.SetInt("textScoreKulturaTimeKey", 0);
            textScoreKultura.text = $"{PlayerPrefs.GetInt("textScoreKulturaKey")} / {PlayerPrefs.GetInt("textScoreKulturaTimeKey")}";
        }
    }
}
