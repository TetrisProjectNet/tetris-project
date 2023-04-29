using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Game
{
    public class IPiece : TetrisPiece
    {
        private readonly Block[][] blocks = new Block[][]
        {
            new Block[] { new(0, 1, 2), new(1, 1, 2), new(2, 1, 2), new(3, 1, 2), },
            new Block[] { new(2, 0, 2), new(2, 1, 2), new(2, 2, 2), new(2, 3, 2), },
            new Block[] { new(0, 2, 2), new(1, 2, 2), new(2, 2, 2), new(3, 2, 2), },
            new Block[] { new(1, 0, 2), new(1, 1, 2), new(1, 2, 2), new(1, 3, 2), }
        };
        
        public int stateNumber = 0;
        public override Block[][] Blocks => blocks;
    }
}
