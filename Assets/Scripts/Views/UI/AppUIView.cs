using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI.States;

namespace Views.UI
{
	public class AppUIView : UIView
	{
		private AppViewState _appViewState;
		
		public AppUIView(VisualElement parentElement) : base(parentElement)
		{
			SetVisualElements();
		}

		private void SetVisualElements()
		{
			_appViewState = _rootElement.Q<AppViewState>();
		}
		
		public void UpdateAppViewState(AppViewStates state)
		{
			_appViewState.SetState(state);
		}
	}
}
