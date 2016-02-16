namespace Chummer
{
    public interface ISkill
    {
        /// <summary>
        /// The Character object being used by the Skill.
        /// </summary>
        Character CharacterObject { get; }

        /// <summary>
        /// The name of the object as it should be displayed in lists. Name (Extra).
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Is this Skill a Knowledge Skill?
        /// </summary>
        bool KnowledgeSkill { get; set; }

        /// <summary>
        /// Is this Skill an Exotic Skill?
        /// </summary>
        bool ExoticSkill { get; set; }

        /// <summary>
        /// Attribute the Skill is linked to.
        /// </summary>
        string Attribute { get; set; }

        /// <summary>
        /// Sourcebook.
        /// </summary>
        string Source { get; set; }

        /// <summary>
        /// Page Number.
        /// </summary>
        string Page { get; set; }

        /// <summary>
        /// Skill's total Rating (Rating + Modifiers).
        /// </summary>
        int TotalRating { get; }

        /// <summary>
        /// Translated Attribute.
        /// </summary>
        string DisplayAttribute { get; }

        bool HasSpecialization(string strSpec);
    }
}