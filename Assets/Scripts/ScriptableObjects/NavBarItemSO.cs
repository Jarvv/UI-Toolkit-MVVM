using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI.States;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "NavBarItemSO")]
	public class NavBarItemSO : ScriptableObject
	{
		[HideInInspector] public string id = Guid.NewGuid().ToString();	
		public string Label;
		public Sprite Icon;
		public AppViewStates State;
		public string ElementID;
		[HideInInspector] public Button NavButton;
	}
}
