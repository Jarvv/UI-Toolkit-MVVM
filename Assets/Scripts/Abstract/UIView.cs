using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Util;

public abstract class UIView
{
	protected UIDocument UI;
	
	protected VisualElement rootElement;
	
	protected EventRegistry eventRegistry;
	
	public UIView(VisualElement parentElement)
	{
		rootElement = parentElement;
		eventRegistry = new EventRegistry();
	}
	
	protected virtual void Disable()
	{
		eventRegistry.Dispose();
	}
}