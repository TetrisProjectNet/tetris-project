using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Game
{
    public class LPiece : TetrisPiece
    {
        private readonly Block[][] blocks = new Block[][]
        {
            new Block[] { new(0, 1, 3), new(1, 1, 3), new(2, 1, 3), new(2, 0, 3) },
            new Block[] { new(1, 0, 3), new(1, 1, 3), new(1, 2, 3), new(2, 2, 3) },
            new Block[] { new(0, 1, 3), new(0, 2, 3), new(1, 1, 3), new(2, 1, 3) },
            new Block[] { new(0, 0, 3), new(1, 0, 3), new(1, 1, 3), new(1, 2, 3) },
        };

        public override Block[][] Blocks => blocks;
    }
}
