// -------------------------------------------------------
// Copyright (c) Ken Swan. All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using Microsoft.Extensions.Configuration;

namespace AutomationIoC.CommandLine;

/// <summary>
///     Service Container and Configurations used to bind command line services to actions/handlers.
/// </summary>
public interface IAutomationContext
{
    /// <summary>
    ///     Returns the configuration used to bind command line services
    ///     to actions/handlers. This configuration can be used to access
    ///     application settings, environment variables, or other configuration sources.
    /// </summary>
    /// <returns>Custom type binding for downstream dependency injection</returns>
    IConfiguration Configuration { get; }

    /// <summary>
    ///     Returns the service provider used to resolve services
    ///     registered in the command line application.
    ///     This is typically used to resolve services that are not
    ///     directly bound to command handlers, such as logging or configuration services.
    /// </summary>
    /// <returns>Framework Service Provider</returns>
    IServiceProvider ServiceProvider { get; }
}
