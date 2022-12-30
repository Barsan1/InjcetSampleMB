using System.Collections.Generic;

namespace Core.Models
{
    public class ComponentFather
    {
        public List<ComponentSon> SampleComponents { get; set; } = new List<ComponentSon>();
        public string ComponentsArea { get; set; }

        public ComponentFather(string componentArea)
        {
            ComponentsArea = componentArea;
        }
    }
}
