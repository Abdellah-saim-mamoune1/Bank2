namespace bankApI.BusinessLayer.Dto_s.ClientDto_s
{
    public class DCNotificationsGet
    {

        public int Id { get; set; }
        public string Notification { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }
        public DateOnly Date { get; set; }

        public bool Isviewed { get; set; }
    }
}
