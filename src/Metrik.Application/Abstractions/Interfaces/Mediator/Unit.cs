namespace Metrik.Application.Abstractions.Interfaces.Mediator
{
    /// <summary>
    /// Represents a void response in the Mediator pattern.
    /// </summary>
    public struct Unit
    {
        /// <summary>
        /// Singleton instance of Unit.
        /// </summary>
        public static readonly Unit Value = new();

        /// <summary>
        /// Returns a string representation of the Unit.
        /// </summary>
        public override readonly string ToString() => "()";
    }
}
