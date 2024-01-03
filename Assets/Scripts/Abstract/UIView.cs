using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Util;

public abstract class UIView
{
	public Action<Enum, object> ViewEvent;
	
	protected UIDocument UI;
	
	protected VisualElement rootElement;
	
	protected EventRegistry eventRegistry;
	
	public UIView(VisualElement parentElement)
	{
		rootElement = parentElement;
		eventRegistry = new EventRegistry();
	}

	public virtual void Disable()
	{
		eventRegistry.Dispose();
	}
}