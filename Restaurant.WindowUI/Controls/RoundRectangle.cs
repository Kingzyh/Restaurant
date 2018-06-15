using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Controls {
    public class RoundRectangle {

        public Rectangle Rect { get; set; }
        public CornerRadius CornerRadius { get; set; }

        public RoundRectangle(Rectangle rect, int radius) : this(rect, new CornerRadius(radius)) {

        }

        public RoundRectangle(Rectangle rect, CornerRadius cornerRedius) {
            this.Rect = rect;
            this.CornerRadius = cornerRedius;
        }

        #region Methods

        /// <summary>
        /// 获取该圆角矩形的GraphicsPath对象(圆角使用Bezier曲线实现)
        /// </summary>
        /// <returns>
        /// Return a data(or instance) of GraphicsPath.
        /// </returns>
        /// User:Ryan  CreateTime:2011-07-20 11:52.
        public GraphicsPath ToGraphicsBezierPath() {
            GraphicsPath path = new GraphicsPath();
            int x = this.Rect.X;
            int y = this.Rect.Y;
            int w = this.Rect.Width;
            int h = this.Rect.Height;
            path.AddBezier(x, y + this.CornerRadius.TopLeft, x, y, x + this.CornerRadius.TopLeft, y, x + this.CornerRadius.TopLeft, y);
            path.AddLine(x + this.CornerRadius.TopLeft, y, x + w - this.CornerRadius.TopRight, y);
            path.AddBezier(x + w - this.CornerRadius.TopRight, y, x + w, y, x + w, y + this.CornerRadius.TopRight, x + w, y + this.CornerRadius.TopRight);
            path.AddLine(x + w, y + this.CornerRadius.TopRight, x + w, y + h - this.CornerRadius.BottomRight);
            path.AddBezier(x + w, y + h - this.CornerRadius.BottomRight, x + w, y + h, x + w - this.CornerRadius.BottomRight, y + h, x + w - this.CornerRadius.BottomRight, y + h);
            path.AddLine(x + w - this.CornerRadius.BottomRight, y + h, x + this.CornerRadius.BottomLeft, y + h);
            path.AddBezier(x + this.CornerRadius.BottomLeft, y + h, x, y + h, x, y + h - this.CornerRadius.BottomLeft, x, y + h - this.CornerRadius.BottomLeft);
            path.AddLine(x, y + h - this.CornerRadius.BottomLeft, x, y + this.CornerRadius.TopLeft);
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 获取该圆角矩形的GraphicsPath对象(圆角使用矩形圆弧曲线曲线实现)
        /// </summary>
        /// <returns></returns>
        /// User:K.Anding  CreateTime:2011-7-31 23:25.
        public GraphicsPath ToGraphicsArcPath() {
            GraphicsPath path = new GraphicsPath();
            int x = this.Rect.X;
            int y = this.Rect.Y;
            int w = this.Rect.Width;
            int h = this.Rect.Height;
            path.AddArc(x, y, this.CornerRadius.TopLeft, this.CornerRadius.TopLeft, 180, 90);
            path.AddArc(x + w - this.CornerRadius.TopRight, y, this.CornerRadius.TopRight, this.CornerRadius.TopRight, 270, 90);
            path.AddArc(x + w - this.CornerRadius.BottomRight, y + h - this.CornerRadius.BottomRight,
                this.CornerRadius.BottomRight, this.CornerRadius.BottomRight,
                0, 90);
            path.AddArc(x, y + h - this.CornerRadius.BottomLeft, this.CornerRadius.BottomLeft, this.CornerRadius.BottomLeft, 90, 90);
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 获取该圆角矩形的GraphicsPath对象(天使之翼的区域样式，主要用于Tabcontrol的标签样式)
        /// </summary>
        /// <returns>
        /// Return a data(or instance) of GraphicsPath.
        /// </returns>
        /// User:Ryan  CreateTime:2011-07-20 11:52.
        public GraphicsPath ToGraphicsAnglesWingPath() {
            GraphicsPath path = new GraphicsPath();
            int x = this.Rect.X;
            int y = this.Rect.Y;
            int w = this.Rect.Width;
            int h = this.Rect.Height;
            path.AddBezier(x, y + this.CornerRadius.TopLeft, x, y, x + this.CornerRadius.TopLeft, y, x + this.CornerRadius.TopLeft, y);
            path.AddLine(x + this.CornerRadius.TopLeft, y, x + w - this.CornerRadius.TopRight, y);
            path.AddBezier(x + w - this.CornerRadius.TopRight, y, x + w, y, x + w, y + this.CornerRadius.TopRight, x + w, y + this.CornerRadius.TopRight);
            path.AddLine(x + w, y + this.CornerRadius.TopRight, x + w, y + h - this.CornerRadius.BottomRight);
            path.AddBezier(x + w, y + h - this.CornerRadius.BottomRight, x + w, y + h, x + w + this.CornerRadius.BottomRight, y + h, x + w + this.CornerRadius.BottomRight, y + h);
            path.AddLine(x + w + this.CornerRadius.BottomRight, y + h, x - this.CornerRadius.BottomLeft, y + h);
            path.AddBezier(x - this.CornerRadius.BottomLeft, y + h, x, y + h, x, y + h - this.CornerRadius.BottomLeft, x, y + h - this.CornerRadius.BottomLeft);
            path.AddLine(x, y + h - this.CornerRadius.BottomLeft, x, y + this.CornerRadius.TopLeft);
            path.CloseFigure();
            return path;
        }

        #endregion
    }
}
