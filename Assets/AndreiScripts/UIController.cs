using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class UIController : MonoBehaviour
{
    [Header("Ёкран старта")]
    public GameObject[] StartTextArray;
    public GameObject[] StartBackgroundArray;
    public GameObject StartPanel;
    [Header("Ёкран секундомера")]
    public GameObject TimerPanel;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI YourLap;
    public TextMeshProUGUI[] LapTimes;
    [Header("Ёкран отсчета")]
    public GameObject CountdownPanel;
    public GameObject[] NumberArray;
    [Header("Ёкран окончани€ гонки")]
    public GameObject FinishPanel;
    public GameObject Restartbutton;

    // Ћогика
    bool WaitToStart;
    [HideInInspector]
    public bool Race;
    float CurrentTime;
    float BestTime;
    [HideInInspector]
    public int RaceCount;


    // —сылки и ивенты
    public static event Action GO;
    public static event Action RaceDone;
    RaceRecorder RR;
    UIComixEffect UICE;
    private void Start()
    {
        if (TimerText == null)
        {
            Debug.LogError("TimerText is not assigned!");
            return;
        }
        WaitToStart = true;
        RR = FindObjectOfType<RaceRecorder>();
        UICE = FindObjectOfType<UIComixEffect>();
        ClearText();
        StartCoroutine(StartTextProgressionCo());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & WaitToStart)
        {
            StartTheRace();
        }
    }
    IEnumerator StartTextProgressionCo()
    {
        // красивые эффекты "Start"
        yield return null;
        while (WaitToStart)
        {
            ClearText();
            int x = UnityEngine.Random.Range(0, StartTextArray.Length);
            int u = UnityEngine.Random.Range(0, StartTextArray.Length);
            StartTextArray[x].SetActive(true);
            StartBackgroundArray[u].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        ClearText();
    }
    void ClearText()
    {
        // ну собственно пон€тно
        for (int i = 0; i < StartTextArray.Length; i++)
        {
            StartTextArray[i].SetActive(false);
            StartBackgroundArray[i].SetActive(false);
        }
    }
    public void StartTheRace()
    {
        // начало отсчета
        WaitToStart = false;
        StartPanel.SetActive(false);
        StartCoroutine(CountdownCo());
    }
    IEnumerator CountdownCo()
    {
        //отсчет с визуалом
        NumberArray[3].SetActive(false);
        int PlayerRaceCount = RaceCount + 1;
        YourLap.text = "Lap: " + PlayerRaceCount;
        CountdownPanel.SetActive(true);
        yield return null;
        int i = 0;
        while (i < 3)
        {
            NumberArray[i].SetActive(true);
            i += 1;
            Debug.Log(i);
            yield return new WaitForSeconds(0.6f);
            foreach (GameObject obj in NumberArray)
            {
                obj.SetActive(false);
            }
        }
        Debug.Log("ѕоехали");
        NumberArray[3].SetActive(true);
        GO?.Invoke();
        RR.ToggleRecord();
        if (RaceCount >= 1)
        {
            RR.LaunchGhost();
        }
        Race = true;
        CurrentTime = 0;
        StartCoroutine(RaceTimer());
        
        TimerPanel.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        CountdownPanel.SetActive(false);
    }
    public void RaceEnd()
    {
        //  онец заезда, подвод итогов, доступ к кнопке restart
        Race = false;
        TimerPanel.SetActive(false);
        RaceCount += 1;
        RaceDone?.Invoke();
        RR.ToggleRecord();
        FinishPanel.SetActive(true);
        if(RaceCount <= 1)
        {
            LapTimes[0].text = "Your time: " + CurrentTime.ToString("F2");
            LapTimes[1].text = "Best time: " + CurrentTime.ToString("F2");
            BestTime = CurrentTime;
        }
        else
        {
            LapTimes[0].text = "Your time: " + CurrentTime.ToString("F2");
            if(BestTime < CurrentTime)
            {
                LapTimes[1].text = "Best time: " + BestTime.ToString("F2");
            }
            else
            {
                LapTimes[1].text = "Best time: " + CurrentTime.ToString("F2");
                BestTime = CurrentTime;
            }

        }

        StartCoroutine(RestartButtonShow());
    }
    IEnumerator RestartButtonShow()
    {
        // задержка чтобы машина остановилась
        yield return new WaitForSeconds(2f);
        Restartbutton.SetActive(true);
    }


    public void RaceRestart()
    {
        // возврат экранов на исходную, запуск предстартовых эффектов
        UICE.ShakeDone = false;
        Restartbutton.SetActive(false);
        FinishPanel.SetActive(false);
        WaitToStart = true;
        StartPanel.SetActive(true);
        StartCoroutine(StartTextProgressionCo());
    }




    IEnumerator RaceTimer()
    {
        // —екундомер дл€ заезда
        while (Race)
        {
            CurrentTime += 0.1f;
            TimerText.text = "Your Time: " + CurrentTime.ToString("F1");
            yield return new WaitForSeconds(0.1f);
        }
    }
}
