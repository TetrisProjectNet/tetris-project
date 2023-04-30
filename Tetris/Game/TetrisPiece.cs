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

        public void incrementState()
        {
            if (stateNumber + 1 > Blocks.Length - 1 || Blocks.Length == 1) {
                stateNumber = 0;
                return;
            }
            stateNumber++;
        }

        public int previewNextState()
        {
            if (stateNumber + 1 > Blocks.Length - 1 || Blocks.Length == 1) return 0;
            return stateNumber + 1;
        }
    }
}
