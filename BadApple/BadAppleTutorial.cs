using System;
using Tutorials;
using UI.MMX.Data;
using UnityEngine;
using DarkLog;

namespace BadApple
{
    class BadAppleTutorial : TutorialFSM
    {
        private TutorialPage page1;
		private ModLog log;
        private Action playVideo;

        public BadAppleTutorial(ModLog log, Action playVideo)
        {
            this.playVideo = playVideo;
			this.log = log;
			Page1();
		}

        private void Page1()
        {
            page1 = new TutorialPage("Test")
            {
                GetPageContent = delegate(ContentData content)
                {
                    content.Add(new VerticalLayout());
                    content.Add(new Label("Have you been a bad apple??"));
                    content.Add(new HorizontalLayout());
                    content.Add(new Button("I've been a bad apple.")
                    {
                        OnClicked = delegate
                        {
                            playVideo();
                            EndTutorial();
                        }
                    });
                    content.Add(new Button("No, I don't know what you're talking about!")
                    {
                        OnClicked = delegate
                        {
                            EndTutorial();
                        }
                    });
					content.Add(new EndLayout());
                },
                OnEnter = delegate
                {
                    CursorManager.RequestCursor("BadApple");
                },
                OnLeave = delegate
                {
                    CursorManager.RemoveCursorRequest("BadApple");
                }
            };
            AddPage(page1);
            SetInitialPage(page1);
        }
    }
}
