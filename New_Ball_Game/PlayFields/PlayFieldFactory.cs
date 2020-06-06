using System;
namespace New_Ball_Game.PlayFields
{
    public static class PlayFieldFactory
    {
        public static IPlayField Create(PlayFieldType type) {
            switch (type)
            {
                case PlayFieldType.FirstPlayFild:
                    return new FirstPlayField();
                default:
                    throw new Exception("Undefined type");
            }
        }
    }
}
