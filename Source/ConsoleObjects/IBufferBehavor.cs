namespace ConsoleObjects
{
    public interface IBufferBehavor<TTarget> : IBufferBehavor
    {
        TTarget Target { get; }

        void Attach(Buffer buffer, TTarget target);
    }

    public interface IBufferBehavor
    {
        Buffer Buffer { get; }

        void Attach(Buffer buffer);

        void Detach();
    }
}