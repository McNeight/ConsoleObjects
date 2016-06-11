using System;

namespace ConsoleObjects
{
    public class ConsoleBufferBehavor : IBufferBehavor
    {
        public Buffer Buffer { get; private set; }

        public void Attach(Buffer buffer)
        {
            Buffer = buffer;
            Buffer.ElementChanged += OnElementChanged;
        }

        public void Detach()
        {
            Buffer.ElementChanged -= OnElementChanged;
        }

        private static void OnElementChanged(object sender, ElementChangedEventArgs args)
        {
            var position = Position.FromValues(args.OffsetPosition.Left + args.Element.BufferPosition.Left, args.OffsetPosition.Top + args.Element.BufferPosition.Top);
            args.Element.WriteToConsole(position);
        }
    }
}
