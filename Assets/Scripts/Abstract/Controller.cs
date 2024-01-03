using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
	public static Action<Enum, object> ControllerEvent;

	protected virtual void OnEnable()
	{
		ControllerEvent += OnControllerEvent;
		View.ViewEvent += OnViewEvent;
		View.ViewCallbackEvent += OnViewCallbackEvent;
	}

	protected virtual void OnDisable()
	{
		ControllerEvent -= OnControllerEvent;
		View.ViewEvent -= OnViewEvent;
		View.ViewCallbackEvent -= OnViewCallbackEvent;
	}

	protected virtual void OnControllerEvent(Enum controllerEvent, object data) { }

	protected virtual void OnViewEvent(Enum viewEvent, object data) { }

	protected virtual void OnViewCallbackEvent(Enum viewEvent, object data, Action<object> callback) { }
}

