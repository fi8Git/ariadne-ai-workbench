using Foundation;

namespace Ariadne.Maui;

#pragma warning disable CA1711 // MAUI platform entry point follows the AppDelegate naming convention.
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
#pragma warning restore CA1711
