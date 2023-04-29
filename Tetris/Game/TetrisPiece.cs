using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Game
{
    public abstract class TetrisPiece
    {
        public abstract Block[][] Blocks { get; }
        public int stateNumber = 0;
    }
}
