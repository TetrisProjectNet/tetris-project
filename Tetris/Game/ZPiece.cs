using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Game
{
    public class ZPiece : TetrisPiece
    {
        private readonly Block[][] blocks = new Block[][]
        {
            new Block[] { new(0, 0, 5), new(1, 0, 5), new(1, 1, 5), new(2, 1, 5) }
        };
        
        public int stateNumber = 0;
        public override Block[][] Blocks => blocks;
    }
}
