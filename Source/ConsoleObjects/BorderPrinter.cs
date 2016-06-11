using System.Collections.Generic;
using System.Linq;

namespace ConsoleObjects
{
    public class BorderPrinter
    {
        public static void Print(Buffer buffer)
        {
            var characters = new BorderCharacters();

            foreach (var row in buffer.Rows)
            {
                foreach (var element in row.Elements)
                {
                    if (!element.IsInscribed) continue;

                    var left1 = element.GetRelatedElement(Direction.Left);
                    var left2 = left1?.GetRelatedElement(Direction.Left);
                    var left3 = left2?.GetRelatedElement(Direction.Left);
                    var right1 = element.GetRelatedElement(Direction.Right);
                    var right2 = right1?.GetRelatedElement(Direction.Right);
                    var right3 = right2?.GetRelatedElement(Direction.Right);
                    var down1 = element.GetRelatedElement(Direction.Down);
                    var down2 = down1?.GetRelatedElement(Direction.Down);
                    var down3 = down2?.GetRelatedElement(Direction.Down);
                    var up1 = element.GetRelatedElement(Direction.Up);
                    var up2 = up1?.GetRelatedElement(Direction.Up);
                    var up3 = up2?.GetRelatedElement(Direction.Up);

                    element.SetCharacter(characters.Horizontal, left1, left2, left3);
                    element.SetCharacter(characters.Horizontal, right1, right2, right3);
                    element.SetCharacter(characters.Horizontal, left1, left2, right1);
                    element.SetCharacter(characters.Horizontal, left1, right1, right2);

                    element.SetCharacter(characters.Vertical, up1, up2);
                    element.SetCharacter(characters.Vertical, up1, down1);
                    element.SetCharacter(characters.Vertical, down1, down2);

                    element.SetCharacter(characters.DownAndLeft, down1, left1);
                    element.SetCharacter(characters.DownAndRight, down1, right1);
                    element.SetCharacter(characters.UpAndLeft, up1, left1);
                    element.SetCharacter(characters.UpAndRight, up1, right1);

                    element.SetCharacter(characters.DownAndHorizontal, down1, left1, right1);
                    element.SetCharacter(characters.UpAndHorizontal, up1, left1, right1);
                    element.SetCharacter(characters.VerticalAndLeft, left1, up1, down1);
                    element.SetCharacter(characters.VerticalAndRight, right1, up1, down1);
                    element.SetCharacter(characters.VerticalAndHorizontal, up1, down1, left1, right1);
                }
            }
        }
    }
}