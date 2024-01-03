using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
	public static Action<Enum, object> ControllerEvent;
	
	[HideInInspector] public Model Model => GameObject.FindWithTag("Model").GetComponent<Model>();

	protected virtual void OnEnable()
	{
		ControllerEvent += OnControllerEvent;
	}

	protected virtual void OnDisable()
	{
		ControllerEvent -= OnControllerEvent;
	}

	protected virtual void OnControllerEvent(Enum controllerEvent, object data) { }
}

