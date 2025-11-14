using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using System;
using MongoDB.Bson;
using MongoDB.Driver;
using ModustaAPI.Services;
using Stripe.V2;
using Microsoft.Extensions.Logging;
using System.Globalization;
using ModustaAPI.Models;

namespace ModustaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly CurriculumService _booksService;
        private readonly StripeService _stripeService;
        private readonly IConfiguration _configuration;

        public StripeController(IConfiguration configuration, CurriculumService booksService, StripeService stripeService)
        {
            _booksService = booksService;
            _configuration = configuration;
            _stripeService = stripeService;
        }

        [HttpGet("success")]
        public IActionResult PaymentSuccess()
        {
            return Ok("Paiement réussi ! Merci pour votre abonnement.");
        }

        [HttpGet("cancel")]
        public IActionResult PaymentCancel()
        {
            return Ok("Le paiement a été annulé.");
        }

        /// <summary>
        /// Configuration Stripe avec la clé secrète
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetStripeSecretKey(StripeModel stripeInfo)
        {
            string cle = stripeInfo.SecretKey;
            if (Environment.GetEnvironmentVariable("DOTNET_STRIPE_PRIVATE_KEY") != null)
            {
                cle = Environment.GetEnvironmentVariable("DOTNET_STRIPE_PRIVATE_KEY");
            }
      
            return cle;
        }
        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession()
        {
            var stripeInfo = await _stripeService.GetAsync();
            StripeConfiguration.ApiKey =await  GetStripeSecretKey(stripeInfo);
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card", },
                Mode = "subscription",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "eur",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Abonnement Modusta",
                            },
                            UnitAmount = 1000, // Montant en centimes (10€)
                            Recurring = new SessionLineItemPriceDataRecurringOptions
                            {
                                Interval = "month",
                            },
                        },
                        Quantity = 1,
                    },
                },
                SuccessUrl = stripeInfo.SuccessPage,// _configuration["Stripe:successPage"], // Page en cas de succès
                CancelUrl = stripeInfo.CancelPage,//_configuration["Stripe:cancelPage"],    // Page en cas d'annulation
            };
            var service = new SessionService();                                                                                                                                                                                                            
            Session session = await service.CreateAsync(options);
            return Ok(new { id = session.Id });
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    "your-webhook-secret"
                );

                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Session;
                    if (session.Mode == "subscription")
                    {
                        // Traiter la création d'abonnement ici, par exemple :
                        // 1. Récupérer l'ID de l'utilisateur ou le produit associé
                        // 2. Enregistrer le paiement ou l'abonnement dans la base de données
                        // 3. Activer les fonctionnalités d'abonnement pour l'utilisateur
                    }
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
