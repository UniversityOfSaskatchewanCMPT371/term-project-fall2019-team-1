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
public class InspectorSetupInCorrectly : Exception
{
    public InspectorSetupInCorrectly()
        : base()
    {
    }

    public InspectorSetupInCorrectly(string message)
        : base(message)
    {
    }

    public InspectorSetupInCorrectly(string message, Exception inner)
        : base(message, inner)
    {
    }
}
