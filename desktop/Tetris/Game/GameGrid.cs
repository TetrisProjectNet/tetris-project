using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.References;

namespace Tetris.Game {
    public class GameGrid {
        private int[,] _grid = new int[20, 10];
        private TetrisPiece[] tetrisPieces = new TetrisPiece[]
        {
            new SPiece(),
            new IPiece(),
            new LPiece(),
            new TPiece(),
            new ZPiece(),
            new OPiece(),
            new JPiece(),
        };

        public readonly string[] fullPieces = new string[]
        {
            "fullgreen.png",
            "fulllightblue.png",
            "fullorange.png",
            "fullpurple.png",
            "fullred.png",
            "fullyellow.png",
            "fulldarkblue.png",
        };

        private TetrisPiece _currentPiece;
        private BlockPosition _currentOffset = new(3, 0);
        private Random random = new Random();

        private int _nextId = -1;
        private int _points = 0;
        private int _clearedRows = 0;
        private int _heldPieceId = -1;

        public int NextId {
            get => _nextId;
        }

        public int HeldPieceId {
            get => _heldPieceId;
        }

        public int Points {
            get => _points;
        }

        public int ClearedRows {
            get => _clearedRows;
        }

        public TetrisPiece CurrentPiece {
            get => _currentPiece;
        }

        public BlockPosition CurrentOffset {
            get => _currentOffset;
        }

        public int[,] Grid {
            get => _grid;
        }

        public GameGrid() {
            _nextId = GetRandomTetrisPieceId(random);
            _currentPiece = tetrisPieces[_nextId];
            _nextId = GetRandomTetrisPieceId(random);
        }

        public void SetNewBlock() {
            _currentOffset = new BlockPosition(3, 0);
            _currentPiece = tetrisPieces[_nextId];
            _nextId = GetRandomTetrisPieceId(random);
        }

        private int GetRandomTetrisPieceId(Random random) {
            int index = _nextId;
            do {
                _nextId = random.Next(tetrisPieces.Length);
            } while (index == _nextId);

            return _nextId;
        }

        public void MovePieceRight() {
            if (_currentOffset.X + _currentPiece.Blocks[_currentPiece.stateNumber][3].position.X > 8) return;
            _currentOffset = new(++_currentOffset.X, _currentOffset.Y);
        }

        public void MovePieceLeft() {
            if (_currentOffset.X + _currentPiece.Blocks[_currentPiece.stateNumber][0].position.X < 1) return;
            _currentOffset = new(--_currentOffset.X, _currentOffset.Y);
        }

        public bool MovePieceDown() {
            Block[] NextPlace = _currentPiece.Blocks[_currentPiece.stateNumber];
            for (int i = 0; i < _currentPiece.Blocks[_currentPiece.stateNumber].Length; i++) {
                if (NextPlace[i].position.Y + _currentOffset.Y > 18 || isPositionOccupied(NextPlace[i].position, new BlockPosition(0, 1))) {
                    return true;
                }
            }
            _currentOffset = new(_currentOffset.X, ++_currentOffset.Y);
            return false;
        }

        public bool isValidLeftMove() {
            Block[] current = _currentPiece.Blocks[_currentPiece.stateNumber];
            for (int i = 0; i < 4; i++) {
                if (!isValidGridPosition(current[i].position, new BlockPosition(-1, 0))) return false;
                if (isPositionOccupied(current[i].position, new BlockPosition(-1, 0))) return false;
            }

            return true;
        }

        public bool isValidRightMove() {
            Block[] current = _currentPiece.Blocks[_currentPiece.stateNumber];
            for (int i = 0; i < 4; i++) {
                if (!isValidGridPosition(current[i].position, new BlockPosition(1, 0))) return false;
                if (isPositionOccupied(current[i].position, new BlockPosition(1, 0))) return false;
            }

            return true;
        }

        public bool isValidGridPosition(BlockPosition position, BlockPosition offset) {
            if (position.X + offset.X + _currentOffset.X > 9) return false;
            if (position.X + offset.X + _currentOffset.X < 0) return false;
            return true;
        }

        public void RotatePiece() {
            bool NextRotationValid = isNextRotationValid();
            if (NextRotationValid) _currentPiece.incrementState();
        }

