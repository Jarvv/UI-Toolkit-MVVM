using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI;

namespace Controllers.UI
{
	public class FurnitureUIController : UIController
	{
		private FurnitureUIView _furnitureUIView;
		
		protected override void Awake()
		{
			base.Awake();
			
			_furnitureUIView = new FurnitureUIView(UI.rootVisualElement.Q<VisualElement>("furniture"));
		} 
		
		private void OnEnable()
		{
			UIEvents.CatalogueItemOpened += OnCatalogueItemOpened;
		}

	   	private void OnDisable()
		{
			UIEvents.CatalogueItemOpened -= OnCatalogueItemOpened;
		}
		
		private void OnCatalogueItemOpened(string id)
		{
			FurnitureControllerEvents.GetFurnitureItem(id, (FurnitureSO furnitureItem) => 
			{
				
			});
		}
	}
}
