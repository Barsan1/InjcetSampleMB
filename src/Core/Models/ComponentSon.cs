using System;

namespace Core.Models
{
    public class ComponentSon
    {
        /// <summary>
        /// Represent the name of sample component.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Constractur
        /// </summary>
        /// <param name="name">Assain name to component</param>
        public ComponentSon(string name)
        {
            Name = name;
        }
    }
}
