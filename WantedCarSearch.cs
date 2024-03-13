namespace WantedCarSearchNamespace;

using System;
using System.Collections.Generic;

interface IObserver
{
	void Notify(int licensePlate, string ownerName, string cameraAddress);
}

class ConsoleLogger : IObserver
{
	public void Notify(int licensePlate, string ownerName, string cameraAddress)
	{
		Console.WriteLine($"Виявлено розшукуване авто: Номер {licensePlate}, Власник {ownerName}, Адреса камери {cameraAddress}");
	}
}

class NotificationSystem
{
	private readonly List<IObserver> observers = new();

	public void Attach(IObserver observer)
	{
		if (observer != null) observers.Add(observer);
	}

	public void Detach(IObserver observer)
	{
		if (observer != null)
			observers.Remove(observer);
	}

	public void NotifyObservers(int licensePlate, string ownerName, string cameraAddress)
	{
		foreach (var observer in observers)
		{
			observer.Notify(licensePlate, ownerName, cameraAddress);
		}
	}
}

class Node(int licensePlate, string ownerName)
{
	public int LicensePlate { get; } = licensePlate;
	public string OwnerName { get; } = ownerName ?? throw new ArgumentNullException(nameof(ownerName));
	public Node? Left { get; set; }
	public Node? Right { get; set; }
}

class BinarySearchTree
{
	private Node? root;
	private readonly NotificationSystem notificationSystem;

	public BinarySearchTree(NotificationSystem notificationSystem)
	{
		this.notificationSystem = notificationSystem ?? throw new ArgumentNullException(nameof(notificationSystem));
	}

	public void Insert(int licensePlate, string ownerName)
	{
		if (string.IsNullOrWhiteSpace(ownerName))
			throw new ArgumentException("Ім'я власника не може бути порожнім", nameof(ownerName));

		root = InsertRecursive(root, licensePlate, ownerName);
	}

	public void Search(int licensePlate, string cameraAddress)
	{
		Node? result = SearchRecursive(root, licensePlate);
		if (result != null)
		{
			notificationSystem.NotifyObservers(licensePlate, result.OwnerName, cameraAddress);
		}
		else
		{
			Console.WriteLine($"Автомобіль з номером {licensePlate} не знайдено в базі даних.");
		}
	}

	private static Node? InsertRecursive(Node? current, int licensePlate, string ownerName)
	{
		if (current == null)
		{
			return new Node(licensePlate, ownerName);
		}

		if (licensePlate < current.LicensePlate)
		{
			if (current.Left == null)
			{
				current.Left = new Node(licensePlate, ownerName);
			}
			else
			{
				InsertRecursive(current.Left, licensePlate, ownerName);
			}
		}
		else
		{
			if (current.Right == null)
			{
				current.Right = new Node(licensePlate, ownerName);
			}
			else
			{
				InsertRecursive(current.Right, licensePlate, ownerName);
			}
		}

		return current;
	}

	private static Node? SearchRecursive(Node? current, int licensePlate)
	{
		if (current == null || current.LicensePlate == licensePlate)
		{
			return current;
		}

		if (licensePlate < current.LicensePlate)
		{
			return SearchRecursive(current.Left, licensePlate);
		}
		else
		{
			return SearchRecursive(current.Right, licensePlate);
		}
	}
}