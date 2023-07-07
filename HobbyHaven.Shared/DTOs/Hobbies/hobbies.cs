﻿using HobbyHaven.Shared.DTOs.PersonalityTag;
using HobbyHaven.Shared.DTOs.Havens;

namespace HobbyHaven.Shared.DTOs.Hobbies
{
    public class DTOHobby
    {
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public Guid Id { get; set; }
        public List<DTOPersonalityTag> PersonalityTags { get; set; } = new(); // Tags used to match the users personality/characteristics to a hobby.
        public List<DTOHaven> Havens { get; set; } = new();
    }
}