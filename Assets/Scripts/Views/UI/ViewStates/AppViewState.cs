using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Views.UI.States
{
	public class AppViewState : ViewState
	{
		public new class UxmlFactory : UxmlFactory<AppViewState, UxmlTraits> { }

		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			UxmlEnumAttributeDescription<AppViewStates> _state =
				new UxmlEnumAttributeDescription<AppViewStates> { name = "state", defaultValue = AppViewStates.HOME };

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				var AppViewState = ve as AppViewState;

				// Init states
				AppViewState.EnumStates = new Dictionary<Enum, string>()
				{
					{AppViewStates.HOME, "app--home"},
					{AppViewStates.COLLECTION, "app--collection"},
				};

				AppViewState.State = _state.GetValueFromBag(bag, cc);
			}
		}

		public AppViewStates State
		{
			get; private set;
		}
		
		public AppViewState()
		{
			RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
		}
		
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			UnregisterCallback<AttachToPanelEvent>(OnAttachToPanel);
			SetState(State);
		}
	}

	public enum AppViewStates
	{
		HOME, COLLECTION
	}
}
