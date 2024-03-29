﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextSpeech;
using UnityEngine.Android;

public class VoiceController : MonoBehaviour
{
    const string LANG_CODE = "en-US";

    [SerializeField] Text uiText;

    private void Start()
    {
        Setup(LANG_CODE);

#if UNITY_ANDROID
        SpeechToText.instance.onPartialResultsCallback = OnPartialSpeechResult;
#endif
        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.instance.onDoneCallback = OnSpeakStop;

        CheckPermission();
    }

    void CheckPermission() {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone)) {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
    }

    #region Text to Speech

    public void StartSpeaking(string message) {
        TextToSpeech.instance.StartSpeak(message);
    }

    public void StopSpeaking()
    {
        TextToSpeech.instance.StopSpeak();
    }

    void OnSpeakStart()
    {
        Debug.Log("Talking started...");
    }

    void OnSpeakStop()
    {
        Debug.Log("Talking stopped...");
    }

    #endregion

    #region Speech To Text

    public void StartListening()
    {
        SpeechToText.instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.instance.StopRecording();
    }

    void OnFinalSpeechResult(string result)
    {
        uiText.text = result;
        StartSpeaking(result);
    }

    void OnPartialSpeechResult(string result)
    {
        uiText.text = result;
    }

    #endregion

    void Setup(string langCode) {
        TextToSpeech.instance.Setting(LANG_CODE, 1, 1);
        SpeechToText.instance.Setting(LANG_CODE);
    }
}
