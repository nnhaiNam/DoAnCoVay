using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGoGame
{
    public class ChessBroad
    {
        public int[,] board = new int[,] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0}
        };





        public void SetPosition(int x, int y, int z)
        {
            if (x >= 1 && y >= 1 && x <= 9 && y <= 9)
            {
                board[y - 1, x - 1] = z;

            }
            
            string str = "";
         

        }


        public void Display()
        {
            string str = "";
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    //Console.Write(board[i, j] + " ");
                    str += board[i, j] + " ";
                }
                //Console.WriteLine();
                str += "\n";
            }
            //Console.WriteLine("----------------------");

            MessageBox.Show(str);
        }

        public bool IsStoneCaptured(int row, int col)
        {
            int stoneColor = board[row, col];
            int size = board.GetLength(0);
            bool[,] visited = new bool[size, size];

            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((row, col));
            visited[row, col] = true;

            List<(int, int)> connectedStones = new List<(int, int)>();

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int r = current.Item1;
                int c = current.Item2;

                connectedStones.Add((r, c));

                int[] dr = { -1, 1, 0, 0 };
                int[] dc = { 0, 0, -1, 1 };

                for (int i = 0; i < 4; i++)
                {
                    int nr = r + dr[i];
                    int nc = c + dc[i];

                    if (nr >= 0 && nr < size && nc >= 0 && nc < size && !visited[nr, nc])
                    {
                        if (board[nr, nc] == 0)
                        {
                            return false; 
                        }
                        else if (board[nr, nc] == stoneColor)
                        {
                            queue.Enqueue((nr, nc));
                            visited[nr, nc] = true;
                        }
                    }
                }
            }

            // Check if all connected stones have no liberty
            foreach (var stone in connectedStones)
            {
                int r = stone.Item1;
                int c = stone.Item2;

                int[] dr = { -1, 1, 0, 0 };
                int[] dc = { 0, 0, -1, 1 };

                for (int i = 0; i < 4; i++)
                {
                    int nr = r + dr[i];
                    int nc = c + dc[i];

                    if (nr >= 0 && nr < size && nc >= 0 && nc < size && board[nr, nc] == 0)
                    {
                        return false; 
                    }
                }
            }

            return true; 
        }

        public List<(int, int)> GetCapturedStones()
        {
            List<(int, int)> capturedStones = new List<(int, int)>();
            int size = board.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {

                    if (board[i, j] != 0 && IsStoneCaptured(i, j))
                    {
                        
                        capturedStones.Add((i, j));
                    }
                }
            }
            
            return capturedStones;
        }
        public List<(int, int)> GetCapturedStones2(int side)
        {
            int Piece = 0;
            if(side==0)
            {
                Piece = 1;
            }
            else
            {
                Piece = 2;
            }
            List<(int, int)> capturedStones = new List<(int, int)>();
            int size = board.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {

                    if (board[i, j] != 0 && IsStoneCaptured(i, j) && board[i,j]!=Piece)
                    {

                        capturedStones.Add((i, j));
                    }
                }
            }

            return capturedStones;
        }

        //
        public List<(int, int)> GetCapturedStones3(int side)
        {
            int Piece = 0;
            if (side == 0)
            {
                Piece = 1;
            }
            else
            {
                Piece = 2;
            }
            List<(int, int)> capturedStones = new List<(int, int)>();
            int size = board.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {

                    if (board[i, j] != 0 && IsStoneCaptured(i, j) && board[i, j] == Piece)
                    {

                        capturedStones.Add((i, j));
                    }
                }
            }

            return capturedStones;
        }


        public void ResetBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] =0;
                }                
            }            
        }


    }
}
