using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class countdown : MonoBehaviour
{
    public int timeLeft = 10; 
    public Text countdown1; 
    public GameObject panelGameOver;
    public Text mScore;

    void Start()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
        mScore = panelGameOver.GetComponent<Text>();
    }
    void Update()
    {
        if (timeLeft < 0)
        {
            //mScore = panelGameOver.GetComponent<Text>();
            //mScore.text = "GAME OVER YOUR SCORE: " + GetComponent<LaserScript>().mObjDestroyed.ToString();
            panelGameOver.SetActive(true);
            //mScore.fontSize = 20;
            //GetComponent<LaserScript>().mLaserLineEnabled = false;
        }
        else
        {
            countdown1.text = ("" + timeLeft); 
        }
    }
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}