        public bool isPositionOccupied(BlockPosition position, BlockPosition offset) {
            if (_grid[position.Y + offset.Y + _currentOffset.Y, position.X + offset.X + _currentOffset.X] == 0) return false;
            return true;
        }
        public bool isPositionOccupied(BlockPosition position) {
            if (_grid[position.Y + _currentOffset.Y, position.X + _currentOffset.X] == 0) return false;
            return true;
        }

        public bool isNextRotationValid() {
            Block[] NextRotation = _currentPiece.Blocks[_currentPiece.previewNextState()];
            int nextMaxW;
            for (int i = 0; i < NextRotation.Length; i++) {
                if (!isValidGridPosition(NextRotation[i].position, new BlockPosition(0, 0))) return false;
                if (isPositionOccupied(NextRotation[i].position)) return false;
                nextMaxW = NextRotation[i].position.X + _currentOffset.X;
                if (nextMaxW == 10) {
                    switch (NextRotation[i].Id) {
                        case 1:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            if (!isPositionOccupied(NextRotation[i].position, new BlockPosition(-2, 0))) {
                                _currentOffset.X = 7;
                            }

                            break;
                        case 2:
                            if (!isPositionOccupied(NextRotation[i].position, new BlockPosition(-1, 0))) {
                                _currentOffset.X = 6;
                            }

                            break;
                    }
                }
                if (nextMaxW == 0) {
                    switch (NextRotation[i].Id) {
                        case 1:
                        case 3:
                        case 4:
                        case 5:
                        case 7:
                            if (!isPositionOccupied(NextRotation[i].position, new BlockPosition(1, 0))) {
                                _currentOffset.X = 0;
                            }

                            break;
                        case 2:
                            if (!isPositionOccupied(NextRotation[i].position, new BlockPosition(2, 0))) {
                                _currentOffset.X = 0;
                            }

                            break;
                        case 6:
                            return true;

                            break;
                    }
                }
            }

            return true;
        }

        public void SaveCurrentPieceData() {
            string saved = Preferences.Default.Get($"skinsUsed", "0000000");
            char skinUsed = saved[ShopItemIdReferences.SettingsIdToTile[_currentPiece.Blocks[0][0].Id - 1]];
            int skinId = Preferences.Default.Get($"skinSlot{ShopItemIdReferences.SettingsIdToTile[_currentPiece.Blocks[0][0].Id - 1]}", -1);

            int state = _currentPiece.stateNumber;
            for (int i = 0; i < 4; i++) {
                _grid[_currentPiece.Blocks[state][i].position.Y + _currentOffset.Y, _currentPiece.Blocks[state][i].position.X + _currentOffset.X] =
                    skinId == -1 || skinUsed == '0' ? _currentPiece.Blocks[state][i].Id : skinId + 100;
            }
        }

        public void HandleRowManagement() {
            int count = 0;
            for (int i = 0; i < _grid.GetLength(0); i++) {
                count = 0;
                for (int j = 0; j < _grid.GetLength(1); j++) {
                    if (_grid[i, j] != 0) count++;
                }

                if (count == 10) {
                    ClearRow(i);
                    MoveDownGrid(i);
                    _clearedRows++;
                }
            }
        }

        public void ClearRow(int row) {
            for (int i = 0; i < _grid.GetLength(1); i++) {
                _grid[row, i] = 0;
            }
        }

        public void ClearGrid() {
            for (int i = 0; i < _grid.GetLength(0); i++) {
                for (int j = 0; j < _grid.GetLength(1); j++) {
                    _grid[i, j] = 0;
                }
            }
        }

        public void MoveDownGrid(int row) {
            for (int i = row - 1; i > 0; i--) {
                for (int j = 0; j < _grid.GetLength(1); j++) {
                    _grid[i + 1, j] = _grid[i, j];
                }
            }
        }

        public void SwitchFirst() {
            _currentOffset = new BlockPosition(3, 0);
            _heldPieceId = _currentPiece.Blocks[0][0].Id - 1;
            _currentPiece = tetrisPieces[_nextId];
            _nextId = GetRandomTetrisPieceId(random);
        }

        public void Switch() {
            TetrisPiece tmp = _currentPiece;
            _currentPiece = tetrisPieces[_heldPieceId];
            _heldPieceId = tmp.Blocks[0][0].Id - 1;
        }
    }
}
