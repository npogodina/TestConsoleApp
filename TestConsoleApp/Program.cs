namespace DependencyInjection;

// Credit: https://youtu.be/fvPPlY31glk (by Claudio Bernasconi)

// Dependency Inversion Principle (one of the SOLI >> D << principles of OOP)
// High level modules should not depend on low level modules, but on abstractions.
// Implementation details should depend on abstractions.

public interface INotificationService
{
	public void NotifyUsernameChanged(User user);
}

// High level module
public class User
{
    public string UserName { get; private set; }

	private INotificationService _notificationService;

	// Injecting concrete class implementing INotificationService abstraction through constructor
	public User(string name, INotificationService notificationService)
	{
		UserName = name;
		_notificationService = notificationService;
	}

	public void ChangeUsername(string username)
	{ 
		UserName = username;
		_notificationService.NotifyUsernameChanged(this);
	}
}

// Low level module (implementation details)
public class NotificationService : INotificationService
{
	public void NotifyUsernameChanged(User user)
	{
		Console.WriteLine($"Username changed to {user.UserName}.");
	}
}

public class Demo
{
	public static void Main(string[] args)
	{
		var notificationService = new NotificationService();

        // Injecting concrete class implementing INotificationService abstraction through constructor
        var user = new User("Cody", notificationService);

		user.ChangeUsername("Cody Farmer");
	}
}