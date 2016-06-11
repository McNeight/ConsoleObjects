using System;

namespace ConsoleObjects
{
    public class ElementChangedEventArgs : EventArgs
    {
        public ElementChangedEventArgs(BufferElement element, Position offsetPosition)
        {
            Element = element;
            OffsetPosition = offsetPosition;
        }

        public BufferElement Element { get; }

        public Position OffsetPosition { get; }
    }
}
