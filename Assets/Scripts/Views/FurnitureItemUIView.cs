using ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI.ViewModels;

namespace Views.UI
{
	public class FurnitureItemUIView : UIView
	{	
		private Button _itemButton;
		
		public FurnitureItemUIView(VisualElement parentElement, FurnitureSO furnitureSO) : base(parentElement)
		{
			SetVisualElements();
			RegisterCallbacks();
			
			parentElement.dataSource = new FurnitureItemViewModel(furnitureSO);
		}
		
		private void SetVisualElements()
		{
			_itemButton = _rootElement.Q<Button>();
		}
		
		private void RegisterCallbacks()
		{
			_eventRegistry.RegisterCallback<ClickEvent>(_itemButton, evt => UIEvents.CatalogueItemOpened(((FurnitureItemViewModel)_rootElement.dataSource).Furniture.id));
		} 
	}
}
