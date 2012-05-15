using System.Drawing;

namespace Traffic_Accounting
{
    public class SystemTray
    {
        public Color IconBackColor = Color.Transparent;
        public Color IconFontColor = Color.Black;
        public bool UseDigitsColorRanges = false;
        public bool UseBackColorRanges = false;
        public int[] ColorRanges = new int[2] { 20, 50 };
        public bool UseCircle = true;

        /// <summary>
        /// draw icon with numbers
        /// </summary>
        public Icon getIcon(int value)
        {
            // Calculate font size.
            // If we will draw 1 digit - size can be big,
            // but if we will draw 2 digit - we should use smaller font size.
            // One more limit: we can draw only 1 and 2 digits, no more
            if (value > 99) value = 99;
            if (value < 0) value = 0;
            Font font = new Font("Calibri", 8);

            // Change fore color according to traffic ranges
            if (UseDigitsColorRanges)
            {
                IconFontColor = getRangesColor(value);
            }
            if (UseBackColorRanges)
            {
                IconBackColor = getRangesColor(value);
            }
            // Draw new icon
            Bitmap bitmap = new Bitmap(16, 16);
            Graphics graphic = Graphics.FromImage(bitmap);
            SolidBrush backBrush = new SolidBrush(IconBackColor);
            SolidBrush foreBrush = new SolidBrush(IconFontColor);
            RectangleF canvas = new RectangleF(0, 0, 16, 16);
            if (!UseCircle)
            {
                graphic.FillRectangle(backBrush, canvas);
            }
            else
            {
                graphic.FillEllipse(backBrush, canvas);
            }
            graphic.DrawString(string.Format("{0:00}", value), font, foreBrush, 0.6f, 1.5f);
            return Icon.FromHandle(bitmap.GetHicon());
        }

        /// <summary>
        /// 0 - critical (red)
        /// 1 - warning (yellow)
        /// 2 - normal
        /// </summary>
        private int getRangesColorRepsentation(int value)
        {
            if (value > ColorRanges[1])
            {
                return 2;
            }
            else
            {
                if (value > ColorRanges[0])
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        private Color getRangesColor(int value)
        {
            switch (getRangesColorRepsentation(value))
            {
                case 1:
                    return Color.Goldenrod;
                case 2:
                    return Color.Maroon;;
                default:
                    return Color.DarkGreen;
            }
        }
    }
}
