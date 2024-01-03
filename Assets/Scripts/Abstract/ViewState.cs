using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ViewState : VisualElement
{
	public Dictionary<Enum, string> EnumStates = new Dictionary<Enum, string>();

	protected Enum _state;
	
	public void SetState(Enum state)
	{
		if(parent == null) return;
		
		// Remove all enum states
		foreach (KeyValuePair<Enum, string> kvp in EnumStates)
		{
			parent.RemoveFromClassList(EnumStates[kvp.Key]);
		}

		// Add the new state
		_state = state;

		parent.AddToClassList(EnumStates[_state]);
	}
}
