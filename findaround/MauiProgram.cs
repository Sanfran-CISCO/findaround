using CommunityToolkit.Maui;
using findaround.Services;
using findaround.ViewModels;
using findaround.Views;
using findaround.Helpers;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;
using MonkeyCache.FileStore;

namespace findaround;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		Barrel.ApplicationId = AppInfo.PackageName;

        // Dependencies

        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

		// Services
		builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IPostService, PostService>();

		// ViewModels
		builder.Services.AddSingleton<RegisterPageViewModel>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<CategoriesPageViewModel>();
        builder.Services.AddSingleton<ProfilePageViewModel>();
        builder.Services.AddSingleton<PostDetailsPageViewModel>();
        builder.Services.AddSingleton<NewPostPageViewModel>();
        builder.Services.AddSingleton<ContactsPageViewModel>();
        builder.Services.AddSingleton<SearchPostPageViewModel>();

        // Views
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<CategoriesPage>();
        builder.Services.AddSingleton<ProfilePage>();
        builder.Services.AddSingleton<PostDetailsPage>();
        builder.Services.AddSingleton<NewPostPage>();
        builder.Services.AddSingleton<ContactsPage>();
        builder.Services.AddSingleton<SearchPostPage>();

        return builder.Build();
	}
}

