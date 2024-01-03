using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Views.UI;

namespace Controllers.UI
{
	public class CollectionUIController : UIController
	{
		private CollectionUIView _collectionUIView;
		
		protected override void Awake()
		{
			base.Awake();
			
			_collectionUIView = new CollectionUIView(UI.rootVisualElement.Q<VisualElement>("collection"));
		} 
	}
}
