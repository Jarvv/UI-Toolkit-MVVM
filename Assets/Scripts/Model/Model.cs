using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class Model : MonoBehaviour
{
	public List<UserSO> Users;
	
	private UserSO _currentUser => Users[0];
	
	public UserSO CurrentUser 
	{
		get => _currentUser;
	}
}
