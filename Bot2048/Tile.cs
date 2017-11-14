namespace Bot2048
{
    public struct Tile
    {
        public Tile(int number)
        {
            value = number;
            WasStacked = false;
        }

        private int value;
        public bool IsEmpty
        {
            get
            {
                return value == 0;
            }
        }

        internal bool WasStacked { get; set; }

        public override bool Equals(object obj)
        {
            var tile = (Tile)obj;
            return tile != null &&
                   value == tile.value;
        }

        public override int GetHashCode()
        {
            return -1584136870 + value.GetHashCode();
        }

        public static bool operator ==(Tile a, Tile b)
        {
            return a.value == b.value;
        }
        
        public static bool operator !=(Tile a, Tile b)
        {
            return a.value != b.value;
        }
        
        public void Increment()
        {
            value++;
        }

        public override string ToString()
        {
            return value == 0 ? "" : (1 << value).ToString();
        }
    }
}