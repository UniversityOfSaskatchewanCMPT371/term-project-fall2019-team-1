using System;

/// <summary>
/// <c>NoBestDecision</c>
/// Description: Exception system, meant to throw a exeception when there are no options for the language engine.
/// 
/// pre-condition: Langauge engine 
/// 
/// 
/// post-condition: exception thrown if langauge engine has no options.
/// 
/// </summary>
/// 
/// <author>Mason Demerais</author>
public class NoOptionsAvailable : Exception
{
    public NoOptionsAvailable()
        : base()
    {
    }

    public NoOptionsAvailable(string message)
        : base(message)
    {
    }

    public NoOptionsAvailable(string message, Exception inner)
        : base(message, inner)
    {
    }
}
