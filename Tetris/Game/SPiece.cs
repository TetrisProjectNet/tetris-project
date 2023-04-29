using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Game
{
    public class SPiece : TetrisPiece
    {
        private readonly Block[][] blocks = new Block[][]
        {
            new Block[] { new(0, 1, 1), new(1, 1, 1), new(1, 0, 1), new(2, 0, 1) }
        };
        
        public int stateNumber = 0;
        public override Block[][] Blocks => blocks;
    }
}
