using Microsoft.Extensions.DependencyInjection;

namespace Domain.Auth;

public static class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeLoggedIn", a =>
                a.RequireAuthenticatedUser());
    
            options.AddPolicy("BeDoctor", a =>
                a.RequireAuthenticatedUser().RequireClaim("role", "doctor"));
    
            options.AddPolicy("BeReceptionist", a =>
                a.RequireAuthenticatedUser().RequireClaim("role", "receptionist"));
            
            options.AddPolicy("BeAdministrator", a =>
                a.RequireAuthenticatedUser().RequireClaim("role", "administrator"));
    
            
        });
    }
}