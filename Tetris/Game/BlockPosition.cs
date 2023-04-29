using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Game
{
    public class BlockPosition
    {
        private int _x;
        private int _y;

        public int X
        {
            get => _x;
            set => _x = value;
        }

        public int Y
        {
            get => _y;
            set => _y = value;
        }

        public BlockPosition(int x, int y)
        {
            this._x = x;
            this._y = y;
        }
    }
}
