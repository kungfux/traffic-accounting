using System.Drawing;
using System;

namespace Traffic_Accounting
{
    internal class SystemTray
    {
        /// <summary>
        /// draw icon with numbers
        /// </summary>
        public Icon getIcon(int value)
        {
            Color IconFontColor = Color.FromArgb(ClientParams.Parameters.TrayIconFontColor);
            Color IconBackColor = Color.FromArgb(ClientParams.Parameters.TrayIconBackColor);
            // Calculate font size.
            // If we will draw 1 digit - size can be big,
            // but if we will draw 2 digit - we should use smaller font size.
            // One more limit: we can draw only 1 and 2 digits, no more
            if (value > 999) value = 999;
            if (value < -999) value = -999;
            Font font = new Font("Calibri", 17, FontStyle.Bold);

            // Change fore color according to traffic ranges
            if (ClientParams.Parameters.TrayDigitsColorRangesEnabled)
            {
                IconFontColor = getRangesColor(value);
            }
            if (ClientParams.Parameters.TrayBackColorRangesEnabled)
            {
                IconBackColor = getRangesColor(value);
            }
            // Draw new icon
            Bitmap bitmap = new Bitmap(32, 32);
            Graphics graphic = Graphics.FromImage(bitmap);
            SolidBrush backBrush = new SolidBrush(IconBackColor);
            SolidBrush foreBrush = new SolidBrush(IconFontColor);
            RectangleF canvas = new RectangleF(0, 0, 32, 32);
            if (!ClientParams.Parameters.TrayDrawCircleInsteadOfSquare)
            {
                graphic.FillRectangle(backBrush, canvas);
            }
            else
            {
                graphic.FillEllipse(backBrush, canvas);
            }
            if (ClientParams.Parameters.TrayDisplayDigits)
            {
                SizeF size = graphic.MeasureString(string.Format("{0:00}", Math.Abs(value)), font);
                graphic.DrawString(string.Format("{0:00}", Math.Abs(value)), font, foreBrush, (32 - size.Width)/2, (32 - size.Height) / 2);
            }
            return Icon.FromHandle(bitmap.GetHicon());
        }

        /// <summary>
        /// 0 - absurd (black)
        /// 1 - critical (red)
        /// 2 - warning (yellow)
        /// 3 - normal
        /// </summary>
        private int getRangesColorRepsentation(int value)
        {
            // { 0, 20, 50}
            for (int a = ClientParams.Parameters.TrayTrafficRanges.Length - 1; a >= 0; a--)
            {
                if (value > ClientParams.Parameters.TrayTrafficRanges[a])
                {
                    return ++a;
                }
            }
            return 0;
        }

        /// <summary>
        /// 0 - absurd (black)
        /// 1 - critical (red)
        /// 2 - warning (yellow)
        /// 3 - normal
        /// </summary>
        private Color getRangesColor(int value)
        {
            switch (getRangesColorRepsentation(value))
            {
                case 0:
                    return Color.Black;
                case 1:
                    return Color.Maroon; 
                case 2:
                    return Color.Goldenrod;
                case 3:
                    return Color.DarkGreen;
            }
            return Color.Transparent;
        }
    }
}
