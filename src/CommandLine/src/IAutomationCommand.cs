// -------------------------------------------------------
// Copyright (c) Ken Swan. All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace AutomationIoC.CommandLine;

/// <summary>
/// </summary>
public interface IAutomationCommand
{
    /// <summary>
    /// </summary>
    /// <param name="command"></param>
    public void Initialize(AutomationCommand command);
}
