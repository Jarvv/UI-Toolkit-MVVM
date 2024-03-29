using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Views.UI.States
{

	[UxmlElement]
	public partial class AppViewState : ViewState
	{
		[UxmlAttribute]
		public AppViewStates State
		{
			get; private set;
		}

		public AppViewState() : base()
		{
			RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);

			// Init states
			EnumStates = new Dictionary<Enum, string>()
			{
				{AppViewStates.SPLASH, "app--splash"},
				{AppViewStates.HOME, "app--home"},
				{AppViewStates.CATALOGUE, "app--catalogue"},
				{AppViewStates.FURNITURE, "app--furniture"},
				{AppViewStates.AR, "app--ar"},
			};

			State = AppViewStates.SPLASH;
		}

		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			UnregisterCallback<AttachToPanelEvent>(OnAttachToPanel);
			SetState(State);
		}
	}

	public enum AppViewStates
	{
		SPLASH, HOME, CATALOGUE, FURNITURE, AR
	}
}
