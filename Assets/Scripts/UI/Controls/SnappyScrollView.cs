using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Controls
{
	[UxmlElement]
	partial class SnappyScrollView : ScrollView
	{
		[UxmlAttribute]
		public int ItemWidth { get; set; }

		[UxmlAttribute]
		public VisualTreeAsset ItemTemplate;

		public Action<VisualElement> ItemActive;

		private int _currentChild = 0;
		private float _snapThreshold = 100f;
		private bool _isSnapping = false;
		private bool _isDragging = false;
		private Vector2 _lastScrollPosition;
		private Vector2 _to;
		private float _tolerance = 0.1f;

		private IVisualElementScheduledItem _scrollTask;

		public SnappyScrollView() : this(ScrollViewMode.Horizontal) { }

		public SnappyScrollView(ScrollViewMode scrollViewModel) : base(ScrollViewMode.Horizontal)
		{
			RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);

			elasticity = 0;
			scrollDecelerationRate = 0;
			nestedInteractionKind = NestedInteractionKind.StopScrolling;
		}

		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			UnregisterCallback<AttachToPanelEvent>(OnAttachToPanel);
			this.schedule.Execute(Update).Every(16);
		}

		private void Update()
		{
			float velocity = (scrollOffset.x - _lastScrollPosition.x) / Time.deltaTime;
			_lastScrollPosition = scrollOffset;

			float snapPosition = CalculateSnapPosition();

			if (!_isSnapping && Mathf.Abs(velocity) < _snapThreshold && Input.touchCount == 0)
			{
				_isSnapping = true;
				_isDragging = false;
				_to = new Vector2(snapPosition, 0);

				_scrollTask?.Pause();
				_scrollTask = null;
				_scrollTask = this.schedule.Execute(ScrollTo).Every(16).Until(() => Mathf.Abs(scrollOffset.x - _to.x) < _tolerance || _isDragging);
			}
			else if (Mathf.Abs(velocity) >= _snapThreshold)
			{
				_isSnapping = false;

				if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
					_isDragging = true;
			}
		}

		private float CalculateSnapPosition()
		{
			int itemCount = contentContainer.childCount;

			if (itemCount == 0) return 0;

			float scrollPosition = scrollOffset.x;

			float closestIndex = scrollPosition / ItemWidth;

			if (closestIndex > _currentChild + 0.5)
				closestIndex = Mathf.Clamp(Mathf.CeilToInt(closestIndex), 0, itemCount - 1);
			else if (closestIndex < _currentChild - 0.5)
				closestIndex = Mathf.Clamp(Mathf.FloorToInt(closestIndex), 0, itemCount - 1);
			else
				closestIndex = Mathf.Clamp(Mathf.RoundToInt(scrollPosition / ItemWidth), 0, itemCount - 1);

			if (_currentChild != (int)closestIndex)
			{
				_currentChild = (int)closestIndex;
				ItemActive(Children().ToList()[_currentChild]);
			}

			return closestIndex * ItemWidth;
		}

		private void ScrollTo()
		{
			scrollOffset = Vector2.Lerp(scrollOffset, _to, 0.2f);
		}

		public void AddItem(TemplateContainer item)
		{
			Add(item);

			horizontalScroller.value = 0;
			scrollOffset = Vector2.zero;

			MarkDirtyRepaint();

			ItemActive(Children().ToList()[_currentChild]);
		}

		public void AddMargins()
		{
			RegisterCallback<GeometryChangedEvent>((ev) =>
			{
				List<VisualElement> children = Children().ToList();
				for (int i = 0; i < children.Count; i++)
				{
					VisualElement item = children[i];

					float margin = (resolvedStyle.width / 2f) - ((ItemWidth - 91f) / 2);

					if (i == 0)
						item.style.marginLeft = margin;
					else
					{
						Children().ToList()[i - 1].AddToClassList("item");
						Children().ToList()[i - 1].style.marginRight = StyleKeyword.Null;
						item.style.marginRight = margin;
					}
				}
			});
		}

		public void ScrollToItem(VisualElement element)
		{
			int scrollIndex = Children().ToList().FindIndex(x => x == element);
			_scrollTask?.Pause();
			_scrollTask = null;

			int segments = contentContainer.childCount - 1;
			float width = horizontalScroller.slider.range / segments;
			float normal = Mathf.InverseLerp(0f, horizontalScroller.slider.range, width / segments + (scrollIndex * ItemWidth));
			float bValue = Mathf.Lerp(0f, segments, normal);
			_to = new Vector2(Mathf.RoundToInt(bValue) * width, 0);
			_scrollTask = this.schedule.Execute(ScrollTo).Every(16).Until(() => Mathf.Abs(scrollOffset.x - _to.x) < _tolerance);
		}
	}
}

