using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArtGalleryOnline.Models;
using System.Net.Mail;
using System.Net;

namespace ArtGalleryOnline.Controllers
{
    public class ContactsController : Controller
    {
        //[HttpGet]
        //public IActionResult Contact()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Contact(Contact model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(model);
        //        }

        //        // Thông tin người gửi email
        //        var senderEmail = new MailAddress("mailfromonlineartgallery@gmail.com", "Demo Test");
        //        var password = "OnlineArtGallery.@";

        //        // Thông tin người nhận email (Admin)
        //        var receiverEmail = new MailAddress("vythianhlinh02@gmail.com", "Admin");

        //        // Cấu hình đối tượng SMTP để gửi email
        //        var smtp = new SmtpClient
        //        {
        //            Host = "smtp.gmail.com",
        //            Port = 587,
        //            EnableSsl = true,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            UseDefaultCredentials = false,
        //            Credentials = new NetworkCredential(senderEmail.Address, password)
        //        };

        //        // Tạo nội dung email từ thông tin người dùng
        //        var mailContent = $"Name: {model.ContactName}\n" +
        //                          $"Phone number: {model.ContactPhone}\n" +
        //                          $"Email: {model.ContactEmail}\n" +
        //                          $"Description: {model.Description}";

        //        // Tạo đối tượng MailMessage
        //        using (var message = new MailMessage(senderEmail, receiverEmail)
        //        {
        //            Subject = "Contact Form Submission",
        //            Body = mailContent
        //        })
        //        {
        //            // Gửi email
        //            smtp.Send(message);
        //        }

        //        // Thành công - bạn có thể chuyển người dùng đến một trang "cảm ơn" hoặc thông báo thành công
        //        ViewBag.SuccessMessage = "Email sent successfully!";
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Có lỗi khi gửi email - hiển thị thông báo lỗi
        //        ViewBag.ErrorMessage = "Error sending email: " + ex.Message;
        //        return View(model);
        //    }

        //}

    }
}
