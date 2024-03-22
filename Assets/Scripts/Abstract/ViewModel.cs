using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ViewModel : INotifyBindablePropertyChanged, IDataSourceViewHashProvider
{
	public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;
	private long m_ViewVersion;

	public bool SetProperty<T>(ref T field, T newValue)
	{
		if(!EqualityComparer<T>.Default.Equals(field, newValue))
		{
			field = newValue;
		   	Notify();
			return true;
		}

		return false;
	}	
	
	private void Notify([CallerMemberName] string property = "")
	{
		propertyChanged?.Invoke(this, new BindablePropertyChangedEventArgs(property));
	}
	
	public void Publish()
	{
		++m_ViewVersion;
	}
	
	public long GetViewHashCode()
	{
		return m_ViewVersion;
	}
}
