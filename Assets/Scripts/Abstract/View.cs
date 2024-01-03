using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
	public static Action<Enum, object> ViewEvent;
	public static Action<Enum, object, Action<object>> ViewCallbackEvent;
}