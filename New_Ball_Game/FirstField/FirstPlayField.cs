using System;
using System.Text;
using System.Threading;
using New_Ball_Game.PlayFields;

namespace New_Ball_Game
{
    public class FirstPlayField: IPlayField 
    {
        private Cell[,] _field;
        private Random _random = new Random();
        private bool _lost = false;
        private int _height = 10;
        private int _width = 25;
        private int _leftEdge = 0;
        private int _rightEdge;
        private int _upEdge = 0;
        private int _bottomEdge;
        private int _currentX = 3;
        private int _currentY = 5;
        private int _energyBallcoordinatesX;
        private int _energyBallcoordinatesY;
        private const int _minimalX = 0;
        private const int _minimalY = 0;
        private int _ballStartPositionX = 1;
        private int _ballStartPositionY = 1;

        private bool _isBorderX(int x) => x == _leftEdge || x >= _rightEdge;

        private bool _isBorderY(int y) => y == _upEdge || y >= _bottomEdge;

        private void Add(Cell cell) => _field[cell.X, cell.Y] = cell;


        private Cell _get(int x, int y) => _field[x, y];

        public FirstPlayField()
        {
            _field = new Cell[_width, _height];
            _bottomEdge = _height - 1;
            _rightEdge = _width - 1;
            _energyBallcoordinatesX = _random.Next(_width);
            _energyBallcoordinatesY = _random.Next(_height);
            ControllUser();
            SetBall();
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(ToString());
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {

                    sb.Append(_field[x, y].Value);
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public void initField()
        {

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (IsBall(j, i))
                    {
                        CreateCell(j, i, CellType.BallCell);
                    } else if(IsUserCell(j , i)){
                        CreateCell(j, i, CellType.UserCell);
                    }else 
                    if (_isBorderX(j) || _isBorderY(i) || IsObstacles(j, i))
                    {
                        CreateCell(j, i, CellType.BorderCell);
                    }
                    else if (IsReward(j, i))
                    {
                        CreateCell(j, i, CellType.Coins);
                    }
                    else if (IsEnergyBall(j, i))
                    {
                        CreateCell(j, i, CellType.EnergyBall);
                    }
                    else {
                        CreateCell(j, i, CellType.EmptyCell);
                    }
                }

            }


        }

        public bool IsUserCell(int x , int y)
        {
            return x == _currentX && _currentY == y;
        }
       

        public bool IsEnergyBall(int x, int y)
        {
            return x == _energyBallcoordinatesX && y == _energyBallcoordinatesY;
        }

        public void CreateCell(int x, int y, CellType type)
        {
            Cell cell = new Cell(x, y, type);
            Add(cell);
        }

        public bool IsObstacles(int x, int y)
        {
            //right side
            if (y == 3 && x >= 5 && x <= 9 || y >= 3 && y <= 5 && x == 5)
            {

                return true;
            }
            //left side
            if (y == _height - 3 && x >= _width - 9 && x <= _width - 5 || y >= _height - 5 && y <= _height - 3 && x == _width - 5)
            {

                return true;
            }

            return false;
        }

        public bool IsReward(int x, int y)
        {

            //right side 
            if (y == 2 && x >= 5 && x <= 9 || y == _height - 2 && x >= _width - 9 && x <= _width - 5)
            {
                return true;
            }
            //left side 
            if (y >= 3 && y <= 5 && x == 2 || y >= _height - 5 && y <= _height - 3 && x == _width - 3)
            {
                return true;
            }

            return false;
        }

        public void setPlayer()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
          
            initField();
            Draw();
        
            do{ ProcessKey(key);} while (key.Key == ConsoleKey.Escape);
        }

       public void ControllUser(){
            initField();
            Draw();
            ConsoleKeyInfo key;
            while (!_lost){
            if (Console.KeyAvailable)
                { 
                    key = Console.ReadKey();

                    ProcessKey(key);

                    initField();
                    Draw();
                }else {
                Thread.Sleep(100);
               }
              }
           }

        private void ProcessKey(ConsoleKeyInfo key)
        {

            switch (key.Key)
            {
                case ConsoleKey.DownArrow:
                    if (InsideField(_currentX, _currentY))
                    {
                        _currentY++;

                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (InsideField(_currentX , _currentY))
                    {
                        _currentY--;

                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (InsideField(_currentX, _currentY))
                    {
                        _currentX--;

                    }
                    break;
                case ConsoleKey.RightArrow:

                    if (InsideField(_currentX, _currentY))
                    {
                        _currentX++;

                    }
                    break;
            }
        }

        public void CatchEnergyBall(int x , int y)
        {
            if (IsBall(x, y) == IsEnergyBall(x, y) || IsBall(x, y) == IsReward(x, y)) {
                CreateCell(x, y, CellType.EmptyCell);
            }
        }

        public bool InsideField(int x , int y)
        {
            if (x > _minimalX && x < _width && y > _minimalY && y < _height) {
                return true;
            }
            else
            {
                Console.WriteLine("Don't leave the borders of play field");
                return false;
            }
        }

        public bool IsBall(int x , int y)
        {
            return x == _ballStartPositionX && y == _ballStartPositionY;
        }

        private void currentNavigate(Navigation nav) {
            switch (nav) {
                case Navigation.Left:
                    _ballStartPositionX--;
                    break;
                case Navigation.Right:
                    _ballStartPositionX++;
                    break;
                case Navigation.Top:
                    _ballStartPositionY++;
                    break;
                case Navigation.Bottom:
                    _ballStartPositionY--;
                    break;
            }
        }

        public void SetBall()
        {
            Navigation nav = Navigation.Right; 
            while (true) {

                Console.Clear();
                
                if (_isBorderX(_ballStartPositionX+1) && _ballStartPositionX == _width-1)
                {

                    _ballStartPositionX--;
                    nav = Navigation.Left;
             
                }
                else if (_isBorderX(_ballStartPositionX-1) && _ballStartPositionX == _minimalX+1)
                {
                    _ballStartPositionX++;
                    nav = Navigation.Right;
                }
                else if (_isBorderY(_ballStartPositionY-1) && _ballStartPositionY == _minimalY+1)
                {
                    _ballStartPositionY++;
                    nav = Navigation.Top;
                }
                else if (_isBorderY(_ballStartPositionY+1) && _ballStartPositionY == _height-1)
                {
                    _ballStartPositionY--;
                    nav = Navigation.Bottom;
                }
                else
                {
                    currentNavigate(nav);
                }

                CatchEnergyBall(_ballStartPositionX, _ballStartPositionY);
                initField();
                Draw();
                Thread.Sleep(100);
            }
        }
    }
}


enum Navigation
{
    Right,
    Left,
    Top,
    Bottom
}