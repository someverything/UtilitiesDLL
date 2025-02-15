using FluentEmail.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Entities;
using UtilitiesDLL.Message.Abstract;

namespace UtilitiesDLL.Message.Concrete
{
    public class EmailManager : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailManager(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendEmailAsync(EmailMetadata metadata)
        {
            await _fluentEmail
                .To(metadata.ToAddress)
                .Subject(metadata.Subject)
                .Body(metadata.Body)
                .SendAsync();
        }
    }
}
