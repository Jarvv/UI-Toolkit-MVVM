using ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI.ViewModels;

namespace Views.UI
{
	public class FurnitureItemUIView : UIView
	{
		private Button _itemButton;

		private FurnitureItemViewModel _furnitureItemViewModel => (FurnitureItemViewModel)_rootElement.dataSource;

		public FurnitureItemUIView(VisualElement parentElement, FurnitureSO furnitureSO) : base(parentElement)
		{
			SetVisualElements();
			RegisterCallbacks();

			parentElement.dataSource = new FurnitureItemViewModel
			{
				Furniture = furnitureSO
			};
		}

		private void SetVisualElements()
		{
			_itemButton = _rootElement.Q<Button>();
		}

		private void RegisterCallbacks()
		{
			_eventRegistry.RegisterCallback<ClickEvent>(_itemButton, evt => UIEvents.CatalogueItemOpened(_furnitureItemViewModel.Furniture.id));
		}
	}
}
