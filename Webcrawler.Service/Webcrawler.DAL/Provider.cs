using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Webcrawler.DAL
{
    public class Provider : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string EntryId { get; set; }
        public Entry Entry { get; set; }
        public string Name { get; set; }

        public Provider()
        {
            CreatedDate = DateTimeOffset.Now;
            UpdatedDate = DateTimeOffset.Now;
        }

        public Provider(string ProviderId)
        {
            Id = ProviderId;
            UpdatedDate = DateTimeOffset.Now;
        }

        public Provider providerExists(string ProviderName)
        {
            Provider provider = null;

            using (DatabaseContext context = new DatabaseContext())
            {
                provider = context.Providers.Where(p => p.Name == ProviderName).FirstOrDefault();
            }

            if (provider != null)
            {
                return provider;
            }
            else
            {
                return null;
            }
        }

        public Provider createProvider(string providerName)
        {
            using (DatabaseContext databaseContext = new DatabaseContext())
            {
                Provider provider = new Provider()
                {
                    Name = providerName,
                };

                databaseContext.Providers.Add(provider);
                databaseContext.SaveChanges();

                return databaseContext.Providers.OrderByDescending(p => p.UpdatedDate).FirstOrDefault();
            }
        }
    }
}