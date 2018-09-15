using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{

	void Awake ()
	{
        AudioManager.instance.PlayBGM(new SoundInfo("Sounds/Music/Town", 0.8f, true));
	}

    public void OnClickExit()
    {
        WindowManager.instance.CreateMsgBox("Do you really want to exit?", "Notice", MSGBOX_TYPE.ENQUIRE,
        () =>
        {
            Application.Quit();
        });
    }
}
