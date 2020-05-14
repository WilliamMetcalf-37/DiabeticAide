using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Twilio;
using Twilio.AspNet;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.Types;
using Twilio.AspNet.Core;
using Microsoft.Extensions.Configuration;
using DiabeticAide.Models.ViewModels;

namespace DiabeticAide.Controllers
{
    public class SmsController : TwilioController
    {


        private readonly IConfiguration Configuration;

        public SmsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult SendSms(string phone, string userId)
        {

            var twilioAcc = new TwilioAccount();
            Configuration.GetSection("Twilio").Bind(twilioAcc);

            var accountSid = twilioAcc.Sid;
            var authToken = twilioAcc.Token;
            TwilioClient.Init(accountSid, authToken);
            var to = new PhoneNumber($"+1{phone}");
            var from = new PhoneNumber("+12057402552");

            var message = MessageResource.Create(to: to,
                from: from,
                body: "Its time to check your blood sugar!");

            return RedirectToAction("Index", "Data");


        }
    }
}