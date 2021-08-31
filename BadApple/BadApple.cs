using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;
using DarkLog;
using Tutorials;
using Sounds;

namespace BadApple
{

    public class BadAppleMod : MonoBehaviour
    {
        private ModLog log;
        private int frameCountdown = 0;
        private GameObject videoGameObject;
        private AudioClip audioClip = null;
        private AudioSource audioSource;
        private VideoPlayer vp;

        //Called when game object is started
        public void Start()
        {
            log = new ModLog("BadApple");
            GameEvents.Game.OnSessionStart.AddListener(StartCountdown);
            if (audioClip == null)
            {
                StartCoroutine(LoadSound());
            }
            log.Log("Start");
        }

        private IEnumerator LoadSound()
        {
            using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip("file:///Addons/BadApple/BadApple-audio.mp3", AudioType.MPEG))
            {
                yield return uwr.SendWebRequest();
                if (uwr.isHttpError || uwr.isNetworkError)
                {
                    log.Log("Failed to load");
                }
                else
                {
                    audioClip = DownloadHandlerAudioClip.GetContent(uwr);
                }
            }
        }
        
        private void StartCountdown(NetworkLogic.SessionMode mode)
        {
            frameCountdown = 250;
        }

        private void ShowTutorial()
        {
            log.Log("Tutorial shown");
            TutorialMMXPanel tutorial = TutorialMMXPanel.Create(new BadAppleTutorial(log, PlayVideo));
        }

        private void PlayVideo()
        {
            if (vp == null)
            {
                GameObject camera = Camera.main.gameObject;
                vp = camera.AddComponent<VideoPlayer>();
                vp.playOnAwake = false;
                vp.renderMode = VideoRenderMode.CameraNearPlane;
                vp.targetCameraAlpha = 0.5f;
                vp.url = "file:///Addons/BadApple/BadApple-video.mp4";                
                vp.prepareCompleted += VideoReady;
                videoGameObject = new GameObject();
                videoGameObject.transform.parent = GameLogic.LocalPlayer.transform;
                audioSource = videoGameObject.AddComponent<AudioSource>();
                audioSource.clip = audioClip;
                audioSource.volume = 0.5f;
            }
            vp.Prepare();
        }

        private void VideoReady(VideoPlayer source)
        {
            source.Play();
            audioSource.Play();
        }

        //Called once per frame
        public void Update()
        {
            if (frameCountdown > 0)
            {
                frameCountdown--;
                if (frameCountdown == 0)
                {
                    ShowTutorial();
                }
            }
        }

        //Called once per frame after Update
        public void LateUpdate()
        {
        }

        //Called once per physics frame (50hz)
        public void FixedUpdate()
        {
            //Log("FixedUpdate");
        }

        //Called to draw the old GUI. Obsolete.
        public void OnGUI()
        {
        }

        //Called when the mod object is enabled
        public void OnEnable()
        {
        }

        //Called when the mod object is disabled.
        public void OnDisable()
        {
        }
    }
}