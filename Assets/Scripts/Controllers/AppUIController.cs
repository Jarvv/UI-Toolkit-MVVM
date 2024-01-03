using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI;

namespace Controllers
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
