using System;
namespace New_Ball_Game
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public char Value { get; private set; }

        public ConsoleColor BackgroundColor { get; set; }

        public void Update(char newVal) => Value = newVal;
        public void SetBorder(CellType cellType) => Update(cellTypeSetter(cellType));

        public bool IsEmpty => Value == cellTypeSetter(CellType.EmptyCell) || Value == cellTypeSetter(CellType.UnInit);

        public Cell(int x, int y, CellType type)
        {
            X = x;
            Y = y;
            BackgroundColor = cellColorSetter(type);
            Value = cellTypeSetter(type);
        }

        public ConsoleColor cellColorSetter(CellType type)
        {
            switch (type)
            {
                case CellType.EnergyBall:
                    return ConsoleColor.Cyan;
                case CellType.BorderCell:
                    return ConsoleColor.White;
                case CellType.UpShield:
                    return ConsoleColor.White;
                case CellType.DownShield:
                    return ConsoleColor.White;
                case CellType.EmptyCell:
                    return ConsoleColor.Black;
                case CellType.UserCell:
                    return ConsoleColor.Red;
                case CellType.BallCell:
                    return ConsoleColor.Blue;
                case CellType.Coins:
                    return ConsoleColor.Yellow;
                default:
                    return ConsoleColor.Black;
            }
        }
    

        public char cellTypeSetter(CellType type)
        {
            switch (type)
            {
                case CellType.EnergyBall:
                    return '@';
                case CellType.BorderCell:
                    return '#';
                case CellType.UpShield:
                    return '/';
                case CellType.DownShield:
                    return '\\';
                case CellType.EmptyCell:
                    return ' ';
                case CellType.UserCell:
                    return 'I';
                case CellType.BallCell:
                    return '•';
                case CellType.Coins:
                    return '$';
                case CellType.UnInit:
                    return char.MinValue;
                default:
                    return ' ';
            }
        }
    }
}

public enum CellType
{
    EnergyBall,
    EmptyCell,
    BorderCell,
    UpShield,
    DownShield,
    BallCell,
    UserCell,
    Coins,
    UnInit,
}

