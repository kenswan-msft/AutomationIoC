// -------------------------------------------------------
// Copyright (c) Ken Swan. All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutomationIoC.CommandLine.Binder;

internal class AutomationContext(string[]? args = null)
    : IAutomationContext
{
    private string[]? args = args;
    private AutomationServiceActivator automationServiceActivator;
    private IDictionary<string, string>? configurationMapping;
    private Action<HostBuilderContext, IConfigurationBuilder>? configure;
    private Action<HostBuilderContext, IServiceCollection>? configureServices;

    public IServiceProvider ServiceProvider => automationServiceActivator.GetServiceProvider();

    public IConfiguration Configuration => ServiceProvider.GetRequiredService<IConfiguration>();

    public void Compile() =>
        automationServiceActivator = new(
            args,
            configurationMapping,
            configure,
            configureServices);

    public void SetConfigure(Action<HostBuilderContext, IConfigurationBuilder> configure) =>
        this.configure = configure;

    public void SetConfigureServices(Action<HostBuilderContext, IServiceCollection> configureServices) =>
        this.configureServices = configureServices;

    public void SetArgs(string[] args) =>
        this.args = args;

    public void SetConfigurationMapping(IDictionary<string, string> configurationMapping) =>
        this.configurationMapping = configurationMapping;
}
