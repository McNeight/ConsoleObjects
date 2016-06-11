using System;

namespace ConsoleObjects
{
    public class Size
    {
        public Size(Dimention width, Dimention height)
        {
            Width = width;
            Height = height;
        }

        public Dimention Height { get; set; }

        public Dimention Width { get; set; }

        public static Size NewSize()
        {
            return FromValues(0, 0);
        }

        public static Size FromRatio(int ratio)
        {
            return new Size(new Dimention { Ratio = ratio }, new Dimention { Ratio = ratio });
        }

        public static Size FromRatios(int widthRatio, int heightRatio)
        {
            return new Size(new Dimention { Ratio = widthRatio }, new Dimention { Ratio = heightRatio });
        }

        public static Size FromSize(Size size, Func<int> getIncrement = null)
        {
            var increment = 0;
            if (getIncrement != null)
            {
                increment = getIncrement.Invoke();
            }

            return new Size
            (
                new Dimention { Ratio = size.Width.Ratio, Value = size.Width.Value + increment },
                new Dimention { Ratio = size.Height.Ratio, Value = size.Height.Value + increment }
            );
        }

        public static Size FromValue(int value, Func<int> getIncrement = null)
        {
            var increment = 0;
            if (getIncrement != null)
            {
                increment = getIncrement.Invoke();
            }
            return new Size(new Dimention { Value = value + increment }, new Dimention { Value = value + increment });
        }

        public static Size FromValues(int width, int height, Func<int> getIncrement = null)
        {
            var increment = 0;
            if (getIncrement != null)
            {
                increment = getIncrement.Invoke();
            }
            return new Size(new Dimention { Value = width + increment }, new Dimention { Value = height + increment });
        }

        public override string ToString()
        {
            return $"{Width.Value} x {Height.Value}";
        }
    }
}