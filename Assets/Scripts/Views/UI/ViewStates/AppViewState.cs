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
				{AppViewStates.CATALOGUE, "app--catalogue"},
			};
			
			State = AppViewStates.CATALOGUE;
		}
		
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			UnregisterCallback<AttachToPanelEvent>(OnAttachToPanel);
			SetState(State);
		}
	}

	public enum AppViewStates
	{
		CATALOGUE
	}
}
