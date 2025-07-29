// -------------------------------------------------------
// Copyright (c) Ken Swan. All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using AutomationIoC.CommandLine.Application;
using AutomationIoC.CommandLine.Binder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;

namespace AutomationIoC.CommandLine.Builder;

internal class AutomationConsoleBuilder(
    RootCommand rootCommand,
    AutomationContext automationContext,
    string[]? args) : IAutomationConsoleBuilder
{
    public IAutomationConsoleBuilder AddCommand<T>(params string[] commandPath) where T : IAutomationCommand, new()
    {
        string addedCommandName = commandPath.Last();

        Command currentCommand = rootCommand;

        // Traverse to proper parent command
        for (int i = 0; i < commandPath.Length - 1; i++)
        {
            string currentName = commandPath[i];

            Command subCommand = currentCommand.Subcommands.FirstOrDefault(command => command.Name == currentName);

            if (subCommand is null)
            {
                currentCommand.Subcommands.Add(new(currentName));

                subCommand = currentCommand.Subcommands.First(command => command.Name == currentName);
            }

            currentCommand = subCommand;
        }

        // Check if command already exists in this path
        // This could happen if the caller registers the command out of order
        // (once in a path, and the other for adding command implementation)
        Command existingCommand =
            currentCommand.Subcommands.FirstOrDefault(command => command.Name == addedCommandName);

        var internalCommand = new T();

        var newCommand =
            AutomationCommand.CreateCommand(
                name: addedCommandName,
                description: null,
                automationContext,
                automationCommand: new T());

        // Add new command if not found at proper path as already existing
        if (existingCommand is null)
        {
            currentCommand.Subcommands.Add(newCommand);
        }
        // Update existing command if found
        else
        {
            AutomationCommand.CopyCommandElements(source: newCommand, target: existingCommand);
        }

        return this;
    }

    public IAutomationConsole Build()
    {
        automationContext.Compile();

        return new AutomationConsoleApplication(rootCommand, args);
    }

    public void ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureServices) =>
        automationContext.SetConfigureServices(configureServices);

    public void Configure(Action<HostBuilderContext, IConfigurationBuilder> configure) =>
        automationContext.SetConfigure(configure);

    public void WithConfigurationMapping(IDictionary<string, string> configurationMapping) =>
        automationContext.SetConfigurationMapping(configurationMapping);
}
