// -------------------------------------------------------
// Copyright (c) Ken Swan. All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using AutomationIoC.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutomationIoC.CommandLine.Binder;

internal class AutomationServiceActivator : IAutomationServiceActivator
{
    private readonly string[] args;
    private readonly Action<HostBuilderContext, IConfigurationBuilder> buildConfiguration;
    private readonly Action<HostBuilderContext, IServiceCollection> buildServices;
    private readonly IDictionary<string, string> configurationMapping;

    private IHost host;

    public AutomationServiceActivator(
        string[] args,
        IDictionary<string, string> configurationMapping,
        Action<HostBuilderContext, IConfigurationBuilder> buildConfiguration,
        Action<HostBuilderContext, IServiceCollection> buildServices)
    {
        this.args = args;
        this.configurationMapping = configurationMapping;
        this.buildConfiguration = buildConfiguration;
        this.buildServices = buildServices;
    }

    public IServiceProvider GetServiceProvider()
    {
        host ??= AutomationRuntime.GenerateRuntimeHost(
            buildConfiguration: buildConfiguration,
            buildServices: buildServices,
            parameters: args,
            parameterConfigurationMappings: configurationMapping);

        return host.Services;
    }
}
