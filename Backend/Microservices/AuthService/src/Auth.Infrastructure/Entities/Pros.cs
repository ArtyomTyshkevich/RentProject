
using UserService.DAL.Enums;

namespace UserService.DAL.Entities
{
    public class Pros
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProsTypes ProsType { get; set; }
    }
}
