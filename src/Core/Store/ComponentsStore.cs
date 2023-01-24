using Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Store
{
    public class ComponentsStore
    {
        public static List<ComponentFather> Components { get; private set; } = new List<ComponentFather>();


        public ComponentsStore(List<Tree> data)
            => BreakingOutDataToComponents(data);

        public static void RegisterComponentsFromCache(List<ComponentFather> dataFromCache)
            => Components = dataFromCache;

        private void BreakingOutDataToComponents(List<Tree> data)
        {
            // src/MudBlazor.Docs/Pages/Components/ChipSet
            // src/MudBlazor.Docs/Pages/Components/ChipSet/ChipSetPage.razor
            // src/MudBlazor.Docs/Pages/Components/ChipSet/Examples
            // src/MudBlazor.Docs/Pages/Components/ChipSet/Examples/ChipSetAddRemoveExample.razor
            int currentIndex = -1;

            for (int i = 0; i < data.Count; i++)
            {
                Tree temp = data[i];

                if (temp.size == 0 && !temp.path.Contains("/Examples"))
                {
                    currentIndex++;
                    Components
                        .Add(new ComponentFather(temp.path.Substring(temp.path.LastIndexOf('/') + 1)));
                }
                else if (temp.path.Contains("/Examples/"))
                {
                    Components
                        .ElementAt(currentIndex)
                        .SampleComponents
                            .Add(new ComponentSon(temp.path.Substring(temp.path.LastIndexOf('/'))));
                }
            }

            // Clean unused examples 
            CleanUnusedExamples();

        }

        /// <summary>
        /// Clean unused examples with no real examples
        /// </summary>
        private void CleanUnusedExamples()
        {
            for (int i = 0; i < Components.Count - 1; i++)
            {
                if (Components.ElementAt(i).SampleComponents.Count == 0)
                    Components.RemoveAt(i);
                else if (Components[i].ComponentsArea == "TemplateComponent")
                    Components.RemoveAt(i);
            }
        }
    }
}
