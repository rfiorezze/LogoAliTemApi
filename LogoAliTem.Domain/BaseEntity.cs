using LogoAliTem.Domain.Identity;
using System;

namespace LogoAliTem.Domain
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            DataCriacao = DateTime.UtcNow;
        }

        public DateTime? DataAlteracao { get; set; }
        public DateTime DataCriacao { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}