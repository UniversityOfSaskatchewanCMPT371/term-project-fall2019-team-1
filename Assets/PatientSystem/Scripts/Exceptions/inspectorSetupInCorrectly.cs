using System; 

/// <summary>
/// <c>inspectorSetupInCorrectly</c>
/// Description: Exception System, meant to throw a exception when the inspector is not correctly setup!
/// 
/// pre-condition: Language Engine needs to be fully operational!
/// 
/// post-condition: exception thrown if user selects both KMP and Word Comparison.
/// 
/// </summary>
/// <author> Matt Radke </author>
public class inspectorSetupInCorrectly : Exception
{
    public inspectorSetupInCorrectly()
        : base()
    {
    }

    public inspectorSetupInCorrectly(string message)
        : base(message)
    {
    }

    public inspectorSetupInCorrectly(string message, Exception inner)
        : base(message, inner)
    {
    }
}
