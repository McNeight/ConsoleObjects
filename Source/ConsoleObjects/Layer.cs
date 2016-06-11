using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleObjects
{
    public class Layer
    {
        public Layer()
        {
            new LayerBufferBehavor().Attach(Buffer, this);
        }

        public Guid InstanceId { get; } = Guid.NewGuid();

        public IList<Buffer> Buffers { get; } = new List<Buffer>();

        public Buffer Buffer { get; set; } = new Buffer();

        public void Init()
        {
            var offsetPosition = Position.FromValues(int.MaxValue, int.MaxValue);
            foreach (var buffer in Buffers)
            {
                if (buffer.OffsetPosition.Left < offsetPosition.Left) offsetPosition.Left = buffer.OffsetPosition.Left;
                if (buffer.OffsetPosition.Top < offsetPosition.Top) offsetPosition.Top = buffer.OffsetPosition.Top;
            }

            var size = Size.NewSize();
            foreach (var buffer in Buffers)
            {
                var effectiveWidth = buffer.Size.Width.Value + buffer.OffsetPosition.Left - offsetPosition.Left;
                if (effectiveWidth > size.Width.Value) size.Width.Value = effectiveWidth;

                var effectiveHeight = buffer.Size.Height.Value + buffer.OffsetPosition.Top - offsetPosition.Top;
                if (effectiveHeight > size.Height.Value) size.Height.Value = effectiveHeight;
            }

            Buffer.OffsetPosition = offsetPosition;
            Buffer.Size = size;
        }
    }
}