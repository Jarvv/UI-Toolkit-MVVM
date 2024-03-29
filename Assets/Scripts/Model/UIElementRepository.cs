using ScriptableObjects;
using UnityEngine;

namespace Model 
{
	public class UIElementRepository
	{
		private NavBarItemSO[] _navBarItems;
		
		public UIElementRepository()
		{
			_navBarItems = LoadNavBarData();
		}
		  
		private NavBarItemSO[] LoadNavBarData()
		{
			return Resources.LoadAll<NavBarItemSO>("NavBar");
		}
		
		public NavBarItemSO[] GetAllNavItems()
		{
			return _navBarItems;
		}
	}
}
