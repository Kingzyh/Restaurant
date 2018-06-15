using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Controls {
    public struct CornerRadius {

        public int TopLeft;
        public int TopRight;
        public int BottomLeft;
        public int BottomRight;

        public CornerRadius(int radius) : this(radius, radius, radius, radius) {

        }

        public CornerRadius(int topLeft, int topRight, int bottomLeft, int bottomRight) {
            this.TopLeft = topLeft;
            this.TopRight = topRight;
            this.BottomLeft = bottomLeft;
            this.BottomRight = bottomRight;
        }
    }
}
