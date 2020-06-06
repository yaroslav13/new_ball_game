using System;
using New_Ball_Game.PlayFields;

namespace New_Ball_Game
{
    public class Game
    {
        

        public Game()
        {
            PlayFieldFactory.Create(PlayFieldType.FirstPlayFild);
        }
    }
}
