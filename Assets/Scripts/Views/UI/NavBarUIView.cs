using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace Views.UI
{
	public class NavBarUIView : UIView
	{
		private Button _activeButton;

		public NavBarUIView(VisualElement parentElement, NavBarItemSO[] navBarItems) : base(parentElement)
		{
			SetVisualElements(navBarItems);
			RegisterCallbacks(navBarItems);
		}

		private void SetVisualElements(NavBarItemSO[] navBarItems)
		{
			for (int i = 0; i < navBarItems.Length; i++)
			{
				navBarItems[i].NavButton = _rootElement.Q<Button>(navBarItems[i].ElementID);

				navBarItems[i].NavButton.dataSource = navBarItems[i];
			}

			_activeButton = navBarItems.First(item => item.State == States.AppViewStates.HOME).NavButton;
			_activeButton.AddToClassList("active");
		}

		private void RegisterCallbacks(NavBarItemSO[] navBarItems)
		{
			for (int i = 0; i < navBarItems.Length; i++)
			{
				_eventRegistry.RegisterCallback<ClickEvent>(navBarItems[i].NavButton, evt =>
				{
					Button button = (Button)evt.target;

					_activeButton.RemoveFromClassList("active");
					_activeButton = button;
					_activeButton.AddToClassList("active");

					UIEvents.NavButtonTapped(((NavBarItemSO)button.dataSource).State);
				});
			}
		}
	}
}
