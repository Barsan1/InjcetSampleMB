namespace Core.Data
{
    public class Rootobject
    {
        public Table[] table { get; set; }
        public Lanthanoid[] lanthanoids { get; set; }
        public Actinoid[] actinoids { get; set; }
    }

    public class Table
    {
        public string wiki { get; set; }
        public Element[] elements { get; set; }
    }

    public class Element
    {
        public string group { get; set; }
        public int position { get; set; }
        public string name { get; set; }
        public int number { get; set; }
        public string small { get; set; }
        public float molar { get; set; }
        public int[] electrons { get; set; }
    }

    public class Lanthanoid
    {
        public string group { get; set; }
        public int position { get; set; }
        public string name { get; set; }
        public int number { get; set; }
        public string small { get; set; }
        public float molar { get; set; }
        public int[] electrons { get; set; }
    }

    public class Actinoid
    {
        public string group { get; set; }
        public int position { get; set; }
        public string name { get; set; }
        public int number { get; set; }
        public string small { get; set; }
        public float molar { get; set; }
        public int[] electrons { get; set; }
    }
}
