using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Controls
{
	[UxmlElement]
	partial class CatalogueScrollView : ScrollView
	{
		[UxmlAttribute]
		public VisualTreeAsset ItemTemplate;
		
		public CatalogueScrollView() : this(ScrollViewMode.Vertical) { }
		
		public CatalogueScrollView(ScrollViewMode scrollViewModel) : base(ScrollViewMode.Vertical) { }
	}
}
