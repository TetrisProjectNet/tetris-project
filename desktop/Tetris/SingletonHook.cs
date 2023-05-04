using SharpHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class SingletonHook {

        private static readonly Lazy<TaskPoolGlobalHook> lazy =
            new Lazy<TaskPoolGlobalHook>(() => new TaskPoolGlobalHook());

        public static TaskPoolGlobalHook Instance { get { return lazy.Value; } }

        private SingletonHook() {
        }
    }
}
