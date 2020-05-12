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

namespace DiabeticAide.Controllers
{
    public class SmsController : TwilioController
    {
        public IActionResult SendSms(string phone, string userId)
        {
            var accountSid = "";
            var authToken = "";
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