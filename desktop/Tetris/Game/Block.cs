using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Game
{
    public class Block
    {
        public BlockPosition position;
        private int id;
        public int Id
        {
            get => id;
        }

        public Block(int x, int y, int id)
        {
            position = new BlockPosition(x, y);
            this.id = id;
        }
    }
}
