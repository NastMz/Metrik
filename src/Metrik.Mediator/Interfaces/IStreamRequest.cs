namespace Metrik.Mediator.Interfaces
{
    /// <summary>
    /// Marks a class as a stream request that produces multiple responses.
    /// </summary>
    /// <typeparam name="TResponse">Type of response elements</typeparam>
    public interface IStreamRequest<out TResponse>
    {
    }
}
