using System.Collections.Generic;
using System.Linq;

namespace ConsoleObjects
{
    public class BufferRow
    {
        public BufferRow(Buffer buffer, int columnCount)
        {
            Buffer = buffer;
            foreach (var index in Enumerable.Range(0, columnCount))
            {
                Elements.Add(new BufferElement(Buffer, this));
            }
        }

        public Buffer Buffer { get; }

        public IList<BufferElement> Elements { get; } = new List<BufferElement>();

        public bool IsFirstInBuffer => Buffer.Rows.First() == this;

        public bool IsLastInBuffer => Buffer.Rows.Last() == this;

        public override string ToString()
        {
            return new string(Elements.Select(element => element.Character).ToArray());
        }
    }
}