using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace Views.UI
{
	public class CatalogueUIView : UIView
	{
		private VisualElement _contentContainer;
		
		public CatalogueUIView(VisualElement parentElement) : base(parentElement)
		{
			SetVisualElements();
		}

		private void SetVisualElements()
		{
			_contentContainer  = _rootElement.Q<ListView>();
		}
		
		public void PopuplateFurnitureList(FurnitureSO[] furniture)
		{
			
		}
	}
}
