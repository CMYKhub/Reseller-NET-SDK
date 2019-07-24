using System.Collections.Generic;

namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class WideFormatWizardFinishing
    {
        public WideFormatWizardFinishing()
        {
            Config = new Dictionary<string, string>();
        }

        public string FinishingId { get; set; }
        public IDictionary<string, string> Config { get; set; }
    }
}
