using System;

/// <summary>
/// <c>NoBestDecision</c>
/// Description: Exception system, meant to throw a exeception when a decision cannot be found in langauge engine.
/// 
/// pre-condition: Langauge engine 
/// 
/// 
/// post-condition: exception thrown if langauge engine cannot find correct response.
/// 
/// </summary>
/// 
/// <author>Mason Demerais</author>
public class NoBestDecision : Exception
{
    public NoBestDecision()
        : base()
    {
    }

    public NoBestDecision(string message)
        : base(message)
    {
    }

    public NoBestDecision(string message, Exception inner)
        : base(message, inner)
    {
    }
}
