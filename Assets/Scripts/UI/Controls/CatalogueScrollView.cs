using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Controls
{
	public class CatalogueScrollView : ScrollView
	{
		public new class UxmlFactory :  UxmlFactory<CatalogueScrollView, UxmlTraits> {}
		
		public new class UxmlTraits : BaseListView.UxmlTraits
		{
			private UxmlAssetAttributeDescription<VisualTreeAsset> m_ItemTemplate = new UxmlAssetAttributeDescription<VisualTreeAsset>
			{
				name = "item-template"
			};

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				CatalogueScrollView CatalogueScrollView = ve as CatalogueScrollView;
				if (m_ItemTemplate.TryGetValueFromBag(bag, cc, out var value))
				{
					CatalogueScrollView.ItemTemplate = value;
				}
			}
		}
		
		public VisualTreeAsset ItemTemplate;
	}
}
