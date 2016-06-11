using System;

namespace ConsoleObjects
{
    public class LayerBufferBehavor : IBufferBehavor<Layer>
    {
        public Buffer Buffer { get; private set; }

        public Layer Target { get; private set; }

        public void Attach(Buffer buffer, Layer target)
        {
            Target = target;
            Attach(buffer);
        }

        public void Attach(Buffer buffer)
        {
            Buffer = buffer;
            Buffer.ElementChanged += OnElementChanged;
        }

        public void Detach()
        {
            Buffer.ElementChanged -= OnElementChanged;
        }

        private void OnElementChanged(object sender, ElementChangedEventArgs args)
        {
            var position = Position.FromValues(args.OffsetPosition.Left + args.Element.BufferPosition.Left, args.OffsetPosition.Top + args.Element.BufferPosition.Top);
            var targetElement = Target.Buffer.Rows[position.Left].Elements[position.Top];
            var sourceElement = args.Element;

            targetElement.Colors = Colors.FromColors(sourceElement.Colors);
            targetElement.Character = sourceElement.Character;
            targetElement.IsInscribed = sourceElement.IsInscribed;
        }
    }
}