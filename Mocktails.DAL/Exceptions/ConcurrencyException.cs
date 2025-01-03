namespace Mocktails.DAL.Exceptions;

/// <summary>
/// Represents an exception that occurs when a concurrency conflict is detected.
/// </summary>
public class ConcurrencyException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
    /// </summary>
    public ConcurrencyException() : base("A concurrency conflict occurred.") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConcurrencyException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ConcurrencyException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConcurrencyException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ConcurrencyException(string message, Exception innerException) : base(message, innerException) { }
}
