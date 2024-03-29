using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI.ViewModels;

namespace Views.UI
{
	public class FurnitureSelectUIView : UIView
	{
		public Action<VisualElement> ScrollToItem;

		private Button _item;

		public FurnitureSelectUIView(VisualElement parentElement, FurnitureSO furnitureSO) : base(parentElement)
		{
			SetVisualElements();
			RegisterCallbacks();

			FurnitureItemViewModel vm = new FurnitureItemViewModel
			{
				Furniture = furnitureSO
			};

			parentElement.dataSource = vm;
		}

		private void SetVisualElements()
		{
			_item = _rootElement.Q<Button>();
		}

		private void RegisterCallbacks()
		{
			_eventRegistry.RegisterCallback<ClickEvent>(_item, evt => ScrollToItem(_rootElement));
		}
	}
}
