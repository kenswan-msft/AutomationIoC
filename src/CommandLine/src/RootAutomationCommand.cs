// -------------------------------------------------------
// Copyright (c) Ken Swan. All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using System.CommandLine;

namespace AutomationIoC.CommandLine;

/// <summary>
/// </summary>
internal class RootAutomationCommand : RootCommand
{
    public RootAutomationCommand(AutomationCommand automationCommand)
        : base(automationCommand.Description)
    {
        AutomationCommand.CopyCommandElements(automationCommand, this);
    }
}
