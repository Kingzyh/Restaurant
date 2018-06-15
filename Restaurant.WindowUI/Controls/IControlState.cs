using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Controls {

    [Flags]
    public enum ControlState {
        None,

        /// <summary>
        /// 默认状态
        /// </summary>
        Default,

        /// <summary>
        /// 高亮状态（鼠标悬浮）
        /// </summary>
        HeightLight,

        /// <summary>
        /// 焦点（鼠标按下、已选择、输入状态等）
        /// </summary>
        Focused,
    }

    public delegate void StateChanged(object sender, ControlState state);

    public class StateChangedEventArgs : EventArgs {
        public StateChangedEventArgs(ControlState state) {
            this.State = state;
        }

        public ControlState State { get; }
    }

    public interface IControlState {
        ControlState ControlState { get; set; }
    }
}
