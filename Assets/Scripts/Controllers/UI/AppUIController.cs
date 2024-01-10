using System;
using System.Collections;
using System.Collections.Generic;
using Controllers.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI;

namespace Controllers.UI
{
	public class AppUIController : UIController
	{
		private AppUIView _appUIView;

		protected override void Awake()
		{
			base.Awake();
			
			_appUIView = new AppUIView(UI.rootVisualElement.Q<VisualElement>("app"));
		}
	}
}
