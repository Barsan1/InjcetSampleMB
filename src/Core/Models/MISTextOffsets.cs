namespace Core.Models
{
    public class MISTextOffsets
    {
        /// <summary>
        /// Where is the section in razor component start
        /// </summary>
        public int SectionOffset { get; set; }

        /// <summary>
        /// Number of lines the section need to add after inject the sample
        /// </summary>
        public int LinesAddedToSection { get; set; }
    }
}
