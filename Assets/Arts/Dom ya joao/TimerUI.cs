using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private bool showMilliseconds = true;
    [SerializeField] private float initialTime = 0f;
    [SerializeField] private bool isCountDown = true;
    [SerializeField] private bool autoStart = true;
    public UnityEvent OnTimerFinished;

    private float currentTime;
    private bool isRunning = false;

    private void Start()
    {
        currentTime = initialTime;
        UpdateTimerDisplay(currentTime);

        if (autoStart)
        {
            StartTimer();
        }
    }

    private void Update()
    {
        if (!isRunning) return;

        if (isCountDown)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isRunning = false;
                OnTimerFinished?.Invoke();
            }
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        UpdateTimerDisplay(currentTime);
    }

    private void UpdateTimerDisplay(float timeToDisplay)
    {
        if (timeToDisplay < 0) timeToDisplay = 0;

        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if (showMilliseconds)
        {
            int milliseconds = Mathf.FloorToInt((timeToDisplay * 100) % 100);
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
        else
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    public void StartTimer() => isRunning = true;
    public void PauseTimer() => isRunning = false;
    public void ResumeTimer() => isRunning = true;
    public void SubtrairTempo(float segundos){
        currentTime -= segundos;

        if (currentTime < 0f){
            currentTime = 0f;
        }

        UpdateTimerDisplay(currentTime);
    }

    public void StopTimer(){
        isRunning = false;
        currentTime = initialTime;
        UpdateTimerDisplay(currentTime);
    }

    public void SetTime(float seconds){
        currentTime = seconds;
        UpdateTimerDisplay(currentTime);
    }

    public float GetCurrentTime() => currentTime;
}