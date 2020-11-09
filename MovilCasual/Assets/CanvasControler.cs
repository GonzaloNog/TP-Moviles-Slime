using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class CanvasControler : MonoBehaviour
{
    public float timeStart;
    public Text timeStartUI;
    public GameObject Lose;
    public GameObject Pause;
    public Text Score;
    public Text ScoreFinal;
    void Start()
    {
        StartCoroutine(StartTime());
    }
    void Update()
    {
        Score.text = ""+GameManager.instance.GetScore();
        ScoreFinal.text = "" + GameManager.instance.GetScore();
    }
    public void EndGame()
    {
        Time.timeScale = 0;
        Lose.SetActive(true);
    }
    public void PauseCanvas()
    {
        if (Pause.activeSelf)
        {
            Pause.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Pause.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
    // Update is called once per frame
    IEnumerator StartTime()
    {
        timeStartUI.text = "" + timeStart;
        if (timeStart > 0)
        {
            yield return new WaitForSeconds(1);
            timeStart--;
            StartCoroutine(StartTime());
        }
        else
        {
            GameManager.instance.SetPlay(true);
            timeStartUI.text = "START";
            yield return new WaitForSeconds(2);
            timeStartUI.gameObject.SetActive(false);
            yield return null;
        }   
    }
}
