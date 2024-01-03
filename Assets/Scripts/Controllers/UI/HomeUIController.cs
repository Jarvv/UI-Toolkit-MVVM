using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI;

namespace Controllers.UI
{
	public class HomeUIController : UIController
	{
		private HomeUIView _homeUIView;
		
		protected override void Awake()
		{
			base.Awake();
			
			_homeUIView = new HomeUIView(UI.rootVisualElement.Q<VisualElement>("home"));
			
			_homeUIView.SetUser(Model.CurrentUser);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			_homeUIView.ViewEvent += OnViewEvent;
		}   

	   	protected override void OnDisable()
		{
			base.OnDisable();
			_homeUIView.ViewEvent -= OnViewEvent;
			
			_homeUIView.Disable();
		}
		
		private void OnViewEvent(Enum viewEvent, object data)
		{
			switch(viewEvent)
			{

			}
		}
	}
	
	public enum HomeUIControllerEvents
	{
		VIEW_MORE
	}
}
