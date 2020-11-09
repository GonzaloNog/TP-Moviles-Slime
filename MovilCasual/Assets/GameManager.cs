using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool play = false;
    private bool stopPoints;
    private GameObject actualyPoint;
    public CanvasControler canv;
    public GameObject StartPoint;
    private int Score = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }
    public void SetStopPoints(bool _stop)
    {
        stopPoints = _stop;
    }
    public bool GetStopPoints()
    {
        return stopPoints;
    }
    public void SetPlay(bool _play)
    {
        play = _play;
    }
    public bool GetPlay()
    {
        return play;
    }
    public GameObject GetPoitEntity()
    {
        return actualyPoint;
    }
    public void SetPointEntity(GameObject point)
    {
        actualyPoint = point;
    }
    public void EndGame()
    {
        canv.EndGame();
    }
    public void StartPointOff()
    {
        StartCoroutine(point());
    }
    IEnumerator point()
    {
        yield return new WaitForSeconds(1);
        StartPoint.SetActive(false);
    }
    public int GetScore()
    {
        return Score;
    }
    public void SetScore(int _score)
    {
        Score += _score;
    } 
}
