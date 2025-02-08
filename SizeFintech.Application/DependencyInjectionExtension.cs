using Microsoft.Extensions.DependencyInjection;
using SizeFintech.Application.AutoMapper;
using SizeFintech.Application.UseCases.Anticipations.GetAll;
using SizeFintech.Application.UseCases.Anticipations.GetById;
using SizeFintech.Application.UseCases.Anticipations.Register;
using SizeFintech.Application.UseCases.Industries.GetAll;
using SizeFintech.Application.UseCases.Login.DoLogin;
using SizeFintech.Application.UseCases.Users.Profile;
using SizeFintech.Application.UseCases.Users.Register;

namespace SizeFintech.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IGetAllAnticipationsUseCase, GetAllAnticipationsUseCase>();
        services.AddScoped<IGetAnticipationByIdUseCase, GetAnticipationByIdUseCase>();
        services.AddScoped<IRegisterAnticipationUseCase, RegisterAnticipationUseCase>();
        services.AddScoped<IGetAllIndustriesUseCase, GetAllIndustriesUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}
