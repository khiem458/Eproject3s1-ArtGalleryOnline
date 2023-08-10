using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ArtGalleryOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ArtgalleryDbContext _context;

        public HomeController(ILogger<HomeController> logger, ArtgalleryDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var artWorks = await _context.ArtWorks
                                           .Include(a => a.AuthorArtWork) // Bao gồm thông tin về tác giả tác phẩm (AuthorArtWork)
                                            .Include(a => a.Category) // Bao gồm thông tin về danh mục tác phẩm (Category)
                                            .ToArrayAsync();
            return View(artWorks);
        }

        public async Task<IActionResult> Store(string searchString)
        {
            IQueryable<ArtWork> query = _context.ArtWorks
                                           .Include(a => a.AuthorArtWork) // Bao gồm thông tin về tác giả tác phẩm (AuthorArtWork)
                                           .Include(a => a.Category); // Bao gồm thông tin về danh mục tác phẩm (Category)

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(a => a.ArtName.Contains(searchString));
            }

            var artWorks = await query.ToListAsync();
            return View(artWorks);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
        public IActionResult GalleryStatue()
        {
            return View();
        }
    public IActionResult Terrible()
    {
        return View();
    }
    public IActionResult Blog()
        {
            var Blog = _context.Blog.Include(b => b.AuthorArtWork);
            return View(Blog.ToList());
        }
       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string mailFrom = "mailfromonlineartgallery@gmail.com"; // Thay đổi địa chỉ email của bạn
                    string password = "OnlineArtgallery.@"; // Thay đổi mật khẩu email của bạn

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(mailFrom);
                    mail.To.Add("vythianhlinh02@gmail.com"); // Thay đổi địa chỉ email của người nhận
                    mail.Subject = "Contact Us Enquiry from " + contact.ContactName;
                    mail.Body = $"Name: {contact.ContactName}\nEmail: {contact.ContactEmail}\nPhone: {contact.ContactPhone}\n\n{contact.Description}";

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(mailFrom, password);
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Send(mail);

                    ViewBag.Message = "Message sent successfully.";
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "An error occurred while sending the email: " + ex.Message;
                }
            }

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactName,ContactEmail,ContactPhone,Description")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }
    }

}
