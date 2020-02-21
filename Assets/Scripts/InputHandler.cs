using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image timerImage;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private Rocket rocket;
    [SerializeField] private CameraMovement camera;

    [SerializeField] private int maxSwipesForLaunch = 10;

    [SerializeField] private float[] timeIntervals;

    private float score = 0;
    private bool roundActive = false;
    private int succesfulSwipeCount = 0;
    private bool dragStarted = false;
    private bool firstTapDone = false;

    private Vector2 pressPosition;
    private Vector2 liftedPosition;

    private DateTime pressedTimeStamp;
    private DateTime previousSwipeEndTimeStamp;

    void Start()
    {
        if(timeIntervals.Length != maxSwipesForLaunch)
        {
            Debug.LogError("Max swipes and time intervals are not matching");
        }
    }

    public void StartGame()
    {
        roundActive = true;
        dragStarted = false;
        firstTapDone = false;
        score = 0;
        scoreText.text = "Score : " + score.ToString();
        timerImage.fillAmount = 1;
        succesfulSwipeCount = 0;

        rocket.ResetRocket();
        camera.ResetCamera();
    }

    void Update()
    {
        if (roundActive && (succesfulSwipeCount == 0 ? dragStarted : true))
        {
            timerImage.fillAmount = 1 - (float)(DateTime.Now - previousSwipeEndTimeStamp).TotalMilliseconds / (timeIntervals[succesfulSwipeCount] * 1000);

            if ((DateTime.Now - previousSwipeEndTimeStamp).TotalMilliseconds >= timeIntervals[succesfulSwipeCount] * 1000)
            {
                roundActive = false;
                dragStarted = false;
                firstTapDone = false;
                Debug.Log("Launch failed!!");
                gameOverPanel.SetActive(true);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (roundActive)
        {
            dragStarted = true;
            pressPosition = eventData.position;
            pressedTimeStamp = DateTime.Now;

            if (!firstTapDone)
            {
                firstTapDone = true;
                previousSwipeEndTimeStamp = pressedTimeStamp;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (roundActive)
        {
            liftedPosition = eventData.position;

            if (pressPosition.y - liftedPosition.y > 250)
            {
                dragStarted = false;
                previousSwipeEndTimeStamp = DateTime.Now;

                Debug.Log("Swipe successful!!");

                if(succesfulSwipeCount < maxSwipesForLaunch - 1)
                    rocket.BurstOnce();

                score += (timeIntervals[succesfulSwipeCount] - (float)(previousSwipeEndTimeStamp - pressedTimeStamp).TotalSeconds);

                Debug.Log("Current score : " + (timeIntervals[succesfulSwipeCount] - (float)(previousSwipeEndTimeStamp - pressedTimeStamp).TotalSeconds));
                scoreText.text = "Score : " + score.ToString();

                succesfulSwipeCount++;
                if (succesfulSwipeCount >= maxSwipesForLaunch)
                {
                    Debug.Log("RocketLaunched!!");
                    rocket.EnableTrail();
                    roundActive = false;
                    rocket.LaunchRocket();
                    camera.WatchRocket();
                }
            }
        }
    }
}
