using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_PlaySound : MonoBehaviour 
{
	//Manager Instance
    Managers.SoundManager soundManager;

    //Actions
    private Action<EventParam> aPlaySound;
    private Action<EventParam> aPlayJumpSound;
    private Action<EventParam> aPlayLandSound;
    private Action<EventParam> aPlayCheerSound;

    void Awake()
    {
        //Init Listeners
        aPlaySound = new Action<EventParam>(PlaySound);
        aPlayJumpSound = new Action<EventParam>(PlayJumpSound);
        aPlayLandSound = new Action<EventParam>(PlayLandSound);
        aPlayCheerSound = new Action<EventParam>(PlayCheerSound);
    }

	void Start () 
	{
		soundManager = Managers.SoundManager.Instance;
	}
	
    void OnEnable()
    {
        //Register With Action variable
        Managers.EventManager.StartListening("PlaySound", aPlaySound);
        Managers.EventManager.StartListening("PlayJumpSound", aPlayJumpSound);
        Managers.EventManager.StartListening("PlayLandSound", aPlayLandSound);
        Managers.EventManager.StartListening("PlayCheerSound", aPlayCheerSound);
    }

    void OnDisable()
    {
        //Un-Register With Action variable
        Managers.EventManager.StopListening("PlaySound", aPlaySound);
        Managers.EventManager.StopListening("PlayJumpSound", aPlayJumpSound);
        Managers.EventManager.StopListening("PlayLandSound", aPlayLandSound);
        Managers.EventManager.StopListening("PlayCheerSound", aPlayCheerSound);
    }

	void PlaySound(EventParam eventParam)
	{
		soundManager.LoadAndPlayClip(eventParam.audioParam);
	}

	void PlayJumpSound(EventParam eventParam)
	{
		soundManager.PlayJumpAudio();
	}

	void PlayLandSound(EventParam eventParam)
	{
		soundManager.PlayLandAudio();
	}

	void PlayCheerSound(EventParam eventParam)
	{
		soundManager.PlayCheerClip();
	}
}
