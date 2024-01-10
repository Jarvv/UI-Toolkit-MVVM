using System.Collections;
using System.Collections.Generic;
using Model;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI;

namespace Controllers.UI
{
	 public class CatalogueUIController : UIController
	 {
		private CatalogueUIView _catalogueUIView;

		protected override void Awake()
		{
			base.Awake();
			
			_catalogueUIView = new CatalogueUIView(UI.rootVisualElement.Q<VisualElement>("catalogue"));
		}
		
		private void Start()
		{
			FurnitureControllerEvents.LoadFurnitureData(PopulateCatalogue);
		}

		private void PopulateCatalogue(FurnitureSO[] furnitureSOs)
		{
			_catalogueUIView.PopuplateFurnitureList(furnitureSOs);
		}
	 }
}
