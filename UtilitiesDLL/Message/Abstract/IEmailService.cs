using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Entities;

namespace UtilitiesDLL.Message.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMetadata metadata);
    }
}
