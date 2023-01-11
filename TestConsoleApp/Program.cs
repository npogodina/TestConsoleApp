using Autofac;

namespace DependencyInjection;

// Credit: https://youtu.be/fvPPlY31glk (by Claudio Bernasconi)

// Dependency Inversion Principle (one of the SOLI >> D << principles of OOP)
// High level modules should not depend on low level modules, but on abstractions.
// Implementation details should depend on abstractions.

public interface INotificationService
{
	public void NotifyUsernameChanged(User user);
}

// High level module, simple class with no dependencies
// Responsibility = represent user
public class User
{
    public string UserName { get; set; }

	public User(string name)
	{
		UserName = name;
	}
}

// Responsibility = make changes to users
public class UserService
{
	private INotificationService _notificationService;

	public UserService(INotificationService notificationService)
	{
		_notificationService = notificationService;
	}

	public void ChangeUserName(User user, string userName)
	{
		user.UserName = userName;
		_notificationService.NotifyUsernameChanged(user);
	}
}


// Low level module (implementation details)
// Responsibility = notify customer of changes
public class NotificationService : INotificationService
{
	public void NotifyUsernameChanged(User user)
	{
		Console.WriteLine($"Username changed to {user.UserName}.");
	}
}

internal class DemoModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<NotificationService>().As<INotificationService>();
        builder.RegisterType<UserService>().AsSelf();
    }
}

public class Demo
{
	public static void Main(string[] args)
	{
		var containerBuilder = new ContainerBuilder();
		containerBuilder.RegisterModule<DemoModule>();
	
		var container = containerBuilder.Build();

		var notificationService = container.Resolve<INotificationService>();
		var userService = container.Resolve<UserService>();

        var user = new User("Cody");
		userService.ChangeUserName(user, "Cody Farmer");
	}
}