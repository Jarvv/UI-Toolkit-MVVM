using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UI.Controls;
using UnityEngine;
using UnityEngine.UIElements;

namespace Views.UI
{
	public class CatalogueUIView : UIView
	{
		private CatalogueScrollView _catalogueScrollView;
		
		private List<FurnitureItemUIView> _furnitureItems;
		
		public CatalogueUIView(VisualElement parentElement) : base(parentElement)
		{
			SetVisualElements();
			_furnitureItems = new List<FurnitureItemUIView>();
		}

		private void SetVisualElements()
		{
			_catalogueScrollView  = _rootElement.Q<CatalogueScrollView>();
		}
		
		public void PopuplateFurnitureList(FurnitureSO[] furniture)
		{
			foreach(FurnitureSO furnitureSO in furniture)
			{
				TemplateContainer furnitureItem = _catalogueScrollView.ItemTemplate.Instantiate();
				FurnitureItemUIView furnitureItemView = new FurnitureItemUIView(furnitureItem, furnitureSO);
				_furnitureItems.Add(furnitureItemView);
				_catalogueScrollView.Add(furnitureItem);
			}
		}

		public override void Disable()
		{
			base.Disable();
			_furnitureItems.ForEach(itemView => itemView.Disable());
		}
		
	}
}
