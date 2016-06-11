namespace ConsoleObjects
{
    public class TitlePrinter
    {
        public static void Print(Section section)
        {
            if (!section.Border.IsVisible || !section.Title.IsVisible) return;

            var position = Position.NewPosition();
            var titleText = new string(' ', section.Title.LeftPadding) + section.Title.Text + new string(' ', section.Title.RightPadding);

            switch (section.Title.Alignment)
            {
                case Alignment.Left:
                    position.IncreaseLeft(section.Title.RightPush - section.Title.LeftPush + 1);
                    break;
                case Alignment.Right:
                    position.IncreaseLeft(section.Size.Width.Value - titleText.Length - section.Title.LeftPush - section.Title.RightPush - 1);
                    break;
                case Alignment.Center:
                    position.IncreaseLeft((section.Size.Width.Value - titleText.Length) / 2 - section.Title.LeftPush + section.Title.RightPush);
                    break;
            }

            section.Buffer.Write(position, titleText);
        }
    }
}