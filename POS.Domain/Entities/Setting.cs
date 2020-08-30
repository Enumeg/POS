namespace POS.Domain.Entities
{
    public class Setting
    {
        public int Id { get; set; }
        public decimal StartBalance { get; set; }
        public bool HasMachines { get; set; }
        public bool HasShifts { get; set; }
        public int SalesPerYear { get; set; }
    }
}
