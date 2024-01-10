using ScriptableObjects;
using UnityEngine.UIElements;
using Views.UI.ViewModels;

namespace Views.UI
{
	public class FurnitureItemUIView : UIView
	{
		private FurnitureItemViewModel _furnitureItemViewModel;
		
		private Button _itemButton;
		
		public FurnitureItemUIView(VisualElement parentElement) : base(parentElement)
		{
			SetVisualElements();
			RegisterCallbacks();
			
			_furnitureItemViewModel = new FurnitureItemViewModel();
			parentElement.dataSource = _furnitureItemViewModel;
		}
		
		private void SetVisualElements()
		{
			_itemButton = _rootElement.Q<Button>();
		}
		
		private void RegisterCallbacks()
		{
			_eventRegistry.RegisterCallback<ClickEvent>(_itemButton, evt => UIEvents.FurnitureItem(_furnitureItemViewModel.Furniture.id));
		} 
		
		public void SetFurnitureItem(FurnitureSO furnitureSO)
		{
			_furnitureItemViewModel.Furniture = furnitureSO;
		}
	}
}
