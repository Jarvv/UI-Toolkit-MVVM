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
	
	protected VisualElement _rootElement;
	
	protected EventRegistry _eventRegistry;
	
	public UIView(VisualElement parentElement)
	{
		_rootElement = parentElement;
		_eventRegistry = new EventRegistry();
	}

	public virtual void Disable()
	{
		_eventRegistry.Dispose();
	}
}