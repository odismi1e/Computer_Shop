using DAL;

namespace BLL
{
    public class ManufacturerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ManufacturerModel() { }
        public ManufacturerModel(Manufacturer s)
        {
            Id = s.id;
            Name = s.name;
        }
    }
}
