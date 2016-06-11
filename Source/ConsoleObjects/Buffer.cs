using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleObjects
{
    public class Buffer
    {
        private Size size = Size.NewSize();

        public event EventHandler<ElementChangedEventArgs> ElementChanged;

        public Colors Colors { get; set; } = Colors.None;

        public Position OffsetPosition { get; set; } = Position.NewPosition();

        public IList<BufferRow> Rows { get; set; } = new List<BufferRow>();

        public Size Size
        {
            get { return Rows.Any() ? Size.FromValues(Rows[0].Elements.Count, Rows.Count) : size; }
            set
            {
                size = value;

                Rows.Clear();
                foreach (var index in Enumerable.Range(0, size.Height.Value))
                {
                    Rows.Add(new BufferRow(this, size.Width.Value));
                }
            }
        }

        public void Inscribe(Position start, int length, Direction direction = Direction.Right)
        {
            switch (direction)
            {
                case Direction.Right:
                    Enumerable.Range(0, length).ToList().ForEach(index =>
                    {
                        Rows[start.Top - 1].Elements[start.Left + index - 1].IsInscribed = true;
                    });
                    break;
                case Direction.Left:
                    Enumerable.Range(0, length).ToList().ForEach(index =>
                    {
                        Rows[start.Top - 1].Elements[start.Left - index - 1].IsInscribed = true;
                    });
                    break;
                case Direction.Up:
                    Enumerable.Range(0, length).ToList().ForEach(index =>
                    {
                        Rows[start.Top - index - 1].Elements[start.Left - 1].IsInscribed = true;
                    });
                    break;
                case Direction.Down:
                    Enumerable.Range(0, length).ToList().ForEach(index =>
                    {
                        Rows[start.Top + index].Elements[start.Left].IsInscribed = true;
                    });
                    break;
            }
        }

        public void Inscribe(Position start, Size size)
        {
            Enumerable.Range(start.Left, size.Width.Value).ToList().ForEach(index =>
            {
                Rows[start.Top].Elements[index].IsInscribed = true;
                Rows[start.Top + size.Height.Value - 1].Elements[index].IsInscribed = true;
            });

            Enumerable.Range(start.Top, size.Height.Value).ToList().ForEach(index =>
            {
                Rows[index].Elements[start.Left].IsInscribed = true;
                Rows[index].Elements[start.Left + size.Width.Value - 1].IsInscribed = true;
            });
        }

        public void RaiseElementChanged(BufferElement element)
        {
            ElementChanged?.Invoke(this, new ElementChangedEventArgs(element, OffsetPosition));
        }

        public void Write(Position position, string value)
        {
            var characters = value.ToCharArray();
            foreach (var index in Enumerable.Range(0, characters.Length))
            {
                Rows[position.Top].Elements[position.Left + index].Character = characters[index];
            }
        }
    }
}