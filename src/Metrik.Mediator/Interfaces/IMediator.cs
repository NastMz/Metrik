namespace Metrik.Mediator.Interfaces
{
    /// <summary>
    /// Defines a mediator that combines publishing and sending capabilities.
    /// </summary>
    public interface IMediator : ISender, IPublisher
    {
    }
}
