using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Entity
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
