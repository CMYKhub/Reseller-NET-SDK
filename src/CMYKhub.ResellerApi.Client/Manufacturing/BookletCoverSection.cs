namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class BookletCoverSection
    {
        public BookletCoverSection()
        {
            Finishing = new PrintWizardFinishing[0];
        }
        public int Pp { get; set; }
        public string PaperId { get; set; }
        public string ProductId { get; set; }
        public PrintWizardFinishing[] Finishing { get; set; }
    }
}
