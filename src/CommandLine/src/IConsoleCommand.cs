// -------------------------------------------------------
// Copyright (c) Ken Swan. All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using AutomationIoC.Runtime;
using System.CommandLine;

namespace AutomationIoC.CommandLine;

/// <summary>
///     Command line automation interface used to register/build commands
/// </summary>
public interface IConsoleCommand : IAutomationStartup
{
    /// <summary>
    ///     Set specific command configuration/properties
    /// </summary>
    /// <param name="context">
    ///     Service Container used to bind command line services to actions/handlers
    /// </param>
    /// <param name="command">Current command context to establish properties, configuration, and attributes</param>
    void ConfigureCommand(IAutomationContext context, Command command);
}
