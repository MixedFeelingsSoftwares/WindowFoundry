using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowFoundry.Core.WinForm
{
    public class WinFoundry
    {
        private readonly Form attachedWindow;

        private readonly Form ParentWindow;
        private Position myPos { get; set; }
        private Point Offset { get; set; }

        /// <summary>
        /// Attach Sub-Window to a Parent Window
        /// </summary>
        /// <param name="Parent">Window Parent</param>
        /// <param name="SubWindow">Child Window</param>
        /// <param name="pos">Position of Attach</param>
        /// <param name="offset">Offset of Attached Window</param>
        public WinFoundry(Form Parent, Form SubWindow, Position pos, Point offset)
        {
            ParentWindow = Parent;
            attachedWindow = SubWindow;
            myPos = pos;
            Offset = offset;
        }

        public enum Position
        {
            Top = 0,
            Bottom = 1,
            Right = 2,
            Left = 3
        }

        public void Attach()
        {
            attachedWindow.ShowInTaskbar = false;
            ParentWindow.AddOwnedForm(attachedWindow);

            MoveWin(Offset);

            ParentWindow.LocationChanged += (s, g) =>
            {
                MoveWin(Offset);
            };

            attachedWindow.ResizeEnd += (s, g) =>
            {
                MoveWin(Offset);
            };
        }

        public void ChangePosition(Position pos)
        {
            myPos = pos;
            MoveWin(Offset);
        }


        private void MoveWin(Point Offset)
        {
            Point pt = Point.Empty;

            switch (myPos)
            {
                case Position.Right:
                    pt = new Point(ParentWindow.Location.X + ParentWindow.Width + Offset.X, ParentWindow.Location.Y + Offset.Y);
                    break;
                case Position.Left:
                    pt = new Point(ParentWindow.Location.X - attachedWindow.Width - Offset.X, ParentWindow.Location.Y - Offset.Y);
                    break;
                case Position.Top:
                    pt = new Point(ParentWindow.Location.X + Offset.X, ParentWindow.Location.Y - attachedWindow.Height - Offset.Y);
                    break;
                case Position.Bottom:
                    pt = new Point(ParentWindow.Location.X + Offset.X, ParentWindow.Location.Y + attachedWindow.Height + Offset.Y);
                    break;
            }
            attachedWindow.Location = pt;
        }
    }
}
