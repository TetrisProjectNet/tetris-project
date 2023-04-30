using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Game
{
    public class JPiece : TetrisPiece
    {
        private readonly Block[][] blocks = new Block[][]
        {
            new Block[] { new(0, 0, 7), new(0, 1, 7), new(1, 1, 7), new(2, 1, 7) },
            new Block[] { new(1, 0, 7), new(1, 1, 7), new(1, 2, 7), new(2, 0, 7) },
            new Block[] { new(0, 1, 7), new(1, 1, 7), new(2, 1, 7), new(2, 2, 7) },
            new Block[] { new(0, 2, 7), new(1, 0, 7), new(1, 1, 7), new(1, 2, 7) },
        };

        public override Block[][] Blocks => blocks;
    }
}
