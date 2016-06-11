using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ConsoleObjects
{
    public class Section
    {
        private readonly IList<Output> outputs = new List<Output>();
        private readonly ObservableCollection<Section> sections = new ObservableCollection<Section>();

        public Section(Section parent, Size size, string title = null)
        {
            Parent = parent ?? this;
            Size = size;
            Title.Text = title;

            if (IsRoot)
            {
                Layer = new Layer();
                Layer.Buffers.Add(Buffer);
                new ConsoleBufferBehavor().Attach(Layer.Buffer);
            }

            sections.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems != null)
                {
                    foreach (Section section in args.NewItems)
                    {
                        section.Layer = Parent.Layer;
                        Layer.Buffers.Add(section.Buffer);

                        new LayerBufferBehavor().Attach(Buffer, Layer);
                    }
                }
                if (args.OldItems != null)
                {
                    foreach (Section section in args.OldItems)
                    {
                        section.Layer = null;
                        Layer.Buffers.Remove(section.Buffer);
                    }
                }
            };

            Parent.Sections.Add(this);
        }

        public Section(Section parent, Size size, Layout layout, bool isBorderVisible = false) : this(parent, size, null)
        {
            Layout = layout;
            Border.IsVisible = isBorderVisible;
        }

        public Colors Colors
        {
            get { return Buffer.Colors; }
            set
            {
                Buffer.Colors = value;
                Border.Colors = value;
            }
        }

        public Border Border { get; } = new Border();

        public Buffer Buffer { get; } = new Buffer();

        public Section LastSection => IsLast ? null : Parent.Sections[Index + 1];

        public Layer Layer { get; set; }

        public Layout Layout { get; set; }

        public int Margin { get; set; }

        public Size MaximumSize { get; set; }

        public Size MinimumSize { get; set; }

        public Position OutputPosition { get; set; }

        public Size OutputSize { get; set; }

        public Scroll OutputScroll { get; set; }

        public Section Parent { get; set; }

        public Position Position
        {
            get { return Buffer.OffsetPosition; }
            set { Buffer.OffsetPosition = value; }
        }

        public Section PreviousSection => IsFirst ? null : Parent.Sections[Index - 1];

        public IList<Section> Sections => sections;

        public Title Title { get; set; } = new Title();

        public int Index => Parent.Sections.IndexOf(this);

        public bool IsFirst => Parent.Sections.First() == this;

        public bool IsLast => Parent.Sections.Last() == this;

        public bool IsRoot => Parent == this;

        public Size Size
        {
            get { return Buffer.Size; }
            set
            {
                Buffer.Size = value;
                Border.Size = value;
            }
        }

        public void Flush()
        {
            if (IsRoot)
            {
                Position = Position.NewPosition();
                OutputPosition = Position.FromPosition(Position, () => Border.IsVisible ? 1 + Margin : Margin);
                OutputSize = Size.FromSize(Size, () => Border.IsVisible ? -2 - 2 * Margin : -2 * Margin);

                if (Colors.IsNullOrNone())
                {
                    Colors = Colors.Default;
                }

                if (Border.Colors.IsNullOrNone())
                {
                    Border.Colors = Colors;
                }

                Colors.SetConsoleColors();
                Console.Clear();

                Layer.Init();
            }

            CalculateSectionDimensions();

            CalculateSectionPositions();

            if (Border.IsVisible)
            {
                Buffer.Inscribe(Position.NewPosition(), Size);
            }

            //BorderPrinter.Print(this);

            TitlePrinter.Print(this);

            foreach (var section in Sections)
            {
                if (section.Colors.IsNullOrNone())
                {
                    section.Colors = Colors;
                }
                if (section.Border.Colors.IsNullOrNone())
                {
                    section.Border.Colors = section.Colors;
                }
                section.Flush();
            }

            if (IsRoot)
            {
                BorderPrinter.Print(Buffer);
            }
        }

        private void CalculateSectionPositions()
        {
            foreach (var section in Sections)
            {
                if (section.IsFirst)
                {
                    section.Position = Position.FromValues(OutputPosition.Left, OutputPosition.Top);
                }
                else
                {
                    var previousSection = Sections[section.Index - 1];
                    var left = previousSection.Position.Left;
                    var top = previousSection.Position.Top;

                    if (Layout == Layout.Vertical)
                    {
                        top += previousSection.Size.Height.Value;
                        if (section.Border.IsTopShared && !section.IsFirst) top -= 1;
                    }
                    else
                    {
                        left += previousSection.Size.Width.Value;
                        if (section.Border.IsLeftShared && !section.IsFirst) left -= 1;
                    }
                    section.Position = Position.FromValues(left, top);
                }

                section.OutputPosition = Position.FromPosition(section.Position, () => section.Border.IsVisible ? 1 + section.Margin : section.Margin);
            }
        }

        private void CalculateSectionDimensions()
        {
            if (Layout == Layout.Vertical)
            {
                var heightValueTotal = Sections.Select(s => s.Size.Height).Sum(h => h.Value);
                var heightRatioTotal = Sections.Select(s => s.Size.Height).Sum(h => h.Ratio);
                if (heightRatioTotal > 0)
                {
                    var heightIncrement = (Size.Height.Value - heightValueTotal) / heightRatioTotal;
                    foreach (var section in Sections)
                    {
                        if (section.Size.Height.Value == 0)
                        {
                            var width = 0;
                            var height = 0;

                            if (section.IsLast)
                            {
                                height = OutputSize.Height.Value - heightValueTotal;
                            }
                            else
                            {
                                height = heightIncrement * section.Size.Height.Ratio;
                            }

                            heightValueTotal += height;
                            width = OutputSize.Width.Value;

                            section.Size = Size.FromValues(width, height);
                        }
                    }
                }
            }
            else
            {
                var widthValueTotal = Sections.Select(s => s.Size.Width).Sum(w => w.Value);
                var widthRatioTotal = Sections.Select(s => s.Size.Width).Sum(w => w.Ratio);
                if (widthRatioTotal > 0)
                {
                    var widthIncrement = (Size.Width.Value - widthValueTotal) / widthRatioTotal;
                    foreach (var section in Sections)
                    {
                        if (section.Size.Width.Value == 0)
                        {
                            var width = 0;
                            var height = 0;

                            if (section.IsLast)
                            {
                                width = OutputSize.Width.Value - widthValueTotal;
                            }
                            else
                            {
                                width = widthIncrement * section.Size.Width.Ratio;
                            }

                            widthValueTotal += width;
                            height = OutputSize.Height.Value;

                            section.Size = Size.FromValues(width, height);
                        }
                    }
                }
            }

            foreach (var section in Sections)
            {
                if (Layout == Layout.Vertical && section.Border.IsTopShared && !section.IsFirst)
                {
                    section.Size = Size.FromValues(section.Size.Width.Value, section.Size.Height.Value + 1);
                }

                if (Layout == Layout.Horizontal && section.Border.IsLeftShared && !section.IsFirst)
                {
                    section.Size = Size.FromValues(section.Size.Width.Value + 1, section.Size.Height.Value);
                }

                section.OutputSize = Size.FromSize(section.Size, () => section.Border.IsVisible ? -2 - 2 * section.Margin : -2 * section.Margin);
            }
        }

        public void Write(string value, char filler)
        {
            var output = new Output(value, filler);
            outputs.Add(output);

            //if (OutputScroll == Scroll.FromBottom)
            //{
            //    if (buffer.Count <= OutputSize.Height.Value)
            //    {
            //        Position.FromValues(OutputPosition.Left, OutputPosition.Top + buffer.Count - 1).SetConsoleCursorPosition();
            //    }
            //    else
            //    {
            //        Console.MoveBufferArea(OutputPosition.Left, OutputPosition.Top + 1, OutputSize.Width.Value, OutputSize.Height.Value - 1, OutputPosition.Left, OutputPosition.Top);
            //        Position.FromValues(OutputPosition.Left, OutputPosition.Top + OutputSize.Height.Value - 1).SetConsoleCursorPosition();
            //    }
            //    Console.Write(output.GetOutputText(OutputSize.Width.Value));
            //}
            //else
            //{
            //    Console.MoveBufferArea(OutputPosition.Left, OutputPosition.Top, OutputSize.Width.Value, OutputSize.Height.Value - 1, OutputPosition.Left, OutputPosition.Top + 1);
            //    Position.FromValues(OutputPosition.Left, OutputPosition.Top).SetConsoleCursorPosition();
            //    Console.Write(output.GetOutputText(OutputSize.Width.Value));
            //}


            //    List<Output> outputs;
            //    if (buffer.Count > OutputSize.Height.Value)
            //    {
            //        outputs = Enumerable.Range(buffer.Count - OutputSize.Height.Value, OutputSize.Height.Value).Select(i => buffer[i]).ToList();
            //    }
            //    else
            //    {
            //        outputs = buffer.ToList();
            //    }

            //    if (OutputScroll == Scroll.FromTop)
            //    {
            //        outputs.Reverse();
            //    }

            //    var outputPosition = Position.FromPosition(OutputPosition);
            //    outputPosition.SetConsoleCursorPosition();
            //    foreach (var output in outputs)
            //    {
            //        Console.Write(output.GetOutputText(OutputSize.Width.Value));
            //        if (output != outputs.Last())
            //        {
            //            outputPosition.SetConsoleCursorPosition(null, 1);
            //        }
            //    }
        }
    }
}