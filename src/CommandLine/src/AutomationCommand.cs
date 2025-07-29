// -------------------------------------------------------
// Copyright (c) Ken Swan. All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using System.CommandLine;

namespace AutomationIoC.CommandLine;

/// <summary>
/// </summary>
/// <param name="name"></param>
/// <param name="description"></param>
public sealed class AutomationCommand(
    string name,
    string? description) : Command(name, description)
{
    private IAutomationContext automationContext;

    /// <summary>
    /// </summary>
    /// <param name="action"></param>
    public void SetAction(Action<ParseResult, IAutomationContext> action) =>
        SetAction(parsedResult => action(parsedResult, automationContext));

    /// <summary>
    /// </summary>
    /// <param name="action"></param>
    public void SetAction(Func<ParseResult, IAutomationContext, CancellationToken, Task> action) =>
        SetAction(async (parsedResult, cancellationToken) =>
        {
            await action(parsedResult, automationContext, cancellationToken).ConfigureAwait(false);
        });

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    private void SetContext(IAutomationContext context) => automationContext = context;

    internal static AutomationCommand CreateCommand(
        string name,
        string? description,
        IAutomationContext automationContext,
        IAutomationCommand automationCommand)
    {
        var newAutomationCommand = new AutomationCommand(name, description);
        newAutomationCommand.SetContext(automationContext);
        automationCommand.Initialize(newAutomationCommand);

        return newAutomationCommand;
    }

    internal static void CopyCommandElements(Command source, Command target)
    {
        target.Description = source.Description;

        // Copy options
        foreach (Option option in source.Options)
        {
            target.Options.Add(option);
        }

        // Copy arguments
        foreach (Argument argument in source.Arguments)
        {
            target.Arguments.Add(argument);
        }

        // Copy handler
        if (source.Action != null)
        {
            target.Action = source.Action;
        }

        foreach (string alias in source.Aliases)
        {
            if (!target.Aliases.Contains(alias))
            {
                target.Aliases.Add(alias);
            }
        }

        // Copy subcommands
        foreach (Command subcommand in source.Subcommands)
        {
            target.Subcommands.Add(subcommand);
        }
    }
}
