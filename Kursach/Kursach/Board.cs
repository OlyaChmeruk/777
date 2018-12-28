using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Kursach
{
    class Board
    {
        const int BOARD_SIZE = 4;
        public Cell[,] gameBoard;
        Random random = new Random();
        int cellAddValue=0;

        /**
        Prepare board on create.
        */
        public Board()
        {
            gameBoard = new Cell[BOARD_SIZE, BOARD_SIZE];
            resetBoard();      
        }
        /**
         Reset Board - clear board.
        */
        public void resetBoard()
        {
            cellAddValue = 0;
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    gameBoard[i, j] = new Cell();
                }
            }
            addNewField();
            addNewField();        
        }

        /**
        draw field and set there beggining value value 
            if field has 2 as a value double it and draw another field.
        */
        public void addNewField()
        {
            int row, column, n;
            bool notValid = true;
            while (notValid)
            {
                row = random.Next(0, BOARD_SIZE);
                column = random.Next(0, BOARD_SIZE);
                if (gameBoard[row, column].getValue() == 0)
                {
                    n = random.Next(10) < 9 ? 2 : 4;
                    gameBoard[row, column].setValue(n);
                    notValid = false;
                }
            }
        }

        /**
        Take Biggest tile from board and returns it.
        */
        public int takeBiggestTile()
        {
            int max = gameBoard[0, 0].getValue();
            for(int i=0;i<BOARD_SIZE;i++)
            {
                for(int j=0;j<BOARD_SIZE;j++)
                {
                    if (gameBoard[i, j].getValue() > max)
                        max = gameBoard[i, j].getValue();
                }
            }
            return max;
        }

        /**
        Check whether game is over. 
        Game over - If grid is full and we can't do any move.
        */
        public bool isGameOver()
        {
            return isGridFull() && !isMovePossible();
        }

        /**
        Check whether grid is full. (Every single cell can't be 0 value).
        */
        private bool isGridFull()
        {
            for (int rows = 0; rows < BOARD_SIZE; rows++)
            {
                for (int columns = 0; columns < BOARD_SIZE; columns++)
                {
                    if (gameBoard[rows, columns].isZeroValue())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /**
        Check whether move is possible.
        First loops for checking right and left move, second for up and down move.
        */
        private bool isMovePossible()
        {
            for (int rows = 0; rows < BOARD_SIZE; rows++)
            {
                for (int columns = 0; columns < (BOARD_SIZE - 1); columns++)
                {
                    int columnsPlus = columns + 1;
                    if (gameBoard[rows, columns].getValue() == gameBoard[rows, columnsPlus].getValue())
                    {
                        return true;
                    }
                }
            }
            for (int columns = 0; columns < BOARD_SIZE; columns++)
            {
                for (int rows = 0; rows < (BOARD_SIZE - 1); rows++)
                {
                    int rowsPlus = rows + 1;
                    if (gameBoard[rows, columns].getValue() == gameBoard[rowsPlus, columns].getValue())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /**
        Move cells up.
        */
        public bool moveCellsUp()
        {
            bool busy = false;

            if (moveCellsUpLoop())
                busy = true;

            for (int rows = 0; rows < BOARD_SIZE; rows++)
            {
                for (int columns = 0; columns < (BOARD_SIZE - 1); columns++)
                {
                    int columnsPlus = columns + 1;
                    busy = combineCells(rows, columnsPlus, rows, columns, busy);
                }
            }
            if (moveCellsUpLoop())
               busy = true;

            return busy;
        }

        /**
        Looped move cells up.
        */
        private bool moveCellsUpLoop()
        {
            bool busy = false;
            for (int rows = 0; rows < BOARD_SIZE; rows++)
            {
                bool columnBusy = false;
                do
                {
                    columnBusy = false;
                    for (int columns = 0; columns < (BOARD_SIZE - 1); columns++)
                    {
                        int columnsPlus = columns + 1;
                        bool cellBusy = moveCell(rows, columnsPlus, rows, columns);
                        if (cellBusy)
                        {
                            columnBusy = true;
                            busy = true;
                        }
                    }
                } while (columnBusy);
            }
            return busy;
        }

        /**
        Move cells down.
        */
        public bool moveCellsDown()
        {
            bool busy = false;
            if (moveCellsDownLoop()) busy = true;
            for (int rows = 0; rows < BOARD_SIZE; rows++)
            {
                for (int columns = BOARD_SIZE - 1; columns > 0; columns--)
                {
                    int columnsPlus = columns - 1;
                    busy = combineCells(rows, columnsPlus, rows, columns, busy);
                }
            }
            if (moveCellsDownLoop()) busy = true;
            return busy;
        }

        /**
       Looped move cells down.
       */
        private bool moveCellsDownLoop()
        {
            bool busy = false;
            for (int rows = 0; rows < BOARD_SIZE; rows++)
            {
                bool columnBusy = false;
                do
                {
                    columnBusy = false;
                    for (int columns = BOARD_SIZE - 1; columns > 0; columns--)
                    {
                        int columnsPlus = columns - 1;
                        bool cellBusy = moveCell(rows, columnsPlus, rows, columns);
                        if (cellBusy)
                        {
                            columnBusy = true;
                            busy = true;
                        }
                    }
                } while (columnBusy);
            }
            return busy;
        }

        /**
        Move cells left.
        */
        public bool moveCellsLeft()
        {
            bool busy = false;
            if (moveCellsLeftLoop()) busy = true;
            for (int columns = 0; columns < BOARD_SIZE; columns++)
            {
                for (int rows = 0; rows < (BOARD_SIZE - 1); rows++)
                {
                    int rowsPlus = rows + 1;
                    busy = combineCells(rowsPlus, columns, rows, columns, busy);
                }
            }
            if (moveCellsLeftLoop()) busy = true;
            return busy;
        }

        /**
       Looped move cells left.
       */
        private bool moveCellsLeftLoop()
        {
            bool busy = false;
            for (int columns = 0; columns < BOARD_SIZE; columns++)
            {
                bool rowBusy = false;
                do
                {
                    rowBusy = false;
                    for (int rows = 0; rows < (BOARD_SIZE - 1); rows++)
                    {
                        int rowsPlus = rows + 1;
                        bool cellBusy = moveCell(rowsPlus, columns, rows, columns);
                        if (cellBusy)
                        {
                            rowBusy = true;
                            busy = true;
                        }
                    }
                } while (rowBusy);
            }
            return busy;
        }

        /**
        Move cells right.
        */
        public bool moveCellsRight()
        {
            bool busy = false;
            if (moveCellsRightLoop()) busy = true;
            for (int columns = 0; columns < BOARD_SIZE; columns++)
            {
                for (int rows = (BOARD_SIZE - 1); rows > 0; rows--)
                {
                    int rowsPlus = rows - 1;
                   busy = combineCells(rowsPlus, columns, rows, columns, busy);
                }
            }
            if (moveCellsRightLoop()) busy = true;
            return busy;
        }

        /**
       Looped move cells right.
       */
        private bool moveCellsRightLoop()
        {
            bool busy = false;
            for (int columns = 0; columns < BOARD_SIZE; columns++)
            {
                bool rowBusy = false;
                do
                {
                    rowBusy = false;
                    for (int rows = (BOARD_SIZE - 1); rows > 0; rows--)
                    {
                        int rowsPlus = rows - 1;
                        bool cellBusy = moveCell(rowsPlus, columns, rows, columns);
                        if (cellBusy)
                        {
                            rowBusy = true;
                            busy = true;
                        }
                    }
                } while (rowBusy);
            }
            return busy;
        }

        /**
        Combine/merge cells with the same values
        */
        private bool combineCells(int x, int y, int x1, int y1,bool busy)
        {
            if (!gameBoard[x, y].isZeroValue())
            {
                int n = gameBoard[x, y].getValue();
                if (gameBoard[x1, y1].getValue() == n)
                {
                    int newValue = n + n;
                    gameBoard[x1, y1].setValue(newValue);
                    gameBoard[x, y].setZeroValue();
                    cellAddValue += newValue;
                    busy = true;
                }
            }
            return busy;
        }
       
        /**
        Get value of current score (after slide).
        */
        public int getScoreValue()
        {
            return cellAddValue;
        }

        /**
        Move cell when there are empty cells (zero value cells).
        */
        private bool moveCell(int x, int y, int x1, int y1)
        {
            bool busy = false;
            if (!gameBoard[x, y].isZeroValue()
                    && (gameBoard[x1, y1].isZeroValue()))
            {
                int n = gameBoard[x, y].getValue();
                gameBoard[x1, y1].setValue(n);
                gameBoard[x, y].setValue(0);
                busy = true;
            }
            return busy;
        }
    }

}


