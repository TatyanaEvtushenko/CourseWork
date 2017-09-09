using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CourseWork.DataLayer.Models
{
    public class Raiting
    {
		[Key]
        public string Id { get; set; }

        public string ProjectId { get; set; }

        public string UserName { get; set; }

        public int RaitingResult { get; set; }
    }
}
