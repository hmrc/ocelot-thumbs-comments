using System;
using System.ComponentModel.DataAnnotations;

namespace ThumbsComments.Models
{
    /// <summary>
    /// Comment Model
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Line Of Business
        /// </summary>
        [Required(ErrorMessage = "Line Of Business Required")]
        [MaxLength(255, ErrorMessage = "Line Of Business Too Long, max 255 characters")]
        public string LineOfBusiness { get; set; }

        /// <summary>
        /// Thumb Comment
        /// </summary>
        public string ThumbComment { get; set; }
    }
}
