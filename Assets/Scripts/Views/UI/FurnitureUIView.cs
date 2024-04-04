using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI.ViewModels;

namespace Views.UI
{
	public class FurnitureUIView : UIView
	{
		private Button _backButton;

		private FurnitureItemViewModel _furnitureItemViewModel => (FurnitureItemViewModel)_rootElement.dataSource;

		public FurnitureUIView(VisualElement parentElement) : base(parentElement)
		{
			SetVisualElements();
			RegisterCallbacks();

			parentElement.dataSource = new FurnitureItemViewModel();
		}

		private void SetVisualElements()
		{
			_backButton = _rootElement.Q("back_button").Q<Button>();
		}

		private void RegisterCallbacks()
		{
			_eventRegistry.RegisterCallback<ClickEvent>(_backButton, ev => UIEvents.FurnitureBackButtonTapped());
		}

		public void SetFurniture(FurnitureSO furnitureSO)
		{
			_furnitureItemViewModel.Furniture = furnitureSO;
		}
	}
}
