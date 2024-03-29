using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Views.UI
{
	public class SplashUIView : UIView
	{
		private Button _enterButton;

		public SplashUIView(VisualElement parentElement) : base(parentElement)
		{
			SetVisualElements();
			RegisterCallbacks();
		}

		private void SetVisualElements()
		{
			_enterButton = _rootElement.Q("enter_button").Q<Button>();
		}

		private void RegisterCallbacks()
		{
			_eventRegistry.RegisterCallback<ClickEvent>(_enterButton, ev => UIEvents.Enter());
		}
	}
}
