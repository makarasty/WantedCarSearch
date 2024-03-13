namespace WantedCarSearchNamespace;

class Program
{
	static void Main(string[] args)
	{
		NotificationSystem notificationSystem = new();

		IObserver consoleLogger = new ConsoleLogger();
		notificationSystem.Attach(consoleLogger);

		BinarySearchTree bst = new(notificationSystem);

		bst.Insert(1234, "Olivia Kalamar");
		bst.Insert(0000, "Anonym");
		bst.Insert(7777, "Herobrin");

		// Принципі можна додати while, або переписати це все на dist :D
		Console.Write("Введіть номер автомобіля: ");
		int licensePlate;
		while (!int.TryParse(Console.ReadLine(), out licensePlate))
		{
			Console.Write("Невірний ввід. Будь ласка, введіть дійсний номер авто: ");
		}

		string cameraAddress = "Десь недалеко від дурдому";

		bst.Search(licensePlate, cameraAddress);
	}
}