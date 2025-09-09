using System;

namespace Nager.Immich.Models
{
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ProfileImagePath { get; set; }

        /// <summary>
        /// Avatar Color
        /// primary, pink, red, yellow, blue, green, purple, orange, gray, amber
        /// </summary>
        public string AvatarColor { get; set; }
        public DateTime ProfileChangedAt { get; set; }
    }
}
