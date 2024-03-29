using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Views.UI.States
{
	[UxmlElement]
	public partial class ARViewState : ViewState
	{
		[UxmlAttribute]
		public ARViewStates State
		{
			get; private set;
		}

		public ARViewState()
		{
			RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);

			// Init states
			EnumStates = new Dictionary<Enum, string>()
			{
				{ARViewStates.DEFAULT, "ar--default"},
				{ARViewStates.SELECT, "ar--select"},
				{ARViewStates.TAP_TO_PLACE, "ar--tap_to_place"},
				{ARViewStates.POSITION, "ar--position"},
			};

			State = ARViewStates.SELECT;
		}

		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			UnregisterCallback<AttachToPanelEvent>(OnAttachToPanel);
			SetState(State);
		}
	}

	public enum ARViewStates
	{
		DEFAULT, SELECT, TAP_TO_PLACE, POSITION
	}
}
