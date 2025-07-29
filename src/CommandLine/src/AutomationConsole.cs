// -------------------------------------------------------
// Copyright (c) Ken Swan. All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using AutomationIoC.CommandLine.Binder;
using AutomationIoC.CommandLine.Builder;
using System.CommandLine;

namespace AutomationIoC.CommandLine;

/// <summary>
///     Console automation class used to register commands and run them based on command line arguments
/// </summary>
public class AutomationConsole
{
    /// <summary>
    ///     Generate default command builder with no root implementation
    /// </summary>
    /// <param name="appDescription">Description of command suite to show in help menu</param>
    /// <param name="args">Command line arguments of current process</param>
    /// <returns>Automation Command builder used to add commands</returns>
    /// <remarks>If command line arguments are not sent, <see cref="Environment.GetCommandLineArgs" /> will be used instead</remarks>
    public static IAutomationConsoleBuilder CreateDefaultBuilder(string? appDescription = null, string[]? args = null)
    {
        var automationContext = new AutomationContext(args);
        var rootCommand = new RootCommand(description: appDescription ?? string.Empty);

        return new AutomationConsoleBuilder(rootCommand, automationContext, args);
    }

    /// <summary>
    ///     Generate default command builder with implementation at a root level
    /// </summary>
    /// <typeparam name="T">Type that represents root level command implementation</typeparam>
    /// <param name="appDescription">Description of command suite to show in help menu</param>
    /// <param name="args">Command line arguments of current process</param>
    /// <returns>Automation Command builder used to add commands</returns>
    /// <remarks>If command line arguments are not sent, <see cref="Environment.GetCommandLineArgs" /> will be used instead</remarks>
    public static IAutomationConsoleBuilder CreateDefaultBuilder<T>(
        string? appDescription = null,
        string[]? args = null)
        where T : IAutomationCommand, new()
    {
        var automationContext = new AutomationContext(args);

        var automationCommand =
            AutomationCommand.CreateCommand(
                name: string.Empty,
                description: appDescription,
                automationContext,
                automationCommand: new T());

        var rootAutomationCommand = new RootAutomationCommand(automationCommand);

        return new AutomationConsoleBuilder(rootAutomationCommand, automationContext, args);
    }
}
