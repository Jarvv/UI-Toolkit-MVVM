using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI.ViewModels;

namespace Views.UI
{
	public class HomeUIView : UIView
	{
		private HomeViewModel _homeViewModel;
		
		private Button _viewMoreButton;
		
		public HomeUIView(VisualElement parentElement) : base(parentElement)
		{
			SetVisualElements();
			RegisterCallbacks();
			
			_homeViewModel = new HomeViewModel();
			parentElement.dataSource = _homeViewModel;
		}

		private void SetVisualElements()
		{
			_viewMoreButton = rootElement.Q<Button>("view_more_button");
		}
		
		private void RegisterCallbacks()
		{
			eventRegistry.RegisterCallback<ClickEvent>(_viewMoreButton, evt => ViewEvent?.Invoke(HomeUIViewEvents.VIEW_MORE, null));
		}
		
		public void SetUser(UserSO user)
		{
			_homeViewModel.User = user;
		} 
	}
	
	public enum HomeUIViewEvents
	{
		VIEW_MORE
	}
}
