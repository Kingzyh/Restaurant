using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Restaurant.Controls.TXButton {
    [DefaultEvent("Click")]
    [ToolboxBitmap(typeof(Button))]
    public partial class TXButton : Button {

        #region Fields

        /// <summary>
        /// 圆角值
        /// </summary>
        private int _cornerRadius = 2;
        /// <summary>
        /// 内容边距间隔
        /// </summary>
        private int _margin = 4;
        /// <summary>
        /// 图标大小
        /// </summary>
        private Size _imageSize = new Size(16, 16);
        /// <summary>
        /// 控件的状态
        /// </summary>
        private ControlState _state = ControlState.Default;

        #endregion

        #region Events

        public event EventHandler<StateChangedEventArgs> StateChanged;

        protected void OnStateChanged(ControlState state) {
            StateChanged?.Invoke(this, new StateChangedEventArgs(state));
            this._state = state;
            this.Invalidate();
            if (this.Focused) {
                if (state == ControlState.Default) {
                    OnStateChanged(ControlState.HeightLight);
                    return;
                }
            }
        }

        #endregion

        #region Constructors

        public TXButton() {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.Size = new Size(100, 28);
            this.ResetRegion();
        }

        #endregion

        #region Properties

        [Category("TXProperties")]
        [Description("圆角的半径值")]
        [DefaultValue(2)]
        public int CornerRadius {
            get { return this._cornerRadius; }
            set {
                this._cornerRadius = value;
                this.Invalidate();
            }
        }

        [Category("TXProperties")]
        [Browsable(true)]
        [Description("图标大小")]
        [DefaultValue(typeof(Size), "16,16")]
        public Size ImageSize {
            get { return this._imageSize; }
            set {
                this._imageSize = value;
                this.Invalidate();
            }
        }



        #endregion

        #region Override Methods

        protected override void OnMouseEnter(EventArgs e) {
            base.OnMouseEnter(e);
            this.OnStateChanged(ControlState.HeightLight);
        }

        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);
            this.OnStateChanged(ControlState.Default);
        }

        protected override void OnMouseDown(MouseEventArgs mevent) {
            base.OnMouseDown(mevent);
            if (mevent.Button == MouseButtons.Left) {
                this.OnStateChanged(ControlState.Focused);
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent) {
            base.OnMouseUp(mevent);
            if (mevent.Button == MouseButtons.Left) {
                this.OnStateChanged(ControlState.Default);
            }
        }

        protected override void OnKeyUp(KeyEventArgs kevent) {
            base.OnKeyUp(kevent);
            if (kevent.KeyCode == Keys.Space) {
                this.OnStateChanged(ControlState.HeightLight);
                this.OnClick(kevent);
            }
        }

        protected override void OnGotFocus(EventArgs e) {
            base.OnGotFocus(e);
            this.OnStateChanged(ControlState.HeightLight);
        }

        protected override void OnLostFocus(EventArgs e) {
            base.OnLostFocus(e);
            this.OnStateChanged(ControlState.Default);
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            this.ResetRegion();
        }

        protected override void OnCreateControl() {
            base.OnCreateControl();
            this.ResetRegion();
        }

        protected override void OnPaint(PaintEventArgs pevent) {
            base.OnPaint(pevent);
            this.ResetRegion();
            this.DrawBackGround(pevent.Graphics);
            this.DrawContent(pevent.Graphics);
        }

        protected override void OnRegionChanged(EventArgs e) {
            base.OnRegionChanged(e);
        }

        #endregion

        #region Methods

        protected virtual void ResetRegion() {
            if (this._cornerRadius > 0) {
                Rectangle rect = new Rectangle(Point.Empty, this.Size);
                RoundRectangle roundRect = new RoundRectangle(rect, new CornerRadius(this._cornerRadius));
                if (this.Region != null) {
                    this.Region.Dispose();
                }

                this.Region = new Region(roundRect.ToGraphicsBezierPath());
            }
        }

        private void DrawBackGround(Graphics g) {
            if (this.Width <= 3 || this.Height <= 3) {
                return;
            }
            Rectangle rect = new Rectangle(1, 1, this.Width - 3, this.Height - 3);
            RoundRectangle roundRect = new RoundRectangle(rect, this._cornerRadius);
            switch (this._state) {
                case ControlState.Default:
                    if (this.FlatStyle != FlatStyle.Flat) {
                        using (GraphicsPath path = roundRect.ToGraphicsBezierPath()) {
                            using (LinearGradientBrush brush = new LinearGradientBrush(roundRect.Rect, Color.Yellow, Color.Blue, LinearGradientMode.Vertical)) {
                                g.FillPath(brush, path);
                            }
                            using (Pen pen = new Pen(Color.Red, 1)) {
                                g.DrawPath(pen, path);
                            }
                        }
                    }
                    break;
                case ControlState.HeightLight:
                    using (GraphicsPath path = roundRect.ToGraphicsBezierPath()) {
                        using (LinearGradientBrush brush = new LinearGradientBrush(roundRect.Rect, Color.Aqua, Color.Aquamarine, LinearGradientMode.Vertical)) {
                            g.FillPath(brush, path);
                        }
                        using (Pen pen = new Pen(Color.Red, 1)) {
                            g.DrawPath(pen, path);
                        }
                    }
                    break;
                case ControlState.Focused:
                    using (GraphicsPath path = roundRect.ToGraphicsBezierPath()) {
                        using (LinearGradientBrush brush = new LinearGradientBrush(roundRect.Rect, Color.Beige, Color.BlanchedAlmond, LinearGradientMode.Vertical)) {
                            g.FillPath(brush, path);
                        }
                        using (Pen pen = new Pen(Color.Red, 1)) {
                            g.DrawPath(pen, path);
                        }
                    }
                    var innerRoundRect = new RoundRectangle(new Rectangle(roundRect.Rect.X + 1, roundRect.Rect.Y + 1, roundRect.Rect.Width - 2, roundRect.Rect.Height - 2), roundRect.CornerRadius);
                    using (GraphicsPath path = innerRoundRect.ToGraphicsBezierPath()) {
                        using (Pen pen = new Pen(Color.Red, 1)) {
                            g.DrawPath(pen, path);
                        }
                    }
                    break;
            }
        }

        private void DrawContent(Graphics g) {
            Rectangle imageRect;
            Rectangle textRect;
            this.CalculateRect(out imageRect, out textRect);
            if (this.Image != null) {
                g.DrawImage(this.Image, imageRect, 0, 0, this._imageSize.Width, this._imageSize.Height, GraphicsUnit.Pixel);
            }

            TextRenderer.DrawText(g, this.Text, this.Font, textRect, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void CalculateRect(out Rectangle imageRect, out Rectangle textRect) {
            imageRect = Rectangle.Empty;
            textRect = Rectangle.Empty;
            if (Image == null) {
                textRect = new Rectangle(
                   this._margin,
                   this._margin,
                   this.Width - this._margin * 2,
                   this.Height - this._margin * 2);
                return;
            }
            Size textSize = TextRenderer.MeasureText(this.Text, this.Font);
            int textMaxWidth = this.Width - this._imageSize.Width - this._margin * 3;
            int textWidth = textSize.Width >= textMaxWidth ? textMaxWidth : textSize.Width;
            int contentWidth = this._margin + this._imageSize.Width + textWidth;
            switch (TextImageRelation) {
                case TextImageRelation.Overlay:
                    imageRect = new Rectangle(
                        this._margin,
                        (this.Height - this._imageSize.Height) / 2,
                        this._imageSize.Width,
                        this._imageSize.Height);
                    textRect = new Rectangle(
                        this._margin,
                        this._margin,
                        this.Width - this._margin * 2,
                        this.Height);
                    break;
                case TextImageRelation.ImageAboveText:
                    imageRect = new Rectangle(
                        (this.Width - this._imageSize.Width) / 2,
                        this._margin,
                        this._imageSize.Width,
                        this._imageSize.Height);
                    textRect = new Rectangle(
                        this._margin,
                        imageRect.Bottom,
                        this.Width - this._margin * 2,
                        this.Height - imageRect.Bottom - this._margin);
                    break;
                case TextImageRelation.ImageBeforeText:
                    imageRect = new Rectangle(
                        (this.Width - contentWidth) / 2,
                        (this.Height - this._imageSize.Height) / 2,
                        this._imageSize.Width,
                        this._imageSize.Height);
                    textRect = new Rectangle(
                        imageRect.Right + this._margin,
                        this._margin,
                        textWidth,
                        this.Height - this._margin * 2);
                    break;
                case TextImageRelation.TextAboveImage:
                    imageRect = new Rectangle(
                        (this.Width - this._imageSize.Width) / 2,
                        this.Height - this._imageSize.Height - this._margin,
                        this._imageSize.Width,
                        this._imageSize.Height);
                    textRect = new Rectangle(
                        this._margin,
                        this._margin,
                        this.Width - this._margin * 2,
                        this.Height - imageRect.Y - this._margin);
                    break;
                case TextImageRelation.TextBeforeImage:
                    imageRect = new Rectangle(
                        (this.Width + contentWidth) / 2 - this._imageSize.Width,
                        (this.Height - this._imageSize.Height) / 2,
                        this._imageSize.Width,
                        this._imageSize.Height);
                    textRect = new Rectangle(
                        (this.Width - contentWidth) / 2,
                        this._margin,
                        textWidth,
                        this.Height - this._margin * 2);
                    break;
            }

            if (RightToLeft == RightToLeft.Yes) {
                imageRect.X = this.Width - imageRect.Right;
                textRect.X = this.Width - textRect.Right;
            }
        }

        #endregion
    }
}
;