﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventMng
{
	private EventMng(){}

	private static Dictionary<string,List<object>> eventDict = new Dictionary<string, List<object>>();

	public static void Clear()
	{
		eventDict.Clear();
	}

	public static void Add(string eventName,Action callback)
	{
		addEvent(eventName,callback);
	}

	public static void Add<T>(string eventName,Action<T> callback)
	{
		addEvent(eventName,callback);
	}

	public static void Add<T,Y>(string eventName,Action<T,Y> callback)
	{
		addEvent(eventName,callback);
	}

	private static void addEvent(string eventName,object callback)
	{
		if(!eventDict.ContainsKey(eventName))
		{
			eventDict[eventName]=new List<object>();
		}
		eventDict[eventName].Add(callback);
	}
	public static void Run(string eventName)
	{
		if( !eventDict.ContainsKey(eventName) || eventDict[eventName].Count==0)
		{
			Debug.LogWarning("[EventMng.Run]无此事件监听:"+eventName);
			return;
		}
		List<object> events = eventDict[eventName];
		foreach(object obj in events)
		{
			Action callback = (Action)obj;
			callback();
		}
	}

	public static void Run<T>(string eventName,T val)
	{
		if( !eventDict.ContainsKey(eventName) || eventDict[eventName].Count==0)
		{
			Debug.LogWarning("[EventMng.Run]无此事件监听:"+eventName);
			return;
		}
		List<object> events = eventDict[eventName];
		foreach(object obj in events)
		{
			Action<T> callback=null;
			try
			{
				callback = (Action<T>)obj;
			}
			catch(Exception e)
			{
				Debug.LogError(e);
				return;
			}
			callback(val);
		}
	}
	

	public static void Run<T,Y>(string eventName,T v1,Y v2 )
	{
		if( !eventDict.ContainsKey(eventName) || eventDict[eventName].Count==0)
		{
			Debug.LogWarning("[EventMng.Run]无此事件监听:"+eventName);
			return;
		}
		List<object> events = eventDict[eventName];
		foreach(object obj in events)
		{
			Action<T,Y> callback=null;
			try
			{
				callback = (Action<T,Y>)obj;
			}
			catch(Exception e)
			{
				Debug.LogError(e);
				return;
			}
			callback(v1,v2);
		}
	}

	public static void Remove(string eventName)
	{
		eventDict[eventName].Clear();
	}
}
