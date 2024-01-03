using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIController : Controller
{
	protected UIDocument UI;
	
	protected virtual void Awake()
	{
		UI = GetComponentInParent<UIDocument>();

		if (UI == null)
		{
			Debug.LogError("Component is not a child of the UI");
		}
	}
}
