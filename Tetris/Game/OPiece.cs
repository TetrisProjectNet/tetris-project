using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Game
{
    public class OPiece : TetrisPiece
    {
        private readonly Block[][] blocks = new Block[][]
        {
            new Block[] { new(1, 0, 6), new(2, 0, 6), new(1, 1, 6), new(2, 1, 6) }
        };
        
        public int stateNumber = 0;
        public override Block[][] Blocks => blocks;
    }
}
